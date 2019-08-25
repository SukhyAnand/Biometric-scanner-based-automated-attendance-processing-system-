using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{

    public static class DailyAttendanceHandler
    {
        public static void NonRecordedAttendance(int EmpId, DateTime day)
        {
            // 1. Check ExceptionAttendance for employee id.
            // 2. If employee exists, mark employee as present
            // 3. Else check for applications, mark employee as absent based on that.

            MEAPSDbEntities context = new MEAPSDbEntities();
            ExceptionAttendance ea = context.ExceptionAttendances.SingleOrDefault(x => (x.EmpId == EmpId) && (x.Date == day));
            if (ea != null) // if in exception attendance, mark as full present.
            {
                LeaveStatus ls = new LeaveStatus();
                ls.Date = day;
                ls.EmployeeEmpId = EmpId;
                ls.PresenceState = "FP";
                context.LeaveStatuses.Add(ls);
                context.SaveChanges();
            }
            else                           // else check for applications
            {
                LeaveStatus ls = new LeaveStatus();
                ls.Date = day;
                ls.EmployeeEmpId = EmpId;                
                ls.PresenceState = "A";

                LeaveApplication la = context.LeaveApplications.SingleOrDefault(x => (x.Date == day) && (x.EmployeeEmpId == EmpId));
                if (la != null)
                {
                    string type = la.Type;
                    switch (type)
                    {
                        case "CL":      
                            ls.FCL = 1;
                            break;
                        case "ML":
                            ls.FML = 1;
                            break;
                        case "EL":
                            ls.EL = 1;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ls.FCL = 1;
                }
                ls = LeaveDeduction.Deduct(ls);
                context.LeaveStatuses.Add(ls);
                context.SaveChanges();
            }
        }
        public static void RecordedAttendance(int EmpId, DateTime day, TimeSpan EntryTime, TimeSpan ExitTime)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            TimeSpan DefaultEntryTime = Convert.ToDateTime("9:50 AM").TimeOfDay;
            TimeSpan DefaultExitTime = Convert.ToDateTime("5:25 PM").TimeOfDay;
            LeaveStatus ls = new LeaveStatus();
            ls.Date = day;
            ls.EmployeeEmpId = EmpId;
            ls = Calculate(ls, EntryTime, ExitTime, day);
            ls = LeaveDeduction.Deduct(ls);
            context.LeaveStatuses.Add(ls);
            context.SaveChanges();
        }
        public static LeaveStatus Calculate(LeaveStatus ls, TimeSpan EntryTime, TimeSpan ExitTime, DateTime day)
        {
            TimeSpan nineFiftyAM = Convert.ToDateTime("09:50 AM").TimeOfDay;
            TimeSpan tenAM = Convert.ToDateTime("10:00 AM").TimeOfDay;
            TimeSpan tenThirtyAM = Convert.ToDateTime("10:30 AM").TimeOfDay;
            TimeSpan oneThirtyPM = Convert.ToDateTime("01:30 PM").TimeOfDay;
            TimeSpan threeThirtyPM = Convert.ToDateTime("03:30 PM").TimeOfDay;
            TimeSpan fiveTwentyFivePM = Convert.ToDateTime("05:25 PM").TimeOfDay;
            string type = "";
            if (LeaveApplicationFinder.Find(ls.EmployeeEmpId, day))
            {
                type = LeaveTypeFinder(ls.EmployeeEmpId, day);
            }

            MEAPSDbEntities context = new MEAPSDbEntities();
            TimeException te = context.TimeExceptions.SingleOrDefault(x => x.Date == day);
            if ((te != null) && (EntryTime <= te.InTime) && (ExitTime >= te.OutTime))
            {
                ls.PresenceState = "FP";
            }
            else
            {
                if (EntryTime <= nineFiftyAM)
                {
                    if (ExitTime <= oneThirtyPM)
                    {
                        switch (type)
                        {
                            case "CL": ls.FCL = 1; break;
                            case "ML": ls.FML = 1; break;
                            case "EL": ls.EL = 1; break;
                            default: ls.FCL = 1; break;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > oneThirtyPM) && (ExitTime <= threeThirtyPM))
                    {
                        switch (type)
                        {
                            case "CL": ls.HCL = 1; break;
                            case "ML": ls.HML = 1; break;
                            default: ls.HCL = 1; break;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > threeThirtyPM) && (ExitTime <= fiveTwentyFivePM))
                    {
                        ls.QCL_Out = 1;
                        ls.PresenceState = "PP";
                    }
                    else
                    {
                        ls.PresenceState = "FP";
                    }
                }
                else if ((EntryTime > nineFiftyAM) && (EntryTime <= tenAM))
                {
                    if (ExitTime <= oneThirtyPM)
                    {
                        switch (type)
                        {
                            case "CL": ls.FCL = 1; break;
                            case "ML": ls.FML = 1; break;
                            case "EL": ls.EL = 1; break;
                            default: ls.FCL = 1; break;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > oneThirtyPM) && (ExitTime <= threeThirtyPM))
                    {
                        ls.QCL_In = 1;
                        if (type == "ML")
                        {
                            ls.HML = 1;
                        }
                        else
                        {
                            ls.HCL = 1;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > threeThirtyPM) && (ExitTime <= fiveTwentyFivePM))
                    {
                        ls.QCL_In = 1;
                        ls.QCL_Out = 1;
                        ls.PresenceState = "PP";
                    }
                    else
                    {
                        ls.QCL_In = 1;
                        ls.PresenceState = "PP";
                    }
                }
                else if ((EntryTime > tenAM) && (EntryTime <= tenThirtyAM))
                {
                    if (ExitTime <= oneThirtyPM)
                    {
                        switch (type)
                        {
                            case "CL": ls.FCL = 1; break;
                            case "ML": ls.FML = 1; break;
                            case "EL": ls.EL = 1; break;
                            default: ls.FCL = 1; break;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > oneThirtyPM) && (ExitTime <= threeThirtyPM))
                    {
                        ls.QCL_In = 1;
                        if (type == "ML")
                        {
                            ls.HML = 1;
                        }
                        else
                        {
                            ls.HCL = 1;
                        }
                        ls.PresenceState = "PP";
                    }
                    else if ((ExitTime > threeThirtyPM) && (ExitTime <= fiveTwentyFivePM))
                    {
                        ls.QCL_In = 1;
                        ls.QCL_Out = 1;
                        ls.PresenceState = "PP";
                    }
                    else
                    {
                        ls.QCL_In = 1;
                        ls.PresenceState = "PP";
                    }
                }
                else if ((EntryTime > tenThirtyAM) && (EntryTime <= oneThirtyPM))
                {
                    if (ExitTime < fiveTwentyFivePM)
                    {
                        switch (type)
                        {
                            case "CL": ls.FCL = 1; break;
                            case "ML": ls.FML = 1; break;
                            case "EL": ls.EL = 1; break;
                            default: ls.FCL = 1; break;
                        }
                        ls.PresenceState = "PP";
                    }
                    else
                    {
                        if (type == "ML")
                        {
                            ls.HML = 1;
                        }
                        else
                        {
                            ls.HCL = 1;
                        }
                        ls.PresenceState = "PP";
                    }
                }
                else //(EntryTime >= oneThirtyPM)
                {
                    switch (type)
                    {
                        case "CL": ls.FCL = 1; break;
                        case "ML": ls.FML = 1; break;
                        case "EL": ls.EL = 1; break;
                        default: ls.FCL = 1; break;
                    }
                    ls.PresenceState = "PP";
                }

            }            
            return ls;
        }
        public static string LeaveTypeFinder(int EmpId, DateTime day)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            LeaveApplication la = context.LeaveApplications.ToList().SingleOrDefault(x => x.EmployeeEmpId == EmpId && x.Date == day);
            return la.Type;
        }
    }
}