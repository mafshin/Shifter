using Shifter.Model;
using System.Collections.Generic;

namespace PlanIt.Model
{
    public interface IRuleEngine
    {
        bool CheckRules(IEnumerable<Person> persons, IEnumerable<WorkShift> workShifts, ApplyStage stage);
        IEnumerable<IRule> GetRules();
    }
}