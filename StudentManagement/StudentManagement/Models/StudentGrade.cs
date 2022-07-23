using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class StudentGrade
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public int? GradeId { get; set; }
        public double? Value { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
