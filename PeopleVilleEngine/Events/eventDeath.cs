using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Locations;

namespace PeopleVilleEngine.Events
{
    public class eventDeath : IEvent
    {
        public EventType Type => EventType.Random;

        public void triggerEvent(Village village)
        {
            RNG rng = RNG.GetInstance();
            int whoDead = rng.Next(0, village.Villagers.Count() - 1);
            BaseVillager deadManWalking = village.Villagers[whoDead];
            Console.WriteLine($"{deadManWalking.ToString()} died");
            ILocation? home = deadManWalking.Home;
            if(home != null)
            {
                home.Villagers().Remove( deadManWalking );
            }
            village.Villagers.Remove(deadManWalking);
        }

        
    }
}
