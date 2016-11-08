using Microsoft.Win32;
using Monitor.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JunctionManager {
    class Program {

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int GetFinalPathNameByHandle(IntPtr handle, [In, Out] StringBuilder path, int bufLen, int flags);

        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("In order to add an item to the Windows Explorer context menu, this program must add keys to the registry that reference this executable's path");
                Console.WriteLine("This means that if you ever move this file, you must run it again, to update the path in the registry");
                Console.WriteLine("Type Y to continue this setup process, or N to close the program");
                if (Console.ReadLine().ToUpper() == "Y") {
                    Registry.SetValue("HKEY_CLASSES_ROOT\\Directory\\shell\\JManager", "", "Switch Drives");
                    Registry.SetValue("HKEY_CLASSES_ROOT\\Directory\\shell\\JManager\\command", "", System.Reflection.Assembly.GetEntryAssembly().Location + " \"%1\"");
                    Console.WriteLine("Setup complete! Press any key to exit.");
                    Console.ReadKey();
                }
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
