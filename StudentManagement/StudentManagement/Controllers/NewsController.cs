using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Newtonsoft.Json;

namespace StudentManagement.Controllers
{
    public class NewsController : Controller
    {
        student_managementContext context = new student_managementContext();
        // GET: NewsController
        public ActionResult Index()
        {
            int lecturerId = 1;
            List<Lecturer> lecturerList = context.Lecturers.ToList();
            List<News> listNews = context.News.Where(x => x.LecturersId == lecturerId).ToList();

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

            return View(listNews);
        }
        public ActionResult List(int? page)
        {
            List<Lecturer> lecturerList = context.Lecturers.ToList();
            //List news
            if (page == null) page = 1;
            var listNews = context.News.OrderByDescending(s => s.Date).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

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

            return View(listNews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewNews(int id)
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


            List<Lecturer> lecturerList = context.Lecturers.ToList();
            News news = context.News.Where(n => n.Id == id).FirstOrDefault();
            //Tin khac
            List<News> listNew = context.News.OrderByDescending(n => n.Date).Take(5).ToList();
            ViewBag.listNew = listNew;

            

            return View(news);
        }
        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            List<Lecturer> lecturerList = context.Lecturers.ToList();
            News news = context.News.Where(n => n.Id == id).FirstOrDefault();

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

            return View(news);
        }

        // GET: NewsController/Create
        public ActionResult Create()
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

            return View();
        }

        // POST: NewsController/Create
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

        // GET: NewsController/Edit/5
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

            List<Lecturer> lecturerList = context.Lecturers.ToList();
            News news = context.News.Where(n => n.Id == id).FirstOrDefault();

            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            try
            {
                News oldnews = context.News.Where(n => n.Id == news.Id).FirstOrDefault();
                oldnews.Title = news.Title;
                oldnews.Description = news.Description;
                oldnews.Date = news.Date;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
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
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(News news)
        {
            try
            {
                context.News.Remove(news);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
