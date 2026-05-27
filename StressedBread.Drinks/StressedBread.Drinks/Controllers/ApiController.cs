using StressedBread.Drinks.Data;
using StressedBread.Drinks.Helpers;
using StressedBread.Drinks.Models.API;
using StressedBread.Drinks.Models.DTOs;
using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;
using static StressedBread.Drinks.Enums;

namespace StressedBread.Drinks.Controllers;
internal class ApiController
{
    private readonly ApiService _apiService;
    private readonly MenuUI _mainMenuUI;
    private readonly SQLService _sqlService;

    internal ApiController(ApiService apiService, MenuUI mainMenuUI, SQLService sqlService)
    {
        _apiService = apiService;
        _mainMenuUI = mainMenuUI;
        _sqlService = sqlService;
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
            var drinkModel = result.Data.Drinks.FirstOrDefault();

            if (drinkModel != null) await _sqlService.IncrementViewCounterAsync(drinkModel);

            byte[] drinkImage = await _apiService.GetDrinkImageAsync(drinkModel?.StrDrinkThumb ?? string.Empty);
            
            var drinkDTO = DrinkDetailMapper.MapToDrinkDetailDTO(drinkModel);

            if (drinkDTO == null)
            {
                _mainMenuUI.DisplayMessage("Drink details not found.");
                return;
            }

            var drinkOption = _mainMenuUI.DisplayDrinkDetails(drinkDTO, drinkImage);
            if (drinkOption == DrinkOption.AddToFavorites && drinkModel != null)
            {
                int rows = await _sqlService.AddToFavoriteDrinks(drinkModel);

                if (rows == 0) _mainMenuUI.DisplayMessage("Drink is already in favorites.");
                else _mainMenuUI.DisplayMessage("Drink added to favorites.");
            }
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }

    internal async Task LoadFavoriteDrinksAsync()
    {
        var favoriteDrinks = await _sqlService.GetFavoriteDrinksAsync();

        if (favoriteDrinks == null || favoriteDrinks.Count == 0)
        {
            _mainMenuUI.DisplayMessage("No favorite drinks found.");
            return;
        } 
            
        var result = _mainMenuUI.DisplayFavoriteDrinks(favoriteDrinks);
        if (result == "0") return;

        if (int.TryParse(result, out int drinkId))
        {
            var drinkToRemove = favoriteDrinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (drinkToRemove != null)
            {
                int rowsAffected = await _sqlService.RemoveFromFavoriteDrinksAsync(drinkId);
                if (rowsAffected > 0)
                {
                    _mainMenuUI.DisplayMessage($"Drink with ID {drinkId} removed from favorites.");
                }
                else
                {
                    _mainMenuUI.DisplayMessage($"Failed to remove drink with ID {drinkId} from favorites.");
                }
            }
            else
            {
                _mainMenuUI.DisplayMessage($"No drink found with ID {drinkId} in favorites.");
            }
        }
        else
        {
            _mainMenuUI.DisplayMessage("Invalid input. Please enter a valid drink ID or 0 to return to the main menu.");
        }
    }

    internal async Task LoadDrinkViewCounterAsync()
    {
        var drinkViewCounts = await _sqlService.GetDrinksViewCountAsync();

        if (drinkViewCounts == null || drinkViewCounts.Count == 0)
        {
            _mainMenuUI.DisplayMessage("No drink view counts found.");
            return;
        }

        _mainMenuUI.DisplayDrinkViewCounter(drinkViewCounts);
    }
}
