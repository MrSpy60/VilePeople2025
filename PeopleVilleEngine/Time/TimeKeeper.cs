using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.EventManagers;
using PeopleVilleEngine.Locations;

namespace PeopleVilleEngine.Time
{
    sealed public class TimeKeeper
    {
        private static TimeKeeper? _timeKeeper = null;
        IEventManager eventManager;
        private static readonly object padlock = new object();
        private Village _village;
        public List<IEvent> preEvent = new List<IEvent>();
        public List<IEvent> postEvent = new List<IEvent>();
        private int _date = 1;
        private TimeKeeper(Village village)
        {
            Console.WriteLine("Creating Time Keeper");
            eventManager = new EManagerCasus(); // #TODO: add random eventmanagers from dll's and pick one (possible users choice)
            _village = village;
        }

        public static TimeKeeper GetInstance(Village village)
        {
            lock (padlock)
            {
                if (_timeKeeper == null) _timeKeeper = new TimeKeeper(village);
                return _timeKeeper;
            }
        }

        public void PassTime()
        {
            Console.WriteLine($"Starting Day {_date}");
            //Call event manager
            eventManager.TriggerEventManager(_village, preEvent, postEvent);
            //Pre-events
            foreach (IEvent e in preEvent)
            {
                e.triggerEvent(_village);
            }

            //Statuses
                //Status Locations
            foreach(ILocation location in _village.Locations)
            {
                // #TODO: loop status.trigger()
            }
                //status Villagers
            foreach (BaseVillager villager in _village.Villagers)
            {
                // #TODO: loop villager.trigger()
            }
            // WORK WORK

            //Post-Events
            foreach (IEvent e in postEvent)
            {
                e.triggerEvent(_village);
            }

            //clean up
            preEvent = [];
            postEvent = [];

            _date++;
        }
    }
}
