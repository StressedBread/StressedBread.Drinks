using StressedBread.Drinks;
using StressedBread.Drinks.Controllers;
using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;
using StressedBread.Drinks.Data;

var databaseAccess = new DatabaseAccess();
var databaseInit = new DatabaseInit(databaseAccess);
databaseInit.InitializeDatabase();

var app = new App();
var apiConfig = new ApiConfig();

var apiService = new ApiService(app.Client, apiConfig);

var menuUi = new MenuUI();

var apiController = new ApiController(apiService, menuUi);

await apiController.LoadDrinkCategoriesAsync();
