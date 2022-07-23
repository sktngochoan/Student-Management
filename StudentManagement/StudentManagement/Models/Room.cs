using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
