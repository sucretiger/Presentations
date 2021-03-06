﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyToDoListApp.Models;
using System.Dynamic;

namespace MyToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            _db = new Tasks();
        }

        private Tasks _db;

        public ActionResult Index()
        {
            var tasks = _db.All();

            ViewBag.AnythingYouWant = "ribeye";

            return View(tasks);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Task task)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            dynamic obj = new ExpandoObject();
            obj.Description = task.Description;

            _db.Insert(obj);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            var task = _db.Single(id.Value);
            ViewBag.Description = task.Description;
            ViewBag.TaskId = task.TaskId;

            return View();
        }

        public ActionResult DeleteForRealz(int? id)
        {
            _db.Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}
