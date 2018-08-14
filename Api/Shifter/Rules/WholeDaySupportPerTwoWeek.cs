using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class OneWholeDaySupportPerTwoWeek : IRule
    {
        public string Name => "One Whole Day Support Per Two Week";
        public string Description => "Each engineer should have completed one whole day of support in any 2 week period. ";

        public const int MaxShiftPerDay = 2;
        public bool CheckRule(IEnumerable<WorkShift> workShifts)
        {
            return true;

            //return workShifts.GroupBy(s => new { s.Date, s.Person })
            //    .All(x => x.Count() < MaxShiftPerDay);
        }
    }
}
