using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITDepartment.Attributes;
using ITDepartment.DataAccess;
using ITDepartment.Models.Task;

namespace ITDepartment.Controllers
{
    [Authorized("Task", "View")]
    public class TaskController : Controller
    {
        private ITDepartmentEntities db = new ITDepartmentEntities();

        // GET: Task
        [Authorized("Task", "View")]
        public ActionResult Index()
        {
            var taskList = from task in db.Task
                select new TaskViewModel
                {
                    TaskId = task.TaskId,
                    SprintId = task.SprintId,
                    IsDone = task.IsDone,
                    TaskName = task.TaskName
                };

            return View(taskList);
        }

        // GET: Task/Details/5
        [Authorized("Task", "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            var taskDetailsViewModel = new TaskDetailsViewModel
            {
                TaskId = task.TaskId,
                SprintId = task.SprintId,
                TaskName = task.TaskName,
                IsDone = task.IsDone,
                TaskDescription = task.TaskDescription
            };

            return View(taskDetailsViewModel);
        }

        // GET: Task/Create
        [Authorized("Task", "Add")]

        public ActionResult Create()
        {
            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "SprintStart");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorized("Task", "Add")]

        public ActionResult Create([Bind(Include = "TaskId,SprintId,TaskName,TaskDescription,IsDone")] TaskCreateModel task)
        {
            if (ModelState.IsValid)
            {
                var newTask = new Task
                {
                    TaskName = task.TaskName,
                    SprintId = task.SprintId,
                    TaskDescription = task.TaskDescription,
                    IsDone = false

                };

                db.Task.Add(newTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "SprintEnd", task.SprintId);
            return View(task);
        }

        // GET: Task/Edit/5
        [Authorized("Task", "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            var taskEditModel = new TaskEditModel
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                IsDone = task.IsDone,
                SprintId = task.SprintId
            };


            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "SprintStart", task.SprintId);
            return View(taskEditModel);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorized("Task", "Edit")]
        public ActionResult Edit([Bind(Include = "TaskId,SprintId,TaskName,TaskDescription,IsDone")] TaskEditModel task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            var taskEntity = db.Task.FirstOrDefault(x => x.TaskId == task.TaskId);
            if (taskEntity == null)
            {
                return HttpNotFound();
            }

            taskEntity.SprintId = task.SprintId;
            taskEntity.TaskName = task.TaskName;
            taskEntity.TaskDescription = task.TaskDescription;
            taskEntity.IsDone = task.IsDone;

            db.Entry(taskEntity).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "SprintId", task.SprintId);
            return RedirectToAction("Index");
        }

        // GET: Task/Delete/5
        [Authorized("Task", "Delete")]
        public ActionResult Delete(int? id)
        {
            var task = db.Task.FirstOrDefault(x => x.TaskId == id);
            if (task != null)
            {
                db.Task.Remove(task);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Task");
        }
        [Authorized("Task", "Delete")]
        public ActionResult DeleteConfirmation(int? id)
        {
            var taskToDelete = db.Task.FirstOrDefault(x => x.TaskId == id);
            if (taskToDelete == null)
            {
                return HttpNotFound();
            }
            return PartialView("ConfirmationBody");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
