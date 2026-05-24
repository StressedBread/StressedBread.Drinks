namespace StressedBread.Drinks.Models.API;
internal class JsonRootModel<T>
{
    public List<T> Drinks { get; set; } = new();
}
