using StressedBread.Drinks;
using StressedBread.Drinks.Endpoints;
using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;

var app = new App();
var apiConfig = new ApiConfig();
var listEndpoint = new ListEndpoint();

var apiService = new ApiService(app.Client, apiConfig, listEndpoint);

var mainMenu = new MainMenuUI();

apiService.GetDrinksByCategory();
