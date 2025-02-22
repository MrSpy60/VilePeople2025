﻿using PeopleVilleEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Locations
{
    public abstract class BaseWorkplace : IWorkplace
    {
        private readonly List<BaseVillager> _workers = new();
        public string Name;
        public int BuildCost;
        public int WorkCost;
        public int WorkId;
        public double CurrentProgress = 0;
        public IEvent WorkEvent_Complete;
        public int Workers => _workers.Count();
        public int MaxWorkers { get; set; }

        string ILocation.Name => this.Name;

        int ILocation.BuildCost => this.BuildCost;

        int IWorkplace.WorkCost => this.WorkCost;

        int IWorkplace.WorkId => this.WorkId;

        public BaseWorkplace()
        {
            var random = RNG.GetInstance();
            MaxWorkers = random.Next(1, 3);
        }

        public List<BaseVillager> Villagers()
        {
            return _workers;
        }

        public bool addWorker(BaseVillager worker)
        {
            if (Villagers().Count() < MaxWorkers)
            {
                Villagers().Add(worker);
                return true;
            }
            return false;
        }

        public bool doWork()
        {
            foreach (var worker in Villagers())
            {
                CurrentProgress += worker.Stats.Efficiency;
            }

            if (CurrentProgress >= WorkCost)
            {
                CurrentProgress -= WorkCost;
                return true;
            }
            return false;
        }


    }
}
