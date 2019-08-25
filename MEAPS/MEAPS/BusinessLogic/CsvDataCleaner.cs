using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MEAPS.BusinessLogic
{
    public static class CsvDataCleaner
    {        
        public static void Clean()
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            var query = from x in context.CSVDatas select x;
            List<CSVData> unprocessed = query.ToList();
            List<Attendance> processed = new List<Attendance>();

            foreach (CSVData item in unprocessed)
            {
                TimeSpan inTime = Convert.ToDateTime(item.In1).TimeOfDay;
                TimeSpan outTime = Convert.ToDateTime(getOutTime(item)).TimeOfDay;
                DateTime date = DateTime.ParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int empid = Convert.ToInt32(item.EmpId);
                Attendance record = new Attendance();
                record.Date = date;
                record.InTime = inTime;
                record.OutTime = outTime;
                record.Employee_EmpId = empid;
                processed.Add(record);
            }

            foreach (Attendance item in processed)
            {
                context.Attendances.Add(item);
                context.SaveChanges();
            }
        }

        private static String getOutTime(CSVData item)
        {
            // sequence is In1, Out1, In2, Out2 and so on...
            if (item.Out5 != "")
                return item.Out5;
            else if (item.In5 != "")
                return item.In5;
            else if (item.Out4 != "")
                return item.Out4;
            else if (item.In4 != "")
                return item.In4;
            else if (item.Out3 != "")
                return item.Out3;
            else if (item.In3 != "")
                return item.In3;
            else if (item.Out2 != "")
                return item.Out2;
            else if (item.In2 != "")
                return item.In2;
            else if (item.Out1 != "")
                return item.Out1;
            else return item.In1;
        }
    }
}
