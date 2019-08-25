using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase_Scratch
{
    class myClass
    {
        public int MyProperty { get; set; }
    }
    class Program
    {        
        static void Main(string[] args)
        {
            myClass mc = new myClass();
            double x = mc.MyProperty * 0.25;
            Console.Write(x);
            Console.Read();
        }
    }
}
