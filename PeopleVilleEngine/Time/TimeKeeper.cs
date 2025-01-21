using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Events;
using PeopleVilleEngine.Events.EventManagers;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Locations.Project;
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

        private Queue<Project> _projectQueue  = new Queue<Project>();
        private Project _currentProject;


        private TimeKeeper(Village village)
        {
            Console.WriteLine("Creating Time Keeper");
            eventManager = new EManagerCasus(); // #TODO: add random event managers from dll's and pick one (possible users choice)
            _village = village;

            // initialise Projects
            _projectQueue.Enqueue(new Project(new FoodStation()));
            _projectQueue.Enqueue(new Project(new HealingStation()));
            
            // sets the first project
            _currentProject = _projectQueue.Dequeue();
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

        public Project GetCurrentProject()
        {
            return _currentProject;
        }


        public string DateToString()
        {
            return $"Day {_date} of Year {_year}";
        }

        public void PassTime()
        {
            Console.WriteLine($"Starting {DateToString()}");
            // Call event manager
            eventManager.TriggerEventManager(_village, preEvent, postEvent);
            // Pre-events
            foreach (IEvent e in preEvent)
            {
                e.triggerEvent(_village);
            }

            // Statuses
            // Status Locations
            foreach (ILocation location in _village.Locations)
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

            // PROJECT: Work on the current project
            int aliveVillagers = _village.Villagers.Count(v => v != null); // count alive villagers
            double additionalWork = aliveVillagers * 1.0; // each villager contributes 1 unit of work per day

            // add work to the current project
            _currentProject.Work(additionalWork);

            // check if the current project is complete
            if (_currentProject.IsComplete())
            {
                // if project is complete, remove + get the next from queue
                if (_projectQueue.Count > 0)
                {
                    _currentProject = _projectQueue.Dequeue(); // get the next project
                }

            }


            // Post-Events
            foreach (IEvent e in postEvent)
            {
                e.triggerEvent(_village);
            }

            // Clean up
            preEvent.Clear();
            postEvent.Clear();

            _date++;
            if (_date > _daysInAYear)
            {
                _year++;
                _date -= _daysInAYear;
            }
        }
    }
}
