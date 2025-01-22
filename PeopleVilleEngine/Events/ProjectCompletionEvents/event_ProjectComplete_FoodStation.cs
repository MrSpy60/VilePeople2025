using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Status;

namespace PeopleVilleEngine.Events.ProjectCompletionEvents
{
    public class event_ProjectComplete_FoodStation : IEvent
    {

        public EventType Type => EventType.Active;

        public void triggerEvent(Village village)
        {
            foreach (BaseVillager HungryVillager in village.Villagers)
            {
                HungryVillager.Stats.Heal(20);
            }
            Console.WriteLine($"The village was fed!");

        }
    }
}
