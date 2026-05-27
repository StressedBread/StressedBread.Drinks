using Microsoft.Extensions.Configuration;
using StressedBread.Drinks;
using StressedBread.Drinks.Controllers;
using StressedBread.Drinks.Data;
using StressedBread.Drinks.Data.DatabaseQueries;
using StressedBread.Drinks.Services;
using StressedBread.Drinks.UI;

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

var databaseInitQueries = new DatabaseInitQueries();
var drinksQueries = new DrinksQueries();

var databaseInit = new DatabaseInit(configuration);
var databaseAccess = new DatabaseAccess(databaseInit);

await databaseAccess.ExecuteAsync(databaseInitQueries.CreateFavoriteDrinksTableQuery());
await databaseAccess.ExecuteAsync(databaseInitQueries.CreateDrinksViewCountTableQuery());

var app = new App();
var apiConfig = new ApiConfig(configuration);

var apiService = new ApiService(app.Client, apiConfig);
var sqlService = new SQLService(databaseAccess, drinksQueries);

var menuUi = new MenuUI();

var apiController = new ApiController(apiService, menuUi, sqlService);
var mainMenu = new MainMenu(menuUi, apiController);

await mainMenu.DisplayMainMenuAsync();