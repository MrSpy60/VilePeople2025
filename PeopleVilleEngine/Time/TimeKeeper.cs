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

        private readonly Queue<Project> _projectQueue = new Queue<Project>();
        private List<Project> _originalProjects = new List<Project>();
        public Dictionary<string, int> _completionCounters = new Dictionary<string, int>();


        private TimeKeeper(Village village)
        {
            Console.WriteLine("Creating Time Keeper");
            eventManager = new EManagerCasus(); // #TODO: add random event managers from dll's and pick one (possible users choice)
            _village = village;

            // initialise Projects
            AddProjectToQueue(new FoodStation());
            AddProjectToQueue(new HealingStation()); ;

            // sets the first project
            _village._currentProject = _projectQueue.Dequeue();
        }

        public static TimeKeeper GetInstance(Village village)
        {
            lock (padlock)
            {
                if (_timeKeeper == null) _timeKeeper = new TimeKeeper(village);
                return _timeKeeper;
            }
        }

        private void AddProjectToQueue(IWorkplace station) // IWorkplace
        {
            var project = new Project(station);
            _projectQueue.Enqueue(project);
            _originalProjects.Add(project);

            string projectName = station.GetType().Name;
            if (!_completionCounters.ContainsKey(projectName))
            {
                _completionCounters[projectName] = 0;
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

            ProcessProject();


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




        // PROJECT
        // PROJECT
        public void ProcessProject()
        {
            if (_village == null || _village._currentProject == null || _projectQueue == null)
            {
                throw new InvalidOperationException("Village or current project or project queue is not initialised.");
            }

            int aliveVillagers = _village.Villagers.Count(v => v != null);
            const double WorkPerVillager = 1.0;
            double additionalWork = aliveVillagers * WorkPerVillager;

            _village._currentProject.Work(additionalWork); // add work to current project

            if (_village._currentProject.IsComplete())
            {
                int workId = _village._currentProject.WorkId;

                // WorkId to track completion
                if (!_completionCounters.ContainsKey(workId.ToString()))
                {
                    _completionCounters[workId.ToString()] = 0;
                }
                _completionCounters[workId.ToString()]++;

                // dq next project or restart the queue
                if (_projectQueue.Count > 0)
                {
                    _village._currentProject = _projectQueue.Dequeue();
                }
                else
                {
                    RestartQueue();
                    _village._currentProject = _projectQueue.Dequeue();
                }
            }

            // irrelevant
            /*
            foreach (var projectCompletion in _completionCounters)
            {
                Console.WriteLine($"Project with WorkId: {projectCompletion.Key} has been completed {projectCompletion.Value} times.");
            }
            */
        }

        public bool RestartQueue()
        {
            foreach (var project in _originalProjects)
            {
                _projectQueue.Enqueue(new Project(project.ProjectType));
            }
            return true;
        }

    }
}