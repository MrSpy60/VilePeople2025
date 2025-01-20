using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleVilleEngine.Locations;
using static PeopleVilleEngine.Locations.Project.BaseProject;

namespace PeopleVilleEngine.Locations.Project
{
    public class Project
    {
        public ILocation ProjectType { get; }
        public double CurrentProgress { get; private set; }

        private List<IProjects> Projects { get; } = new(); // stores different projects

        public Project(ILocation projectType)
        {
            ProjectType = projectType;
            CurrentProgress = 0;
        }

        // add work to the project
        public void Work(double work)
        {
            CurrentProgress += work;
            if (CurrentProgress >= ProjectType.BuildCost)
            {
                CurrentProgress = ProjectType.BuildCost; // ensures it doesn't exceed BuildCost
                RemoveCompletedProject();
            }
        }

        // remove completed project
        public void RemoveCompletedProject()
        {
            var completedProject = Projects.FirstOrDefault(p => p.IsComplete);
            if (completedProject != null)
            {
                Projects.Remove(completedProject);
            }
        }

        public bool IsComplete()
        {
            return CurrentProgress >= ProjectType.BuildCost;
        }
    }

    public class SampleProject : IProjects
    {
        public double BuildCost { get; set; }
        public double CurrentProgress { get; set; }

        // Property to check if the project is complete
        public bool IsComplete => CurrentProgress >= BuildCost;

        public SampleProject(double buildCost)
        {
            BuildCost = buildCost;
            CurrentProgress = 0;
        }

        // add progress to the project
        public void AddProgress(double progress)
        {
            CurrentProgress += progress;
        }
    }
}
