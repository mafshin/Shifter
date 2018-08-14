using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class ShiftSettings
    {
        public int ScheduleDaysCount { get; set; } = 10;
        public int ShiftsPerDay { get; set; } = 2;
        public int MaxPlanRun { get; set; } = 5000;
    }
}
