using System.Data.SQLite;
using System.IO;

namespace JunctionManager {
    class SQLiteManager {

        private static SQLiteConnection junctionDb;

        public static SQLiteDataReader ExecuteSQLiteCommand(string stringCommand) {
            //Get the connection then send the command, then return the response
            GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(stringCommand, GetSQLiteConnection());
            return command.ExecuteReader();
        }

        public static SQLiteConnection GetSQLiteConnection() {
            //Connect to the database in "junctions.db" in the executable's folder
            junctionDb = new SQLiteConnection("Data Source=" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\junctions.db;Version=3");
            junctionDb.Open();
            return junctionDb;
        }

        public static void RemoveJunction(string origin)
        {
            ExecuteSQLiteCommand("DELETE FROM junctions WHERE origin = '" + origin + "';");
            CloseConnection();
        }

        public static void AddJunction(string origin, string target)
        {
            SQLiteManager.ExecuteSQLiteCommand("INSERT INTO junctions VALUES ('" + origin + "', '" + target + "');");
            SQLiteManager.CloseConnection();
        }

        public static void CloseConnection() {
            //Close the connection, so you don't need to reference the connection outside this class
            junctionDb.Close();
        }
    }
}
