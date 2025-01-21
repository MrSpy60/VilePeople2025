using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.EventManagers;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Status;

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
        private int _daysInAYear = 112;
        private int _year = 0;
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

        public int getDate()
        {
            return _date;
        }

        public string DateToString()
        {
            return $"Day {_date} of Year {_year}";
        }

        public void PassTime()
        {
            Console.WriteLine($"Starting {DateToString()}");
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
            Queue<BaseVillager> villagers = new Queue<BaseVillager>(_village.Villagers);
            while (villagers.Count() > 0)
            {
                BaseVillager v = villagers.Dequeue();
                Queue<IStatus> statuses = new(v.statuses);
                while (statuses.Count() > 0)
                {
                    IStatus stat = statuses.Dequeue();
                    stat.effecttrigger(_village);
                }
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
            if (_date > _daysInAYear)
            {
                _year++;
                _date -= _daysInAYear;
            }
        }
    }
}
