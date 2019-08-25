using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAPS;
using MEAPS.BusinessLogic;

namespace TestCase_WorkingDayChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Console.WriteLine("Tuples to insert:");
            int n = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Holiday h = new Holiday();
                Console.Write("Date:");
                string dt = Console.ReadLine();
                h.Date = Convert.ToDateTime(dt);
                Console.Write("Comment:");
                h.Comment = Console.ReadLine();
                context.Holidays.Add(h);
                context.SaveChanges();
            }

            Console.Write("Date to check: ");
            string dt1 = Console.ReadLine();
            DateTime dt2 = Convert.ToDateTime(dt1);
            bool flag = WorkingDayChecker.IsWorkingDay(dt2);
            Console.WriteLine(flag.ToString());
            Console.ReadKey();
        }
    }
}
