using System.Text.Json;

namespace PeopleVilleEngine;
public class VillagerNames
{
    private string[] _maleFirstNames = new string[] { };
    private string[] _femaleFirstNames = new string[] { };
    private string[] _lastNames = new string[] { };
    RNG _random;
    private static VillagerNames? _instance = null;

    private VillagerNames()
    {
        _random = RNG.GetInstance();
        LoadNamesFromJsonFile();
    }

    public static VillagerNames GetInstance()
    {
        if (_instance == null)
            _instance = new VillagerNames();
        return _instance;
    }

    private void LoadNamesFromJsonFile()
    {
        string jsonFile = "lib\\names.json";
        if (!File.Exists(jsonFile))
            throw new FileNotFoundException(jsonFile);
        
        string jsonData = File.ReadAllText(jsonFile);
        var namesData = JsonSerializer.Deserialize<NamesData>(jsonData);
        _maleFirstNames = namesData.MaleFirstNames;
        _femaleFirstNames = namesData.FemaleFirstNames;
        _lastNames = namesData.LastNames;
    }

    private string GetRandomName(string[] names)
    {
        if (names.Length == 0)
            throw new IndexOutOfRangeException("Names data not properly loaded with names.");

        int index = RNG.GetInstance().Next(names.Length);
        return names[index];
    }

    public string GetRandomFirstName(bool isMale) => GetRandomName(isMale ? _maleFirstNames : _femaleFirstNames);
    public string GetRandomLastName() => GetRandomName(_lastNames);

    public (string firstname, string lastname) GetRandomNames(bool isMale) => (GetRandomFirstName(isMale), GetRandomLastName());

    private class NamesData
    {
        public string[] MaleFirstNames { get; set; }
        public string[] FemaleFirstNames { get; set; }
        public string[] LastNames { get; set; }
    }
}


