using StressedBread.Drinks.Models;
using StressedBread.Drinks.Models.API;
using StressedBread.Drinks.Models.DTOs;

namespace StressedBread.Drinks.Helpers;
internal class DrinkDetailMapper
{
    internal static DrinkDetailDTO? MapToDrinkDetailDTO(DrinkDetailModel? drinkDetail)
    {
        if (drinkDetail == null) return null;

        var ingredients = new DrinkDetailMapper().IngredientsMapper(drinkDetail);

        return new DrinkDetailDTO
        {
            Name = drinkDetail.StrDrink ?? string.Empty,
            Category = drinkDetail.StrCategory ?? string.Empty,
            Alcoholic = drinkDetail.StrAlcoholic ?? string.Empty,
            Glass = drinkDetail.StrGlass ?? string.Empty,
            Instructions = drinkDetail.StrInstructions ?? string.Empty,
            DrinkThumb = drinkDetail.StrDrinkThumb ?? string.Empty,
            Ingredients = ingredients
        };
    }

    private List<IngredientModel> IngredientsMapper(DrinkDetailModel drinkDetail)
    {
        var ingredients = new List<IngredientModel>();

        for (int i = 1; i <= 15; i++)
        {
            var ingredient = i switch
            {
                1 => drinkDetail.StrIngredient1,
                2 => drinkDetail.StrIngredient2,
                3 => drinkDetail.StrIngredient3,
                4 => drinkDetail.StrIngredient4,
                5 => drinkDetail.StrIngredient5,
                6 => drinkDetail.StrIngredient6,
                7 => drinkDetail.StrIngredient7,
                8 => drinkDetail.StrIngredient8,
                9 => drinkDetail.StrIngredient9,
                10 => drinkDetail.StrIngredient10,
                11 => drinkDetail.StrIngredient11,
                12 => drinkDetail.StrIngredient12,
                13 => drinkDetail.StrIngredient13,
                14 => drinkDetail.StrIngredient14,
                15 => drinkDetail.StrIngredient15,
                _ => null
            };

            var measure = i switch
            {
                1 => drinkDetail.StrMeasure1,
                2 => drinkDetail.StrMeasure2,
                3 => drinkDetail.StrMeasure3,
                4 => drinkDetail.StrMeasure4,
                5 => drinkDetail.StrMeasure5,
                6 => drinkDetail.StrMeasure6,
                7 => drinkDetail.StrMeasure7,
                8 => drinkDetail.StrMeasure8,
                9 => drinkDetail.StrMeasure9,
                10 => drinkDetail.StrMeasure10,
                11 => drinkDetail.StrMeasure11,
                12 => drinkDetail.StrMeasure12,
                13 => drinkDetail.StrMeasure13,
                14 => drinkDetail.StrMeasure14,
                15 => drinkDetail.StrMeasure15,
                _ => null
            };

            if (!string.IsNullOrEmpty(ingredient) || !string.IsNullOrEmpty(measure))
            {
                ingredients.Add(new IngredientModel
                {
                    Name = ingredient ?? string.Empty,
                    Measure = measure ?? string.Empty
                });
            }
        }

        return ingredients;
    }
}