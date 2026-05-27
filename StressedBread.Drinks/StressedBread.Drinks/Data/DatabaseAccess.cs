using Microsoft.Data.Sqlite;
using Dapper;

namespace StressedBread.Drinks.Data;
internal class DatabaseAccess
{
    private readonly DatabaseInit _databaseInit;
    private readonly string _connectionString;
    internal DatabaseAccess(DatabaseInit databaseInit)
    {
        _databaseInit = databaseInit;
        _connectionString = _databaseInit.DefaultConnectionString;
    }

    internal async Task<int> ExecuteAsync(string query, object? parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.ExecuteAsync(query, parameters);
    }

    internal async Task<IEnumerable<T>> QueryAsync<T>(string query)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(query);
    }
}
