UML Class Diagram

```mermaid

classDiagram
    class Village {
        +Project _currentProject
        +RNG _random
        +TimeKeeper _timeKeeper
        +List~BaseVillager~ Villagers
        +List~ILocation~ Locations
        +List~IVillagerCreator~ villageCreators
        +VillagerNames VillagerNameLibrary
        +Village()
        +void CreateVillage()
        +void CreateVillagers(int number)
        +List~IVillagerCreator~ LoadVillagerCreatorFactories()
        +void LoadVillagerCreatorFactoriesFromType(IEnumerable~Type~ inputTypes, List~IVillagerCreator~ outputVillagerCreators)
        +string ToString()
        +Project GetCurrentProject()
        +void NextDay()
        +int GetDay()
        +void LogProjectUpdate()
        +event EventHandler~int~ EventDayChanged
        +void UpdateDate(int e)
        +event EventHandler~int~ EventHappening
        +void UpdateEvent(int e)
    }

    class VillagerNames {
        -string[] _maleFirstNames
        -string[] _femaleFirstNames
        -string[] _lastNames
        -RNG _random
        -static VillagerNames? _instance
        -VillagerNames()
        +static VillagerNames GetInstance()
        -void LoadNamesFromJsonFile()
        -string GetRandomName(string[] names)
        +string GetRandomFirstName(bool isMale)
        +string GetRandomLastName()
        +(string firstname, string lastname) GetRandomNames(bool isMale)
    }

    class BaseVillager {
        +int Age
        +string FirstName
        +string LastName
        +bool IsMale
        +Village _village
        +ILocation? Home
        +bool HasHome()
        +List~IStatus~ statuses
        +VillagerStats Stats
        +BaseVillager(Village village)
        +string NameToString()
        +string HealthToString()
        +virtual double DoWork()
        +string ToString()
    }

    class ILocation {
        +string Name
        +int BuildCost
        +List~BaseVillager~ Villagers()
    }

    class IWorkplace {
        +int Workers
        +int MaxWorkers
        +int WorkCost
        +int WorkId
    }

    class IVillagerCreator {
        +bool CreateVillager(Village village)
    }

    class Project {
        +IWorkplace ProjectType
        +double CurrentProgress
        +List~IProjects~ Projects
        +Project(IWorkplace workplace)
        +void Work(double work)
        +void RemoveCompletedProject()
        +bool IsComplete()
        +int WorkId
    }

    class RNG {
        -static RNG? _rng
        -Random _random
        -static readonly object padlock
        -RNG()
        +static RNG GetInstance()
        +int Next(int max)
        +int Next(int min, int max)
        +double NextDouble()
        +double NextDouble(double min, double max)
    }

    class TimeKeeper {
        -static TimeKeeper? _timeKeeper
        -IEventManager eventManager
        -static readonly object padlock
        -Village _village
        -List~IEvent~ preEvent
        -List~IEvent~ postEvent
        -int _date
        -int _daysInAYear
        -int _year
        -Queue~Project~ _projectQueue
        -List~Project~ _originalProjects
        -Dictionary~string, int~ _completionCounters
        -TimeKeeper(Village village)
        +static TimeKeeper GetInstance(Village village)
        -void AddProjectToQueue(IWorkplace station)
        +int getDate()
        +string DateToString()
        +void PassTime()
        +void ProcessProject()
        +bool RestartQueue()
    }

    class IStatus {
        +string Name
        +void effecttrigger(Village village)
    }

    class FoodStation {
        +FoodStation()
    }

    class StatusTest {
        +string Name
        +void effecttrigger(Village village)
    }

    Village --> VillagerNames
    Village --> BaseVillager
    Village --> ILocation
    Village --> IVillagerCreator
    Village --> Project
    Village --> RNG
    Village --> TimeKeeper
    BaseVillager --> ILocation
    BaseVillager --> IStatus
    BaseVillager --> VillagerStats
    Project --> IWorkplace
    Project --> IProjects
    TimeKeeper --> IEventManager
    TimeKeeper --> IEvent
    FoodStation --> BaseWorkplace
    FoodStation --> event_ProjectComplete_FoodStation
    StatusTest --> IStatus



```



UML Sequence Diagram

```mermaid

sequenceDiagram
    participant User
    participant Village
    participant RNG
    participant TimeKeeper
    participant IVillagerCreator
    participant Project

    User->>Village: Create instance
    Village->>RNG: GetInstance()
    RNG-->>Village: Return RNG instance
    Village->>TimeKeeper: GetInstance(this)
    TimeKeeper-->>Village: Return TimeKeeper instance
    Village->>Village: CreateVillage()
    Village->>Village: CreateVillagers(villagers)
    Village->>Village: LoadVillagerCreatorFactories()
    Village->>IVillagerCreator: CreateVillager(this)
    IVillagerCreator-->>Village: Return created villager
    Village->>Village: LogProjectUpdate()
    Village->>Project: GetCurrentProject()
    Project-->>Village: Return current project
    Village->>Village: NextDay()
    Village->>TimeKeeper: PassTime()
    TimeKeeper-->>Village: Time passed
    Village->>Village: LogProjectUpdate()




```
