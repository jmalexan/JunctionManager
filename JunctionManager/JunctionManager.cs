/*
 * This code is under the MIT License
 * 
 * Copyright 2016, Jonathan Alexander, All rights reserved
 */

using Monitor.Core.Utilities;
using System.IO;

namespace JunctionManager {
    class JunctionManager {

        public static void MoveWithJunction(string origin, string target) {
            //Copy folder and delete the original folder (essentially a move"
            Program.CopyFolder(origin, target);
            Directory.Delete(origin, true);

            Program.Log("INFO: Moved " + origin + " to " + target);
            //Create a junction at the original location pointing to the new location
            JunctionPoint.Create(origin, target, true);
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
            Program.Log("INFO: Moved " + target + " to " + origin);
        }
    }
}
