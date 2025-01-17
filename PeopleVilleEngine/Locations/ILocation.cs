namespace PeopleVilleEngine.Locations;
public interface ILocation
{
    string Name { get; }

    int WorkCost { get; }

    List<BaseVillager> Villagers();
}

public interface IHouse : ILocation
{
    public int Population { get; }
    public int MaxPopulation { get; }
}

public interface IWorkplace : ILocation
{
    public int Workers { get; }
    public int MaxWorkers { get; }

}