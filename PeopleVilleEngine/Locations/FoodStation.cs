using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.ProjectCompletionEvents;

namespace PeopleVilleEngine.Locations;
public class FoodStation : BaseWorkplace
{
    public FoodStation() : base()
    {    
      private readonly List<BaseVillager> _villagers = new();
      public string Name => $"Food Station";
      public int BuildCost => 50;
      public int WorkCost => 50;
      public int WorkId {get;} => 2;

      public List<BaseVillager> Villagers()
      {
          return _villagers;
      }

      public int Workers => _villagers.Count();
      public int MaxWorkers { get; set; }

          WorkEvent_Complete = (IEvent) new event_ProjectComplete_FoodStation();

    }
}
