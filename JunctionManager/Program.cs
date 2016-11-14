﻿using Monitor.Core.Utilities;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace JunctionManager {
    class Program {

        static SQLiteConnection junctionDb;

        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!File.Exists("junctions.db")) {
                SQLiteConnection.CreateFile("junctions.db");
            }
            if (args.Length == 0) {
                Application.Run(new ConfigurationForm());
            } else {
                Application.Run(new TransferForm(args[0]));
            }
        }

        public static void MoveWithJunction(string path, string target) {
            if (Directory.Exists(target)) {
                Directory.Delete(target, true);
            }
            Program.CopyFolder(path, target);
            Directory.Delete(path, true);
            JunctionPoint.Create(path, target, false);
        }

        public static void MoveReplaceJunction(string path, string junctionPath) {
            JunctionPoint.Delete(path);
            Program.CopyFolder(junctionPath, path);
            Directory.Delete(junctionPath, true);
        }

        static public String GetOtherDiskPath(String path) {
            return Properties.Settings.Default.StorageLocation;
        }

        static public void CopyFolder(string sourceFolder, string destFolder) {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files) {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders) {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
    }
}
