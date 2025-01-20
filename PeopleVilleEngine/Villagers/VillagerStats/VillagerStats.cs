namespace PeopleVilleEngine.Villagers.VillagerStats
{
    public class VillagerStats
    {
        public VillagerHealth Health { get; private set; }
        public VillagerWorkEfficiency WorkEfficiency { get; private set; }

        public VillagerStats(RNG rng) // temp rng for testing
        {
            int maxHealth = 100; // Max hp set to 100
            int currentHealth = rng.Next(1, maxHealth + 1); // current HP is random
            double efficiency = rng.NextDouble(); // Random efficiency

            Health = new VillagerHealth(maxHealth, currentHealth);
            WorkEfficiency = new VillagerWorkEfficiency(efficiency);
        }
    }

    public class VillagerHealth
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public VillagerHealth(int maxHealth, int currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - damage);
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }
    }

    public class VillagerWorkEfficiency
    {
        public double Efficiency { get; private set; }

        public VillagerWorkEfficiency(double efficiency)
        {
            Efficiency = efficiency;
        }

        public void AdjustEfficiency(double adjustment) // For changing efficiency
        {
            Efficiency += adjustment;
        }
    }
}
