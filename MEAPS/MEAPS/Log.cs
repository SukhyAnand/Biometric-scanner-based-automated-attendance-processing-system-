using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MEAPS
{
    public static class Log
    {
        private static Logger logWin;
        public static void Write(String args)
        {
            if(logWin == null)
            {
                logWin = new Logger();
                logWin.Closed += (a, b) => logWin = null;
                logWin.Show();
            }
            else
            {
                logWin.Show();
            }                        
            String log = "\n" + args + "\n\n";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Logger))
                {
                    (window as Logger).lblLog.Content += log;
                }
            }
        }
    }
}
