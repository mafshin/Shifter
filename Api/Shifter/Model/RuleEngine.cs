using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class RuleEngine : IRuleEngine
    {
        private readonly IRuleProvider ruleProvider;

        public RuleEngine(IRuleProvider ruleProvider)
        {
            this.ruleProvider = ruleProvider;
        }        
        public bool CheckRules(IEnumerable<WorkShift> workShifts)
        {
            var finalResult = true;

            foreach (var rule in ruleProvider.GetRules())
            {
                var result = rule.CheckRule(workShifts);

                Logger.Log($"Checking rule {rule.GetType()}: {result}");

                finalResult &= result;

                if (!finalResult)
                {
                    break;
                }
            }

            return finalResult;
        }

        public IEnumerable<IRule> GetRules()
        {
            return this.ruleProvider.GetRules();
        }
    }
}
