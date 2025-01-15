using PeopleVilleEngine.Locations;
using PeopleVilleEngine;
using PeopleVilleEngine.Villagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Villagers.Creators;

namespace PeopleVilleVillagerHomeless.Creator;
public class HomelessVillageCreator : IVillagerCreator
{
    public bool CreateVillager(Village village)
    {
        if(village.Villagers.Count(v => v.HasHome() == false) * 10 > village.Villagers.Count )
            return false; //No more the 10% can be homeless
        
        var random = RNG.GetInstance();
        if (random.Next(1, 6) != 3)
            return false; //1 of 6 chance to create a homeless
        
        var adult = new AdultVillager(village, random.Next(20, 65));
        //Add to village
        village.Villagers.Add(adult);
        return true;
    }
}
