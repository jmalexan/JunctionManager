

using Monitor.Core.Utilities;
using System.IO;

namespace JunctionManager {
    class JunctionManager {

        public static void MoveWithJunction(string path, string target) {
            Program.CopyFolder(path, target);
            Directory.Delete(path, true);
            JunctionPoint.Create(path, target, false);
            SQLiteManager.ExecuteSQLiteCommand("INSERT INTO junctions VALUES ('" + path + "', '" + target + "');");
            SQLiteManager.CloseConnection();
        }

        public static void MoveReplaceJunction(string path, string junctionPath) {
            JunctionPoint.Delete(path);
            Program.CopyFolder(junctionPath, path);
            Directory.Delete(junctionPath, true);
            SQLiteManager.ExecuteSQLiteCommand("DELETE FROM junctions WHERE origin = '" + path + "';");
            SQLiteManager.CloseConnection();
        }
    }
}
