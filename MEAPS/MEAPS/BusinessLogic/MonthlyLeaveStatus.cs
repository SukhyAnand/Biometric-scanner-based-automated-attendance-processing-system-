using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAPS.BusinessLogic
{
    public class MonthlyLeaveStatus
    {
        public int EmployeeId { get; set; }
        public string Month { get; set; }
        public double CL { get; set; }
        public double ML { get; set; }
        public double EL { get; set; }
        public double LWP { get; set; }

    }
}
