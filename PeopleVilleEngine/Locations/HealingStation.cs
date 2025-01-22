using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.ProjectCompletionEvents;
using System.Xml.Linq;

namespace PeopleVilleEngine.Locations;
public class HealingStation : BaseWorkplace
{
    public HealingStation() : base()
    {
        var random = RNG.GetInstance();
        MaxWorkers = random.Next(1, 3);
    }
    private readonly List<BaseVillager> _villagers = new();
    public string Name => $"Healing Station";
    public int BuildCost => 50;
    public int WorkCost => 50;
    public IEvent WorkEvent_Complete => new eventDeath(); // Add health restoration event here.
    public int WorkId = 1;
        Name = "Healing Station";
        BuildCost = 50;
        WorkCost = 50;
        WorkEvent_Complete = (IEvent)new event_ProjectComplete_HealingStation();

    }

    public int Workers => _villagers.Count();
    public int MaxWorkers { get; set; }

}
