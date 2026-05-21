using StressedBread.Drinks.Endpoints;
using StressedBread.Drinks.Models.API;
using Newtonsoft.Json;

namespace StressedBread.Drinks.Services;
internal class ApiService
{
    private readonly HttpClient _client;
    private readonly ApiConfig _apiConfig;
    private readonly ListEndpoint _listEndpoint;

    public ApiService(HttpClient client, ApiConfig apiConfig, ListEndpoint listEndpoint)
    {
        _client = client;
        _apiConfig = apiConfig;
        _listEndpoint = listEndpoint;
    }

    internal JsonRootModel<DrinkCategoryModel>? GetDrinksByCategory()
    {
        var json = _client.GetStringAsync($"{_apiConfig.BaseUrl}{_listEndpoint.Category}").Result;
        return JsonConvert.DeserializeObject<JsonRootModel<DrinkCategoryModel>>(json);
    }
}
