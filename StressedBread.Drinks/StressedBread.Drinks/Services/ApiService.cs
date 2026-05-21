using StressedBread.Drinks.Endpoints;
using StressedBread.Drinks.Models.API;
using Newtonsoft.Json;
using System.Net.Http.Json;
using StressedBread.Drinks.Models;
using static StressedBread.Drinks.Enums;

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

    internal async Task<ApiResult<T>> GetJsonAsync<T>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();

            return ApiResult<T>.Success(result);
        }
        catch (HttpRequestException ex)
        {
            return ApiResult<T>.Failure(ex.Message, ErrorType.HttpError);
        }
        catch (TaskCanceledException ex)
        {
            return ApiResult<T>.Failure(ex.Message, ErrorType.Timeout);
        }
        catch (JsonException ex)
        {
            return ApiResult<T>.Failure(ex.Message, ErrorType.JsonError);
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Failure(ex.Message, ErrorType.UnknownError);
        }
    }

    internal async Task<ApiResult<JsonRootModel<DrinkCategoryModel>>> GetDrinksByCategory()
    {
        return await GetJsonAsync<JsonRootModel<DrinkCategoryModel>>($"{_apiConfig.BaseUrl}{_listEndpoint.Category}");
    }
}
