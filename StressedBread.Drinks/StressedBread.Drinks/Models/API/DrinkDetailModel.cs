using Newtonsoft.Json;

namespace StressedBread.Drinks.Models.API;
internal class DrinkDetailModel
{
    internal string? IdDrink { get; set; }
    internal string? StrDrink { get; set; }
    internal string? StrDrinkAlternate { get; set; }
    internal string? StrTags { get; set; }
    internal string? StrVideo { get; set; }
    internal string? StrCategory { get; set; }
    internal string? StrIBA { get; set; }
    internal string? StrAlcoholic { get; set; }
    internal string? StrGlass { get; set; }
    internal string? StrInstructions { get; set; }
    internal string? StrInstructionsES { get; set; }
    internal string? StrInstructionsDE { get; set; }
    internal string? StrInstructionsFR { get; set; }
    internal string? StrInstructionsIT { get; set; }
    [JsonProperty("strInstructionsZH-HANS")]
    internal string? StrInstructionsZHHANS { get; set; }

    [JsonProperty("strInstructionsZH-HANT")]
    internal string? StrInstructionsZHHANT { get; set; }
    internal string? StrDrinkThumb { get; set; }
    internal string? StrIngredient1 { get; set; }
    internal string? StrIngredient2 { get; set; }
    internal string? StrIngredient3 { get; set; }
    internal string? StrIngredient4 { get; set; }
    internal string? StrIngredient5 { get; set; }
    internal string? StrIngredient6 { get; set; }
    internal string? StrIngredient7 { get; set; }
    internal string? StrIngredient8 { get; set; }
    internal string? StrIngredient9 { get; set; }
    internal string? StrIngredient10 { get; set; }
    internal string? StrIngredient11 { get; set; }
    internal string? StrIngredient12 { get; set; }
    internal string? StrIngredient13 { get; set; }
    internal string? StrIngredient14 { get; set; }
    internal string? StrIngredient15 { get; set; }
    internal string? StrMeasure1 { get; set; }
    internal string? StrMeasure2 { get; set; }
    internal string? StrMeasure3 { get; set; }
    internal string? StrMeasure4 { get; set; }
    internal string? StrMeasure5 { get; set; }
    internal string? StrMeasure6 { get; set; }
    internal string? StrMeasure7 { get; set; }
    internal string? StrMeasure8 { get; set; }
    internal string? StrMeasure9 { get; set; }
    internal string? StrMeasure10 { get; set; }
    internal string? StrMeasure11 { get; set; }
    internal string? StrMeasure12 { get; set; }
    internal string? StrMeasure13 { get; set; }
    internal string? StrMeasure14 { get; set; }
    internal string? StrMeasure15 { get; set; }
    internal string? StrImageSource { get; set; }
    internal string? StrImageAttribution { get; set; }
    internal string? StrCreativeCommonsConfirmed { get; set; }
    internal string? DateModified { get; set; }
}