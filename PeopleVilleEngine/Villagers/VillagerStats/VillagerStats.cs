namespace PeopleVilleEngine.Villagers.VillagerStats
{
    public class VillagerStats
    {
        private int MaxHealth = 100;
        public int CurrentHealth { get; private set; }

        public double Efficiency = 1;

        public VillagerStats(RNG rng) // temp rng for testing
        {
            CurrentHealth = rng.Next(80, MaxHealth); // current HP is random
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
}
