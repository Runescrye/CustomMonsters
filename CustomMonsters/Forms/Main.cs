using System;
using System.Windows.Forms;
using MonsterPorter.Renderers;
using Masterplan.Data;
using Masterplan.Tools;

namespace MonsterPorter
{
    public partial class Main : Form
    {
        Options options = new Options();
        CreatureRepository creatureRepo;
        private WebBrowser Browser;

        public Main()
        {
            InitializeComponent();
            Browser = new WebBrowser();
            pnlDetails.Controls.Add(Browser);
            Browser.Dock = DockStyle.Fill;
            Browser.DocumentText = string.Empty;
            contextLibrary.Items.Add("Copy Name").Click += LibraryContextCopyNameClick;
            contextLibrary.Items.Add("Copy Macro").Click += LibraryContextCopyMacroClick;
            creatureLibrary.ContextMenuStrip = contextLibrary; 
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "mnuOptions")
            {
                var prevLibraryPath = Properties.Settings.Default.LibraryDirectoryPath;
                options.ShowDialog();
                var newLibraryPath = Properties.Settings.Default.LibraryDirectoryPath;
                if (prevLibraryPath != newLibraryPath)
                {
                    lstCreatures.SelectedIndex = -1;
                    txtCreatureMacro.Text = string.Empty;
                    Reload();
                }
            }
        }

        void LibraryContextCopyNameClick(object sender, EventArgs e)
        {
            if (creatureLibrary.SelectedItem == null)
                return;

            Clipboard.SetText(creatureLibrary.SelectedItem.Name);
        }

        void LibraryContextCopyMacroClick(object sender, EventArgs e)
        {
            if (creatureLibrary.SelectedItem == null)
                return;

            Clipboard.SetText(txtCreatureMacro.Text);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            ListCreatures();
            if (string.IsNullOrEmpty(Properties.Settings.Default.LibraryDirectoryPath))
                return;
            creatureRepo = new CreatureRepository(Properties.Settings.Default.LibraryDirectoryPath);
            creatureRepo.Load();
            creatureLibrary.Populate(creatureRepo);
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
            lstCreatures.BeginUpdate();
            if (lstCreatures.SelectedItem != null)
            {
                var selectedCreatureFile = (CreatureFile)lstCreatures.SelectedItem;
                var creature = selectedCreatureFile.ExtractCreature();
                var renderer = CreateRenderer(Properties.Settings.Default.RenderType);
                var macro = renderer.Render(creature);
                txtCreatureMacro.Text = macro;
                DisplayCreature(creature);
            }

            lstCreatures.EndUpdate();
        }

        private void DisplayCreature(ICreature creature)
        {
            EncounterCard card = new EncounterCard(creature);
            string html = HTML.StatBlock(card, null, null, true, false, true, CardMode.View, DisplaySize.Small);
            Browser.Document.OpenNew(true);
            Browser.Document.Write(html);
        }

        private IRenderer CreateRenderer(string rendererName)
        {
            switch (rendererName)
            {
                case "Roll20":
                    return new Roll20Renderer();
                case "Json":
                    return new JsonRenderer();
                case "FoundryVTT":
                    return new FoundryVTTRenderer(creatureRepo.LibraryByCreature);
                default:
                    return new Roll20Renderer();
            }
        }

        private void creatureLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (creatureLibrary.SelectedItem != null)
            {
                var selectedCreature = (ICreature)creatureLibrary.SelectedItem;
                var renderer = CreateRenderer(Properties.Settings.Default.RenderType);
                var macro = renderer.Render(selectedCreature);
                txtCreatureMacro.Text = macro;
                DisplayCreature(selectedCreature);
            }
        }
    }
}