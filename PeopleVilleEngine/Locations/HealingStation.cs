using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.ProjectCompletionEvents;
using System.Xml.Linq;

namespace PeopleVilleEngine.Locations;
public class HealingStation : BaseWorkplace
{
    public HealingStation() : base()
    {
        Name = "Healing Station";
        BuildCost = 50;
        WorkCost = 50;
        WorkEvent_Complete = (IEvent)new event_ProjectComplete_HealingStation();

    }

}
