namespace PeopleVilleEngine.Villagers;
public class ChildVillager : BaseVillager
{
    public ChildVillager(Village village) : base(village)
    {
        Age = RNG.GetInstance().Next(0, 18);
    }

    public ChildVillager(Village village, int age) : this(village)
    {
        Age = age;
    }
    public ChildVillager(Village village, int age, bool isMale) :this(village, age)
    {
        IsMale=isMale;
    }
}
