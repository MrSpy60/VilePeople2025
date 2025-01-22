using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Status;
using PeopleVilleEngine.Villagers.VillagerStats;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;
    public List<IStatus> statuses { get; set; }
    public VillagerStats Stats { get; set; }

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        Stats = new VillagerStats(RNG.GetInstance());
        statuses = [];
    }

    public string NameToString()
    {
        return $"{FirstName} {LastName} ({Age} years)";
    }

    public string HealthToString()
    {
        return $"Health: {Stats.Health.CurrentHealth}/{Stats.Health.MaxHealth}";
    }
}