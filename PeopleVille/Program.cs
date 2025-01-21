using PeopleVilleEngine;

Console.WriteLine("PeopleVille");

var village = new Village();
Console.WriteLine(village.ToString());

bool isPaused = false;

while (village.Villagers.Count > 0)
{
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.Spacebar)
        {
            isPaused = !isPaused;
            Console.WriteLine(isPaused ? "Game Paused" : "Game Resumed");
        }
    }

    if (!isPaused)
    {
        village.NextDay();
    }

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
