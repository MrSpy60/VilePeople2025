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
            if (sender != null)
            {
                return;
            }
            Console.WriteLine(day);
            Console.WriteLine(e);
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

        private void Village_Happening(object? sender, string e)
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

        public void SetUpEventHandler(EventHandler<int> day,EventHandler<string> happening )
        {
            day += Village_Day;
            happening += Village_Happening;
        }
    }
}
