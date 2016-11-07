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
                Console.ReadKey();
                //Add registery code
            } else {
                Console.Write(args[0]);
                if (JunctionPoint.Exists(args[0])) {
                    Console.WriteLine("Stuff");
                }
                Console.ReadKey();
            }
        }
    }
}
