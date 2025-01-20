namespace PeopleVilleEngine;
using PeopleVilleEngine.Villagers.Creators;
using PeopleVilleEngine.Locations;
using System.Reflection;
using System.Linq;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Locations.Project;

public class Village
{
    private readonly RNG _random = RNG.GetInstance();
    public List<BaseVillager> Villagers { get; } = new();
    public List<ILocation> Locations { get; } = new();
    public VillagerNames VillagerNameLibrary { get; } = VillagerNames.GetInstance();

    public Village()
    {
        Console.WriteLine("Creating villager");
        CreateVillage();
        TestBuildingHealingStation(); // Call the test method here
    }

    private void CreateVillage()
    {
        var villagers = _random.Next(10, 24);
        Console.ForegroundColor = ConsoleColor.Red;

        var villageCreators = LoadVillagerCreatorFactories();
        Console.ResetColor();
        Console.WriteLine();

        int villageCreatorindex = 0;

        for (int i = 0; i < villagers; i++)
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
        // Load from this Assembly
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes());
        LoadVillagerCreatorFactoriesFromType(
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()),
            villageCreators);

        // Load from library Files
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
            Console.WriteLine($"Village Creator loaded: {type}");
            outputVillagerCreators.Add((IVillagerCreator)Activator.CreateInstance(type));
        }
    }

    private void TestBuildingHealingStation()
    {
        // HealingStation instance
        HealingStation healingStation = new HealingStation();
        Console.WriteLine($"Created: {healingStation.Name}");

        // HealingStation
        Project project = new Project(healingStation);

        // Simulate working on project
        double workAmount = 21; // Amount of work to simulate
        while (project.CurrentProgress < healingStation.BuildCost)
        {
            project.Work(workAmount);
            Console.WriteLine($"Current Progress: {project.CurrentProgress}/{healingStation.BuildCost}");
        }

        Console.WriteLine("Project completed!");
    }

    public override string ToString()
    {
        return $"Village has {Villagers.Count} villagers, where {Villagers.Count(v => v.HasHome() == false)} are homeless.";
    }
}
