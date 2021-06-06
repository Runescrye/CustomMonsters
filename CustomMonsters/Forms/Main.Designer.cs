
namespace MonsterPorter
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCreatureMacro = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tabCreatures = new System.Windows.Forms.TabControl();
            this.tabCustom = new System.Windows.Forms.TabPage();
            this.lstCreatures = new System.Windows.Forms.ListBox();
            this.tabLibraries = new System.Windows.Forms.TabPage();
            this.creatureLibrary = new MonsterPorter.Controls.LibraryCreatureList();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabCreatures.SuspendLayout();
            this.tabCustom.SuspendLayout();
            this.tabLibraries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(914, 24);
            this.menu.TabIndex = 1;
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // mnuOptions
            // 
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.tabCreatures);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 559);
            this.panel1.TabIndex = 4;
            // 
            // txtCreatureMacro
            // 
            this.txtCreatureMacro.BackColor = System.Drawing.SystemColors.Window;
            this.txtCreatureMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreatureMacro.Location = new System.Drawing.Point(0, 0);
            this.txtCreatureMacro.Margin = new System.Windows.Forms.Padding(300, 3, 300, 3);
            this.txtCreatureMacro.Multiline = true;
            this.txtCreatureMacro.Name = "txtCreatureMacro";
            this.txtCreatureMacro.PlaceholderText = "Creature Macro Text";
            this.txtCreatureMacro.ReadOnly = true;
            this.txtCreatureMacro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCreatureMacro.Size = new System.Drawing.Size(544, 150);
            this.txtCreatureMacro.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(360, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 559);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // tabCreatures
            // 
            this.tabCreatures.Controls.Add(this.tabLibraries);
            this.tabCreatures.Controls.Add(this.tabCustom);
            this.tabCreatures.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabCreatures.Location = new System.Drawing.Point(0, 0);
            this.tabCreatures.Name = "tabCreatures";
            this.tabCreatures.SelectedIndex = 0;
            this.tabCreatures.Size = new System.Drawing.Size(360, 559);
            this.tabCreatures.TabIndex = 3;
            // 
            // tabCustom
            // 
            this.tabCustom.Controls.Add(this.lstCreatures);
            this.tabCustom.Location = new System.Drawing.Point(4, 24);
            this.tabCustom.Name = "tabCustom";
            this.tabCustom.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustom.Size = new System.Drawing.Size(352, 531);
            this.tabCustom.TabIndex = 0;
            this.tabCustom.Text = "Custom";
            this.tabCustom.UseVisualStyleBackColor = true;
            // 
            // lstCreatures
            // 
            this.lstCreatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCreatures.FormattingEnabled = true;
            this.lstCreatures.ItemHeight = 15;
            this.lstCreatures.Location = new System.Drawing.Point(3, 3);
            this.lstCreatures.Name = "lstCreatures";
            this.lstCreatures.Size = new System.Drawing.Size(346, 525);
            this.lstCreatures.TabIndex = 0;
            this.lstCreatures.SelectedIndexChanged += new System.EventHandler(this.lstCreatures_SelectedIndexChanged);
            // 
            // tabLibraries
            // 
            this.tabLibraries.Controls.Add(this.creatureLibrary);
            this.tabLibraries.Location = new System.Drawing.Point(4, 24);
            this.tabLibraries.Name = "tabLibraries";
            this.tabLibraries.Padding = new System.Windows.Forms.Padding(3);
            this.tabLibraries.Size = new System.Drawing.Size(352, 531);
            this.tabLibraries.TabIndex = 1;
            this.tabLibraries.Text = "Libraries";
            this.tabLibraries.UseVisualStyleBackColor = true;
            // 
            // creatureLibrary
            // 
            this.creatureLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creatureLibrary.Location = new System.Drawing.Point(3, 3);
            this.creatureLibrary.Name = "creatureLibrary";
            this.creatureLibrary.Size = new System.Drawing.Size(346, 525);
            this.creatureLibrary.TabIndex = 0;
            this.creatureLibrary.SelectedIndexChanged += new System.EventHandler(this.creatureLibrary_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(370, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtCreatureMacro);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlDetails);
            this.splitContainer1.Size = new System.Drawing.Size(544, 559);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 5;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(544, 405);
            this.pnlDetails.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 583);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Main";
            this.Text = "MonsterPorter";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabCreatures.ResumeLayout(false);
            this.tabCustom.ResumeLayout(false);
            this.tabLibraries.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCreatureMacro;
        private System.Windows.Forms.TabControl tabCreatures;
        private System.Windows.Forms.TabPage tabCustom;
        private System.Windows.Forms.TabPage tabLibraries;
        private System.Windows.Forms.ListBox lstCreatures;
        private Controls.LibraryCreatureList creatureLibrary;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlDetails;
    }
}

