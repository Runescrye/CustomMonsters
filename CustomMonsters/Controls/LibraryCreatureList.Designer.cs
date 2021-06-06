
namespace MonsterPorter.Controls
{
    public partial class LibraryCreatureList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelFilters = new System.Windows.Forms.Panel();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.comboRole = new System.Windows.Forms.ComboBox();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.comboSources = new System.Windows.Forms.ComboBox();
            this.lstCreatures = new System.Windows.Forms.ListView();
            this.clmLevel = new System.Windows.Forms.ColumnHeader();
            this.clmRole = new System.Windows.Forms.ColumnHeader();
            this.clmRank = new System.Windows.Forms.ColumnHeader();
            this.clmName = new System.Windows.Forms.ColumnHeader();
            this.lblSource = new System.Windows.Forms.Label();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.lblSource);
            this.panelFilters.Controls.Add(this.txtNameFilter);
            this.panelFilters.Controls.Add(this.comboType);
            this.panelFilters.Controls.Add(this.comboRole);
            this.panelFilters.Controls.Add(this.txtLevel);
            this.panelFilters.Controls.Add(this.comboSources);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(349, 63);
            this.panelFilters.TabIndex = 1;
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Location = new System.Drawing.Point(74, 6);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.PlaceholderText = "Name...";
            this.txtNameFilter.Size = new System.Drawing.Size(147, 23);
            this.txtNameFilter.TabIndex = 8;
            this.txtNameFilter.TextChanged += new System.EventHandler(this.txtNameFilter_TextChanged);
            // 
            // comboType
            // 
            this.comboType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] {
            "",
            "Animate",
            "Humanoid",
            "Beast",
            "MagicalBeast"});
            this.comboType.Location = new System.Drawing.Point(0, 33);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(67, 24);
            this.comboType.TabIndex = 7;
            this.comboType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboType_DrawItem);
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
            // 
            // comboRole
            // 
            this.comboRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Items.AddRange(new object[] {
            "",
            "Artillery",
            "Brute",
            "Controller",
            "Lurker",
            "Skirmisher",
            "Soldier"});
            this.comboRole.Location = new System.Drawing.Point(73, 33);
            this.comboRole.Name = "comboRole";
            this.comboRole.Size = new System.Drawing.Size(67, 24);
            this.comboRole.TabIndex = 6;
            this.comboRole.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboRole_DrawItem);
            this.comboRole.SelectedIndexChanged += new System.EventHandler(this.comboRole_SelectedIndexChanged);
            // 
            // txtLevel
            // 
            this.txtLevel.Location = new System.Drawing.Point(0, 6);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.PlaceholderText = "Level...";
            this.txtLevel.Size = new System.Drawing.Size(68, 23);
            this.txtLevel.TabIndex = 5;
            this.txtLevel.TextChanged += new System.EventHandler(this.txtLevel_TextChanged);
            this.txtLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLevel_KeyPress);
            // 
            // comboSources
            // 
            this.comboSources.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboSources.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboSources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSources.DropDownWidth = 500;
            this.comboSources.FormattingEnabled = true;
            this.comboSources.Location = new System.Drawing.Point(146, 33);
            this.comboSources.Name = "comboSources";
            this.comboSources.Size = new System.Drawing.Size(75, 24);
            this.comboSources.Sorted = true;
            this.comboSources.TabIndex = 4;
            this.comboSources.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboSources_DrawItem);
            this.comboSources.SelectedIndexChanged += new System.EventHandler(this.comboSources_SelectedIndexChanged);
            // 
            // lstCreatures
            // 
            this.lstCreatures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLevel,
            this.clmRole,
            this.clmRank,
            this.clmName});
            this.lstCreatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCreatures.FullRowSelect = true;
            this.lstCreatures.HideSelection = false;
            this.lstCreatures.Location = new System.Drawing.Point(0, 63);
            this.lstCreatures.MultiSelect = false;
            this.lstCreatures.Name = "lstCreatures";
            this.lstCreatures.ShowGroups = false;
            this.lstCreatures.Size = new System.Drawing.Size(349, 422);
            this.lstCreatures.TabIndex = 2;
            this.lstCreatures.UseCompatibleStateImageBehavior = false;
            this.lstCreatures.View = System.Windows.Forms.View.Details;
            this.lstCreatures.SelectedIndexChanged += new System.EventHandler(this.lstCreatures_SelectedIndexChanged);
            // 
            // clmLevel
            // 
            this.clmLevel.Text = "LVL";
            // 
            // clmRole
            // 
            this.clmRole.Text = "Role";
            // 
            // clmRank
            // 
            this.clmRank.Text = "Rank";
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(228, 13);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(0, 15);
            this.lblSource.TabIndex = 9;
            // 
            // LibraryCreatureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstCreatures);
            this.Controls.Add(this.panelFilters);
            this.Name = "LibraryCreatureList";
            this.Size = new System.Drawing.Size(349, 485);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.ComboBox comboSources;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.ComboBox comboRole;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.TextBox txtNameFilter;
        private System.Windows.Forms.ListView lstCreatures;
        private System.Windows.Forms.ColumnHeader clmLevel;
        private System.Windows.Forms.ColumnHeader clmRole;
        private System.Windows.Forms.ColumnHeader clmRank;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.Label lblSource;
    }
}
