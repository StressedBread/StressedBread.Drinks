using StressedBread.Drinks.Models.API;
using Spectre.Console;
using static StressedBread.Drinks.Enums;
using StressedBread.Drinks.Models.DTOs;
using System.Diagnostics;

namespace StressedBread.Drinks.UI;
internal class MenuUI
{
    internal string DisplayDrinksCategories(List<DrinkCategoryDTO> result)
    {
        AnsiConsole.Clear();

        var choices = result.Select(d => d.Category).ToList();
        choices.Add("Exit");

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]drink category[/]:")
            .PageSize(15)
            .AddChoices(choices));
    }

    internal string DisplayDrinksByCategory(List<FilterDrinksByCategoryDTO> result)
    {
        AnsiConsole.Clear();

        var choices = result.Select(d => d.Name).ToList();
        choices.Add("Back");

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]drink[/]:")
            .PageSize(15)
            .AddChoices(choices));
    }

    internal void DisplayDrinkDetails(DrinkDetailDTO drinkDetail, byte[] drinkImage)
    {
        AnsiConsole.Clear();

        var grid = new Grid();
        grid.AddColumn(new GridColumn { Alignment = Justify.Center });
        grid.AddColumn(new GridColumn { Alignment = Justify.Center });

        var image = new CanvasImage(drinkImage).MaxWidth(20);
        var table = new Table();

        table.AddColumn("Info");
        table.AddColumn("Value");

        table.AddRow("Name", drinkDetail.Name);
        table.AddRow("Category", drinkDetail.Category);
        table.AddRow("Alcoholic", drinkDetail.Alcoholic);
        table.AddRow("Glass", drinkDetail.Glass);
        table.AddRow("Instructions", drinkDetail.Instructions);
        
        for (int i = 1; i <= drinkDetail.Ingredients.Count; i++)
        {
            var ingredient = drinkDetail.Ingredients[i - 1].Name;
            var measure = drinkDetail.Ingredients[i - 1].Measure;
            if (!string.IsNullOrEmpty(ingredient))
            {
                ingredient = ingredient.Trim();
                measure = measure.Trim();
                table.AddRow($"Ingredient {i}", $"{measure} of {ingredient}");
            }
        }

        grid.AddRow("[green]Drink Details[/]", "[green]Drink Image[/]");
        grid.AddRow(
            table,
            image
            );

        AnsiConsole.Write(grid);
        AnsiConsole.MarkupLine("Press any key to return to the main menu...");
        Console.ReadKey(true);
    }

    internal void DisplayError(string errorMessage, ErrorType errorType)
    {
        AnsiConsole.Clear();

        switch (errorType)
        {
            case ErrorType.HttpError:
                AnsiConsole.MarkupLine($"[red]HTTP Error:[/] {errorMessage}");
                break;
            case ErrorType.Timeout:
                AnsiConsole.MarkupLine($"[red]Timeout Error:[/] {errorMessage}");
                break;
            case ErrorType.JsonError:
                AnsiConsole.MarkupLine($"[red]JSON Parsing Error:[/] {errorMessage}");
                break;
            default:
                AnsiConsole.MarkupLine($"[red]An unexpected error occurred:[/] {errorMessage}");
                break;
        }
    }

    internal void DisplayMessage(string message)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(message);
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}