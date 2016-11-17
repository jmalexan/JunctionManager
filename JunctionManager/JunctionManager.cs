using Monitor.Core.Utilities;
using System.IO;

namespace JunctionManager {
    class JunctionManager {

        public static void MoveWithJunction(string path, string target) {
            //Copy folder and delete the original folder (essentially a move"
            Program.CopyFolder(path, target);
            Directory.Delete(path, true);
            //Create a junction at the original location pointing to the new location
            JunctionPoint.Create(path, target, false);
            //Update the SQLite database with the new junction created
            SQLiteManager.ExecuteSQLiteCommand("INSERT INTO junctions VALUES ('" + path + "', '" + target + "');");
            SQLiteManager.CloseConnection();
        }

        public static void MoveReplaceJunction(string path, string junctionPath) {
            //Delete the junction
            JunctionPoint.Delete(path);
            //Copy the folder back and delete the original folder (essentially moving the function)
            Program.CopyFolder(junctionPath, path);
            Directory.Delete(junctionPath, true);
            //Update the SQLite database
            SQLiteManager.ExecuteSQLiteCommand("DELETE FROM junctions WHERE origin = '" + path + "';");
            SQLiteManager.CloseConnection();
        }
    }
}
