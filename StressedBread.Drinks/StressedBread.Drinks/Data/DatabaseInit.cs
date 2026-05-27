using Microsoft.Extensions.Configuration;

namespace StressedBread.Drinks.Data;
internal class DatabaseInit
{
    internal string DefaultConnectionString { get; }
    private readonly IConfiguration _configuration;

    internal DatabaseInit(IConfiguration configuration)
    {
        _configuration = configuration;
        DefaultConnectionString = _configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
    }
}
