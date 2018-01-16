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
    public class PromoterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promoter
        public ActionResult Index()
        {
            var entertainments = db.Entertainments.Include(e => e.Event).Include(e => e.Venue);
            return View(entertainments.ToList());
        }

        // GET: Promoter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.Entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            return View(entertainment);
        }

        // GET: Promoter/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name");
            return View();
        }

        // POST: Promoter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StartTime,EndTime,StartDate,EndDate,Restriction,VenueId,EventId")] Entertainment entertainment, int Id)
        {
            if (ModelState.IsValid)
            {
                string venueId = Request.Form["Venue"];
                entertainment.EventId = Id;
                entertainment.VenueId = Convert.ToInt32(venueId);
                db.Entertainments.Add(entertainment);
                db.SaveChanges();
                return RedirectToAction("Create", "Promoter", Id);
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", entertainment.EventId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", entertainment.VenueId);
            return View(entertainment);
        }
        public ActionResult CreateVenue(int? Id)
        {
            ViewBag.Id = Id;
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");
            return View();
        }

        // POST: Promoter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVenue([Bind(Include = "Id,Name,EventId")] Venue venue, int Id)
        {
            if (ModelState.IsValid)
            {
                venue.EventId = Id;
                db.Venues.Add(venue);
                db.SaveChanges();
                return RedirectToAction("Create", "Promoter", Id);
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", venue.EventId);
            return View(venue);
        }
        // GET: Promoter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.Entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", entertainment.EventId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", entertainment.VenueId);
            return View(entertainment);
        }

        // POST: Promoter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartTime,EndTime,StartDate,EndDate,Restriction,VenueId,EventId")] Entertainment entertainment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entertainment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", entertainment.EventId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", entertainment.VenueId);
            return View(entertainment);
        }

        // GET: Promoter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.Entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            return View(entertainment);
        }

        // POST: Promoter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entertainment entertainment = db.Entertainments.Find(id);
            db.Entertainments.Remove(entertainment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Promoter/Events
        public ActionResult Events()
        {
            var events = db.Events;
            return View(events.ToList());
        }
        // GET: Promoter/MyInfo
        public ActionResult MyInfo()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(n => n.Id == userId);
            return View(user.ToList());
        }
        // GET: Promoter/CreateEvent
        public ActionResult CreateEvent()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Name");
            return View();
        }

        // POST: Promoter/CreateEvent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind(Include = "Name,StartTime,EndTime,StartDate,EndDate,Weblink,AddressId")] Event newEvent)
        {
            //Address address = new Address();
            //newEvent.AddressId = address.Id;
            if (ModelState.IsValid)
            {
                db.Events.Add(newEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(newEvent);
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
