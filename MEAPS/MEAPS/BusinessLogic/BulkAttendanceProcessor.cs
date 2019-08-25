using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{
    public static class BulkAttendanceProcessor
    {
        public static void calculate(DateTime StartDate, DateTime EndDate)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            foreach (Employee emp in context.Employees)
            {
                if ((emp.JoiningDate >= StartDate) && (emp.JoiningDate <= EndDate))
                {
                    StartDate = emp.JoiningDate;
                }
                foreach (DateTime day in ListOfDays.EachDay(StartDate,EndDate))
                {
                    if (WorkingDayChecker.IsWorkingDay(day))
                    {
                        AttendanceFinder.Find(emp.EmpId, day);
                    }
                }             
            }          

        }
    }
}
