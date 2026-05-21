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

    internal async Task GetDrinksByCategory()
    {
        var result = await _apiService.GetDrinksByCategory();
        if (result.IsSuccess)
        {
            _mainMenuUI.DisplayDrinksByCategory(result.Data);
        }
        else
        {
            _mainMenuUI.DisplayError(result.ErrorMessage, result.ErrorType);
        }
    }
}
