namespace StressedBread.Drinks.Data.DatabaseQueries;
internal class DrinksQueries
{
    internal string AddToFavoriteDrinksQuery()
    {
        return @"
            INSERT INTO FavoriteDrinks (DrinkId, DrinkName)
            VALUES (@DrinkId, @DrinkName)
            ON CONFLICT(DrinkId) DO NOTHING;    
            ";
    }

    internal string GetFavoriteDrinksQuery()
    {
        return @"
            SELECT DrinkName, DrinkId
            FROM FavoriteDrinks;
        ";
    }

    internal string RemoveFromFavoriteDrinksQuery()
    {
        return @"
            DELETE FROM FavoriteDrinks
            WHERE DrinkId = @DrinkId;
        ";
    }

    internal string GetDrinkViewCountQuery()
    {
        return @"
            SELECT DrinkName, ViewCount
            FROM DrinksViewCount
            ORDER BY ViewCount DESC;
        ";
    }

    internal string IncrementDrinkViewCountQuery()
    {
        return @"
            INSERT INTO DrinksViewCount (DrinkId, DrinkName, ViewCount)
            VALUES (@DrinkId, @DrinkName, 1)
            ON CONFLICT(DrinkId) DO UPDATE SET ViewCount = ViewCount + 1
        ";
    }
}
