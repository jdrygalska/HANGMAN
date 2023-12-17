using System;
using System.Data.SQLite;

public class HangmanDatabase
{
    private const string ConnectionString = "Data Source=HangmanDB.sqlite;Version=3;";

    public static void InitializeDatabase()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();


            string createTableQuery = "CREATE TABLE IF NOT EXISTS Scores (Id INTEGER PRIMARY KEY, Username TEXT, Score INTEGER, Difficulty TEXT);";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static void InsertScore(string username, int score, string difficulty)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string insertQuery = "INSERT INTO Scores (Username, Score, Difficulty) VALUES (@Username, @Score, @Difficulty);";
            using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Score", score);
                command.Parameters.AddWithValue("@Difficulty", difficulty);
                command.ExecuteNonQuery();
            }
        }
    }
}
