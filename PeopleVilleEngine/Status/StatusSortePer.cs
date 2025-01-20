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
        private BaseVillager Owner;
        public StatusSortePer(Village village)
        {
            int index = _rng.Next(village.Villagers.Count() - 1);
            Owner = village.Villagers[index];
            Owner.statuses.Add(this);
        }
        public void effecttrigger(Village village)
        {
            int ownerIndex = village.Villagers.IndexOf(Owner);
            int index = _rng.Next(village.Villagers.Count()-2);
            if (index >= ownerIndex) index++;
            BaseVillager newOwner = (BaseVillager)village.Villagers[index];
            newOwner.statuses.Add(this);
            Owner.statuses.Remove(this);
            Owner = newOwner;
            Console.WriteLine($"{Owner.ToString()} is the new owner of SortePer");
        }
    }
}
