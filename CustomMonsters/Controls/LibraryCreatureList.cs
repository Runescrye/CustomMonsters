using System;
using System.Windows.Forms;
using Masterplan.Data;

namespace MonsterPorter.Controls
{
    public partial class LibraryCreatureList : UserControl
    {
        private CreatureRepository repo;

        public event EventHandler SelectedIndexChanged
        {
            add { lstCreatures.SelectedIndexChanged += value; }
            remove { lstCreatures.SelectedIndexChanged -= value; }
        }

        public LibraryCreatureList()
        {
            InitializeComponent();

        }

        public void Populate(CreatureRepository repo)
        {
            this.repo = repo;
            PopulateList(repo);

            comboSources.BeginUpdate();
            comboSources.Items.Add("");
            foreach (var library in repo.Libraries)
            {
                if (library.Value.Creatures.Count == 0)
                    continue;

                comboSources.Items.Add(library.Value.Name);
            }
            comboSources.EndUpdate();
        }

        public int SelectedIndex
        {
            get { return lstCreatures.SelectedIndex; }
            set { lstCreatures.SelectedIndex = value; }
        }

        public object SelectedItem
        {
            get { return lstCreatures.SelectedItem; }
            set { lstCreatures.SelectedItem = value; }
        }

        private void comboSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }


        private void comboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }

        private void txtLevel_TextChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }

        private void PopulateList(CreatureRepository repo)
        {
            lstCreatures.BeginUpdate();
            lstCreatures.Items.Clear();
            lstCreatures.Sorted = false;

            int parsedLevel;
            RoleType parsedRole;
            CreatureType parsedType;

            int? filterLevel = (int.TryParse(txtLevel.Text, out parsedLevel)) ? parsedLevel : null;
            RoleType? filterRole = Enum.TryParse(comboRole.Text, out parsedRole) ? parsedRole : null;
            CreatureType? filterType = Enum.TryParse(comboType.Text, out parsedType) ? parsedType : null;

            foreach (var creature in repo.GetCreatureBy(filterLevel, txtNameFilter.Text, comboSources.Text, filterRole, filterType))
            {
                lstCreatures.Items.Add(creature);
            }
            
            lstCreatures.Sorted = true;
            lstCreatures.EndUpdate();
        }

        private void txtLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ComboAddPlaceholderText(ComboBox combo, DrawItemEventArgs e, string placeholderText)
        {
            var text = placeholderText;
            var color = System.Drawing.Color.Gray;

            if (e.Index > 0)
            {
                text = combo.GetItemText(combo.Items[e.Index]);
                color = e.ForeColor;
            }

            e.DrawBackground();
            TextRenderer.DrawText(e.Graphics, text, combo.Font, e.Bounds, color, TextFormatFlags.Left);
        }

        private void comboRole_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboAddPlaceholderText(sender as ComboBox, e, "Role...");
        }

        private void comboSources_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboAddPlaceholderText(sender as ComboBox, e, "Source...");
        }

        private void comboType_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboAddPlaceholderText(sender as ComboBox, e, "Type...");
        }
    }
}
