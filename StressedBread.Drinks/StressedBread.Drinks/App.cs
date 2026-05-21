using System.Net.Http.Headers;

namespace StressedBread.Drinks;
internal class App
{
    private readonly HttpClient _client = new();
    internal HttpClient Client
    {   
        get { return _client; } 
    }

    internal App()
    {
        ConfigureHttpClient();
    }

    internal void ConfigureHttpClient()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Add("User-Agent", "Drinks C# App");
    }
}
