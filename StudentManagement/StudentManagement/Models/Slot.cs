using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int SlotId { get; set; }
        public string SlotTime { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
