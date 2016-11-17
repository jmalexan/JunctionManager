using Monitor.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager
{
    public partial class ExistingJunctionForm : Form
    {
        public ExistingJunctionForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (junctionPathBox.Text.Length == 0) {
                ActiveControl = junctionPathBox;
                System.Media.SystemSounds.Exclamation.Play();
            } else {
                string origin = junctionPathBox.Text;
                if (JunctionPoint.Exists(origin)) {
                    string target = JunctionPoint.GetTarget(origin);
                    SQLiteManager.AddJunction(origin, target);
                    Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Start the folder browser with the new folder button enabled
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                junctionPathBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
