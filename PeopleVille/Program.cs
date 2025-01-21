using PeopleVilleEngine;

Console.WriteLine("PeopleVille");

var village = new Village();
Console.WriteLine(village.ToString());


while (village.Villagers.Count > 0)
{
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Spacebar)
        {
            Console.WriteLine("Game Paused");
            PrintStatus(village);
            Console.ReadKey(true);
            Console.WriteLine("Game Resumed");
        }
    }


    village.NextDay();


    Thread.Sleep(100);
}

PrintStatus(village);

static void PrintStatus(Village village)
{
    // Print locations with villagers to screen
    foreach (var location in village.Locations)
    {
        var locationStatus = location.Name;
        foreach (var villager in location.Villagers().OrderByDescending(v => v.Age))
        {
            locationStatus += $" {villager}";
        }
        Console.WriteLine(locationStatus);
    }
}
