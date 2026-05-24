namespace StressedBread.Drinks.Data.DatabaseQueries;
internal class DatabaseInitQueries
{
    internal string CreateDrinksViewCountTableQuery()
    {
        return @"
        CREATE TABLE IF NOT EXISTS DrinksViewCount (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            DrinkId INTEGER NOT NULL,
            ViewCount INTEGER NOT NULL
        );";
    }

    internal string CreateFavoriteDrinksTableQuery()
    {
        return @"
        CREATE TABLE IF NOT EXISTS FavoriteDrinks (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            DrinkId INTEGER NOT NULL,
            DrinkName TEXT NOT NULL
        );";
    }
}
