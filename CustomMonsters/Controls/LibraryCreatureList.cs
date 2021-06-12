using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Masterplan.Data;

namespace MonsterPorter.Controls
{
    public partial class LibraryCreatureList : UserControl
    {
        public ContextMenuStrip ContextMenu { get { return lstCreatures.ContextMenuStrip; } set { lstCreatures.ContextMenuStrip = value; } }

        private CreatureRepository repo;
        private Dictionary<Guid, ListViewItem> creatureViewItems = new Dictionary<Guid, ListViewItem>();
        private Dictionary<string, string> sourceAbbreviation = new Dictionary<string, string>();
        private ListViewColumnSorter columnSorter = new ListViewColumnSorter();

        public event EventHandler SelectedIndexChanged
        {
            add { lstCreatures.SelectedIndexChanged += value; }
            remove { lstCreatures.SelectedIndexChanged -= value; }
        }

        public LibraryCreatureList()
        {
            InitializeComponent();
            lstCreatures.ListViewItemSorter = columnSorter;
        }

        public void Populate(CreatureRepository repo)
        {
            this.repo = repo;

            foreach (var creature in repo.Creatures)
            {
                var viewItem = new ListViewItem(new[] { creature.Level.ToString(), creature.Role.Type.ToString(), creature.Role.Flag.ToString(), creature.Name });
                viewItem.Tag = creature;
                creatureViewItems.Add(creature.ID, viewItem);
            }

            PopulateList(repo);

            comboSources.BeginUpdate();
            comboSources.Items.Add("");

            foreach (var library in repo.Libraries)
            {
                if (library.Value.Creatures.Count == 0)
                    continue;

                var libraryShortName = library.Value.Name.Replace("Monster Manual", "MM");
                sourceAbbreviation.Add(libraryShortName, library.Value.Name);
                comboSources.Items.Add(libraryShortName);
            }
            comboSources.EndUpdate();
        }

        public int SelectedIndex
        {
            get { return lstCreatures.SelectedItems[0].Index; }
        }

        public ICreature SelectedItem
        {
            get
            {
                return lstCreatures.SelectedItems.Count > 0 ? lstCreatures.SelectedItems[0].Tag as ICreature : null;
            }
        }

        private void comboSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateList(repo);
        }

        private void lstCreatures_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            columnSorter.Order = (e.Column != columnSorter.SortColumn || columnSorter.Order != SortOrder.Ascending) ? SortOrder.Ascending : SortOrder.Descending;
            columnSorter.SortColumn = e.Column;

            // Perform the sort with these new sort options.
            lstCreatures.Sort();
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
            lstCreatures.ListViewItemSorter = null;
            lstCreatures.BeginUpdate();
            lstCreatures.Items.Clear();

            int parsedLevel;
            RoleType parsedRole;
            CreatureType parsedType;

            int? filterLevel = (int.TryParse(txtLevel.Text, out parsedLevel)) ? parsedLevel : null;
            RoleType? filterRole = Enum.TryParse(comboRole.Text, out parsedRole) ? parsedRole : null;
            CreatureType? filterType = Enum.TryParse(comboType.Text, out parsedType) ? parsedType : null;
            var libraryName = string.IsNullOrEmpty(comboSources.Text) ? string.Empty : sourceAbbreviation[comboSources.Text];

            foreach (var creature in repo.GetCreatureBy(filterLevel, txtNameFilter.Text, libraryName, filterRole, filterType))
            {
                lstCreatures.Items.Add(creatureViewItems[creature.ID]);
            }
            
            lstCreatures.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            lstCreatures.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            lstCreatures.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            lstCreatures.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
            lstCreatures.ListViewItemSorter = columnSorter;
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

        private void lstCreatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = ((ListView)sender);

            if (list.SelectedItems.Count > 0)
            {
                var creature = (ICreature)list.SelectedItems[0].Tag;
                lblSource.Text = repo.LibraryByCreature[creature.ID].Name;
            }
        }
    }
}
