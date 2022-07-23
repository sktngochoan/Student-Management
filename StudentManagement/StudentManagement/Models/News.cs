using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagement.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public int? LecturersId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        public virtual Lecturer Lecturers { get; set; }
    }
}
