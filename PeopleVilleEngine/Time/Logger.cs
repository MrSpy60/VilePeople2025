using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Time
{
    internal class Logger
    {
        private static Logger? _Instance;
        private int day;
        Queue<string> happenings = new();

        private void Village_Day(object? sender, int e)
        {

        }

        private void Village_Happening(object? sender, int e)
        {

        }

        public static Logger GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Logger();
            }
            return _Instance;
        }

        public void SetVillage(Village village)
        {

        }
    }
}
