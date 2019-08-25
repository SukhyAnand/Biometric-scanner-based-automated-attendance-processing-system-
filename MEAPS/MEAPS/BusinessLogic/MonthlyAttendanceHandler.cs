using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{   
    public static class MonthlyAttendanceHandler
    {
        public static List<MonthlyLeaveStatus> Calculate(int Date)
        {
            List<MonthlyLeaveStatus> ls_mon = new List<MonthlyLeaveStatus>();
            MEAPSDbEntities context = new MEAPSDbEntities();
            foreach (var emp in context.Employees)
            {
                double sumCL = 0, sumML = 0, sumEL = 0, sumLWP = 0;
                List<LeaveStatus> ls_emp = (from x in context.LeaveStatuses where x.EmployeeEmpId == emp.EmpId select x).ToList();
                for (int i = 1; i <= 12; i++)
                {
                    List<LeaveStatus> ls_emp_mon = (from x in ls_emp where x.Date.Month == i && x.Date.Year == Date select x).ToList();
                    sumCL = ls_emp_mon.Sum(x => ((x.FCL ?? 0) + (0.5 * (x.HCL ?? 0)) + (0.25 * ((x.QCL_In ?? 0) + (x.QCL_Out ?? 0)))));
                    sumML = ls_emp_mon.Sum(x => ((x.FML ?? 0) + (0.5 * (x.HML ?? 0))));
                    sumEL = ls_emp_mon.Sum(x => (x.EL ?? 0));
                    sumLWP = ls_emp_mon.Sum(x => x.LWP);
                    MonthlyLeaveStatus mls = new MonthlyLeaveStatus();
                    mls.EmployeeId = emp.EmpId;
                    mls.Month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                    mls.CL = sumCL;
                    mls.ML = sumML;
                    mls.EL = sumEL;
                    ls_mon.Add(mls);
                }

            }
            return ls_mon;

        }
    }
}
