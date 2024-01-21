using System.Data.SQLite;

namespace CarpMc.Utils
{
    public class Sqlite
    {
        private SQLiteConnection _connection;

        public Sqlite() 
        {
            _connection = new SQLiteConnection("Data Source=data.db");
            _connection.Open();
        }

        public void initializeDatabaseTable()
        {
            using (var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS settings (Key TEXT PRIMARY KEY, Value TEXT)", _connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS profile (Uid TEXT PRIMARY KEY, Username TEXT, Password TEXT)", _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void insertDataIntoSettings(string keyName, string value)
        {
            using (SQLiteCommand command = new SQLiteCommand("INSERT OR REPLACE INTO settings (Key, Value) VALUES (@Value1, @Value2)", _connection))
            {
                command.Parameters.AddWithValue("@Value1", keyName);
                command.Parameters.AddWithValue("@Value2", value);
                command.ExecuteNonQuery();
            }
        }

        public string? getDataFromSettings(string keyName)
        {
            using (var command = new SQLiteCommand("SELECT Value FROM settings WHERE Key = @Param", _connection))
            {
                command.Parameters.AddWithValue("@Param", keyName);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string result = reader.GetString(0); // 0表示第一列
                        return result;
                    }
                }
            }

            return null;
        }

        public void insertDataIntoProfile(string uid, string username, string password)
        {
            using (SQLiteCommand command = new SQLiteCommand("INSERT OR REPLACE INTO profile (Uid, Username, Password) VALUES (@Value1, @Value2, @Value3)", _connection))
            {
                command.Parameters.AddWithValue("@Value1", uid);
                command.Parameters.AddWithValue("@Value2", username);
                command.Parameters.AddWithValue("@Value3", password);
                command.ExecuteNonQuery();
            }
        }
        public string?[]? getDataFromProfile(string username, string password)
        {
            using (var command = new SQLiteCommand("SELECT * FROM profile WHERE Username = @Param1 AND Password = @Param2", _connection))
            {
                command.Parameters.AddWithValue("@Param1", username);
                command.Parameters.AddWithValue("@Param2", password);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] row = new object[reader.FieldCount];
                        reader.GetValues(row);
                        string?[] stringRow = row.Select(x => x.ToString()).ToArray();

                        return stringRow;
                    }
                }
            }

            return null;
        }

        
    }
}
