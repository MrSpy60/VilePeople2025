namespace PeopleVilleEngine.Villagers;
public class SlackerVillager : AdultVillager
{
    public SlackerVillager(Village village) : base(village)
    {
       
    }
    public SlackerVillager(Village village, int age) : base(village)
    {
        Age = age;
    }

    public override double DoWork()
    {
        return 0;
    }
}
