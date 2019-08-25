using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAPS;

namespace TestCase_LeaveDuration
{
    public static class LeaveDuration
    {
        static LeaveStatus Calculate(Int32 EmpId, TimeSpan EntryTime, TimeSpan ExitTime, DateTime Date, Int32 Type)
        {
            LeaveStatus ls = new LeaveStatus();
            ls.Date = Date;
            ls.EmployeeEmpId = EmpId;

            TimeSpan nineFiftyAM = Convert.ToDateTime("09:50 AM").TimeOfDay;
            TimeSpan tenAM = Convert.ToDateTime("10:00 AM").TimeOfDay;
            TimeSpan tenThirtyAM = Convert.ToDateTime("10:30 AM").TimeOfDay;
            TimeSpan oneThirtyPM = Convert.ToDateTime("01:30 PM").TimeOfDay;
            TimeSpan threeThirtyPM = Convert.ToDateTime("03:30 PM").TimeOfDay;
            TimeSpan fiveTwentyFivePM = Convert.ToDateTime("05:25 PM").TimeOfDay;
            

                       
            return ls;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
