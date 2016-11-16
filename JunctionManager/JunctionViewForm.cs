using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class JunctionViewForm : Form {
        public JunctionViewForm() {
            InitializeComponent();

            ActiveControl = button2;

            refreshDataGrid();

            dataGridView1.Columns[0].HeaderText = "Junction";
            dataGridView1.Columns[1].HeaderText = "Path";
        }

        private void refreshDataGrid() {
            DataSet dataSet = new DataSet();

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM junctions;", SQLiteManager.GetSQLiteConnection());
            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
            SQLiteManager.CloseConnection();
        }

        private void JunctionViewForm_Load(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            (new TransferForm(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
            refreshDataGrid();
        }

        private void button4_Click(object sender, EventArgs e) {
            refreshDataGrid();
        }
    }
}
