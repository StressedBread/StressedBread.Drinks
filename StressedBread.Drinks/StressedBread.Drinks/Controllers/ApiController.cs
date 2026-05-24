using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;

namespace StressedBread.Drinks.Controllers;
internal class ApiController
{
    private readonly ApiService _apiService;
    private readonly MenuUI _mainMenuUI;

    internal ApiController(ApiService apiService, MenuUI mainMenuUI)
    {
        _apiService = apiService;
        _mainMenuUI = mainMenuUI;
    }

    internal async Task LoadDrinkCategoriesAsync()
    {
        while (true)
        {
            var result = await _apiService.GetDrinksCategoriesAsync();
            if (result.IsSuccess && result.Data != null)
            {
                string drinksCategory = _mainMenuUI.DisplayDrinksCategories(result.Data.Drinks);
                await LoadDrinksByCategoryAsync(drinksCategory);
            }
            else
            {
                _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
            }
        }
    }

    internal async Task LoadDrinksByCategoryAsync(string drinksCategory)
    {
        drinksCategory = drinksCategory.Replace(" ", "_");
        var result = await _apiService.GetDrinksByCategoryAsync(drinksCategory);
        List<byte[]> drinkImages = new();

        foreach (var drink in result.Data?.Drinks ?? [])
        {
            if (!string.IsNullOrEmpty(drink.StrDrinkThumb))
            {
                var imageResult = await _apiService.GetDrinkImageAsync(drink.StrDrinkThumb);
                drinkImages.Add(imageResult);                
            }
        }

        if (result.IsSuccess && result.Data != null)
        {
            _mainMenuUI.DisplayDrinksByCategory(result.Data.Drinks, drinkImages);
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }
}
