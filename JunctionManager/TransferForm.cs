/*
 * This code is under the MIT License
 * 
 * Copyright 2016, Jonathan Alexander, All rights reserved
 */

using Monitor.Core.Utilities;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class TransferForm : Form {

        string origin;
        string target;
        Boolean junctionArg;

        public TransferForm(string arg) {
            InitializeComponent();
            //Select the confirm button by default
            ActiveControl = confirmButton;
            origin = arg;
            //Get any junctions that point to the folder being moved, and save where they were moved from if they exist
            SQLiteDataReader reader = SQLiteManager.ExecuteSQLiteCommand("SELECT origin, target FROM junctions WHERE target = '" + origin + "';");
            string databaseOrigin = null;
            if (reader.Read()) {
                databaseOrigin = reader.GetString(reader.GetOrdinal("origin"));
            }
            reader.Close();
            SQLiteManager.CloseConnection();
            if (!JunctionPoint.Exists(origin)) {
                //If the directory given is not a junction and is registered in the database...
                if (databaseOrigin != null) {
                    //Hide some elements to just show a message instead of getting input from the user
                    junctionArg = true;
                    destinationInput.Visible = false;
                    browseButton.Visible = false;
                    //Assign the variables their proper values
                    target = origin;
                    origin = databaseOrigin;
                    //Give the user a message that this folder has been moved by this app, and ask if they want to move it back
                    label1.Text = target + " is already moved from " + origin + "! Would you like to move it back?";
                } else {
                    //If the directory given is not a junction and isn't registered in the database...
                    //Leave the window in its standard move with junction state, and put the last used location as the default
                    junctionArg = false;
                    destinationInput.Text = Program.GetLastStorage() + "\\" + new DirectoryInfo(origin).Name;
                }
            } else {
                //if the directory given is a junction
                //Hide some elements to just show a message instead of getting input from the user
                junctionArg = true;
                destinationInput.Visible = false;
                browseButton.Visible = false;
                //Get the directory the junction is pointing to and ask the user if he would like to move that folder back
                target = JunctionPoint.GetTarget(origin);
                label1.Text = "Move " + target + " back to its original location at " + origin + "?";
            }
        }

        private async void button2_Click(object sender, EventArgs e) {
            if (!junctionArg) {
                //If this form is not involving an existing junction...
                //Find the target by getting the input from the user
                target = destinationInput.Text;

                //If the destinatino box is empty, select it, play a tone, and quit the method
                if (destinationInput.Text.Length == 0) {
                    ActiveControl = destinationInput;
                    System.Media.SystemSounds.Exclamation.Play();
                    return;
                }

                //Warn the user if they are attempting to put the folder into the folder, which will lead to recursion
                if (destinationInput.Text == origin.Substring(0, target.LastIndexOf('\\'))) {
                    DialogResult recursionCaution = MessageBox.Show("You're attempting to move a folder within itself, this will put this folder within itself forever until the path is to long.", "Recursion Warning", MessageBoxButtons.OK);
                } else if (destinationInput.Text == origin) {
                    DialogResult recursionCaution = MessageBox.Show("You can't move a folder to where it currently is", "No Move Warning", MessageBoxButtons.OK);
                } else if (JunctionPoint.Exists(origin)) {
                    DialogResult recursionCaution = MessageBox.Show("Moving a junction isn't allowed, please restore the junction at " + origin + " first.", "Can't Move Junction", MessageBoxButtons.OK);
                } else {
                    DialogResult confirmDialog = MessageBox.Show("Are you sure you want to create a junction at " + origin + " that links to " + target + "?", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirmDialog == DialogResult.No) {
                        return;
                    }
                    //If a directory already exists where the folder is going to be moved, ask the user if he is sure he wants to delete it
                    if (Directory.Exists(target) && Directory.EnumerateFileSystemEntries(target).Any()) {
                        //
                        DialogResult dialogResult = MessageBox.Show("There is already an existing folder at " + target + ", would you like to delete it?", "Existing Folder Found", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes) {
                            Directory.Delete(target, true);
                        }
                        else {
                            return;
                        }
                    }

                    //Enable an ambiguous loading indicator
                    progressBar.Style = ProgressBarStyle.Marquee;

                    //Move the folder and make a junction
                    await Task.Run(() => JunctionManager.MoveWithJunction(origin, target));
                    //Take the final destination choice of the user and save it for next time
                    Program.SetLastStorage(target.Substring(0, target.LastIndexOf('\\')));
                }
            } else {
                //Enable an ambiguous loading indicator
                progressBar.Style = ProgressBarStyle.Marquee;
                //Move the folder back and replace the junction
                await Task.Run(() => JunctionManager.MoveReplaceJunction(origin, target));
            }
            //Close the form
            Close();
        }


        private void button1_Click(object sender, EventArgs e) {
            //Close the form
            Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            //Start the folder browser with the new folder button enabled
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                destinationInput.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
