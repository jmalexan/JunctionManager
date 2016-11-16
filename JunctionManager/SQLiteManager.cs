

using System.Data.SQLite;
using System.IO;

namespace JunctionManager {
    class SQLiteManager {

        public static SQLiteConnection junctionDb;

        public static SQLiteDataReader ExecuteSQLiteCommand(string stringCommand) {
            GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(stringCommand, GetSQLiteConnection());
            return command.ExecuteReader();
        }

        public static SQLiteConnection GetSQLiteConnection() {
            junctionDb = new SQLiteConnection("Data Source=" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\junctions.db;Version=3");
            junctionDb.Open();
            return junctionDb;
        }

        public static void CloseConnection() {
            junctionDb.Close();
        }
    }
}
