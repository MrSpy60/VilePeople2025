using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.EventManagers;

namespace PeopleVilleEngine.Time
{
    sealed public class TimeKeeper
    {
        private static TimeKeeper? _timeKeeper = null;
        IEventManager eventManager;
        private static readonly object padlock = new object();
        private Village _village;
        //
        public List<IEvent> preEvent = new List<IEvent>();
        public List<IEvent> postEvent = new List<IEvent>();
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
            //Call event manager
            eventManager.TriggerEventManager(_village, preEvent, postEvent);
            //Pre-events
            foreach (IEvent e in preEvent)
            {
                e.triggerEvent(_village);
            }

            //Statuses
                //Status Locations
                //status Villagers
            // WORK WORK
                
            //Post-Events
            foreach (IEvent e in postEvent)
            {
                e.triggerEvent(_village);
            }

            //clean up
            preEvent = [];
            postEvent = [];
        }
    }
}
