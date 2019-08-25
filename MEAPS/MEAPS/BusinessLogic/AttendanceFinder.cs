using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{
    public static class AttendanceFinder
    {
        public static void Find(int EmpId, DateTime Day)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Attendance at = context.Attendances.SingleOrDefault(x => (x.Date == Day) && (x.Employee_EmpId == EmpId));
            
            if (at == null)
            {
                DailyAttendanceHandler.NonRecordedAttendance(EmpId, Day);
            }
            else
            {
                TimeSpan EntryTime = at.InTime;
                TimeSpan ExitTime = at.OutTime;
                DailyAttendanceHandler.RecordedAttendance(EmpId, Day, EntryTime, ExitTime);
            }
        }
    }
}
