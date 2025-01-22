using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events.AfflictionEvents
{
    public class eventDecay : IEvent
    {
        public EventType Type => EventType.Timed;

        public void triggerEvent(Village village)
        {
            foreach (var villager in  village.Villagers)
            {
                villager.Stats.TakeDamage(5);
            }
        }
    }
}
