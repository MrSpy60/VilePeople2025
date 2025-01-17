using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events.EventManagers
{
    internal class EManagerCasus : IEventManager
    {
        public string Name => "Casus";

        public void TriggerEventManager(Village village)
        {
            throw new NotImplementedException();
        }
    }
}
