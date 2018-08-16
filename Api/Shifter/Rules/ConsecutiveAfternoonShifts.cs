using Shifter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class ConsecutiveAfternoonShifts : IRule
    {
        public ApplyStage ApplyStages
        {
            get { return ApplyStage.WithinProcess; }
        }
        public string Name => "Consecutive Afternoon Shifts";
        public string Description => "An engineer cannot have two afternoon shifts on consecutive days";

        public bool CheckRule(IEnumerable<Person> persons, IEnumerable<WorkShift> workShifts)
        {
            var shifts = workShifts.ToArray();
            return shifts.Where(x => x.Number == 2).GroupBy(s => new { s.Person })
                .All(x =>
                {
                    var sum = 0;
                    x.Select(d => (int)d.Date.Subtract(DateTime.MinValue).TotalDays)
                    .OrderBy(d => d)
                    .Aggregate(0, (d1, d2) =>
                    {
                        sum += d2 - d1 == 1 ? 1 : 0;
                        return d2;
                    }, d => d);

                    return sum == 0;
                });
        }
    }
}
