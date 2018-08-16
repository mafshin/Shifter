using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Model;

namespace PlanIt.Model
{
    public class StaticRuleProvider : IRuleProvider
    {
        List<IRule> rules = new List<IRule>();
        public StaticRuleProvider()
        {
            rules.Add(new SingleShiftPerDay());
            rules.Add(new ConsecutiveAfternoonShifts());
            rules.Add(new OneWholeDaySupportPerTwoWeek());

        }
        public IEnumerable<IRule> GetRules()
        {
            return rules;
        }
    }
}
