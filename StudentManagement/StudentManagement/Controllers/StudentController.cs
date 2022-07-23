using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Controllers
{
//    dotnet ef dbcontext scaffold "server =(local); database = MySaleDB;uid=sa;pwd=matma123;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models

//"ConnectionStrings": {
//    "MySaleDB": "server=localhost;database=MySaleDB;uid=sa;pwd=123456"
//  }
public class StudentController : Controller
    {
        student_managementContext context = new student_managementContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            var session = HttpContext.Session;

            int countStudent = 0;
            var liststudent = context.Students.ToList();
            var student = new Student();
            foreach (var s in liststudent)
            {
                if (s.StudentEmail.ToString().Equals(email) && s.StudentPass.ToString().Equals(pass))
                {
                    countStudent++;
                    student = s;
                }
            }

            int countLecturer = 0;
            var listlecture = context.Lecturers.ToList();
            var lecturer = new Lecturer();
            foreach (var l in listlecture)
            {
                if (l.LecturerEmail.ToString().Equals(email) && l.LecturerPass.ToString().Equals(pass))
                {
                    countLecturer++;
                    lecturer = l;
                }
            }

            if (countStudent != 0)
            {
                string jsonaccount = JsonConvert.SerializeObject(student);
                session.SetString("account", jsonaccount);
                session.SetString("role", "1");
                return RedirectToAction("Student");
            }

            if (countLecturer != 0)
            {
                string jsonaccount = JsonConvert.SerializeObject(lecturer);
                session.SetString("account", jsonaccount);
                session.SetString("role", "2");
                return RedirectToAction("Lecturer");
            }
            else
            {
                ViewBag.Message = "Login Fail";
                return RedirectToAction("Login");
            }

        }

        public IActionResult Student()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student;
            //var test = Convert.ToDateTime(student.StudentBofd).ToString("yyyyy-MM-dd");
            //ViewBag.Test = test;
            return View();
        }

        public IActionResult Lecturer()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult StudentProfile()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student;
            return View();
        }

        public IActionResult StudentUpdateProfile()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student;
            var dateofbirth = Convert.ToDateTime(student.StudentBofd).ToString("yyyy-MM-dd");
            ViewBag.Dateofbirth = dateofbirth;
            return View();
        }

        [HttpPost]
        public object StudentUpdateProfile(string image, string id, string name, string dob,
            string gender, string email, string address)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            if (name == null || name.Equals(""))
            {
                ViewBag.nameMess = "Name can not empty!";
            }
            if (email == null || email.Equals(""))
            {
                ViewBag.emailMess = "Email can not empty!";
            }
            if (address == null || address.Equals(""))
            {
                ViewBag.addressMess = "Address can not empty!";
            }
            if (name != null && email != null && address != null)
            {
                int idS = Int32.Parse(id);
                Student s = context.Students.Find(idS);
                if (image == null || image.Equals(""))
                {
                    image = context.Students.Find(idS).StudentImg;
                }
                s.StudentImg = image;
                s.StudentName = name;
                s.StudentBofd = Convert.ToDateTime(dob);
                s.StudentEmail = email;
                s.StudentAddress = address;
                s.StudentGender = bool.Parse(gender);
                context.Students.Update(s);
                context.SaveChanges();
                string setSession = JsonConvert.SerializeObject(context.Students.Find(idS));
                session.SetString("account", setSession);
                jsonaccount = session.GetString("account");
                student = new Student();
                if (jsonaccount != null)
                {
                    student = JsonConvert.DeserializeObject<Student>(jsonaccount);
                }
            }
            var dateofbirth = Convert.ToDateTime(student.StudentBofd).ToString("yyyy-MM-dd");
            ViewBag.Dateofbirth = dateofbirth;
            ViewBag.Student = student;
            return View();

        }
        public IActionResult LecturerProfile()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            return View();
        }

        public IActionResult LecturerUpdateProfile()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            return View();
        }

        [HttpPost]
        public object LecturerUpdateProfile(string image, string id, string name, string email)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            if (name == null || name.Equals(""))
            {
                ViewBag.nameMess = "Name can not empty!";
            }
            if (email == null || email.Equals(""))
            {
                ViewBag.emailMess = "Email can not empty!";
            }
            if (name != null && email != null)
            {
                int idS = Int32.Parse(id);
                Lecturer l = context.Lecturers.Find(idS);
                if (image == null || image.Equals(""))
                {
                    image = context.Lecturers.Find(idS).LecturerImg;
                }
                l.LecturerName = name;
                l.LecturerEmail = email;
                l.LecturerImg = image;
                context.Lecturers.Update(l);
                context.SaveChanges();
                string setSession = JsonConvert.SerializeObject(context.Lecturers.Find(idS));
                session.SetString("account", setSession);
                jsonaccount = session.GetString("account");
                if (jsonaccount != null)
                {
                    lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
                }
            }
            ViewBag.Lecturer = lecturer;
            return View();
        }


        public IActionResult TimeTable()
        {
            var session = HttpContext.Session;
            ViewBag.role = session.GetString("role");
            // getDate
            List<Week> listWeek1 = context.Weeks.ToList();
            int weekid = 0;
            foreach (var item in listWeek1)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)item.EndDate) < 0)
                {
                    weekid = item.WeekId;
                    break;
                }
            }
            List<Schedule> ls = context.Schedules.
                Where(p => p.WeekId == weekid).ToList();
            Dictionary<int, List<Schedule>> map = new Dictionary<int, List<Schedule>>();
            for (int i = 1; i <= 8; i++)
            {
                List<Schedule> schedules = new List<Schedule>();
                foreach (var s in ls)
                {
                    if (s.SlotId == i)
                    {
                        schedules.Add(s);
                    }
                }
                map.Add(i, schedules);
            }

            Dictionary<int, Dictionary<string, Schedule>> map2 = new Dictionary<int, Dictionary<string, Schedule>>();
            foreach (var m in map)
            {
                int key = m.Key;
                List<Schedule> schedules = m.Value;
                Dictionary<string, Schedule> map3 = new Dictionary<string, Schedule>();
                foreach (var item in schedules)
                {
                    map3.Add(Convert.ToDateTime(item.ScheduleDate).ToString("dd/MM/yyyy"), item);
                }
                map2.Add(key, map3);
            }

            var date_raw = (from Schedule in context.Schedules
                            where Schedule.WeekId == weekid
                            select Schedule.ScheduleDate)
                            .Distinct().ToList();
            List<String> listDate = new List<String>();
            for (int i = 0; i < date_raw.Count; i++)
            {
                listDate.Add(Convert.ToDateTime(date_raw[i]).ToString("dd/MM/yyyy"));
            }

            ViewBag.TimeTable = map2;
            List<Slot> listSlot = context.Slots.ToList();

            var listDay = (from Schedule in context.Schedules
                           select Schedule.ScheduleDate)
                            .Distinct().ToList();
            List<string> listWeek = new List<string>();
            List<string> Weeks = new List<string>();
            int count = 1;
            for (int i = 0; i < listDay.Count; i++)
            {
                if (count == 1 || count % 7 == 0)
                {
                    String[] listElement = Convert.ToDateTime(listDay[i]).ToString("dd/MM/yyyy").Split("/");
                    listWeek.Add(listElement[0] + "/" + listElement[1]);
                    if (count == 7)
                    {
                        count = 1;
                    }
                    else
                    {
                        count++;
                    }
                }
                else
                {
                    count++;
                }
            }
            // giang update session
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            Student student = new Student();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            ViewBag.Student = student;
            // giang update session
            for (int i = 0; i < listWeek.Count; i++)
            {
                if (i == 0 || i % 2 == 0)
                    Weeks.Add(listWeek[i] + "-" + listWeek[i + 1]);
            }

            List<StudentAttended> listAttend = (from StudentAttended in context.StudentAttendeds
                                                where StudentAttended.StudentId == student.StudentId
                                                select StudentAttended
                                                ).ToList();
            ViewBag.studentAttend = listAttend;
            List<int> attendance = new List<int>();
            Dictionary<int, int> attended = new Dictionary<int, int>();
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < listAttend.Count; j++)
                {
                    if (ls[i].ScheduleId == listAttend[j].ScheduleId)
                    {
                        if (listAttend[j].StudentStatus == 2)
                        {
                            attended.Add(ls[i].ScheduleId, 2);
                            i++;
                        }
                        else
                        {
                            attended.Add(ls[i].ScheduleId, 1);
                            i++;
                        }
                    }
                }
            }
            for (int i = 0; i < ls.Count; i++)
            {
                if (!attended.ContainsKey(ls[i].ScheduleId))
                {
                    attended.Add(ls[i].ScheduleId, 0);
                }
            }
            ViewBag.Count = ls.Count();
            ViewBag.Subject = context.Subjects.ToList();
            ViewBag.Room = context.Rooms.ToList();
            ViewBag.slot = listSlot;
            ViewBag.week = context.Weeks.ToList();
            ViewBag.attend = attended;
            ViewBag.Date = listDate;
            ViewBag.selectedWeek = weekid;
            ViewBag.IsAttend = 1;

            

            return View();
        }
        [HttpPost]
        public IActionResult TimeTable(int weekid)
        {
            var session = HttpContext.Session;
            ViewBag.role = session.GetString("role");
            List<Week> listWeek1 = context.Weeks.ToList();
            int weekid_check = 0;
            foreach (var item in listWeek1)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)item.EndDate) < 0)
                {
                    weekid_check = item.WeekId;
                    break;
                }
            }
            if (weekid == weekid_check)
            {
                ViewBag.IsAttend = 1;
            }
            else
            {
                ViewBag.IsAttend = 2;
            }
            List<Schedule> ls = context.Schedules.
                Where(p => p.WeekId == weekid).ToList();
            Dictionary<int, List<Schedule>> map = new Dictionary<int, List<Schedule>>();
            for (int i = 1; i <= 8; i++)
            {
                List<Schedule> schedules = new List<Schedule>();
                foreach (var s in ls)
                {
                    if (s.SlotId == i)
                    {
                        schedules.Add(s);
                    }
                }
                map.Add(i, schedules);
            }
            Dictionary<int, Dictionary<string, Schedule>> map2 = new Dictionary<int, Dictionary<string, Schedule>>();
            foreach (var m in map)
            {
                int key = m.Key;
                List<Schedule> schedules = m.Value;
                Dictionary<string, Schedule> map3 = new Dictionary<string, Schedule>();
                foreach (var item in schedules)
                {
                    map3.Add(Convert.ToDateTime(item.ScheduleDate).ToString("dd/MM/yyyy"), item);
                }
                map2.Add(key, map3);
            }

            var date_raw = (from Schedule in context.Schedules
                            where Schedule.WeekId == weekid
                            select Schedule.ScheduleDate)
                            .Distinct().ToList();
            List<String> listDate = new List<String>();
            for (int i = 0; i < date_raw.Count; i++)
            {
                listDate.Add(Convert.ToDateTime(date_raw[i]).ToString("dd/MM/yyyy"));
            }

            ViewBag.TimeTable = map2;
            List<Slot> listSlot = context.Slots.ToList();

            var listDay = (from Schedule in context.Schedules
                           select Schedule.ScheduleDate)
                            .Distinct().ToList();
            List<string> listWeek = new List<string>();
            List<string> Weeks = new List<string>();
            int count = 1;
            for (int i = 0; i < listDay.Count; i++)
            {
                if (count == 1 || count % 7 == 0)
                {
                    String[] listElement = Convert.ToDateTime(listDay[i]).ToString("dd/MM/yyyy").Split("/");
                    listWeek.Add(listElement[0] + "/" + listElement[1]);
                    if (count == 7)
                    {
                        count = 1;
                    }
                    else
                    {
                        count++;
                    }
                }
                else
                {
                    count++;
                }
            }
            // giang update session
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            Student student = new Student();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            ViewBag.Student = student;
            // giang update session
            for (int i = 0; i < listWeek.Count; i++)
            {
                if (i == 0 || i % 2 == 0)
                    Weeks.Add(listWeek[i] + "-" + listWeek[i + 1]);
            }
            List<StudentAttended> listAttend = (from StudentAttended in context.StudentAttendeds
                                                where StudentAttended.StudentId == student.StudentId
                                                select StudentAttended
                                                ).ToList();
            ViewBag.studentAttend = listAttend;
            List<int> attendance = new List<int>();
            Dictionary<int, int> attended = new Dictionary<int, int>();
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < listAttend.Count; j++)
                {
                    if (ls[i].ScheduleId == listAttend[j].ScheduleId)
                    {
                        if (listAttend[j].StudentStatus == 2)
                        {
                            attended.Add(ls[i].ScheduleId, 2);
                            i++;
                        }
                        else
                        {
                            attended.Add(ls[i].ScheduleId, 1);
                            i++;
                        }
                    }
                }
            }
            for (int i = 0; i < ls.Count; i++)
            {
                if (!attended.ContainsKey(ls[i].ScheduleId))
                {
                    attended.Add(ls[i].ScheduleId, 0);
                }
            }
            ViewBag.Count = ls.Count();
            ViewBag.Subject = context.Subjects.ToList();
            ViewBag.Room = context.Rooms.ToList();
            ViewBag.slot = listSlot;
            ViewBag.week = context.Weeks.ToList();
            ViewBag.attend = attended;
            ViewBag.Date = listDate;
            ViewBag.selectedWeek = weekid;

            return View();
        }

        public IActionResult ActivityDetail(int scheduleId)
        {
            var schedule = (from Schedule in context.Schedules
                            where Schedule.ScheduleId == scheduleId
                            select Schedule);

            ViewBag.schedule = schedule;
            ViewBag.Class = context.Classes.ToList();
            ViewBag.Lecture = context.Lecturers.ToList();
            ViewBag.Subject = context.Subjects.ToList();

            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session
            return View();
        }

        public IActionResult ClassDetail(int classId)
        {
            var listStudent = (from Student in context.Students
                               where Student.ClassId == classId
                               select Student).ToList();
            ViewBag.student = listStudent;

            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session

            return View();
        }

        public IActionResult Attendance(int classId, int scheduleId)
        {
            var ListStudent = (from Student in context.Students
                               where Student.ClassId == classId
                               select Student);
            ViewBag.student = ListStudent;
            ViewBag.scheduleId = scheduleId;
            ViewBag.classId = classId;

            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session

            return View();
        }
        //public IActionResult EditAttendance(int classId, int scheduleId)
        //{
        //    var ListStudent = (from Student in context.Students
        //                       where Student.ClassId == classId
        //                       select Student);
        //    List<StudentAttended> listSA = (from StudentAttended in context.StudentAttendeds
        //                                    where StudentAttended.ScheduleId == scheduleId
        //                                    select StudentAttended).ToList();
        //    ViewBag.student = ListStudent;
        //    ViewBag.listSA = listSA;
        //    ViewBag.scheduleId = scheduleId;
        //    ViewBag.classId = classId;

        //    // giang update session
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Lecturer lecturer = new Lecturer();
        //    if (jsonaccount != null)
        //    {
        //        lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
        //    }
        //    ViewBag.Lecturer = lecturer;
        //    // giang update session

        //    return View();
        //}

        //public IActionResult CheckAttendance(List<int> attendance, int scheduleId, int classId)
        //{
        //    List<Student> listStudent = (from Student in context.Students
        //                                 where Student.ClassId == classId
        //                                 select Student).ToList();
        //    if (listStudent.Count() == attendance.Count())
        //    {
        //        for (int i = 0; i < listStudent.Count(); i++)
        //        {
        //            StudentAttended studentAttended1 = new StudentAttended();
        //            studentAttended1.StudentId = listStudent[i].StudentId;
        //            studentAttended1.ScheduleId = scheduleId;
        //            studentAttended1.StudentStatus = 2;
        //            studentAttended1.StudentAttendedDate = DateTime.Now;
        //            context.StudentAttendeds.Add(studentAttended1);
        //            context.SaveChanges();
        //            i++;

        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < listStudent.Count(); i++)
        //        {
        //            for (int j = 0; j < attendance.Count(); j++)
        //            {
        //                if (listStudent[i].StudentId == attendance[j])
        //                {
        //                    StudentAttended studentAttended1 = new StudentAttended();
        //                    studentAttended1.StudentId = listStudent[i].StudentId;
        //                    studentAttended1.ScheduleId = scheduleId;
        //                    studentAttended1.StudentStatus = 2;
        //                    studentAttended1.StudentAttendedDate = DateTime.Now;
        //                    context.StudentAttendeds.Add(studentAttended1);
        //                    context.SaveChanges();
        //                    i++;
        //                }

        //            }
        //            StudentAttended studentAttended = new StudentAttended();
        //            studentAttended.StudentId = listStudent[i].StudentId;
        //            studentAttended.ScheduleId = scheduleId;
        //            studentAttended.StudentStatus = 1;
        //            studentAttended.StudentAttendedDate = DateTime.Now;
        //            context.StudentAttendeds.Add(studentAttended);
        //            context.SaveChanges();

        //        }
        //    }

        //    ViewBag.check = attendance;
        //    // giang update session
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Lecturer lecturer = new Lecturer();
        //    if (jsonaccount != null)
        //    {
        //        lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
        //    }
        //    ViewBag.Lecturer = lecturer;
        //    // giang update session
        //    return RedirectToAction("TimeTable");
        //}

        //giang update
        public IActionResult EditAttendance(int classId, int scheduleId)
        {
            var ListStudent = (from Student in context.Students
                               where Student.ClassId == classId
                               select Student);
            List<StudentAttended> listSA = (from StudentAttended in context.StudentAttendeds
                                            where StudentAttended.ScheduleId == scheduleId
                                            select StudentAttended).ToList();
            ViewBag.student = ListStudent;
            ViewBag.listSA = listSA;
            ViewBag.scheduleId = scheduleId;
            ViewBag.classId = classId;

            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session

            return View();
        }
        public IActionResult CheckAttendance(List<int> attendance, int scheduleId, int classId)
        {
            List<Student> listStudent = (from Student in context.Students
                                         where Student.ClassId == classId
                                         select Student).ToList();
            Schedule schedule = context.Schedules.SingleOrDefault(s => s.ScheduleId == scheduleId);
            schedule.Status = true;
            context.SaveChanges();
            if (listStudent.Count() == attendance.Count())
            {
                for (int i = 0; i < listStudent.Count(); i++)
                {
                    StudentAttended studentAttended1 = new StudentAttended();
                    studentAttended1.StudentId = listStudent[i].StudentId;
                    studentAttended1.ScheduleId = scheduleId;
                    studentAttended1.StudentStatus = 2;
                    studentAttended1.StudentAttendedDate = DateTime.Now;
                    context.StudentAttendeds.Add(studentAttended1);
                    context.SaveChanges();
                    i++;

                }
            }
            else
            {
                for (int i = 0; i < listStudent.Count(); i++)
                {
                    for (int j = 0; j < attendance.Count(); j++)
                    {
                        if (listStudent[i].StudentId == attendance[j])
                        {
                            StudentAttended studentAttended1 = new StudentAttended();
                            studentAttended1.StudentId = listStudent[i].StudentId;
                            studentAttended1.ScheduleId = scheduleId;
                            studentAttended1.StudentStatus = 2;
                            studentAttended1.StudentAttendedDate = DateTime.Now;
                            context.StudentAttendeds.Add(studentAttended1);
                            context.SaveChanges();
                            i++;
                        }

                    }
                    StudentAttended studentAttended = new StudentAttended();
                    studentAttended.StudentId = listStudent[i].StudentId;
                    studentAttended.ScheduleId = scheduleId;
                    studentAttended.StudentStatus = 1;
                    studentAttended.StudentAttendedDate = DateTime.Now;
                    context.StudentAttendeds.Add(studentAttended);
                    context.SaveChanges();
                }
            }
            ViewBag.check = attendance;
            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session

            return RedirectToAction("TimeTable");
        }
        public IActionResult CheckEditAttendance(List<int> attendance, int scheduleId, int classId)
        {
            List<Student> listStudent = (from Student in context.Students
                                         where Student.ClassId == classId
                                         select Student).ToList();
            if (listStudent.Count() == attendance.Count())
            {
                for (int i = 0; i < listStudent.Count(); i++)
                {
                    StudentAttended studentAttended1 = context.StudentAttendeds.
                        FirstOrDefault(x => x.StudentId == listStudent[i].StudentId
                        && x.ScheduleId == scheduleId);
                    studentAttended1.StudentStatus = 2;
                    context.SaveChanges();
                    i++;
                }
            }
            else
            {
                for (int i = 0; i < listStudent.Count(); i++)
                {
                    for (int j = 0; j < attendance.Count(); j++)
                    {
                        if (listStudent[i].StudentId == attendance[j])
                        {
                            StudentAttended studentAttended1 = context.StudentAttendeds.
                                FirstOrDefault(x => x.StudentId == listStudent[i].StudentId
                                && x.ScheduleId == scheduleId);
                            studentAttended1.StudentStatus = 2;
                            context.SaveChanges();
                            i++;
                        }
                    }
                    StudentAttended studentAttended = context.StudentAttendeds.
                        FirstOrDefault(x => x.StudentId == listStudent[i].StudentId
                        && x.ScheduleId == scheduleId);
                    studentAttended.StudentStatus = 1;
                    context.SaveChanges();
                }
            }
            ViewBag.check = attendance;
            // giang update session
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturer = new Lecturer();
            if (jsonaccount != null)
            {
                lecturer = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturer;
            // giang update session
            return RedirectToAction("TimeTable");
        }
        //giang update

        //public IActionResult StudentDetails(int StudentID)
        //{
        //    StudentID = 4;
        //    Student student = context.Students.Where(x => x.StudentId == StudentID).FirstOrDefault();
        //    if (student == null)
        //    {
        //        for (int i = 0; i < listStudent.Count(); i++)
        //        {
        //            StudentAttended studentAttended1 = context.StudentAttendeds.
        //                FirstOrDefault(x => x.StudentId == listStudent[i].StudentId
        //                && x.ScheduleId == scheduleId);
        //            studentAttended1.StudentStatus = 2;
        //            context.SaveChanges();
        //            i++;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < listStudent.Count(); i++)
        //        {
        //            for (int j = 0; j < attendance.Count(); j++)
        //            {
        //                if (listStudent[i].StudentId == attendance[j])
        //                {
        //                    StudentAttended studentAttended1 = context.StudentAttendeds.
        //                        FirstOrDefault(x => x.StudentId == listStudent[i].StudentId
        //                        && x.ScheduleId == scheduleId);
        //                    studentAttended1.StudentStatus = 2;
        //                    context.SaveChanges();
        //                    i++;
        //                }
        //                return View("Error");
        //            }

        //            return View(student);
        //        }

        public IActionResult StudentDetails()
        {
            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student2 = new Student();
            if (jsonaccount != null)
            {
                student2 = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student2;
            // giang update sesion

            int StudentID = student2.StudentId;
            Student student = context.Students.Where(x => x.StudentId == StudentID).FirstOrDefault();
            if (student == null)
            {
                return View("Error");
            }

            return View(student);
        }

        public IActionResult StudentGrade()
        {
            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student;
            // giang update sesion

            int StudentId = student.StudentId;
            //List semester
            List<Semester> listSemeters = context.Semesters.ToList();
            ViewBag.listSemeters = listSemeters;
            //List Grade
            List<Grade> grades = context.Grades.ToList();
            //List Grade Category
            List<GradeCategory> gradeCategories = context.GradeCategories.ToList();
            //List Subject
            var listSubject = (from st in context.Students
                               join su in context.Subjects on st.ClassId equals su.ClassId
                               where st.StudentId == StudentId
                               select su).ToList();
            ViewBag.listSubjects = listSubject;

            

            return View();
        }

        public IActionResult GradeOfSubject(int SubjectId)
        {
            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Student = student;
            // giang update sesion
            int StudentId = student.StudentId;

            //List semester
            List<Semester> listSemeters = context.Semesters.ToList();
            ViewBag.listSemeters = listSemeters;
            //List Grade
            List<Grade> grades = context.Grades.ToList();
            ViewBag.grades = grades;
            //List Grade Category
            List<GradeCategory> gradeCategories = context.GradeCategories.ToList();
            //List Subject
            var listSubject = (from st in context.Students
                               join su in context.Subjects on st.ClassId equals su.ClassId
                               where st.StudentId == StudentId
                               select su).ToList();
            ViewBag.listSubjects = listSubject;
            List<StudentGrade> studentGrades = context.StudentGrades.Where(x => x.SubjectId == SubjectId && x.StudentId == StudentId).OrderBy(x => x.GradeId).OrderBy(x => x.Grade.GradeCategoryId).ToList();
            ViewBag.studentGrades = studentGrades;

            float avgDiem = 0;
            int count = 0;
            bool check = false;
            foreach (StudentGrade grade in studentGrades)
            {
                foreach (Grade grade1 in grades)
                {
                    if (grade.GradeId == grade1.GradeId && grade.Value != null)
                    {
                        avgDiem = avgDiem + (float)(grade.Value);
                        count = count + 1;
                    }
                    if (grade.Value == null)
                    {
                        check = true;
                    }
                }
            }
            avgDiem /= count;
            ViewBag.avgDiem = avgDiem;
            ViewBag.Status = "Passed";
            if(check == true)
            {
                ViewBag.avgDiem = 0;
                ViewBag.Status = "Not pass";
            }
            
            return View(studentGrades);
        }
        //public IActionResult FeedBack()
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    int classId = (int)student.ClassId;
        //    ViewBag.ListSubject = context.Subjects.Where(x => x.ClassId == classId).ToList();
        //    ViewBag.ListClass = context.Classes.ToList();
        //    ViewBag.Lecture = context.Lecturers.ToList();
        //    ViewBag.Student = student;
        //    return View();
        //}
        //public IActionResult EditFeedback(int subjectId, int lecturerId)
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
        //    ViewBag.Lecturer = context.Lecturers.ToList();
        //    ViewBag.Class = context.Classes.ToList();
        //    Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
        //    ViewBag.feedback = feedback;
        //    ViewBag.Student = student;
        //    return View();
        //}
        //public IActionResult AddFeedback(int subjectId, int lecturerId)
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
        //    ViewBag.Lecturer = context.Lecturers.ToList();
        //    ViewBag.Class = context.Classes.ToList();
        //    ViewBag.Student = student;
        //    return View();
        //}
        //public IActionResult SaveFeedback(int subjectId, int lecturerId, int tb, int tsk, int tac, int tsg, int trt, String comment)
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    Feedback feedback = new Feedback();
        //    feedback.StudentId = student.StudentId;
        //    feedback.SubjectId = subjectId;
        //    feedback.LectureId = lecturerId;
        //    feedback.TeacherPunctuality = tb;
        //    feedback.TeacherSkill = tsk;
        //    feedback.TeacherCoverTopics = tac;
        //    feedback.TeacherSupport = tsg;
        //    feedback.TeacherRespond = trt;
        //    feedback.Comment = comment;
        //    context.Feedbacks.Add(feedback);
        //    context.SaveChanges();
        //    ViewBag.Student = student;
        //    return RedirectToAction("FeedBack");
        //}

        //public IActionResult EditInfoFeedBack(int subjectId, int lecturerId, int tb, int tsk, int tac, int tsg, int trt, String comment)
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
        //    feedback.TeacherPunctuality = tb;
        //    feedback.TeacherSkill = tsk;
        //    feedback.TeacherCoverTopics = tac;
        //    feedback.TeacherSupport = tsg;
        //    feedback.TeacherRespond = trt;
        //    feedback.Comment = comment;
        //    context.SaveChanges();
        //    ViewBag.Student = student;
        //    return RedirectToAction("FeedBack");
        //}
        //public IActionResult EditComment(int subjectId, int lecturerId)
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
        //    ViewBag.feedback = feedback;
        //    ViewBag.Student = student;
        //    return View();
        //}
        //public IActionResult EditInfoComment(int subjectId, int lecturerId, String comment)
        //{

        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
        //    feedback.Comment = comment;
        //    context.SaveChanges();
        //    ViewBag.Student = student;
        //    return RedirectToAction("FeedBack");
        //}

        //update
        //public IActionResult FeedBack()
        //{
        //    var session = HttpContext.Session;
        //    string jsonaccount = session.GetString("account");
        //    Student student = new Student();
        //    if (jsonaccount != null)
        //    {
        //        student = JsonConvert.DeserializeObject<Student>(jsonaccount);
        //    }
        //    int classId = (int)student.ClassId;
        //    ViewBag.ListSubject = context.Subjects.Where(x => x.ClassId == classId).ToList();
        //    ViewBag.ListClass = context.Classes.ToList();
        //    ViewBag.Lecture = context.Lecturers.ToList();
        //    ViewBag.Student = student;
        //    return View();
        //}
        public IActionResult FeedBack()
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            int classId = (int)student.ClassId;
            List<int> checkfeedback = new List<int>();
            List<Subject> listSub = context.Subjects.Where(x => x.ClassId == classId).ToList();
            ViewBag.ListSubject = listSub;
            List<Feedback> listFeedbackOfStudent = new List<Feedback>();
            for (int i = 0; i < listSub.Count; i++)
            {
                Feedback f = context.Feedbacks.Where(x => x.StudentId == student.StudentId && x.SubjectId == listSub[i].SubjectId).FirstOrDefault();
                listFeedbackOfStudent.Add(f);
                if (f == null)
                {
                    checkfeedback.Add(1);
                }
                else
                {
                    checkfeedback.Add(0);
                }
            }
            ViewBag.listF = listFeedbackOfStudent;
            ViewBag.listCheck = checkfeedback;
            ViewBag.ListClass = context.Classes.ToList();
            ViewBag.Lecture = context.Lecturers.ToList();
            ViewBag.Student = student;
            return View();
        }
        public IActionResult EditFeedback(int subjectId, int lecturerId)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
            ViewBag.Lecturer = context.Lecturers.ToList();
            ViewBag.Class = context.Classes.ToList();
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
            ViewBag.feedback = feedback;
            ViewBag.Student = student;
            return View();
        }
        public IActionResult AddFeedback(int subjectId, int lecturerId)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
            ViewBag.Lecturer = context.Lecturers.ToList();
            ViewBag.Class = context.Classes.ToList();
            ViewBag.Student = student;
            return View();
        }
        public IActionResult SaveFeedback(int subjectId, int lecturerId, int tb, int tsk, int tac, int tsg, int trt, String comment)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            Feedback feedback = new Feedback();
            feedback.StudentId = student.StudentId;
            feedback.SubjectId = subjectId;
            feedback.LectureId = lecturerId;
            feedback.TeacherPunctuality = tb;
            feedback.TeacherSkill = tsk;
            feedback.TeacherCoverTopics = tac;
            feedback.TeacherSupport = tsg;
            feedback.TeacherRespond = trt;
            feedback.Comment = comment;
            feedback.FeedbackDate = DateTime.Now;
            context.Feedbacks.Add(feedback);
            context.SaveChanges();
            ViewBag.Student = student;
            return RedirectToAction("FeedBack");
        }

        public IActionResult EditInfoFeedBack(int subjectId, int lecturerId, int tb, int tsk, int tac, int tsg, int trt, String comment)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
            feedback.TeacherPunctuality = tb;
            feedback.TeacherSkill = tsk;
            feedback.TeacherCoverTopics = tac;
            feedback.TeacherSupport = tsg;
            feedback.TeacherRespond = trt;
            feedback.Comment = comment;
            context.SaveChanges();
            ViewBag.Student = student;
            return RedirectToAction("FeedBack");
        }
        public IActionResult EditComment(int subjectId, int lecturerId)
        {
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
            ViewBag.feedback = feedback;
            ViewBag.Student = student;
            return View();
        }
        public IActionResult EditInfoComment(int subjectId, int lecturerId, String comment)
        {

            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Student student = new Student();
            if (jsonaccount != null)
            {
                student = JsonConvert.DeserializeObject<Student>(jsonaccount);
            }
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == student.StudentId).FirstOrDefault();
            feedback.Comment = comment;
            context.SaveChanges();
            ViewBag.Student = student;
            return RedirectToAction("FeedBack");
        }
        //update
        public IActionResult ViewFeedBack()
        {

            Lecturer lecturer = context.Lecturers.Where(x => x.LecturerId == 1).FirstOrDefault();
            List<Subject> listSubjec = context.Subjects.Where(x => x.LecturerId == lecturer.LecturerId).ToList();
            int[,] totalFeedback = new int[listSubjec.Count, 5];
            for (int i = 0; i < listSubjec.Count; i++)
            {
                totalFeedback[i, 0] = context.Feedbacks.Where(x => x.SubjectId == listSubjec[i].SubjectId
                                        && x.LectureId == listSubjec[i].LecturerId).Sum(x => x.TeacherPunctuality).Value;
                totalFeedback[i, 1] = context.Feedbacks.Where(x => x.SubjectId == listSubjec[i].SubjectId
                                        && x.LectureId == listSubjec[i].LecturerId).Sum(x => x.TeacherSkill).Value;
                totalFeedback[i, 2] = context.Feedbacks.Where(x => x.SubjectId == listSubjec[i].SubjectId
                                        && x.LectureId == listSubjec[i].LecturerId).Sum(x => x.TeacherCoverTopics).Value;
                totalFeedback[i, 3] = context.Feedbacks.Where(x => x.SubjectId == listSubjec[i].SubjectId
                                        && x.LectureId == listSubjec[i].LecturerId).Sum(x => x.TeacherRespond).Value;
                totalFeedback[i, 4] = context.Feedbacks.Where(x => x.SubjectId == listSubjec[i].SubjectId
                                        && x.LectureId == listSubjec[i].LecturerId).Sum(x => x.TeacherSupport).Value;
            }
            ViewBag.totalFeedback = totalFeedback;
            ViewBag.Lecturer = lecturer;
            ViewBag.Class = context.Classes.ToList();
            ViewBag.ListSubject = context.Subjects.Where(x => x.LecturerId == lecturer.LecturerId).ToList();

            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturerr = new Lecturer();
            if (jsonaccount != null)
            {
                lecturerr = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturerr;
            // giang update sesion

            return View();
        }
        public IActionResult ViewFeedbackDetail(int subjectId, int lecturerId)
        {
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId).FirstOrDefault();
            ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
            ViewBag.Lecturer = context.Lecturers.Where(x => x.LecturerId == lecturerId).FirstOrDefault();
            ViewBag.Class = context.Classes.ToList();
            List<Student> listStudent = context.Students.Where(x => x.ClassId == feedback.Subject.ClassId).ToList();

            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturerr = new Lecturer();
            if (jsonaccount != null)
            {
                lecturerr = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturerr;
            // giang update sesion

            ViewBag.listStudent = listStudent;

            return View();
        }
        public IActionResult ViewFeedbackStudentDetail(int subjectId, int lecturerId, int studentId)
        {

            ViewBag.Subject = context.Subjects.Where(x => x.SubjectId == subjectId).FirstOrDefault();
            ViewBag.Lecturer = context.Lecturers.ToList();
            ViewBag.Class = context.Classes.ToList();
            Feedback feedback = context.Feedbacks.Where(x => x.SubjectId == subjectId && x.LectureId == lecturerId && x.StudentId == studentId).FirstOrDefault();
            ViewBag.feedback = feedback;
            // giang update sesion
            var session = HttpContext.Session;
            string jsonaccount = session.GetString("account");
            Lecturer lecturerr = new Lecturer();
            if (jsonaccount != null)
            {
                lecturerr = JsonConvert.DeserializeObject<Lecturer>(jsonaccount);
            }
            ViewBag.Lecturer = lecturerr;
            // giang update sesion

            return View();
        }
    }
}

