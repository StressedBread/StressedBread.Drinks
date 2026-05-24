using Microsoft.Extensions.Configuration;

namespace StressedBread.Drinks.Data;
internal class DatabaseInit
{
    internal string DefaultConnectionString { get; }

    private DatabaseAccess _databaseAccess;

    internal DatabaseInit(DatabaseAccess databaseAccess)
    {
        _databaseAccess = databaseAccess;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        DefaultConnectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
    }

    internal void InitializeDatabase()
    {
        _databaseAccess.Execute(DefaultConnectionString, new DatabaseQueries.DatabaseInitQueries().CreateDrinksViewCountTableQuery());
        _databaseAccess.Execute(DefaultConnectionString, new DatabaseQueries.DatabaseInitQueries().CreateFavoriteDrinksTableQuery());
    }
}
