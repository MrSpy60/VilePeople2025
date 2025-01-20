using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Locations.Project
{
    public class BaseProject
    {
        public interface IProjects
        {
            bool IsComplete { get; }
        }
    }
}