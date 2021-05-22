
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRenderType = new System.Windows.Forms.Label();
            this.comboRenderType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtCreatureLocation
            // 
            resources.ApplyResources(this.txtCreatureLocation, "txtCreatureLocation");
            this.txtCreatureLocation.Name = "txtCreatureLocation";
            this.txtCreatureLocation.Leave += new System.EventHandler(this.txtCreatureLocation_Leave);
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            resources.GetString("comboRenderType.Items1")});
            resources.ApplyResources(this.comboRenderType, "comboRenderType");
            this.comboRenderType.Name = "comboRenderType";
            this.comboRenderType.SelectedIndexChanged += new System.EventHandler(this.comboRenderType_SelectedIndexChanged);
            // 
            // Options
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboRenderType);
            this.Controls.Add(this.lblRenderType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
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
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRenderType;
        private System.Windows.Forms.ComboBox comboRenderType;
    }
}