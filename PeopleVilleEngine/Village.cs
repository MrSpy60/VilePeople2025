namespace PeopleVilleEngine;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleEngine.Locations;
using System.Reflection;
using System.Linq;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Time;
using PeopleVilleEngine.Status;
using PeopleVilleEngine.Locations.Project;

public class Village
{
    public Project _currentProject;
    private readonly RNG _random = RNG.GetInstance();
    private TimeKeeper _timeKeeper;
    public List<BaseVillager> Villagers { get; } = new();
    public List<ILocation> Locations { get; } = new();
    private List<IVillagerCreator> villageCreators;
    public VillagerNames VillagerNameLibrary { get; } = VillagerNames.GetInstance();
    public Village()
    {
        _timeKeeper = TimeKeeper.GetInstance(this);
        Console.WriteLine("Creating villager");
        CreateVillage();
        new StatusSortePer(this);
    }


    private void CreateVillage()
    {
        var villagers = _random.Next(10, 24);
        CreateVillagers(villagers);
    }

    public void CreateVillagers(int number)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (villageCreators == null)
        {
            villageCreators = LoadVillagerCreatorFactories();
        }
        Console.ResetColor();

        int villageCreatorindex = 0;

        for (int i = 0; i < number; i++)
        {
            var created = false;
            do
            {
                created = villageCreators[villageCreatorindex].CreateVillager(this);
                villageCreatorindex = villageCreatorindex + 1 < villageCreators.Count ? villageCreatorindex + 1 : 0;
            } while (!created);
        }

        Console.ResetColor();
    }
    private List<IVillagerCreator> LoadVillagerCreatorFactories()
    {
        var villageCreators = new List<IVillagerCreator>();
        //Load from this Assembly
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes());
        LoadVillagerCreatorFactoriesFromType(
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()),
            villageCreators);

        //Load from library Files
        var libraryFiles = Directory.EnumerateFiles("lib").Where(f => Path.GetExtension(f) == ".dll");
        foreach (var libraryFile in libraryFiles)
        {
            LoadVillagerCreatorFactoriesFromType(
                Assembly.LoadFrom(libraryFile).ExportedTypes,
                villageCreators);
        }
        return villageCreators;
    }

    private void LoadVillagerCreatorFactoriesFromType(IEnumerable<Type> inputTypes, List<IVillagerCreator> outputVillagerCreators)
    {
        var createVillagerInterface = typeof(IVillagerCreator);
        var createrTypes = inputTypes.Where(p => createVillagerInterface.IsAssignableFrom(p) && !p.IsInterface).ToList();
        foreach (var type in createrTypes)
        {
            Console.WriteLine($"Village Creeater loaded: {type}");
            outputVillagerCreators.Add((IVillagerCreator)Activator.CreateInstance(type));
        }
    }

    public override string ToString()
    {
        return $"Village have {Villagers.Count} villagers, where {Villagers.Count(v => v.HasHome() == false)} are homeless.";
    }

    public Project GetCurrentProject()
    {
        return _currentProject;
    }

    public void NextDay()
    {
        _timeKeeper.PassTime();
        LogProjectUpdate();
    }

    public int GetDay()
    {
        return _timeKeeper.getDate();
    }

    private void LogProjectUpdate()
    {
        var currentProject = GetCurrentProject();
        if (currentProject != null)
        {
            Console.WriteLine($"Working on {currentProject.ProjectType.Name}");
            Console.WriteLine($"Progress: {currentProject.CurrentProgress}/{currentProject.ProjectType.BuildCost}");
        }
    }
}
