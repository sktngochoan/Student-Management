using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public int? LectureId { get; set; }
        public int? TeacherPunctuality { get; set; }
        public int? TeacherSkill { get; set; }
        public int? TeacherCoverTopics { get; set; }
        public int? TeacherSupport { get; set; }
        public int? TeacherRespond { get; set; }
        public string Comment { get; set; }
        public DateTime? FeedbackDate { get; set; }

        public virtual Lecturer Lecture { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
