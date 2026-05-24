using StressedBread.Drinks.Models.API;
using Spectre.Console;
using static StressedBread.Drinks.Enums;
using StressedBread.Drinks.Models.DTOs;

namespace StressedBread.Drinks.UI;
internal class MenuUI
{
    internal string DisplayDrinksCategories(List<DrinkCategoryDTO> result)
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]drink category[/]:")
            .PageSize(15)
            .AddChoices(result.Select(d => d.Category).ToArray()));
    }

    internal string DisplayDrinksByCategory(List<FilterDrinksByCategoryDTO> result, List<byte[]> drinkImages)
    {
        AnsiConsole.Clear();

        foreach (var imageData in drinkImages)
        { 
            var image = new CanvasImage(imageData)
                .MaxWidth(20);
            AnsiConsole.Write(image);
        }

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]drink[/]:")
            .PageSize(15)
            .AddChoices(result.Select(d => d.Name).ToArray()));
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
}