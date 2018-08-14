using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class SingleShiftPerDay : IRule
    {
        public string Name => "Single Shift Per Day";
        public string Description => "An engineer can do at most one-half day shift in a day";

        public const int MaxShiftPerDay = 2;
        public bool CheckRule(IEnumerable<WorkShift> workShifts)
        {
            return workShifts.GroupBy(s => new { s.Date, s.Person })
                .All(x => x.Count() < MaxShiftPerDay);
        }
    }
}
