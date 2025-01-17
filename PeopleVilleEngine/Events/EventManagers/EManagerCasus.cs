using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events.EventManagers
{
    internal class EManagerCasus : IEventManager
    {
        private readonly RNG _rng = RNG.GetInstance();
        public string Name => "Casus";


        public void TriggerEventManager(Village village, List<IEvent> preEvents, List<IEvent> postEvents)
        {
            if (_rng.Next(100) > 80)
            {
                preEvents.Add(new eventDeath());
            };
        }
    }
}
