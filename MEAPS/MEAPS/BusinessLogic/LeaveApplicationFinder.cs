using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{
    public static class LeaveApplicationFinder
    {
        public static bool Find(int EmpId, DateTime date)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            var query = from x in context.LeaveApplications
                        where ((x.EmployeeEmpId == EmpId) && (x.Date == date))
                        select x;
            if (query.ToList().Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }
    }
}
