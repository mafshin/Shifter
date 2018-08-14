using System.Collections.Generic;

namespace PlanIt.Model
{
    public interface IRuleEngine
    {
        bool CheckRules(IEnumerable<WorkShift> workShifts);
        IEnumerable<IRule> GetRules();
    }
}