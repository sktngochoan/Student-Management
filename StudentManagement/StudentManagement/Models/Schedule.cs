using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            StudentAttendeds = new HashSet<StudentAttended>();
        }

        public int ScheduleId { get; set; }
        public int? RoomId { get; set; }
        public int? SlotId { get; set; }
        public int? LecturerId { get; set; }
        public int? SubjectId { get; set; }
        public int? ClassId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public bool? Status { get; set; }
        public string Day { get; set; }
        public int? WeekId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Week Week { get; set; }
        public virtual ICollection<StudentAttended> StudentAttendeds { get; set; }
    }
}
