using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanIt.Model
{
    public class Planner
    {
        public Planner(IRuleEngine ruleEngine, ShiftSettings settings, IEnumerable<Person> persons)
        {
            RuleEngine = ruleEngine;
            Settings = settings;
            Persons = persons;
        }

        public IRuleEngine RuleEngine { get; }
        public ShiftSettings Settings { get; }
        public IEnumerable<Person> Persons { get; }

        public IEnumerable<WorkShift> PlanShifts(DateTime startDate)
        {
            var rand = new Random(DateTime.Now.Millisecond);

            List<WorkShift> shifts = new List<WorkShift>();

            for (int i = 0; i < Settings.ScheduleDaysCount; i++)
            {
                IEnumerable<WorkShift> shiftsToCheck = null;
                WorkShift newShift = null;

                var planRun = 0;
                var date = startDate.AddDays(i);
                int personsCount = Persons.Count();
                for (int s = 0; s < Settings.ShiftsPerDay; s++)
                {
                    do
                    {
                        newShift = new WorkShift()
                        {
                            Date = date,
                            Number = s + 1,
                            Person = Persons.ElementAt(rand.Next(0, personsCount - 1))
                        };

                        shiftsToCheck = shifts.ToList().Union(new[] { newShift });
                        planRun++;
                    }
                    while (!RuleEngine.CheckRules(shiftsToCheck) && planRun <= Settings.MaxPlanRun);

                    if (planRun > Settings.MaxPlanRun)
                    {
                        throw new Exception($"Maximum Plan Run crossed. Max Run: {Settings.MaxPlanRun}, Current Run: {planRun}");
                    }

                    shifts.Add(newShift);
                }
            }

            return shifts;
        }
    }
}
