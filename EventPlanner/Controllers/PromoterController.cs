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
        ApplicationDbContext db;
        public PromoterController()
        {
            db = new ApplicationDbContext();
        }

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
        public ActionResult CreateEntertainment(int id)
        {
            var EntertainmentModel = new CreateEntertainmentViewModel
            {
                PreMadeVenues = new SelectList(db.Venues, "Id", "Name")
            };
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach (ApplicationUser user in db.Users)
            {
                if (user.Id == currentUserId)
                {
                    EntertainmentModel.Promoter = user;
                }
            }
            foreach (Event foundEvent in db.Events)
            {
                if (foundEvent.Id == id)
                {
                    EntertainmentModel.CurrentEvent = foundEvent;
                    EntertainmentModel.CurrentEventId = foundEvent.Id;
                }
            }
            return View(EntertainmentModel);

        }
        [HttpPost]
        public ActionResult CreateEntertainment(CreateEntertainmentViewModel createEntertainmentViewModel)
        {
            var newEntertainment = new Entertainment
            {
                Name = createEntertainmentViewModel.CurrentEntertainment.Name,
                StartTime = createEntertainmentViewModel.CurrentEntertainment.StartTime,
                EndTime = createEntertainmentViewModel.CurrentEntertainment.EndTime,
                StartDate = createEntertainmentViewModel.CurrentEntertainment.StartDate,
                EndDate = createEntertainmentViewModel.CurrentEntertainment.EndDate,
                Restriction = createEntertainmentViewModel.CurrentEntertainment.Restriction,
                VenueId = createEntertainmentViewModel.CurrentVenueId,
                EventId = createEntertainmentViewModel.CurrentEventId,
            };
            db.Entertainments.Add(newEntertainment);
            db.SaveChanges();
            return RedirectToAction("Index", "Events");

        }
        [HttpGet]
        public ActionResult CreateVenue(int id)
        {
            var newCreateVenueModel = new CreateVenueViewModel
            {
                IdOfEvent = id
            };
            return View(newCreateVenueModel);
        }
        [HttpPost]
        public ActionResult CreateVenue(CreateVenueViewModel model)
        {
            var newVenue = new Venue
            {
                Name = model.CurrentVenue.Name,
                Latitude = model.CurrentVenue.Latitude,
                Longitude = model.CurrentVenue.Longitude,
                IsDisabledFriendly = model.CurrentVenue.IsDisabledFriendly,
                IsOutdoors = model.CurrentVenue.IsOutdoors,
                HasSeating = model.CurrentVenue.HasSeating,
            };
            foreach (Event foundEvent in db.Events)
            {
                if (foundEvent.Id == model.IdOfEvent)
                {
                    newVenue.Event = foundEvent;
                }
            }
            int eventId = model.IdOfEvent;
            db.Venues.Add(newVenue);
            db.SaveChanges();
            return RedirectToAction("Index", "Events", new { id = eventId });

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

        public ActionResult ViewVenues(int Id)
        {
            var viewVenuesViewModel = new ViewVenuesViewModel();
            viewVenuesViewModel.UserVenues = new List<Venue>();
            viewVenuesViewModel.UserEntertainment = new List<Entertainment>();
            //try
            //{
            foreach (Venue venue in db.Venues)
            {
                if (Id == venue.EventId)
                {
                    viewVenuesViewModel.UserVenues.Add(venue);
                }
            }
            foreach (Entertainment entertainment in db.Entertainments)
            {
                if(Id == entertainment.EventId)
                {
                    viewVenuesViewModel.UserEntertainment.Add(entertainment);
                }
            }
            return View(viewVenuesViewModel);
        }
    }
}