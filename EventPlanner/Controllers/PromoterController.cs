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

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEntertainment(int id)
        {
            var EntertainmentModel = new CreateEntertainmentViewModel();
            EntertainmentModel.PreMadeVenues = new List<Venue>();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach (ApplicationUser user in db.Users)
            {
                if (user.Id == currentUserId)
                {
                    EntertainmentModel.Promoter= user;
                }
            }
            foreach (Event foundEvent in db.Events)
            {
                if (foundEvent.Id == id)
                {
                    EntertainmentModel.CurrentEvent = foundEvent;
                    foreach (Venue venue in db.Venues)
                    {
                        if (venue.Event == foundEvent)
                        {
                            EntertainmentModel.PreMadeVenues.Add(venue);
                        }
                    }
                }
            }
            return View(EntertainmentModel);

        }
        [HttpPost]
        public ActionResult CreateEntertainment (CreateEntertainmentViewModel createEntertainmentViewModel)
        {
            var newEntertainment = new Entertainment
            {
                Name = createEntertainmentViewModel.CurrentEntertainment.Name,
                StartTime = createEntertainmentViewModel.CurrentEntertainment.StartTime,
                EndTime = createEntertainmentViewModel.CurrentEntertainment.EndTime,
                StartDate = createEntertainmentViewModel.CurrentEntertainment.StartDate,
                EndDate = createEntertainmentViewModel.CurrentEntertainment.EndDate,
                Restriction = createEntertainmentViewModel.CurrentEntertainment.Restriction,
                VenueId = createEntertainmentViewModel.VenueId,
                Event = createEntertainmentViewModel.CurrentEvent
            };
            db.Entertainments.Add(newEntertainment);
            return RedirectToAction("Create", "Promoter");

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
        ////GET: Promoter/View_Venues_Shows
        //public ActionResult View_Venues(int Id)
        //{
        //    var venue_entertainment = new Venue_Entertainment();
        //    venue_entertainment.venues = new List<Venue>();
        //    venue_entertainment.entertainment = new List<Entertainment>();
        //    foreach (Venue venue in db.Venues)
        //    {
        //        if (venue.EventId == Id)
        //        {
        //            venue_entertainment.venues.Add(venue);
        //        }
        //    }
        //    foreach (Entertainment show in db.Entertainments)
        //    {
        //        if (show.EventId == Id)
        //        {
        //            venue_entertainment.entertainment.Add(show);
        //        }
        //    }
        //    return View(venue_entertainment);
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
