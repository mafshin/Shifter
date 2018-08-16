using System;
using System.Collections.Generic;

namespace PlanIt.Model
{
    public class Person
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<WorkShift> WorkShifts { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
