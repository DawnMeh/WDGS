using System;
using System.IO;
using WDGS.iOS;
using Xamarin.Forms;

[assembly: Dependency (typeof (DBConnectiOS))]

namespace WDGS.iOS
{
    public class DBConnectiOS : DBConnect
    {
        public DBConnectiOS () {}
        public SQLite.SQLiteConnection GetConnection ()
        {
            var sqliteFilename = "WDGSDatabase.db3";
            string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
