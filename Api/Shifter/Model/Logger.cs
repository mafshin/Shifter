using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class Logger
    {
        public static void Log(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
