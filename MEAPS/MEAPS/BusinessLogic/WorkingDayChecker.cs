using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{
    public static class WorkingDayChecker
    {
        public static bool IsWorkingDay(DateTime day)
        {
            bool flag = true;
            MEAPSDbEntities context = new MEAPSDbEntities();
            var query = from x in context.Holidays
                        select x.Date;
            List<DateTime> ListOfHolidays = query.ToList();
            if(ListOfHolidays.Contains(day))
            {
                flag = false;
            }
            else if ((day.DayOfWeek==DayOfWeek.Saturday)||(day.DayOfWeek==DayOfWeek.Sunday))
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }
    }
}
