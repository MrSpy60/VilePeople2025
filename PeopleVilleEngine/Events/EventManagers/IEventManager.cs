using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events.EventManagers
{
    public interface IEventManager
    {
        string Name { get; }
        void TriggerEventManager(Village village);
    }
}
