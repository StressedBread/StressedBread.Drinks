using StressedBread.Drinks.Models.DTOs;
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
                var categories = result.Data.Drinks
                    .Select(d => new DrinkCategoryDTO 
                    { 
                        Category = d.StrCategory ?? string.Empty 
                    })
                    .ToList();

                string drinksCategory = _mainMenuUI.DisplayDrinksCategories(categories);
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

        //implement drink images later
        /*foreach (var drink in result.Data?.Drinks ?? [])
        {
            if (!string.IsNullOrEmpty(drink.StrDrinkThumb))
            {
                var imageResult = await _apiService.GetDrinkImageAsync(drink.StrDrinkThumb);
                drinkImages.Add(imageResult);                
            }
        }*/

        if (result.IsSuccess && result.Data != null)
        {
            var drinks = result.Data.Drinks
                    .Select(d => new FilterDrinksByCategoryDTO
                    {
                        Name = d.StrDrink ?? string.Empty,
                        Image = d.StrDrinkThumb ?? string.Empty
                    })
                    .ToList();

            _mainMenuUI.DisplayDrinksByCategory(drinks, drinkImages);
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }
}
