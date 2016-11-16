using Monitor.Core.Utilities;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace JunctionManager {
    class Program {

        [STAThread]
        static void Main(string[] args) {
            //Form initialization
            if (Environment.OSVersion.Version.Major >= 6) {
                SetProcessDPIAware();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Create the database, if it doesn't exist already, in the executable's folder
            string exeFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!File.Exists(exeFolder + "\\junctions.db")) {
                Boolean stuff = true;
                SQLiteConnection.CreateFile(exeFolder + "\\junctions.db");
            }
            //Create the table in the database, if it doesn't exist already
            SQLiteManager.ExecuteSQLiteCommand("CREATE TABLE IF NOT EXISTS junctions (origin VARCHAR(255), target VARCHAR(255));");
            SQLiteManager.CloseConnection();
            //Launch the main junction list form if no argument is supplied (launched directly or through start menu),
            //or launch the transfer form if a argument is supplied
            if (args.Length == 0) {
                Application.Run(new JunctionViewForm());
            } else {
                Application.Run(new TransferForm(args[0]));
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        //
        // Summary:
        //     Get the last storage destination, stored in the properties.
        //
        // Returns:
        //     The path of the last storage destination
        //
        static public String GetLastStorage() {
            return Properties.Settings.Default.LastStorageFolder;
        }

        //
        // Summary:
        //     Sets the last storage destination, which will be stored in the properties
        //
        // Parameters:
        //   path:
        //     The path to store
        //
        static public void SetLastStorage(String path) {
            Properties.Settings.Default.LastStorageFolder = path;
            Properties.Settings.Default.Save();
        }

        //
        // Summary:
        //     Copies an entire folder from one location to another, supports copying across disks
        //
        // Parameters:
        //   sourceFolder:
        //     The folder that will be copied
        //
        //   destFolder:
        //     Where the folder will be copied to
        //
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
