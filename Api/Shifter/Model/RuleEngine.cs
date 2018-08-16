using Microsoft.Extensions.Logging;
using Shifter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class RuleEngine : IRuleEngine
    {
        private readonly IRuleProvider ruleProvider;

        public RuleEngine(IRuleProvider ruleProvider, ILogger logger)
        {
            this.ruleProvider = ruleProvider;
            Logger = logger;
        }

        public ILogger Logger { get; }

        public bool CheckRules(IEnumerable<Person> persons, IEnumerable<WorkShift> workShifts, ApplyStage stage)
        {
            var finalResult = true;
            var rules = ruleProvider.GetRules().Where(x => x.ApplyStages.HasFlag(stage));

            Logger.LogInformation($"Checking {stage} stage rules with stage ...");

            foreach (var rule in rules)
            {
                var result = rule.CheckRule(persons, workShifts);

                Logger.LogInformation($"Checking rule {rule.GetType()}: {result}");

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
