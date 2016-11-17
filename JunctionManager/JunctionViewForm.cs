using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class JunctionViewForm : Form {
        public JunctionViewForm() {
            InitializeComponent();

            //Set the active button to be the done button
            ActiveControl = doneButton;

            //Update the data grid
            refreshDataGrid();

            //Label the columns nicer names
            dataGridView1.Columns[0].HeaderText = "Junction";
            dataGridView1.Columns[1].HeaderText = "Path";
        }

        private void refreshDataGrid() {
            //Create a DataSet object
            DataSet dataSet = new DataSet();

            //Get the adapter for the grid view, which will contain every junction in the table
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM junctions;", SQLiteManager.GetSQLiteConnection());
            dataAdapter.Fill(dataSet);

            //Fill the view with the dataset
            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
            //Close the connection
            SQLiteManager.CloseConnection();
        }

        private void JunctionViewForm_Load(object sender, EventArgs e) {

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
    }
}
