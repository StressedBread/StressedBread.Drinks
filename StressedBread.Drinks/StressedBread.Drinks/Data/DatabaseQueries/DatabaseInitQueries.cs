namespace StressedBread.Drinks.Data.DatabaseQueries;
internal class DatabaseInitQueries
{
    internal string CreateDrinksViewCountTableQuery()
    {
        return @"
        CREATE TABLE IF NOT EXISTS DrinksViewCount (
            DrinkId INTEGER NOT NULL PRIMARY KEY,
            DrinkName TEXT NOT NULL,
            ViewCount INTEGER NOT NULL
        );";
    }

    internal string CreateFavoriteDrinksTableQuery()
    {
        return @"
        CREATE TABLE IF NOT EXISTS FavoriteDrinks (
            DrinkId INTEGER NOT NULL PRIMARY KEY,
            DrinkName TEXT NOT NULL
        );";
    }
}
