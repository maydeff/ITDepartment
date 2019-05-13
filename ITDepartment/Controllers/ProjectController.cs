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
using ITDepartment.Models;
using ITDepartment.Models.Project;

namespace ITDepartment.Controllers
{
    [Authorized]
    public class ProjectController : Controller
    {
        private ITDepartmentEntities db = new ITDepartmentEntities();

        // GET: Project
        public ActionResult Index()
        {
            var projectList =
                from project in db.Project
                select new ProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectDeadline = project.ProjectDeadline
                };

            return View(projectList);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var projectDetailsViewModel =  new ProjectDetailsViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectDeadline = project.ProjectDeadline,
                ProjectDescription = project.ProjectDescription
            };


            return View(projectDetailsViewModel);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectName,ProjectDescription,ProjectDeadline")] ProjectCreateModel project)
        {
            if (ModelState.IsValid)
            {
                var newProject = new Project
                {
                    ProjectName = project.ProjectName,
                    ProjectDescription = project.ProjectDescription,
                    ProjectDeadline = project.ProjectDeadline
                };

                db.Project.Add(newProject);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var projectEditModel = new ProjectEditModel
            {
                ProjectId = project.ProjectId,
                ProjectDeadline = project.ProjectDeadline,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription
            };
            return View(projectEditModel);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectDescription,ProjectDeadline")] ProjectEditModel project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            var projectEntity = db.Project.FirstOrDefault(x => x.ProjectId == project.ProjectId);
            if (projectEntity == null)
            {
                return HttpNotFound();
            }

            projectEntity.ProjectName = project.ProjectName;
            projectEntity.ProjectDeadline = project.ProjectDeadline;
            projectEntity.ProjectDescription = project.ProjectDescription;

            db.Entry(projectEntity).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var project = db.Project.FirstOrDefault(x => x.ProjectId == id);
            if (project != null)
            {
                db.Project.Remove(project);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult DeleteConfirmation(int? id)
        {
            var projectToDelete = db.Project.FirstOrDefault(x => x.ProjectId == id);
            if (projectToDelete == null)
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
