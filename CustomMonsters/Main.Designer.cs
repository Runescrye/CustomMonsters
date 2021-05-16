
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
            this.lstCreatures = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCreatureMacro = new System.Windows.Forms.TextBox();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // mnuOptions
            // 
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // lstCreatures
            // 
            this.lstCreatures.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstCreatures.FormattingEnabled = true;
            this.lstCreatures.ItemHeight = 15;
            this.lstCreatures.Location = new System.Drawing.Point(0, 24);
            this.lstCreatures.Name = "lstCreatures";
            this.lstCreatures.Size = new System.Drawing.Size(155, 426);
            this.lstCreatures.TabIndex = 2;
            this.lstCreatures.SelectedIndexChanged += new System.EventHandler(this.lstCreatures_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(155, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 426);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCreatureMacro);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(158, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 426);
            this.panel1.TabIndex = 4;
            // 
            // txtCreatureMacro
            // 
            this.txtCreatureMacro.BackColor = System.Drawing.SystemColors.Window;
            this.txtCreatureMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreatureMacro.Location = new System.Drawing.Point(0, 0);
            this.txtCreatureMacro.Multiline = true;
            this.txtCreatureMacro.Name = "txtCreatureMacro";
            this.txtCreatureMacro.PlaceholderText = "Creature Macro Text";
            this.txtCreatureMacro.ReadOnly = true;
            this.txtCreatureMacro.Size = new System.Drawing.Size(642, 426);
            this.txtCreatureMacro.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lstCreatures);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Main";
            this.Text = "MonsterPorter";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ListBox lstCreatures;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCreatureMacro;
    }
}

