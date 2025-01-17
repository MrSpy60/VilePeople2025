namespace PeopleVilleEngine.Locations;
public class FoodStation : IWorkplace
{
    public FoodStation()
    {
        var random = RNG.GetInstance();
        MaxWorkers = random.Next(1, 3);
    }
    private readonly List<BaseVillager> _villagers = new();
    public string Name => $"Food Station, with {Workers} workers.";
    public int BuildCost => 50;

    public List<BaseVillager> Villagers()
    {
        return _villagers;
    }

    public int Workers => _villagers.Count;
    public int MaxWorkers { get; set; }
}
