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
        [Authorize]
        public ActionResult Index()
        {
            var AttendeeGroupModel = new AttendeeGroupViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var acceptedGroups = (from x in db.UserToGroups where x.UserId == currentUserId && x.AcceptedInvite == true select x.Group).ToList();
            var invitedGroups = (from x in db.UserToGroups where x.UserId == currentUserId && x.AcceptedInvite == false select x.Group).ToList();
            AttendeeGroupModel.CurrentGroups = acceptedGroups;
            AttendeeGroupModel.Invites = invitedGroups;
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

        public ActionResult LeaveGroup (string userId, int? groupId)
        {

            UserToGroup userToGroupToRemove = (from row in db.UserToGroups where row.UserId == userId && row.GroupId == groupId select row).First();
            db.UserToGroups.Remove(userToGroupToRemove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AcceptGroupInvite (int? id)
        {
            int userToGroupToAccept = (from x in db.UserToGroups where x.GroupId == id && x.AcceptedInvite == false select x.Id).First();
            db.UserToGroups.Find(userToGroupToAccept).AcceptedInvite = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DeclineGroupInvite (int? id)
        {
            var userToGroupToDecline = (from x in db.UserToGroups where x.GroupId == id && x.AcceptedInvite == false select x).First();
            db.UserToGroups.Remove(userToGroupToDecline);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        
        public ActionResult RemoveMemberFromGroup(string userId, int groupId)
        {
            var idSearched = (from row in db.UserToGroups where row.UserId == userId && row.GroupId == groupId select row.Id);
            int idToSearch = idSearched.First();
            var UserToGroupToRemove = db.UserToGroups.Find(idToSearch);
            db.UserToGroups.Remove(UserToGroupToRemove);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = groupId });

        }

        //GET
        public ActionResult AddEventToGroup(int? groupId)
        {
            AddEventToGroupViewModel addEventToGroupModel = new AddEventToGroupViewModel();
            addEventToGroupModel.Events = new SelectList(db.Events, "Id", "Name");
            addEventToGroupModel.Group = db.Groups.Find(groupId);
            return View(addEventToGroupModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddEventToGroup(AddEventToGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                GroupToEvents groupToEventToAdd = new GroupToEvents { GroupId = model.Group.Id, EventId = model.Event.Id };
                db.GroupToEvents.Add(groupToEventToAdd);
                db.SaveChanges();                
            }
            return RedirectToAction("Details", new { id = model.Group.Id });
        }

        public ActionResult RemoveEventFromGroup(int eventId, int groupId)
        {
            var idsearched = (from row in db.GroupToEvents where row.EventId == eventId && row.GroupId == groupId select row.Id);
            int idToSearch = idsearched.First();
            var GroupToEventsToRemove = db.GroupToEvents.Find(idToSearch);
            db.GroupToEvents.Remove(GroupToEventsToRemove);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = groupId });

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
            GroupCreateViewModel groupCreateModel = new GroupCreateViewModel();
            groupCreateModel.CurrentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            groupCreateModel.Events = new SelectList(db.Events, "Id", "Name");

            return View(groupCreateModel);
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
                var newGroup = new Group{ Name = model.Group.Name };
                var newUserToGroup = new UserToGroup { UserId = model.CurrentUserId, GroupId = newGroup.Id, AcceptedInvite = true };
                var newGroupToEvent = new GroupToEvents { GroupId = newGroup.Id, EventId = model.Event.Id };
                db.Groups.Add(newGroup);
                db.UserToGroups.Add(newUserToGroup);
                db.GroupToEvents.Add(newGroupToEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
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
        [HttpGet]
        public ActionResult DeleteGroup(int id)
        {
            DeleteGroupViewModel DeleteGroupModel = new DeleteGroupViewModel();
            DeleteGroupModel.Group = db.Groups.Find(id);     
            return View(DeleteGroupModel);
        }

        // POST: Attendee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroup(DeleteGroupViewModel DeleteGroupModel)
        {
            foreach (GroupToEvents groupToEvent in db.GroupToEvents)
            {
                if (DeleteGroupModel.Group.Id == groupToEvent.GroupId)
                {
                    db.GroupToEvents.Remove(groupToEvent);
                }
            }
            foreach (Group group in db.Groups)
            {
                if (DeleteGroupModel.Group.Id == group.Id)
                {
                    db.Groups.Remove(group);
                    break;
                }
            }
            foreach (UserToGroup userToGroup in db.UserToGroups)
            {
                if (DeleteGroupModel.Group.Id == userToGroup.GroupId)
                {
                    db.UserToGroups.Remove(userToGroup);
                }
            }
            foreach (EntertainmentPreference entertainmentPreferenceRow in db.EntertainmentPreferences)
            {
                if (DeleteGroupModel.Group.Id == entertainmentPreferenceRow.GroupId)
                {
                    db.EntertainmentPreferences.Remove(entertainmentPreferenceRow);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index","Attendee");
        }
        //Get: Attendee/InviteToGroup
        public ActionResult InviteToGroup(int id)
        {
            UserToGroup UsersGroup = new UserToGroup();
            UsersGroup.Group = db.Groups.Find(id);
            return View(UsersGroup);
        }
        //POST: Attendee/InviteToGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteToGroup(UserToGroup model)
        {
            if (ModelState.IsValid)
            {
                UserToGroup userToGroup = new UserToGroup();
                foreach (ApplicationUser user in db.Users)
                {
                    if (user.Email == model.User.Email)
                    {
                        ApplicationUser invitee = user;
                        userToGroup.UserId = invitee.Id;
                    }
                }
                    userToGroup.GroupId = model.Group.Id;
                    userToGroup.AcceptedInvite = false;
                    db.UserToGroups.Add(userToGroup);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            UserToGroup UsersGroup = new UserToGroup();
            UsersGroup.Group = db.Groups.Find(model.Group.Id);
            return View(UsersGroup);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //GET: /Attendee/Vote
        [HttpGet]
        public ActionResult Vote(int GroupId)
        {

            VoteViewModel entertainments = new VoteViewModel();
            entertainments.entertainments = new List<Entertainment>();
            var EventId = (from x in db.GroupToEvents where x.GroupId == GroupId select x.EventId);
            int firstEventId = EventId.AsQueryable().First();
            foreach (var entertainment in db.Entertainments.Include(e => e.Venue))  
            {
                if (entertainment.EventId == firstEventId)
                {
                    entertainments.entertainments.Add(entertainment);
                }
            }
            entertainments.group = db.Groups.Find(GroupId);
            return View(entertainments);
        }
        //POST: /Attendee/Vote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (Entertainment entertainment in model.entertainments)
                {
                    EntertainmentPreference preference = new EntertainmentPreference();
                    preference.PreferenePoints = entertainment.VenueId; //Here VenueId is being used to hold points input from the view.  I'm so sorry.
                    preference.GroupId = model.group.Id;
                    preference.UserId = User.Identity.GetUserId();
                    preference.EntertainmentId = entertainment.Id;
                    db.EntertainmentPreferences.Add(preference);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: /Attendee/Schedule
        [HttpGet]
        public ActionResult Schedule(int GroupId)
        {

            //Getting all entertainments for a certain group Id

            GroupVoteViewModel votes = new GroupVoteViewModel();
            votes.EntertainmentPreferences = new List<EntertainmentPreference>();
            foreach (EntertainmentPreference vote in db.EntertainmentPreferences.Include(e => e.Entertainment))
            {
                if (vote.GroupId == GroupId)
                {
                    votes.EntertainmentPreferences.Add(vote);
                }
            }

            //getting info out of model form and into objects that can be passed into an instantiated class

            List<AttendeeVote> attendeeVotes = new List<AttendeeVote>();
            foreach (EntertainmentPreference vote in votes.EntertainmentPreferences)
            {
                AttendeeVote attendeeVote = new AttendeeVote(vote.Entertainment.Name, vote.PreferenePoints);
                attendeeVotes.Add(attendeeVote);
            }
            OrganizeSchedule organizeSchedule = new OrganizeSchedule(attendeeVotes);

            List<AttendeeVote> orderedVotes = organizeSchedule.TabulateVotes();

            //getting organized info out of objects and into model

            VoteViewModel schedule = new VoteViewModel();
            schedule.entertainments = new List<Entertainment>();
            foreach (AttendeeVote vote in orderedVotes)
            {
                Entertainment preference = new Entertainment();
                preference.Name = vote.show;
                preference.VenueId = vote.points; //Once again, using venueId as a placeholder for points.  Sorry again. - Aaron
                schedule.entertainments.Add(preference);
            }

            return View(schedule);
        }
    }
}

