using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Locations;

namespace PeopleVilleEngine.Events
{
    public class eventMigration : IEvent
    {
        public EventType Type => EventType.None;

        public void triggerEvent(Village village)
        {
            RNG rng = RNG.GetInstance();
            int freeSpots = 0;
            foreach(var location in village.Locations.Where(l => l is IHouse))
            {
                IHouse house = (IHouse)location;
                freeSpots += (house.MaxPopulation - house.Population);
            }
            Console.Write($"Migrants Have arrived");
            if (freeSpots > 0)
            {
                int maxPop = 10;
                int quarterpop = (village.Villagers.Count / 4);
                if (quarterpop - maxPop > 0)
                {
                    maxPop = quarterpop;
                }
                int migrants = rng.Next(5, maxPop);
                
                if (freeSpots >= migrants)
                {
                    Console.WriteLine($", {migrants} joined the village");
                    village.CreateVillagers(migrants);
                    return;
                }
            }
            Console.WriteLine(", but there was no free housing");
        }
    }
}
