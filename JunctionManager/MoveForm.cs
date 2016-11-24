/*
 * This code is under the MIT License
 * 
 * Copyright 2016, Jonathan Alexander, All rights reserved
 */

using Monitor.Core.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class MoveForm : Form {

        private string target;
        private string origin;
        private string junction;

        public MoveForm() {
            InitializeComponent();
            Close();
        }

        public MoveForm(string origin, string target) {
            InitializeComponent();
            junction = origin;
            this.origin = target;

            ActiveControl = confirmButton;

            destinationInput.Text = this.origin;
        }

        private void confirmButton_Click(object sender, EventArgs e) {
            //If the destinatino box is empty, select it, play a tone, and quit the method
            if (destinationInput.Text.Length == 0) {
                ActiveControl = destinationInput;
                System.Media.SystemSounds.Exclamation.Play();
                return;
            }

            //Warn the user if they are attempting to put the folder into the folder, which will lead to recursion
            if (destinationInput.Text == origin.Substring(0, origin.LastIndexOf('\\'))) {
                DialogResult recursionCaution = MessageBox.Show("You're attempting to move a folder within itself, this will put this folder within itself forever until the path is to long.", "Recursion Warning", MessageBoxButtons.OK);
            } else if (destinationInput.Text == origin) {
                DialogResult recursionCaution = MessageBox.Show("You can't move a folder to where it currently is", "No Move Warning", MessageBoxButtons.OK);
            } else if (JunctionPoint.Exists(origin)) {
                DialogResult recursionCaution = MessageBox.Show("Moving a junction isn't allowed, please restore the junction at " + origin + " first.", "Can't Move Junction", MessageBoxButtons.OK);
            } else {

                //If this form is not involving an existing junction...
                //Find the target by getting the input from the user
                target = destinationInput.Text;

                DialogResult confirmDialog = MessageBox.Show("Are you sure you want to move " + origin + " to " + target + " and update the junction at " + junction + " to point to it?", "Confirmation", MessageBoxButtons.YesNo);
                if (confirmDialog == DialogResult.No) {
                    return;
                }

                //If a directory already exists where the folder is going to be moved, ask the user if he is sure he wants to delete it
                if (Directory.Exists(target) && Directory.EnumerateFileSystemEntries(target).Any()) {
                    //
                    DialogResult dialogResult = MessageBox.Show("There is already an existing folder at " + target + ", would you like to delete it?", "Existing Folder Found", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes) {
                        Directory.Delete(target, true);
                    } else {
                        return;
                    }
                }

                //Enable an ambiguous loading indicator
                progressBar.Style = ProgressBarStyle.Marquee;

                //Move the folder and make a junction
                Program.CopyFolder(origin, target);
                Directory.Delete(origin, true);

                Program.Log("INFO: Moved " + origin + " to " + target);

                JunctionPoint.Create(junction, target, true);

                Program.Log("INFO: Updated junction at " + junction + " to point to " + target);

                SQLiteManager.ExecuteSQLiteCommand("UPDATE junctions SET target = '" + target + "' WHERE origin = '" + junction + "';");

                Close();
            }
        }

        private void browseButton_Click(object sender, EventArgs e) {
            //Start the folder browser with the new folder button enabled
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                destinationInput.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void abortButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
