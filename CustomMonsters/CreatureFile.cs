using Masterplan.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MonsterPorter
{
    public class CreatureFile
    {
        public CreatureFile(string path)
        {
            Path = path;
            Name = ExtractName(path);
        }

        public string Path { get; set; }
        public string Name { get; set; }

        private string ExtractName(string path)
        {
            var start = path.LastIndexOf('\\') + 1;
            var end = path.LastIndexOf('.');

            return path.Substring(start, end - start);
        }

        public ICreature ExtractCreature()
        {
            ICreature creature;
            using (var fs = new FileStream(Path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                creature = (ICreature)formatter.Deserialize(fs);
            }

            return creature;
        }
 
        public override string ToString()
        {
            return Name;
        }
    }
}
