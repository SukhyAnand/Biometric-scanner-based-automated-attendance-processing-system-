//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MEAPS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int SISOId { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan InTime { get; set; }
        public System.TimeSpan OutTime { get; set; }
        public int Employee_EmpId { get; set; }
    }
}