using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAPS.BusinessLogic;

namespace TestCase_DaysList
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start, end;
            string _start, _end;
            _start = Console.ReadLine();
            _end = Console.ReadLine();
            start = Convert.ToDateTime(_start);
            end = Convert.ToDateTime(_end);
            //test call
            IEnumerable<DateTime> dt = ListOfDays.EachDay(start,end);

            //print
            foreach (var item in dt.ToList())
            {
                Console.WriteLine(item.Date.ToShortDateString() + " " + item.DayOfWeek);
            }
            Console.ReadKey();
        }
    }
}
