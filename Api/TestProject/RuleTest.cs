using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanIt.Model;
using System;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class RuleTest
    {
        ShiftSettings settings;
        List<Person> Persons;
        RuleEngine RuleEngine;

        [TestInitialize]
        public void Initialize()
        {
            settings = new ShiftSettings()
            {
                MaxPlanRun = 100000,
                ScheduleDaysCount = 14,
                ShiftsPerDay = 2
            };

            Persons = GetPersons();

            RuleEngine = new RuleEngine(new StaticRuleProvider(), new DebugLogger("Test"));
        }

        [TestMethod]
        public void MultipleShiftsPerPersonOnASingleDayMustFail()
        {
            List<WorkShift> shifts = new List<WorkShift>();
            shifts.Add(new WorkShift() { Date = DateTime.Today, Number = 1, Person = Persons[0] });
            shifts.Add(new WorkShift() { Date = DateTime.Today, Number = 2, Person = Persons[0] });

            Assert.IsFalse(RuleEngine.CheckRules(Persons, shifts, Shifter.Model.ApplyStage.WithinProcess));
        }


        [TestMethod]
        public void ConsecutiveAfternoonShiftsForAPersonMustFail()
        {
            List<WorkShift> shifts = new List<WorkShift>();
            shifts.Add(new WorkShift() { Date = DateTime.Today, Number = 2, Person = Persons[0] });
            shifts.Add(new WorkShift() { Date = DateTime.Today.AddDays(1), Number = 2, Person = Persons[0] });

            Assert.IsFalse(RuleEngine.CheckRules(Persons, shifts, Shifter.Model.ApplyStage.WithinProcess));
        }



        [TestMethod]
        public void ForEachPersonNotHavingWholeDaySupportPerTwoWeekMustFail()
        {
            List<WorkShift> shifts = new List<WorkShift>();
            shifts.Add(new WorkShift() { Date = DateTime.Today, Number = 1, Person = Persons[0] });
            shifts.Add(new WorkShift() { Date = DateTime.Today, Number = 2, Person = Persons[1] });
            shifts.Add(new WorkShift() { Date = DateTime.Today.AddDays(1), Number = 1, Person = Persons[2] });
            shifts.Add(new WorkShift() { Date = DateTime.Today.AddDays(1), Number = 2, Person = Persons[0] });

            Assert.IsFalse(RuleEngine.CheckRules(Persons, shifts, Shifter.Model.ApplyStage.Postprocess));
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
