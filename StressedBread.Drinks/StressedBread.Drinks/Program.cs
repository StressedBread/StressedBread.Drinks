using StressedBread.Drinks;
using StressedBread.Drinks.Controllers;
using StressedBread.Drinks.Endpoints;
using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;

var app = new App();
var apiConfig = new ApiConfig();
var listEndpoint = new ListEndpoint();

var apiService = new ApiService(app.Client, apiConfig, listEndpoint);

var menuUi = new MenuUI();

var apiController = new ApiController(apiService, menuUi);

await apiController.GetDrinksByCategory();
