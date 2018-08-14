using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public interface IRuleProvider
    {
        IEnumerable<IRule> GetRules();
    }
}
