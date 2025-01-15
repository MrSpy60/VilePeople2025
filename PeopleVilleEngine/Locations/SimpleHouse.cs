namespace PeopleVilleEngine.Locations;
public class SimpleHouse : IHouse
{
    public SimpleHouse()
    {
        var random = RNG.GetInstance();
        MaxPopulation = random.Next(1, 5);
    }
    private readonly List<BaseVillager> _villagers = new();
    public string Name => $"Simple House, with a population of {Population}.";

    public List<BaseVillager> Villagers()
    {
        return _villagers;
    }

    public int Population => _villagers.Count;
    public int MaxPopulation { get; set; }
}