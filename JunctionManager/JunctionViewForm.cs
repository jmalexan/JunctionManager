using Monitor.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class JunctionViewForm : Form {
        public JunctionViewForm() {
            InitializeComponent();

            restoreButton.Enabled = false;

            //Set the active button to be the done button
            ActiveControl = doneButton;

            //Update the data grid
            refreshDataGrid();

            //Label the columns nicer names
            dataGridView1.Columns[0].HeaderText = "Junction";
            dataGridView1.Columns[1].HeaderText = "Path";
        }

        private void refreshDataGrid() {

            SQLiteDataReader reader =  SQLiteManager.ExecuteSQLiteCommand("SELECT * FROM junctions;");
            List<string> sqlCommandQueue = new List<string>();
            while (reader.Read()) {
                string origin = reader.GetString(reader.GetOrdinal("origin"));
                string target = reader.GetString(reader.GetOrdinal("target"));
                if (!JunctionPoint.Exists(origin)) {
                    if (Directory.Exists(origin)) {
                        MessageBox.Show("The junction at " + origin + " that pointed to " + target + " has been replaced by a folder by the same name.  If you moved the folder back yourself this is fine, otherwise you might wanna look into this", "Junction is now a folder", MessageBoxButtons.OK);
                        sqlCommandQueue.Add("DELETE FROM junctions WHERE origin = '" + origin + "';");
                        Program.Log("WARNING: Junction at " + origin + " that pointed to " + target + " replaced by a folder with the same name");
                        continue;
                    } else {
                        MessageBox.Show("The junction at " + origin + " that pointed to " + target + " is not there, it could have been moved or deleted.", "Missing junction", MessageBoxButtons.OK);
                        sqlCommandQueue.Add("DELETE FROM junctions WHERE origin = '" + origin + "';");
                        Program.Log("WARNING: Junction at " + origin + " that pointed to " + target + " missing");
                        continue;
                    }
                    
                }
                string realTarget = JunctionPoint.GetTarget(origin);
                if (realTarget != target) {
                    MessageBox.Show("The junction at " + origin + " has changed targets from " + target + " to " + realTarget + ".", "Moved junction target", MessageBoxButtons.OK);
                    sqlCommandQueue.Add("UPDATE junctions SET target = '" + realTarget + "' WHERE origin = '" + origin + "';");
                    target = realTarget;
                    Program.Log("WARNING: Junction at " + origin + " is now pointing to " + realTarget + ", was pointing to " + target);
                }
                if (!Directory.Exists(realTarget)) {
                    MessageBox.Show("The folder at " + target + " is missing, the junction " + origin + " pointed to it.", "Folder missing", MessageBoxButtons.OK);
                    JunctionPoint.Delete(origin);
                    sqlCommandQueue.Add("DELETE FROM junctions WHERE origin = '" + origin + "';");
                    Program.Log("WARNING: " + target + " is missing, pointed to by junction at " + origin);
                }
            }
            SQLiteManager.CloseConnection();
            foreach (string s in sqlCommandQueue) {
                SQLiteManager.ExecuteSQLiteCommand(s);
                SQLiteManager.CloseConnection();
            }
            SQLiteManager.CloseConnection();

            //Create a DataSet object
            DataSet dataSet = new DataSet();

            //Get the adapter for the grid view, which will contain every junction in the table
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM junctions;", SQLiteManager.GetSQLiteConnection());
            dataAdapter.Fill(dataSet);

            //Fill the view with the dataset
            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
            //Close the connection
            SQLiteManager.CloseConnection();

            if (dataGridView1.Rows.Count == 0) {
                restoreButton.Enabled = false;
                moveButton.Enabled = false;
            } else {
                restoreButton.Enabled = true;
                moveButton.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            //Close the form
            Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            //Open a transfer form to move the folder back to the junction location
            (new TransferForm(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
            //Update the grid view
            refreshDataGrid();
        }

        private void button4_Click(object sender, EventArgs e) {
            //Update the grid view
            refreshDataGrid();
        }

        private void addExistingJunctionToolStripMenuItem_Click(object sender, EventArgs e) {
            (new ExistingJunctionForm()).ShowDialog();
            refreshDataGrid();
        }

        private void createJunctionToolStripMenuItem_Click(object sender, EventArgs e) {
            (new CreateForm()).ShowDialog();
            refreshDataGrid();
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(Program.GetExeFolder() + "\\junction.log.txt");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            (new AboutForm()).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) {
            (new MoveForm(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString())).ShowDialog();
        }
    }
}
