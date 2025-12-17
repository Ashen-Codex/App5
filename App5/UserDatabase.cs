using Microsoft.Data.Sqlite;
using System.IO;
using Windows.Storage;

namespace App5
{
    public class UserDatabase
    {
        private string _dbPath;

        public UserDatabase()
        {
            _dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "users.db");
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Email TEXT PRIMARY KEY,
                        PasswordHash TEXT NOT NULL
                    )";
                command.ExecuteNonQuery();
            }
        }

        public bool AddUser(string email, string passwordHash)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Users (Email, PasswordHash)
                    VALUES (@email, @passwordHash)";
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@passwordHash", passwordHash);

                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public User GetUser(string email)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users WHERE Email = @email";
                command.Parameters.AddWithValue("@email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Email = reader.GetString(0),
                            PasswordHash = reader.GetString(1)
                        };
                    }
                    return null;
                }
            }
        }
    }
}
