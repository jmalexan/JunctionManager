using System;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class ConfigurationForm : Form {
        public ConfigurationForm() {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.StorageLocation;
            button2.Select();
        }

        private void button1_Click(object sender, EventArgs e) {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            Properties.Settings.Default.StorageLocation = textBox1.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
}
