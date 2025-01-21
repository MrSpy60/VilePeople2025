using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Status;

namespace PeopleVilleEngine.Events.AfflictionEvents
{
    public class eventExhaust : IEvent
    {
        public EventType Type => EventType.Random;

        public void triggerEvent(Village village)
        {
            RNG rng = RNG.GetInstance();
            int Affected = rng.Next(0, village.Villagers.Count() - 1);
            BaseVillager AffectedVillager = village.Villagers[Affected];
            Console.WriteLine($"{AffectedVillager.ToString()} became exhausted!");
            AffectedVillager.statuses.Add(new Exhaustion { Owner = AffectedVillager });

        }
    }
}
