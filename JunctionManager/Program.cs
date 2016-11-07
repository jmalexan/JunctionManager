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
                Registry.SetValue("HKEY_CLASSES_ROOT\\Directory\\shell\\JManager", "", "Switch Drives");
                Registry.SetValue("HKEY_CLASSES_ROOT\\Directory\\shell\\JManager\\command", "", System.Reflection.Assembly.GetEntryAssembly().Location + " \"%1\"");
                Console.Write(System.Reflection.Assembly.GetEntryAssembly().Location + " \"%1\"");
                //Add registery code
            } else {
                Console.Write(args[0]);
                if (!JunctionPoint.Exists(args[0])) {
                    Console.WriteLine("Stuff");
                    Console.WriteLine("D:\\CStorage" + args[0].Substring(2, args[0].Length - 2));
                    CopyFolder(args[0], "D:\\CStorage" + args[0].Substring(2, args[0].Length - 2));
                    Directory.Delete(args[0], true);
                    JunctionPoint.Create(args[0], "D:\\CStorage" + args[0].Substring(2, args[0].Length - 2), false);
                } else {
                    Console.WriteLine("Things");
                    JunctionPoint.Delete(args[0]);
                    CopyFolder("D:\\CStorage" + args[0].Substring(2, args[0].Length - 2), args[0]);
                    Directory.Delete("D:\\CStorage" + args[0].Substring(2, args[0].Length - 2), true);
                }
            }
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
