using Microsoft.Win32;
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
                Console.Write("Testing");
                Console.ReadKey();
                //Add registery code
            } else {
                Console.Write(args[0]);
                using (FileStream fs = File.OpenRead(@args[0])) {
                    StringBuilder path = new StringBuilder(512);
                    GetFinalPathNameByHandle(fs.SafeFileHandle.DangerousGetHandle(), path, path.Capacity, 0);
                    Console.WriteLine(path.ToString());
                }
                Console.ReadKey();
            }
        }
    }
}
