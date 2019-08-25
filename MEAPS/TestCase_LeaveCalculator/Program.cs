using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAPS.BusinessLogic;
using MEAPS;

namespace TestCase_LeaveCalculator
{
    class Program
    {
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
        static void Main(string[] args)
        {
            LeaveStatus ls = new LeaveStatus();
            Console.WriteLine("EmpId:");
            int empid = Int32.Parse(Console.ReadLine());
            ls.EmployeeEmpId = empid;
            Console.WriteLine("Date:");
            DateTime dt = Convert.ToDateTime(Console.ReadLine());
            ls.Date = dt;
            Console.WriteLine("Entry Time:");
            TimeSpan en = Convert.ToDateTime(Console.ReadLine()).TimeOfDay;
            Console.WriteLine("Exit Time:");
            TimeSpan ex = Convert.ToDateTime(Console.ReadLine()).TimeOfDay;

            Console.WriteLine("1: Enter leave application details. \n2. Delete. \n3. Continue...");
            int n = Int32.Parse(Console.ReadLine());
            if (n == 1)
            {
                MEAPSDbEntities context = new MEAPSDbEntities();
                LeaveApplication la = new LeaveApplication();
                Console.WriteLine("Enter type:");
                string type = Console.ReadLine();
                la.Date = dt;
                la.EmployeeEmpId = empid;
                la.Type = type;
                la.Remarks = "test";
                context.LeaveApplications.Add(la);
                context.SaveChanges();
            }
            else if (n == 2)
            {
                MEAPSDbEntities context = new MEAPSDbEntities();
                LeaveApplication la = context.LeaveApplications.SingleOrDefault(x => (x.EmployeeEmpId == empid) && (x.Date == dt));
                context.LeaveApplications.Remove(la);
                context.SaveChanges();
            }

            Console.WriteLine("1: Enter time exception details. \n2. Delete. \n3. Continue...");
            n = Int32.Parse(Console.ReadLine());
            if (n==1)
            {
                MEAPSDbEntities context = new MEAPSDbEntities();
                TimeException te = new TimeException();
                Console.WriteLine("Entry Time:");
                en = Convert.ToDateTime(Console.ReadLine()).TimeOfDay;
                Console.WriteLine("Exit Time:");
                ex = Convert.ToDateTime(Console.ReadLine()).TimeOfDay;
                te.Date = dt; te.InTime = en; te.OutTime = ex;
                context.TimeExceptions.Add(te);
                context.SaveChanges();
            }
            if (n==2)
            {
                MEAPSDbEntities context = new MEAPSDbEntities();
                TimeException te = context.TimeExceptions.SingleOrDefault(x => x.Date == dt);
                context.TimeExceptions.Remove(te);
                context.SaveChanges();
            }

            ls = Calculate(ls, en, ex, dt);

            Console.WriteLine("Date: " + ls.Date.ToShortDateString());
            Console.WriteLine("Employee ID: " + ls.EmployeeEmpId);
            Console.WriteLine("Presence: " + ls.PresenceState);
            Console.WriteLine("QCL In: " + ls.QCL_In);
            Console.WriteLine("QCL Out: " + ls.QCL_Out);
            Console.WriteLine("HCL: " + ls.HCL);
            Console.WriteLine("FCL: " + ls.FCL);
            Console.WriteLine("HML: " + ls.HML);
            Console.WriteLine("FML: " + ls.FML);
            Console.WriteLine("EL: " + ls.EL);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
