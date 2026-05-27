using StressedBread.Drinks.Controllers;
using StressedBread.Drinks.UI;
using static StressedBread.Drinks.Enums;

namespace StressedBread.Drinks.Services;
internal class MainMenu
{
    private readonly MenuUI _menuUi;
    private readonly ApiController _apiController;

    internal MainMenu(MenuUI menuUi, ApiController apiController)
    {
        _menuUi = menuUi;
        _apiController = apiController;
    }

    internal async Task DisplayMainMenuAsync()
    {
        while (true)
        {
            var option = _menuUi.DisplayMainMenu();

            switch (option)
            {
                case MenuOption.ViewDrinkCategories:
                    await _apiController.LoadDrinkCategoriesAsync();
                    break;
                case MenuOption.ViewFavoriteDrinks:
                    await _apiController.LoadFavoriteDrinksAsync();
                    break;
                case MenuOption.ViewDrinkViewCounter:
                    await _apiController.LoadDrinkViewCounterAsync();
                    break;
                case MenuOption.Exit:
                    return;
            }
        }
    }
}
