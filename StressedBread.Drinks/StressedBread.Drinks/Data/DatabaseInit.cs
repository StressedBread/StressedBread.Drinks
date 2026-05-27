using Microsoft.Extensions.Configuration;
using StressedBread.Drinks.Data.DatabaseQueries;

namespace StressedBread.Drinks.Data;
internal class DatabaseInit
{
    internal string DefaultConnectionString { get; }

    internal DatabaseInit()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        DefaultConnectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
    }
}
