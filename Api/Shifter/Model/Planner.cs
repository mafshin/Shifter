using Shifter.Model;
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
            var planRun = 0;

            var rand = new Random(DateTime.Now.Millisecond);

            List<WorkShift> shifts = null;

            do
            {
                shifts = new List<WorkShift>();

                for (int i = 0; i < Settings.ScheduleDaysCount; i++)
                {
                    IEnumerable<WorkShift> shiftsToCheck = null;
                    WorkShift newShift = null;                    
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
                                Person = SelectCandidate(shifts, rand)
                            };

                            shiftsToCheck = shifts.ToList().Union(new[] { newShift });
                            planRun++;
                        }
                        while (!RuleEngine.CheckRules(Persons, shiftsToCheck, ApplyStage.WithinProcess) && planRun <= Settings.MaxPlanRun);

                        if (planRun > Settings.MaxPlanRun)
                        {
                            throw new Exception($"Maximum Plan Run crossed. Max Run: {Settings.MaxPlanRun}, Current Run: {planRun}");
                        }

                        shifts.Add(newShift);
                    }
                }
            }
            while (!RuleEngine.CheckRules(Persons, shifts, ApplyStage.Postprocess));

            return shifts;
        }

        private Person SelectCandidate(IEnumerable<WorkShift> shifts, Random rand)
        {
            //Old method: All Persons with equal weight
            //return Persons.ElementAt(rand.Next(0, personsCount - 1));

            var personShifts = 
                shifts.GroupBy(x => x.Person.Id)
                .Select(x => new { PID = x.Key, Count = x.Count() });

            var query = from p in Persons
                        join ps in personShifts on p.Id equals ps.PID into gj
                        from g in gj.DefaultIfEmpty()
                        select new { p.Id, ShiftCount = (g?.Count ?? 0) + 1 }; // plus 1 is required for weights of persons with no shifts

            var maxShfitPerPerson = query.Max(d => d.ShiftCount);

            // Persons with less shifts have heigher weights
            var q2 = query.Select(d => new { d.Id, Weight = maxShfitPerPerson - d.ShiftCount + 1 });

            var candidates = q2.SelectMany(d => Enumerable.Repeat(d.Id, d.Weight)).OrderBy(d => Guid.NewGuid());

            var selected = candidates.ElementAt(rand.Next(0, candidates.Count() - 1));

            return Persons.First(d => d.Id == selected);
        }
    }
}
