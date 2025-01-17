using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events
{
    public interface IEvent
    {
        void triggerEvent(Village village);
        
        EventType Type { get; }
    }

    public enum EventType
    {
        None = 0,
        Passive = 1,
        Active = 2,
        Random = 3
    }
}
