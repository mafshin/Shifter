using Shifter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class OneWholeDaySupportPerTwoWeek : IRule
    {
        public ApplyStage ApplyStages
        {
            get { return ApplyStage.Postprocess; }
        }
        public string Name => "One Whole Day Support Per Two Week";
        public string Description => "Each engineer should have completed one whole day of support in any 2 week period. ";

        public const int MinShiftPerTwoWeek = 2;
        public bool CheckRule(IEnumerable<Person> persons, IEnumerable<WorkShift> workShifts)
        {
            return
                persons.Select(x => x.Id).Except(
                workShifts.GroupBy(s => s.Person)
                .Where(x => x.Count() >= MinShiftPerTwoWeek)
                .Select(x => x.Key.Id)
                ).Count() == 0;
        }
    }
}
