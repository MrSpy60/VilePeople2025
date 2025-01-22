using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Status;

namespace PeopleVilleEngine.Events.AfflictionEvents
{
    public class eventDisease_CommonCold : IEvent
    {
        public EventType Type => EventType.Random;

        public void triggerEvent(Village village)
        {
            RNG rng = RNG.GetInstance();
            int Affected = rng.Next(0, village.Villagers.Count() - 1);
            BaseVillager AffectedVillager = village.Villagers[Affected];
            Console.WriteLine($"{AffectedVillager.ToString()} caught the cold!");
            AffectedVillager.statuses.Add(new Common_Cold { Owner = AffectedVillager });

        }
    }
}
