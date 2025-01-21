using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events.EventManagers
{
    internal class BaseEventManager : IEventManager
    {
        public string Name => throw new NotImplementedException();

        public void TriggerEventManager(Village village, List<IEvent> preEvents, List<IEvent> postEvents)
        {
            throw new NotImplementedException();
        }
    }
}
