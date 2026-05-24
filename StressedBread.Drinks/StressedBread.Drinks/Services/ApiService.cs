using StressedBread.Drinks.Models.API;
using Newtonsoft.Json;
using System.Net.Http.Json;
using StressedBread.Drinks.Models;
using static StressedBread.Drinks.Enums;
using StressedBread.Drinks.Endpoints;

namespace StressedBread.Drinks.Services;
internal class ApiService
{
    private readonly HttpClient _client;
    private readonly ApiConfig _apiConfig;

    public ApiService(HttpClient client, ApiConfig apiConfig)
    {
        _client = client;
        _apiConfig = apiConfig;
    }

    private async Task<ApiResult<T>> GetJsonAsync<T>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();

            if (result == null)
            {
                return ApiResult<T>.Failure("Received empty response.", ErrorType.JsonError);
            }

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

    internal async Task<ApiResult<JsonRootModel<DrinkCategoryModel>>> GetDrinksCategoriesAsync()
    {
        return await GetJsonAsync<JsonRootModel<DrinkCategoryModel>>($"{_apiConfig.BaseUrl}{ListEndpoint.ListByCategory}");
    }

    internal async Task<ApiResult<JsonRootModel<FilterDrinksByCategoryModel>>> GetDrinksByCategoryAsync(string drinksCategory)
    {
        return await GetJsonAsync<JsonRootModel<FilterDrinksByCategoryModel>>($"{_apiConfig.BaseUrl}{ListEndpoint.FilterByCategory}{drinksCategory}");
    }

    internal async Task<ApiResult<JsonRootModel<DrinkDetailModel>>> GetDrinkDetailsAsync(string drinkId)
    {
        return await GetJsonAsync<JsonRootModel<DrinkDetailModel>>($"{_apiConfig.BaseUrl}{ListEndpoint.LookupById}{drinkId}");
    }

    internal async Task<byte[]> GetDrinkImageAsync(string imageUrl)
    {
        try
        {
            return await _client.GetByteArrayAsync(imageUrl);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            return Array.Empty<byte>();
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"Timeout Error: {ex.Message}");
            return Array.Empty<byte>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return Array.Empty<byte>();
        }
    }
}
