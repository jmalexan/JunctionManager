using Monitor.Core.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace JunctionManager {
    class Program {

        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0) {
                Application.Run(new ConfigurationForm());
            } else {
                if (!JunctionPoint.Exists(args[0])) {
                    Console.WriteLine("Moving " + args[0] + " to " + GetOtherDiskPath(args[0]));
                    Console.WriteLine("Type Y to confirm, or anything else to abort");
                    if (Console.ReadLine().ToUpper() == "Y") {
                        CopyFolder(args[0], GetOtherDiskPath(args[0]));
                        Directory.Delete(args[0], true);
                        JunctionPoint.Create(args[0], GetOtherDiskPath(args[0]), false);
                    }
                } else {
                    string target = JunctionPoint.GetTarget(args[0]);
                    Console.WriteLine("Moving " + target + " to " + args[0]);
                    Console.WriteLine("Type Y to confirm, or anything else to abort");
                    if (Console.ReadLine().ToUpper() == "Y") {
                        JunctionPoint.Delete(args[0]);
                        CopyFolder(target, args[0]);
                        Directory.Delete(target, true);
                    }
                }
            }
        }

        static private String GetOtherDiskPath(String path) {
            return Properties.Settings.Default.StorageLocation + path.Substring(2, path.Length - 2);
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
