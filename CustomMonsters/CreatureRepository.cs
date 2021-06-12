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

        public Dictionary<string, Dictionary<Guid, ICreature>> CreatureByLibrary { get; private set; }
        public Dictionary<Guid, Library> LibraryByCreature { get; private set; }
        public Dictionary<Guid, ICreature> CreatureByID { get; private set; }
        public Dictionary<string, ICreature> CreatureByName { get; private set; }

        public CreatureRepository(string librariesDirectoryPath)
        {
            Path = librariesDirectoryPath;
            Creatures = new List<ICreature>();
            Libraries = new Dictionary<string, Library>();
            CreatureByLibrary = new Dictionary<string, Dictionary<Guid, ICreature>>();
            CreatureByID = new Dictionary<Guid, ICreature>();
            LibraryByCreature = new Dictionary<Guid, Library>();
            CreatureByName = new Dictionary<string, ICreature>();
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
                    // Filter duplicates
                    if (CreatureByID.ContainsKey(creature.ID) || CreatureByName.ContainsKey(creature.Name))
                        continue;

                    CreatureByID.Add(creature.ID, creature);
                    Creatures.Add(creature);
                    AddValue(CreatureByLibrary, library.Name, creature.ID, creature);
                    CreatureByName[creature.Name] = creature;
                    LibraryByCreature[creature.ID] = library;
                }
            }
        }

        private void AddValue<T,K>(Dictionary<T, Dictionary<K, ICreature>> dict, T key, K identifier, ICreature creature)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new Dictionary<K, ICreature>());
            }

            if (dict[key].ContainsKey(identifier))
            {
                return;
            }

            dict[key].Add(identifier, creature);
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
                filters.Add(new Predicate<ICreature>(x => CreatureByLibrary.ContainsKey(libraryName) && CreatureByLibrary[libraryName].ContainsKey(x.ID)));

            if (role.HasValue)
                filters.Add(new Predicate<ICreature>(x => x.Role.Type == role.Value));

            if (type.HasValue)
                filters.Add(new Predicate<ICreature>(x => x.Type == type.Value));

            return Creatures.Where((p => filters.All(f => f(p))));
        }
    }
}
