using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            Feedbacks = new HashSet<Feedback>();
            News = new HashSet<News>();
            Schedules = new HashSet<Schedule>();
            Subjects = new HashSet<Subject>();
        }

        public int LecturerId { get; set; }
        public string LecturerName { get; set; }
        public string LecturerEmail { get; set; }
        public string LecturerPass { get; set; }
        public string LecturerImg { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
