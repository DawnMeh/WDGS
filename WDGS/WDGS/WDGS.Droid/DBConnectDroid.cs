using System.IO;
using WDGS.Droid;
using Xamarin.Forms;

[assembly: Dependency (typeof (DBConnectDroid))]

namespace WDGS.Droid
{
    public class DBConnectDroid : DBConnect
    {
        public DBConnectDroid () {}
        public SQLite.SQLiteConnection GetConnection () {
            var sqliteFilename = "WDGSDatabase.db3";
            string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}