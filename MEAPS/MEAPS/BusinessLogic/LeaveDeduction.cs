using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAPS;

namespace MEAPS.BusinessLogic
{
    public static class LeaveDeduction
    {
        public static LeaveStatus Deduct(LeaveStatus ls)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            int EmpId = ls.EmployeeEmpId;
            Employee em = context.Employees.SingleOrDefault(x => x.EmpId == EmpId);

            double reqCL = Convert.ToDouble(((ls.QCL_In??0 + ls.QCL_Out??0) * 0.25) + (ls.HCL??0)*0.5 + (ls.FCL??0));
            double reqML = Convert.ToDouble((ls.FML??0) + (ls.HML??0) * 0.5);
            double reqEL = Convert.ToDouble(ls.EL??0);
            double remCL = em.CL;
            double remML = em.ML;
            double remEL = em.EL;
            double LWP = em.LWP;

            #region DedcutionLogic
            if (reqCL > remCL)
            {
                if (reqCL > remML)
                {
                    if (reqCL > remEL)
                    {
                        LWP += reqCL;
                        ls.LWP = reqCL;
                        ls.QCL_In = null;
                        ls.QCL_Out = null;
                        ls.HCL = null;
                        ls.FCL = null;
                    }
                    else
                    {
                        remEL -= reqCL;
                    }
                }
                else
                {
                    remML -= reqCL;
                }
            }
            else
            {
                remCL -= reqCL;
            }

            if (reqML > remML)
            {
                if (reqML > remEL)
                {
                    LWP += reqML;
                    ls.LWP = reqML;
                    ls.HML = null;
                    ls.FML = null;
                }
                else
                {
                    remEL -= reqML;
                }
            }
            else
            {
                remML -= reqML;
            }

            if (reqEL > remEL)
            {
                LWP += reqEL;
                ls.LWP = reqEL;
                ls.EL = null;
            }
            else
            {
                remEL -= reqML;
            }
            #endregion

            em.CL = remCL;
            em.ML = remML;
            em.EL = remML;
            em.LWP = LWP;
            context.SaveChanges();

            return ls;  
        }
    }
}
