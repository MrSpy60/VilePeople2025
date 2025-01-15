using PeopleVilleEngine.Locations;

namespace PeopleVilleEngine.Villagers.Creators;
public class VillagerCreatorChild : IVillagerCreator
{
    public bool CreateVillager(Village village)
    {
        var home = FindHome(village);
        if (home == null) return false;

        var random = RNG.GetInstance();
        var child = new ChildVillager(village);

        var first = home.Villagers().First(v => v.GetType() == typeof(AdultVillager));
        child.LastName = first.LastName;

        home.Villagers().Add(child);
        child.Home = home;
        village.Villagers.Add(child);
        return true;
    }

    private IHouse? FindHome(Village village)
    {
        var random = RNG.GetInstance();

        var potentialHomes = village.Locations.Where(p => p.GetType().IsAssignableTo(typeof(IHouse)))
           .Where(p => p.Villagers().Count(v => v.GetType() == typeof(AdultVillager)) >= 2)
           .Where(p => ((IHouse)p).Population < ((IHouse)p).MaxPopulation).ToList();

        if (potentialHomes.Count == 0)
            return null;

        return (IHouse)potentialHomes[random.Next(0, potentialHomes.Count)];
    }

}
