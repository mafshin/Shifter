using Microsoft.AspNetCore.Mvc;
using PlanIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Controllers
{
    [Route("api/[controller]")]
    public class ShiftsController : Controller
    {
        public ShiftsController(IRuleEngine ruleEngine)
        {            
            RuleEngine = ruleEngine;
        }

        #region Properties
        public IRuleEngine RuleEngine { get; }
        #endregion


        [HttpGet("rules")]
        public IEnumerable<IRule> GetRules()
        {
            return RuleEngine.GetRules();
        }

        [HttpGet("[action]")]
        public ActionResult Generate()
        {
            ShiftSettings settings = new ShiftSettings()
            {
                MaxPlanRun = 100000,
                ScheduleDaysCount = 14,
                ShiftsPerDay = 2
            };

            List<Person> Persons = GetPersons();

            Planner planner = new Planner(RuleEngine, settings, Persons);

            var shifts = planner.PlanShifts(DateTime.Today);

            //;return shifts;

            return new JsonResult(shifts.GroupBy(x => x.Date).OrderBy(x => x.Key)
            .Select(x => new
            {
                Date = x.Key,
                Shifts = x.Select(d => new { d.Number, Person = d.Person.ToString() })
            }));
        }

        private List<Person> GetPersons()
        {
            List<Person> list = new List<Person>();

            list.AddRange(new[]{
                new Person() { FirstName = "Mohsen", LastName = "Afshin"},
                new Person() { FirstName = "Mat", LastName = "Wilson"},
                new Person() { FirstName = "Wiliam", LastName = "Robinson"},
                new Person() { FirstName = "Zahed", LastName = "Mahdi"},
                new Person() { FirstName = "John", LastName = "Tedd"},
                new Person() { FirstName = "Jack", LastName = "Noman"},
                new Person() { FirstName = "Sara", LastName = "Vanderov"},
                new Person() { FirstName = "Aida", LastName = "Miladi"},
                new Person() { FirstName = "Steve", LastName = "Jobs"},
                new Person() { FirstName = "Jeff", LastName = "Ritcher"}
            });

            return list;
        }
    }
}
