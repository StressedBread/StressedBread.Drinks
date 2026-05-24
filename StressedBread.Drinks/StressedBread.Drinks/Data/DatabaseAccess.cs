using Microsoft.Data.Sqlite;
using Dapper;

namespace StressedBread.Drinks.Data;
internal class DatabaseAccess
{
    internal void Execute(string connectionString, string query, object? parameters = null)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        connection.Execute(query, parameters);
    }
}
