using StressedBread.Drinks.Models.API;
using Spectre.Console;
using static StressedBread.Drinks.Enums;

namespace StressedBread.Drinks.UI;
internal class MenuUI
{
    internal void DisplayDrinksByCategory(JsonRootModel<DrinkCategoryModel> result)
    {
        AnsiConsole.Clear();

        AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]drink category[/]:")
            .PageSize(15)
            .AddChoices(result.Drinks.Select(d => d.StrCategory).ToArray()));
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