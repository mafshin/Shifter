using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Model;

namespace PlanIt.Model
{
    public interface IRuleProvider
    {
        IEnumerable<IRule> GetRules();
    }
}
