using Monitor.Core.Utilities;
using System.IO;

namespace JunctionManager {
    class JunctionManager {

        public static void MoveWithJunction(string origin, string target) {
            //Copy folder and delete the original folder (essentially a move"
            Program.CopyFolder(origin, target);
            Directory.Delete(origin, true);
            //Create a junction at the original location pointing to the new location
            JunctionPoint.Create(origin, target, false);
            //Update the SQLite database with the new junction created
            SQLiteManager.AddJunction(origin, target);
        }

        public static void MoveReplaceJunction(string origin, string target) {
            //Delete the junction
            JunctionPoint.Delete(origin);
            //Copy the folder back and delete the original folder (essentially moving the function)
            Program.CopyFolder(target, origin);
            Directory.Delete(target, true);
            //Update the SQLite database with the removal of the junction
            SQLiteManager.RemoveJunction(origin);
        }
    }
}
