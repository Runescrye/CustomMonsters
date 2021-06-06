using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Masterplan.Data;

namespace MonsterPorter
{
    public class CreatureRepository
    {
        public Dictionary<string, Library> Libraries { get; private set; }
        public List<ICreature> Creatures { get; private set; }
        public string Path { get; private set; }

        public Dictionary<string, Dictionary<string, ICreature>> CreatureByLibrary { get; private set; }
        public Dictionary<int, Dictionary<string, ICreature>> CreatureByLevel { get; private set; }
        public Dictionary<RoleType, Dictionary<string, ICreature>> CreatureByRole { get; private set; }
        public Dictionary<CreatureType, Dictionary<string, ICreature>> CreatureByType { get; private set; }
        public Dictionary<string, Library> LibraryByCreature { get; private set; }

        public CreatureRepository(string librariesDirectoryPath)
        {
            Path = librariesDirectoryPath;
            Creatures = new List<ICreature>();
            Libraries = new Dictionary<string, Library>();
            CreatureByLibrary = new Dictionary<string, Dictionary<string, ICreature>>();
            CreatureByLevel = new Dictionary<int, Dictionary<string, ICreature>>();
            CreatureByRole = new Dictionary<RoleType, Dictionary<string, ICreature>>();
            LibraryByCreature = new Dictionary<string, Library>();
        }

        public void Load()
        {
            if (Path == null)
                return;

            var libraryFiles = System.IO.Directory.GetFiles(Path, "*.library");
            foreach (var filePath in libraryFiles)
            {
                var library = ExtractLibrary(filePath);
                Libraries.Add(library.Name, library);

                foreach (var creature in library.Creatures)
                {
                    Creatures.Add(creature);
                    AddValue(CreatureByLibrary,library.Name, creature);
                    LibraryByCreature[creature.Name] = library;
                    /*AddValue(CreatureByLevel, creature.Level, creature);
                    AddValue(CreatureByRole, creature.Role.Type, creature);
                    AddValue(CreatureByType, creature.Type, creature);*/
                }
            }
        }

        private void AddValue<T>(Dictionary<T, Dictionary<string, ICreature>> dict, T key, ICreature value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new Dictionary<string, ICreature>());
            }

            if (dict[key].ContainsKey(value.Name))
            {
                return;
            }

            dict[key].Add(value.Name, value);
        }

        public Library ExtractLibrary(string libraryPath)
        {
            Library library;
            using (var fs = new FileStream(libraryPath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                library = (Library)formatter.Deserialize(fs);
            }

            return library;
        }

        public bool compareName(string name, ICreature creature)
        {
            return (!string.IsNullOrEmpty(name) && !creature.Name.Contains(name));
        }

        public IEnumerable<ICreature> GetCreatureBy(int? level, string name, string libraryName, RoleType? role, CreatureType? type)
        {

            var normalizedName = name.Trim().ToLower();
            List<Predicate<ICreature>> filters = new List<Predicate<ICreature>>();

            if (!string.IsNullOrEmpty(name))
                filters.Add(new Predicate<ICreature>(x=> x.Name.ToLower().Contains(normalizedName)));

            if (level.HasValue)
                filters.Add(new Predicate<ICreature>(x => x.Level == level.Value));

            if (!string.IsNullOrEmpty(libraryName))
                filters.Add(new Predicate<ICreature>(x => CreatureByLibrary.ContainsKey(libraryName) && CreatureByLibrary[libraryName].ContainsKey(x.Name)));

            if (role.HasValue)
                filters.Add(new Predicate<ICreature>(x => x.Role.Type == role.Value));

            if (type.HasValue)
                filters.Add(new Predicate<ICreature>(x => x.Type == type.Value));

            return Creatures.Where((p => filters.All(f => f(p))));
        }
    }
}
