using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class AttendeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendee
        public ActionResult Index()
        {
            var userToGroups = db.UserToGroups.Include(u => u.Group).Include(u => u.User);
            return View(userToGroups.ToList());
        }

        // GET: Attendee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToGroup userToGroup = db.UserToGroups.Find(id);
            if (userToGroup == null)
            {
                return HttpNotFound();
            }
            return View(userToGroup);
        }

        // GET: Attendee/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Attendee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupCreateViewModel model)
        //public ActionResult Create([Bind(Include = "Id,UserId,GroupId")] UserToGroup userToGroup)
        {
            if (ModelState.IsValid)
            {
                var newGroup = new Group{ Name = model.Name };
                var newUserToGroup = new UserToGroup { UserId = model.UserId, GroupId = newGroup.Id };
                db.Groups.Add(newGroup);
                db.UserToGroups.Add(newUserToGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // GET: Attendee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToGroup userToGroup = db.UserToGroups.Find(id);
            if (userToGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", userToGroup.GroupId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userToGroup.UserId);
            return View(userToGroup);
        }

        // POST: Attendee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,GroupId")] UserToGroup userToGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userToGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", userToGroup.GroupId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userToGroup.UserId);
            return View(userToGroup);
        }

        // GET: Attendee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserToGroup userToGroup = db.UserToGroups.Find(id);
            if (userToGroup == null)
            {
                return HttpNotFound();
            }
            return View(userToGroup);
        }

        // POST: Attendee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserToGroup userToGroup = db.UserToGroups.Find(id);
            db.UserToGroups.Remove(userToGroup);
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
