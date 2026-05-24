namespace StressedBread.Drinks.Models.API;
internal class JsonRootModel<T>
{
    internal List<T> Drinks { get; set; } = new();
}
