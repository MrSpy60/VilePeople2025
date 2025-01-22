using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Time
{
    public class Logger
    {
        private static Logger? _Instance;
        private int day;
        Queue<string> happenings = new();

        public void Village_Day(object? sender, int e)
        {
            if (sender == null)
            {
                return;
            }

            if (day != e && happenings.Count() >0)
            {
                Console.WriteLine($"Day {e}");
                while (happenings.Count() > 0)
                {
                    Console.WriteLine(happenings.Dequeue());
                }
        }
            day = e;
        }

        public void Village_Happening(object? sender, string e)
        {
            if (sender != null)
            {
                happenings.Enqueue(e);
            }
        }

        public static Logger GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Logger();
            }
            return _Instance;
        }
    }
}
