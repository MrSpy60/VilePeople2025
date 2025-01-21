using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Status
{
    internal class StatusSortePer : IStatus
    {
        RNG _rng = RNG.GetInstance();
        public int traded = 0;
        private BaseVillager Owner;
        public StatusSortePer(Village village)
        {
            int index = _rng.Next(village.Villagers.Count() - 1);
            Owner = village.Villagers[index];
            Owner.statuses.Add(this);
        }
        public string Name => "SortePer";

        public void effecttrigger(Village village)
        {
            if (traded != village.GetDay()) // only trade SortePer once a day
            {
                int ownerIndex = village.Villagers.IndexOf(Owner);
                int index = _rng.Next(village.Villagers.Count()-2);
                if (index >= ownerIndex) index++;
                BaseVillager newOwner = (BaseVillager)village.Villagers[index];
                newOwner.statuses.Add(this);
                Owner.statuses.Remove(this);
                Owner = newOwner;
                Console.WriteLine($"{Owner.ToString()} is the new owner of {Name}");
                traded = village.GetDay();
            }
        }
    }
}
