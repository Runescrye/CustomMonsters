using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MonsterPorter.Renderers;

namespace MonsterPorter
{
    public partial class Main : Form
    {
        Options options = new Options();

        public Main()
        {
            InitializeComponent();
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "mnuOptions")
            {
                options.ShowDialog();
                lstCreatures.SelectedIndex = -1;
                txtCreatureMacro.Text = string.Empty;
                ListCreatures();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ListCreatures();
        }

        private void ListCreatures()
        {
            lstCreatures.Items.Clear();
            if (Properties.Settings.Default.CreatureDirectoryPath != string.Empty)
            {
                var creatureFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.CreatureDirectoryPath, "*.creature");
                foreach (var filePath in creatureFiles)
                {
                   lstCreatures.Items.Add(new CreatureFile(filePath));
                }
            }
        }

        private void lstCreatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCreatures.SelectedItem != null)
            {
                var selectedCreatureFile = (CreatureFile)lstCreatures.SelectedItem;
                var creature = selectedCreatureFile.ExtractCreature();
                var renderer = CreateRenderer(Properties.Settings.Default.RenderType);
                var macro = renderer.Render(creature);
                txtCreatureMacro.Text = macro;
            }
        }

        private IRenderer CreateRenderer(string rendererName)
        {
            switch (rendererName)
            {
                case "Roll20":
                    return new Roll20Renderer();
                case "Json":
                    return new JsonRenderer();
                default:
                    return new Roll20Renderer();
            }
        }
    }

    public enum RenderType
    {

    }
}