using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Models
{
    public class CreateEntertainmentViewModel
    {
        public Entertainment CurrentEntertainment { get; set; }
        public ApplicationUser Promoter { get; set; }
        public Event CurrentEvent { get; set; }
        public SelectList PreMadeVenues { get; set; }
        public int CurrentVenueId { get; set; }
        public int CurrentEventId { get; set; }

    }
}