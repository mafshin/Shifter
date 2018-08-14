using System;

namespace PlanIt.Model
{
    public class WorkShift
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get;set; }
        public Person Person { get; set; }
    }
}
