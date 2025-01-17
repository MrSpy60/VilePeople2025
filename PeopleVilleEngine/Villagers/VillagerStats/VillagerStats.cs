namespace PeopleVilleEngine.Villagers.VillagerStats
{
    public class VillagerStats
    {
        public VillagerHealth Health { get; private set; }
        public VillagerWorkEfficiency WorkEfficiency { get; private set; }

        public VillagerStats(RNG rng)
        {
            int maxHealth = 100; // Max hp is 100
            int currentHealth = rng.Next(1, maxHealth + 1); // current HP is random
            double efficiency = rng.NextDouble(); // Random efficiency

            Health = new VillagerHealth(maxHealth, currentHealth);
            WorkEfficiency = new VillagerWorkEfficiency(efficiency);
        }
    }
}
