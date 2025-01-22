using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.ProjectCompletionEvents;

namespace PeopleVilleEngine.Locations;
public class FoodStation : BaseWorkplace
{
    public FoodStation() : base()
    {
        Name = "Food Station";
        BuildCost = 50;
        WorkCost = 50;
        WorkEvent_Complete = (IEvent)new event_ProjectComplete_FoodStation();
        WorkId = 2;
    }
}
