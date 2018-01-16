using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventPlanner.Models;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Controllers
{
    public class AttendeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendee
        public ActionResult Index()
        {
            var AttendeeGroupModel = new AttendeeGroupViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var groups = (from x in db.UserToGroups where x.UserId == currentUserId select x.Group).ToList();
            AttendeeGroupModel.CurrentGroups = groups;
            AttendeeGroupModel.User = db.Users.Find(currentUserId);
            foreach(ApplicationUser user in db.Users)
            {
                if (user.Id == currentUserId)
                {
                    AttendeeGroupModel.User = user;
                }
            }
            return View(AttendeeGroupModel);
        }

        public ActionResult RemoveMemberFromGroup(string userId, int groupId)
        {
            var idToSearch = (from row in db.UserToGroups where row.UserId == userId && row.GroupId == groupId select row.Id);
            var UserToGroupToRemove = db.UserToGroups.Find(idToSearch);
            db.UserToGroups.Remove(UserToGroupToRemove);
            db.SaveChanges();
            return View();

        }

        public ActionResult RemoveEventFromGroup(int eventId, int groupId)
        {
            var idToSearch = (from row in db.GroupToEvents where row.EventId == eventId && row.GroupId == groupId select row.Id);
            var GroupToEventsToRemove = db.GroupToEvents.Find(idToSearch);
            db.GroupToEvents.Remove(GroupToEventsToRemove);
            db.SaveChanges();
            return View();

        }

        // GET: Attendee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupToEventsViewModel GroupToEvents = new GroupToEventsViewModel();
            Group Group = db.Groups.Find(id);            
            var users = (from row in db.UserToGroups where row.GroupId == id select row.User).ToList();
            var events = (from row in db.GroupToEvents where row.GroupId == id select row.Event).ToList();
            GroupToEvents.Group = Group;
            GroupToEvents.CurrentUsers = users;
            GroupToEvents.Events = events;

            return View(GroupToEvents);
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
            else
            {
                var GroupToAttendees = new GroupToAttendeesViewModel();
                foreach(Group group in db.Groups)
                {
                    if(group.Id == id)
                    {
                        GroupToAttendees.Group = group;
                    }
                }
                var currentUsers = (from row in db.UserToGroups where row.GroupId == id select row.User).ToList();
                GroupToAttendees.CurrentUsers = currentUsers;
                return View(GroupToAttendees);
            }            
        }

        // POST: Attendee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupEditViewModel model, UserToGroup userToGroup, int? id)
        {
            if (ModelState.IsValid)
            {
                var groupToUpdate = db.Groups.Find(db.UserToGroups.Find(userToGroup.Id).GroupId);
                var userToGroupToUpdate = db.UserToGroups.Find(userToGroup.Id);
                userToGroupToUpdate.GroupId = groupToUpdate.Id;
                groupToUpdate.Name = model.Name;
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
