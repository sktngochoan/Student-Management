using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Class
    {
        public Class()
        {
            Schedules = new HashSet<Schedule>();
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }

        public int ClassId { get; set; }
        public int? SemesterId { get; set; }
        public string ClassName { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
