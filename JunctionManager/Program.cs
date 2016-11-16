using Monitor.Core.Utilities;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace JunctionManager {
    class Program {

        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\junctions.db")) {
                SQLiteConnection.CreateFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\junctions.db");
            }
            SQLiteManager.ExecuteSQLiteCommand("CREATE TABLE IF NOT EXISTS junctions (origin VARCHAR(255), target VARCHAR(255));");
            SQLiteManager.CloseConnection();
            if (args.Length == 0) {
                Application.Run(new JunctionViewForm());
            } else {
                Application.Run(new TransferForm(args[0]));
            }
        }

        static public String GetLastStorage() {
            return Properties.Settings.Default.LastStorageFolder;
        }

        static public void SetLastStorage(String path) {
            Properties.Settings.Default.LastStorageFolder = path;
            Properties.Settings.Default.Save();
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
