using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Controllers
{
    public class MarksController : Controller
    {
        student_managementContext context = new student_managementContext();
        // GET: MarksController
        public ActionResult Index(int subjectId)
        {
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

            int lecturersId = 2;
            //subjectId = 4;
            if (subjectId != null)
            {
                ViewBag.subjectId = subjectId;
            }
            int classId = 2;
            //Non-list support for main list
            List<Grade> grades = context.Grades.ToList();
            List<Student> students = context.Students.ToList();
            List<Subject> subjects = context.Subjects.ToList();
            List<GradeCategory> gradeCategories = context.GradeCategories.ToList();
            List<Class> classes = context.Classes.ToList();
            //List Class
            List<Subject> subjectList = context.Subjects.Where(s => s.ClassId == classId && s.LecturerId == lecturersId).ToList();
            ViewBag.subjectList = subjectList;
            //End non-list
            List<StudentGrade> studentGrades = context.StudentGrades.Where(s => s.SubjectId == subjectId && s.Subject.LecturerId == lecturersId && s.Subject.Class.ClassId == classId).OrderBy(s => s.GradeId).OrderBy(s => s.Grade.GradeCategoryId).ToList();
            var studentGrades2 = studentGrades.GroupBy(x => x.StudentId).Select(g => g.First());
            var studentGrades1 = studentGrades.GroupBy(x => x.GradeId).Select(g => g.First());
            ViewBag.studentGrades1 = studentGrades1;
            return View(studentGrades2.OrderBy(s => s.SubjectId));
        }
        public ActionResult ViewMark(int subjectId, int studentId)
        {
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

            //Non-list support for main list
            List<Grade> grades = context.Grades.ToList();
            List<Student> students = context.Students.ToList();
            List<Subject> subjects = context.Subjects.ToList();
            List<GradeCategory> gradeCategories = context.GradeCategories.ToList();
            List<Class> classes = context.Classes.ToList();
            //
            List<StudentGrade> studentGrades = context.StudentGrades.Where(s => s.SubjectId == subjectId && s.StudentId == studentId).OrderBy(s => s.GradeId).OrderBy(s => s.Grade.GradeCategoryId).ToList();
            return View(studentGrades);
        }

        // GET: MarksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarksController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: MarksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            try
            {
                context.News.Add(news);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarksController/Edit/5
        public ActionResult Edit(int id)
        {
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

            //Non-list support for main list
            List<Grade> grades = context.Grades.ToList();
            List<Student> students = context.Students.ToList();
            List<Subject> subjects = context.Subjects.ToList();
            List<GradeCategory> gradeCategories = context.GradeCategories.ToList();
            List<Class> classes = context.Classes.ToList();
            //
            StudentGrade student = context.StudentGrades.Where(s => s.Id == id).FirstOrDefault();
            ViewBag.StudentName = student.Student.StudentName;
            return View(student);
        }

        // POST: MarksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGrade studentGrades)
        {
            try
            {
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

                var oldStudentGrade = context.StudentGrades.Where(s => s.Id == studentGrades.Id).FirstOrDefault();
                if(oldStudentGrade != null)
                {
                    oldStudentGrade.Value = studentGrades.Value;
                    context.SaveChanges();
                }
                return View(oldStudentGrade);
            }
            catch
            {
                return View();
            }
        }

        // GET: MarksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
