/*
 * This code is under the MIT License
 * 
 * Copyright 2016, Jonathan Alexander, All rights reserved
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunctionManager {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            label5.Text = "Fuck";
            label5.Text = "Version " + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        private void AboutForm_Load(object sender, EventArgs e) {

        }
    }
}
