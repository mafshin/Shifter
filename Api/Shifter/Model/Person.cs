using System.Collections.Generic;

namespace PlanIt.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<WorkShift> WorkShifts { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
