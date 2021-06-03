using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonsterPorter
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void btnCreatureBrowse_Click(object sender, EventArgs e)
        {
            var result = browseCreatureFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.CreatureDirectoryPath = browseCreatureFolder.SelectedPath;
            }

            SetCreaturePath(browseCreatureFolder.SelectedPath);
        }

        private void btnLibraryBrowse_Click(object sender, EventArgs e)
        {
            var result = browseLibraryFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.LibraryDirectoryPath = browseLibraryFolder.SelectedPath;
            }

            SetLibraryPath(browseLibraryFolder.SelectedPath);
        }

        private void txtCreatureLocation_Leave(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txtCreatureLocation.Text))
            {
                SetCreaturePath(txtCreatureLocation.Text);
            }
        }

        private void Options_Load(object sender, EventArgs e)
        {
            txtCreatureLocation.Text = Properties.Settings.Default.CreatureDirectoryPath;
            txtLibraryLocation.Text = Properties.Settings.Default.LibraryDirectoryPath;
            comboRenderType.SelectedItem = Properties.Settings.Default.RenderType;
        }

        private void SetCreaturePath(string path)
        {
            txtCreatureLocation.Text = Properties.Settings.Default.CreatureDirectoryPath;
            Properties.Settings.Default.Save();
        }

        private void SetLibraryPath(string path)
        {
            txtLibraryLocation.Text = Properties.Settings.Default.LibraryDirectoryPath;
            Properties.Settings.Default.Save();
        }

        private void comboRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RenderType = comboRenderType.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void txtLibraryLocation_Leave(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txtLibraryLocation.Text))
            {
                SetLibraryPath(txtLibraryLocation.Text);
            }
        }
    }
}
