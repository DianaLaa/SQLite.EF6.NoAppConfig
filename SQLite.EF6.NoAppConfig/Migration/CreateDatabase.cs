using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite.EF6.NoAppConfig.Migration
{
    /// <summary>
    /// Migration for Sqlite Entity Framework Code First is not supported.
    /// To create a new, empty database, instantiate this class and call Initialize()
    /// </summary>
    public class CreateDatabase
    {
        private readonly string _databasePath;
        private readonly SQLiteConnection _dbConnection;
        private static Version version = new Version(1, 0);

        public CreateDatabase(string databasePath)
        {
            if (string.IsNullOrEmpty(databasePath))
            {
                throw new ArgumentNullException(nameof(databasePath));
            }

            _databasePath = databasePath;
            _dbConnection = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;");
        }

        public void Run()
        {
            // Always creates a brand new database
            if (!File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }

            SQLiteConnection.CreateFile(_databasePath);

            _dbConnection.Open();
            using (SQLiteCommand command = _dbConnection.CreateCommand())
            {
                // ------------------------------
                // Create your tables here

                command.CommandText =
                    "CREATE TABLE Persons (Id INTEGER NOT NULL, " +
                    "Name VARCHAR(50) NOT NULL, " +
                    "PRIMARY KEY (Id))";
                command.ExecuteNonQuery();

                // ------------------------------
            }
            _dbConnection.Close();
        }
    }
}
