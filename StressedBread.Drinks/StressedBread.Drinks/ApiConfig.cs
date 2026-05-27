using Microsoft.Extensions.Configuration;

namespace StressedBread.Drinks;
internal class ApiConfig
{
    internal string BaseUrl { get; }
    private readonly IConfiguration _configuration;

    internal ApiConfig(IConfiguration configuration)
    {
        _configuration = configuration;
        BaseUrl = _configuration["ApiSettings:BaseUrl"]
                  ?? throw new InvalidOperationException("Base URL not found in appsettings.json.");
    }
}