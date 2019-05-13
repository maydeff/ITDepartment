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
using ITDepartment.Models.Sprint;
using WebGrease.Configuration;

namespace ITDepartment.Controllers
{
    [Authorized]
    public class SprintController : Controller
    {
        private ITDepartmentEntities db = new ITDepartmentEntities();

        // GET: Sprint
        public ActionResult Index()
        {
            var sprintList = from sprint in db.Sprint
                select new SprintViewModel
                {
                    SprintId = sprint.SprintId,
                    ProjectId = sprint.ProjectId,
                    SprintStart = sprint.SprintStart,
                    SpintEnd = sprint.SpintEnd,
                    ProjectName = sprint.Project.ProjectName
                };

            return View(sprintList);
        }

        // GET: Sprint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TODO: Change all finds to firstordefault
            //TODO: extend all details view models to contain foreign objects
            Sprint sprint = db.Sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            var sprintDetailsViewModel = new SprintDetailsViewModel
            {
                SprintId = sprint.SprintId,
                ProjectId = sprint.ProjectId,
                SprintStart = sprint.SprintStart,
                SpintEnd = sprint.SpintEnd,
                ProjectName = sprint.Project.ProjectName
            };

            return View(sprintDetailsViewModel);
        }

        // GET: Sprint/Create
        public ActionResult Create()
        {
            //TODO: change it to type model
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Sprint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintId,ProjectId,SprintStart,SpintEnd")] SprintCreateModel sprint)
        {
            if (ModelState.IsValid)
            {
                var newSprint = new Sprint
                {
                    ProjectId = sprint.ProjectId,
                    SprintStart = sprint.SprintStart,
                    SpintEnd = sprint.SpintEnd
                };


                db.Sprint.Add(newSprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //TODO: stronly typed view
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", sprint.ProjectId);
            return View(sprint);
        }

        // GET: Sprint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sprint = db.Sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            var sprintEditModel = new SprintEditModel
            {
                SprintId = sprint.SprintId,
                ProjectId = sprint.ProjectId,
                SprintStart = sprint.SprintStart,
                SpintEnd = sprint.SpintEnd
            };

            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", sprint.ProjectId);
            return View(sprintEditModel);
        }

        // POST: Sprint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintId,ProjectId,SprintStart,SpintEnd")] SprintEditModel sprint)
        {
            if (!ModelState.IsValid)
            {
                return View(sprint);
            }

            var sprintEntity = db.Sprint.FirstOrDefault(x => x.SprintId == sprint.SprintId);
            if (sprintEntity == null)
            {
                return HttpNotFound();
            }

            sprintEntity.ProjectId = sprint.ProjectId;
            sprintEntity.SprintStart = sprint.SprintStart;
            sprintEntity.SpintEnd = sprint.SpintEnd;

            db.Entry(sprintEntity).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", sprint.ProjectId);
            return RedirectToAction("Index");
        }

        // GET: Sprint/Delete/5
        public ActionResult Delete(int id)
        {
            var sprint = db.Sprint.FirstOrDefault(x => x.SprintId == id);
            if (sprint != null)
            {
                db.Sprint.Remove(sprint);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Sprint");
        }

        public ActionResult DeleteConfirmation(int? id)
        {
            var sprintToDelete = db.Sprint.FirstOrDefault(x => x.SprintId == id);
            if (sprintToDelete == null)
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
