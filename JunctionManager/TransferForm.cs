using Monitor.Core.Utilities;
using System;
using System.Data.SQLite;
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
            ActiveControl = button2;
            path = arg;
            SQLiteDataReader reader = SQLiteManager.ExecuteSQLiteCommand("SELECT origin, target FROM junctions WHERE target = '" + path + "';");
            string databaseOrigin = null;
            if (reader.Read()) {
                databaseOrigin = reader.GetString(reader.GetOrdinal("origin"));
            }
            reader.Close();
            SQLiteManager.CloseConnection();
            if (!JunctionPoint.Exists(path)) {
                if (databaseOrigin != null) {
                    junctionArg = true;
                    textBox1.Visible = false;
                    button3.Visible = false;
                    junctionPath = path;
                    path = databaseOrigin;
                    label1.Text = junctionPath + " is already moved from " + path + "! Would you like to move it back?";
                } else {
                    junctionArg = false;
                    textBox1.Text = Program.GetLastStorage();
                }
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
                if (textBox1.Text == path) {
                    DialogResult recursionCaution = MessageBox.Show("You're attempting to move a folder within itself, this will put this folder within itself forever until the path is to long.", "Recursion Warning", MessageBoxButtons.OK);
                } else {
                    if (Directory.Exists(target)) {
                        DialogResult dialogResult = MessageBox.Show("There is already an existing folder at " + target + ", would you like to delete it?", "Existing Folder Found", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes) {
                            Directory.Delete(target, true);
                        }
                        else {
                            Close();
                        }
                    }
                    await Task.Run(() => JunctionManager.MoveWithJunction(path, target));
                    Program.SetLastStorage(textBox1.Text);
                }
            } else {
                await Task.Run(() => JunctionManager.MoveReplaceJunction(path, junctionPath));
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
