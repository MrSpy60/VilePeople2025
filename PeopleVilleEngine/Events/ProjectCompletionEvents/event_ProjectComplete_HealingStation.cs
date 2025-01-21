using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Status;
using PeopleVilleEngine.Villagers.VillagerStats;

namespace PeopleVilleEngine.Events.ProjectCompletionEvents
{
    public class event_ProjectComplete_HealingStation
    {
        public EventType Type => EventType.Active;

        public void triggerEvent(Village village)
        {
            foreach (BaseVillager UnhealthyVillager in village.Villagers)
            {
                UnhealthyVillager.Stats.Heal(100);
            }
            Console.WriteLine($"The village was healed!"); 

        }
    }
}
