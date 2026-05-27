namespace StressedBread.Drinks;
internal class Enums
{
    internal enum ErrorType
    {
        None,
        HttpError,
        Timeout,
        JsonError,
        UnknownError
    }

    internal enum DrinkOption
    {
        AddToFavorites,
        Exit
    }

    internal enum MenuOption
    {
        ViewDrinkCategories,
        ViewFavoriteDrinks,
        ViewDrinkViewCounter,
        Exit
    }
}
