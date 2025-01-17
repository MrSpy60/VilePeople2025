namespace PeopleVilleEngine.Villagers.VillagerStats
{
    public class VillagerWorkEfficiency
    {
        public double Efficiency { get; private set; }

        public VillagerWorkEfficiency(double efficiency)
        {
            Efficiency = efficiency;
        }

        public void AdjustEfficiency(double adjustment)
        {
            Efficiency = Math.Max(0, Efficiency + adjustment);
        }
    }
}
