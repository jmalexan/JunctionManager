using Monitor.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class TransferForm : Form {

        string path;

        public TransferForm(string arg) {
            InitializeComponent();
            path = arg;
            label1.Text = "Move " + path + " to " + Program.GetOtherDiskPath(path) + "?";
        }

        private void TransferForm_Load(object sender, EventArgs e) {

        }

        private async void button2_Click(object sender, EventArgs e) {
            progressBar1.Style = ProgressBarStyle.Marquee;
            if (!JunctionPoint.Exists(path)) {
                await Task.Run(() => Program.MoveWithJunction(path));
            } else {
                await Task.Run(() => Program.MoveReplaceJunction(path));
            }          
            Close();
        }


        private void button1_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
