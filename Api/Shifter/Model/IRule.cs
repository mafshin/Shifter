using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public interface IRule
    {
        bool CheckRule(IEnumerable<WorkShift> workShifts);

        string Name { get; }

        string Description { get;  }
    }
}
