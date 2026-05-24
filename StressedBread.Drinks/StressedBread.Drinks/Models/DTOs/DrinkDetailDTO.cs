namespace StressedBread.Drinks.Models.DTOs;
internal class DrinkDetailDTO
{
    internal string Name { get; set; } = string.Empty;
    internal string Category { get; set; } = string.Empty;
    internal string Alcoholic { get; set; } = string.Empty;
    internal string Glass { get; set; } = string.Empty;
    internal string Instructions { get; set; } = string.Empty;
    internal string DrinkThumb { get; set; } = string.Empty;
    internal List<IngredientModel> Ingredients { get; set; } = new();
}
