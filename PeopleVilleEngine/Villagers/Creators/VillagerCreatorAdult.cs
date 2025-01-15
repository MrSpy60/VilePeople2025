using PeopleVilleEngine.Locations;
namespace PeopleVilleEngine.Villagers.Creators;
public class VillagerCreatorAdult : IVillagerCreator
{
    public bool CreateVillager(Village village)
    {
        var random = RNG.GetInstance();
        var adult = new AdultVillager(village, random.Next(18, 40));
        //Find house
        var home = FindHome(village);

        if (home.Villagers().Count(v => v.GetType() == typeof(AdultVillager)) >= 1)
        {
            var first = home.Villagers().First(v => v.GetType() == typeof(AdultVillager));
            adult.LastName = first.LastName;
            adult.IsMale = !first.IsMale;
            adult.FirstName = village.VillagerNameLibrary.GetRandomFirstName(adult.IsMale);
        }

        home.Villagers().Add(adult);
        adult.Home = home;

        //Add to village
        village.Villagers.Add(adult);
        return true;
    }

    private IHouse FindHome(Village village)
    {
        var random = RNG.GetInstance();

        var potentialHomes = village.Locations.Where(p => p.GetType().IsAssignableTo(typeof(IHouse)))
            .Where(p => p.Villagers().Count(v => v.GetType() == typeof(AdultVillager)) < 2)
            .Where(p => ((IHouse)p).Population < ((IHouse)p).MaxPopulation).ToList();

        if (potentialHomes.Count > 0 && random.Next(1, 5) != 1) //Return current house
            return (IHouse)potentialHomes[random.Next(0, potentialHomes.Count)];

        //create a new house
        IHouse house = new SimpleHouse();
        village.Locations.Add(house);
        return house;

    }
}