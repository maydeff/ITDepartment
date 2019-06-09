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

namespace ITDepartment.Controllers
{
    [Authorized("Release", "View")]
    public class ReleaseController : Controller
    {
        private ITDepartmentEntities db = new ITDepartmentEntities();

        // GET: Release
        [Authorized("Release", "View")]
        public ActionResult Index()
        {
            var release = db.Release.Include(r => r.Project);
            return View(release.ToList());
        }

        // GET: Release/Details/5
        [Authorized("Release", "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Release.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        // GET: Release/Create
        [Authorized("Release", "Add")]
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Release/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorized("Release", "Add")]
        public ActionResult Create([Bind(Include = "ReleaseId,ProjectId,ReleaseDate,ReleaseName,ReleaseDescription,ServerName")] Release release)
        {
            if (ModelState.IsValid)
            {
                db.Release.Add(release);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", release.ProjectId);
            return View(release);
        }

        // GET: Release/Edit/5
        [Authorized("Release", "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Release.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", release.ProjectId);
            return View(release);
        }

        // POST: Release/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorized("Release", "Edit")]
        public ActionResult Edit([Bind(Include = "ReleaseId,ProjectId,ReleaseDate,ReleaseName,ReleaseDescription,ServerName")] Release release)
        {
            if (ModelState.IsValid)
            {
                db.Entry(release).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", release.ProjectId);
            return View(release);
        }

        // GET: Release/Delete/5
        [Authorized("Release", "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Release.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        // POST: Release/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorized("Release", "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Release release = db.Release.Find(id);
            db.Release.Remove(release);
            db.SaveChanges();
            return RedirectToAction("Index");
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
