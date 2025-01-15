namespace PeopleVilleEngine.Villagers;
public class AdultVillager : BaseVillager
{
    public AdultVillager(Village village) : base(village)
    {
        Age = 30;

    }
    public AdultVillager(Village village, int age) : base(village)
    {
        Age = age;
    }
}
