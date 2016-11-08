using Monitor.Core.Utilities;
using System;
using System.IO;

namespace JunctionManager {
    class Program {
        static void Main(string[] args) {
            if (args.Length > 0) {
                if (!JunctionPoint.Exists(args[0])) {
                    Console.WriteLine("Moving " + args[0] + " to " + GetOtherDiskPath(args[0]));
                    Console.WriteLine("Type Y to confirm, or anything else to abort");
                    if (Console.ReadLine().ToUpper() == "Y") {
                        CopyFolder(args[0], GetOtherDiskPath(args[0]));
                        Directory.Delete(args[0], true);
                        JunctionPoint.Create(args[0], GetOtherDiskPath(args[0]), false);
                    }
                } else {
                    Console.WriteLine("Moving " + GetOtherDiskPath(args[0]) + " to " + args[0]);
                    Console.WriteLine("Type Y to confirm, or anything else to abort");
                    if (Console.ReadLine().ToUpper() == "Y") {
                        JunctionPoint.Delete(args[0]);
                        CopyFolder(GetOtherDiskPath(args[0]), args[0]);
                        Directory.Delete(GetOtherDiskPath(args[0]), true);
                    }
                }
            }
        }

        static private String GetOtherDiskPath(String path) {
            return "D:\\CStorage" + path.Substring(2, path.Length - 2);
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
