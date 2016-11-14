using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class JunctionViewForm : Form {
        public JunctionViewForm() {
            InitializeComponent();

            refreshDataGrid();

            dataGridView1.Columns[0].HeaderText = "Junction";
            dataGridView1.Columns[1].HeaderText = "Path";
        }

        private void refreshDataGrid() {
            DataSet dataSet = new DataSet();

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM junctions;", Program.GetSQLiteConnection());
            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
            Program.junctionDb.Close();
        }

        private void JunctionViewForm_Load(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            (new ConfigurationForm()).ShowDialog();
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
