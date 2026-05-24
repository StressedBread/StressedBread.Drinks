using StressedBread.Drinks.Helpers;
using StressedBread.Drinks.Models.API;
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

                if (categories.Count == 0)
                {
                    _mainMenuUI.DisplayMessage("No drink categories found.");
                    return;
                }

                string drinksCategory = _mainMenuUI.DisplayDrinksCategories(categories);

                if (String.Equals(drinksCategory, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

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

        if (result.IsSuccess && result.Data != null)
        {
            var drinks = result.Data.Drinks
                    .Select(d => new FilterDrinksByCategoryDTO
                    {
                        Name = d.StrDrink ?? string.Empty,
                        Image = d.StrDrinkThumb ?? string.Empty
                    })
                    .ToList();

            if (drinks.Count == 0)
            {
                _mainMenuUI.DisplayMessage("No drinks found for the selected category.");
                return;
            }

            string selectedDrink = _mainMenuUI.DisplayDrinksByCategory(drinks);

            if (String.Equals(selectedDrink, "Back", StringComparison.OrdinalIgnoreCase))
            {
                await LoadDrinkCategoriesAsync();
                return;
            }

            string selectedDrinkId = result.Data.Drinks.FirstOrDefault(d => d.StrDrink == selectedDrink)?.IdDrink ?? string.Empty;

            await LoadDrinkDetailsAsync(selectedDrinkId);
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }

    internal async Task LoadDrinkDetailsAsync(string drinkId)
    {
        if (string.IsNullOrEmpty(drinkId))
        {
            _mainMenuUI.DisplayMessage("Invalid drink selection.");
            return;
        }

        var result = await _apiService.GetDrinkDetailsAsync(drinkId);

        if (result.IsSuccess && result.Data != null)
        {
            byte[] drinkImage = await _apiService.GetDrinkImageAsync(result.Data.Drinks
                .Select(d => d.StrDrinkThumb).FirstOrDefault() ?? string.Empty);
            
            var drink = DrinkDetailMapper.MapToDrinkDetailDTO(result.Data.Drinks.FirstOrDefault());

            if (drink == null)
            {
                _mainMenuUI.DisplayMessage("Drink details not found.");
                return;
            }

            _mainMenuUI.DisplayDrinkDetails(drink, drinkImage);
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }
}
