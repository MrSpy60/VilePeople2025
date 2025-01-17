namespace PeopleVilleEngine.Villagers.VillagerStats
{
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
}
