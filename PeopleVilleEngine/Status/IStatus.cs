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
    }
    public interface INegative : IStatus
    {
        BaseVillager Owner { get; set; }
        double ValueDebuff { get; }
    }

    public class Tool25 : ITool
    {
        public string Name => "Hammer";
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => 25.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }

    }

    public class Tool50 : ITool
    {
        public string Name => "Axe";
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => 50.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }

    }

    public class Tool75 : ITool
    {
        public string Name => "Golden Spoon";
        public required BaseVillager Owner { get; set; }
        public double ValueBuff => 75.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }
        
    }
    public class Exhaustion : INegative
    {
        public string Name => "Exhaustion";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -35.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }
    }
    public class Common_Cold : INegative
    {
        public string Name => "Common Cold";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -25.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }
    }
    public class Extreme_Sadness : INegative
    {
        public string Name => "The Big Sad";
        public required BaseVillager Owner { get; set; }
        public double ValueDebuff => -45.0;

        public void effecttrigger(Village village)
        {
            throw new NotImplementedException();
        }
    }
}

