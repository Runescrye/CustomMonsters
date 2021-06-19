
namespace MonsterPorter
{
    partial class Options
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.browseCreatureFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.txtCreatureLocation = new System.Windows.Forms.TextBox();
            this.btnCreatureBrowse = new System.Windows.Forms.Button();
            this.lblCreatureLocation = new System.Windows.Forms.Label();
            this.lblRenderType = new System.Windows.Forms.Label();
            this.comboRenderType = new System.Windows.Forms.ComboBox();
            this.lblLibraryLocation = new System.Windows.Forms.Label();
            this.txtLibraryLocation = new System.Windows.Forms.TextBox();
            this.btnLibraryBrowse = new System.Windows.Forms.Button();
            this.browseLibraryFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // txtCreatureLocation
            // 
            resources.ApplyResources(this.txtCreatureLocation, "txtCreatureLocation");
            this.txtCreatureLocation.Name = "txtCreatureLocation";
            this.txtCreatureLocation.Leave += new System.EventHandler(this.txtCreatureLocation_Leave);
            // 
            // btnCreatureBrowse
            // 
            resources.ApplyResources(this.btnCreatureBrowse, "btnCreatureBrowse");
            this.btnCreatureBrowse.Name = "btnCreatureBrowse";
            this.btnCreatureBrowse.UseVisualStyleBackColor = true;
            this.btnCreatureBrowse.Click += new System.EventHandler(this.btnCreatureBrowse_Click);
            // 
            // lblCreatureLocation
            // 
            resources.ApplyResources(this.lblCreatureLocation, "lblCreatureLocation");
            this.lblCreatureLocation.Name = "lblCreatureLocation";
            // 
            // lblRenderType
            // 
            resources.ApplyResources(this.lblRenderType, "lblRenderType");
            this.lblRenderType.Name = "lblRenderType";
            // 
            // comboRenderType
            // 
            this.comboRenderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRenderType.FormattingEnabled = true;
            this.comboRenderType.Items.AddRange(new object[] {
            resources.GetString("comboRenderType.Items"),
            resources.GetString("comboRenderType.Items1"),
            resources.GetString("comboRenderType.Items2")});
            resources.ApplyResources(this.comboRenderType, "comboRenderType");
            this.comboRenderType.Name = "comboRenderType";
            this.comboRenderType.SelectedIndexChanged += new System.EventHandler(this.comboRenderType_SelectedIndexChanged);
            // 
            // lblLibraryLocation
            // 
            resources.ApplyResources(this.lblLibraryLocation, "lblLibraryLocation");
            this.lblLibraryLocation.Name = "lblLibraryLocation";
            // 
            // txtLibraryLocation
            // 
            resources.ApplyResources(this.txtLibraryLocation, "txtLibraryLocation");
            this.txtLibraryLocation.Name = "txtLibraryLocation";
            this.txtLibraryLocation.Leave += new System.EventHandler(this.txtLibraryLocation_Leave);
            // 
            // btnLibraryBrowse
            // 
            resources.ApplyResources(this.btnLibraryBrowse, "btnLibraryBrowse");
            this.btnLibraryBrowse.Name = "btnLibraryBrowse";
            this.btnLibraryBrowse.UseVisualStyleBackColor = true;
            this.btnLibraryBrowse.Click += new System.EventHandler(this.btnLibraryBrowse_Click);
            // 
            // Options
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLibraryBrowse);
            this.Controls.Add(this.txtLibraryLocation);
            this.Controls.Add(this.lblLibraryLocation);
            this.Controls.Add(this.comboRenderType);
            this.Controls.Add(this.lblRenderType);
            this.Controls.Add(this.lblCreatureLocation);
            this.Controls.Add(this.btnCreatureBrowse);
            this.Controls.Add(this.txtCreatureLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog browseCreatureFolder;
        private System.Windows.Forms.TextBox txtCreatureLocation;
        private System.Windows.Forms.Button btnCreatureBrowse;
        private System.Windows.Forms.Label lblCreatureLocation;
        private System.Windows.Forms.Label lblRenderType;
        private System.Windows.Forms.ComboBox comboRenderType;
        private System.Windows.Forms.Label lblLibraryLocation;
        private System.Windows.Forms.TextBox txtLibraryLocation;
        private System.Windows.Forms.Button btnLibraryBrowse;
        private System.Windows.Forms.FolderBrowserDialog browseLibraryFolder;
    }
}