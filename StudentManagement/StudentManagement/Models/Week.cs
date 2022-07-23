using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Week
    {
        public Week()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int WeekId { get; set; }
        public string WeekDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
