namespace PeopleVilleEngine;
sealed public class RNG
{
    private static RNG? _rng = null;
    private Random _random;
    private static readonly object padlock = new object();
    private RNG()
    {
        _random = new Random();
    }

    public static RNG GetInstance()
    {
        lock (padlock) //simple threadsafe (Better use Lazy...)
        {
            if (_rng == null) _rng = new RNG();
            return _rng;
        }
    }

    public int Next(int max) => _random.Next(max);
    public int Next(int min, int max) => _random.Next(min, max);
    
    public double NextDouble() => _random.NextDouble();
    public double NextDouble(double min, double max) => min + (_random.NextDouble() * (max - min));
}
