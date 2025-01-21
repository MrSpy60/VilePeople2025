using PeopleVilleEngine.Events;

namespace PeopleVilleEngine.Locations;
public class HealingStation : IWorkplace
{
    public HealingStation()
    {
        var random = RNG.GetInstance();
        MaxWorkers = random.Next(1, 3);
    }
    private readonly List<BaseVillager> _villagers = new();
    public string Name => $"Healing Station";
    public int BuildCost => 50;
    public int WorkCost => 50;
    public IEvent WorkEvent_Complete => new eventDeath(); // Add health restoration event here.

    public List<BaseVillager> Villagers()
    {
        return _villagers;
    }

    public int Workers => _villagers.Count();
    public int MaxWorkers { get; set; }
}
