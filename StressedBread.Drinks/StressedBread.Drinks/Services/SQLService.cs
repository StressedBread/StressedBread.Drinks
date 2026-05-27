using StressedBread.Drinks.Data;
using StressedBread.Drinks.Data.DatabaseQueries;
using StressedBread.Drinks.Models.API;
using Dapper;
using StressedBread.Drinks.Models.DTOs;

namespace StressedBread.Drinks.Services;
internal class SQLService
{
    private readonly DatabaseAccess _databaseAccess;
    private readonly DrinksQueries _drinksQueries;

    internal SQLService(DatabaseAccess databaseAccess, DrinksQueries drinksQueries)
    {
        _databaseAccess = databaseAccess;
        _drinksQueries = drinksQueries;
    }

    internal async Task<int> AddToFavoriteDrinks(DrinkDetailModel drinkDetailModel)
    {
        DynamicParameters parameters = new();

        parameters.Add("@DrinkId", drinkDetailModel.IdDrink);
        parameters.Add("@DrinkName", drinkDetailModel.StrDrink);

        return await _databaseAccess.ExecuteAsync(_drinksQueries.AddToFavoriteDrinksQuery(), parameters);
    }

    internal async Task<List<FavoriteDrinksDTO>> GetFavoriteDrinksAsync()
    {
       var result = await _databaseAccess.QueryAsync<FavoriteDrinksDTO>(_drinksQueries.GetFavoriteDrinksQuery());
       return result.ToList();
    }

    internal async Task IncrementViewCounterAsync(DrinkDetailModel drinkDetailModel)
    {
        DynamicParameters parameters = new();
        parameters.Add("@DrinkId", drinkDetailModel.IdDrink);
        parameters.Add("@DrinkName", drinkDetailModel.StrDrink);

        await _databaseAccess.ExecuteAsync(_drinksQueries.IncrementDrinkViewCountQuery(), parameters);
    }

    internal async Task <List<DrinksViewCountDTO>> GetDrinksViewCountAsync()
    {
        var result = await _databaseAccess.QueryAsync<DrinksViewCountDTO>(_drinksQueries.GetDrinkViewCountQuery());
        return result.ToList();
    }

    internal async Task<int> RemoveFromFavoriteDrinksAsync(int drinkId)
    {
        DynamicParameters parameters = new();

        parameters.Add("@DrinkId", drinkId);

        return await _databaseAccess.ExecuteAsync(_drinksQueries.RemoveFromFavoriteDrinksQuery(), parameters);
    }
}
