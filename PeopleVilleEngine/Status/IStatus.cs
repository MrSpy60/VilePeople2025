using PeopleVilleEngine.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Status
{
    public interface IStatus
    {
        public string Name { get; }
        public void effecttrigger(Village village);

    }

    public interface ITool : IStatus
    {
        BaseVillager Owner { get; set; }
        double ValueBuff { get; }
        int ToolID { get;} // Used to find a random Tool when generating new villagers in VillagerCreatorAdult.cs
    }
    public interface INegative : IStatus
    {
        BaseVillager Owner { get; set; }
        double ValueDebuff { get; }
    }

    public class Tool25 : ITool
    {
        public string Name => "Hammer";
        public int ToolID { get; } = 1;
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => .25;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueBuff;
        }

    }

    public class Tool50 : ITool
    {
        public string Name => "Axe";
        public int ToolID { get; } = 2;
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => .5;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueBuff;
        }

    }

    public class Tool75 : ITool
    {
        public string Name => "Golden Spoon";
        public int ToolID { get; } = 3;
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => .75;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueBuff;
        }
        
    }
    public class Exhaustion : INegative
    {
        public string Name => "Exhaustion";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -.35;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueDebuff;
        }
    }
    public class Common_Cold : INegative
    {
        public string Name => "Common Cold";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -.25;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueDebuff;
        }
    }
    public class Extreme_Sadness : INegative
    {
        public string Name => "The Big Sad";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -.45;

        public void effecttrigger(Village village)
        {
            Owner.Stats.Efficiency += ValueDebuff;
        }
    }
}

