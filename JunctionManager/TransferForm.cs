using Monitor.Core.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class TransferForm : Form {

        string path;
        string junctionPath;
        Boolean junctionArg;

        public TransferForm(string arg) {
            InitializeComponent();
            path = arg;
            if (!JunctionPoint.Exists(path)) {
                junctionArg = false;
                textBox1.Text = Program.GetOtherDiskPath(path);
            } else {
                junctionArg = true;
                textBox1.Visible = false;
                button3.Visible = false;
                junctionPath = JunctionPoint.GetTarget(path);
                label1.Text = "Move " + junctionPath + " back to its original location at " + path + "?";
            }
        }

        private void TransferForm_Load(object sender, EventArgs e) {

        }

        private async void button2_Click(object sender, EventArgs e) {
            progressBar1.Style = ProgressBarStyle.Marquee;
            if (!junctionArg) {
                string target = textBox1.Text + "\\" + new DirectoryInfo(path).Name;
                await Task.Run(() => Program.MoveWithJunction(path, target));
            } else {
                await Task.Run(() => Program.MoveReplaceJunction(path, junctionPath));
            }          
            Close();
        }


        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
