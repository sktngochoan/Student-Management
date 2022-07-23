using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management.Controllers
{
    public class ViewAttendanceController : Controller
    {
        student_managementContext db = new student_managementContext();
        public IActionResult Index()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            var studentId = student.StudentId;
            var listSubject = (from st in db.Students
                               join su in db.Subjects on st.ClassId equals su.ClassId
                               where st.StudentId == studentId
                               select su).ToList();
            ViewBag.listSubject = listSubject;
            ViewBag.Student = student;

            return View();
        }

        public IActionResult ViewAttendanceDetail(int subjectId)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            var studentId = student.StudentId;

            var listSubject = (from st in db.Students
                               join su in db.Subjects on st.ClassId equals su.ClassId
                               where st.StudentId == studentId
                               select su).ToList();
            ViewBag.listSubject = listSubject;

            //int total = (from s in db.StudentAttendeds
            //             join sc in db.Schedules on s.ScheduleId equals sc.ScheduleId
            //             where sc.SubjectId == subjectId && s.StudentId == studentId
            //             select s).Count();
            int total = 30;

            int absent = (from s in db.StudentAttendeds
                          join sc in db.Schedules on s.ScheduleId equals sc.ScheduleId
                          where sc.SubjectId == subjectId && s.StudentStatus == 1 && s.StudentId == studentId
                          select s).Count();
            int percent = (int)(absent / 0.3);

            ViewBag.Percent = percent;
            ViewBag.Total = total;
            ViewBag.Absent = absent;

            List<Slot> slots = db.Slots.ToList();
            List<Room> rooms = db.Rooms.ToList();
            List<Lecturer> lecturers = db.Lecturers.ToList();
            List<Class> classes = db.Classes.ToList();
            List<Schedule> schedule = db.Schedules.ToList();
            List<StudentAttended> studentAttendeds = (from st in db.StudentAttendeds
                                                      join sc in db.Schedules on st.Schedule.ScheduleId equals sc.ScheduleId
                                                      where sc.SubjectId == subjectId && st.StudentId == studentId
                                                      select st).ToList();
            ViewBag.Student = student;
            return View(studentAttendeds);
        }
    }
}