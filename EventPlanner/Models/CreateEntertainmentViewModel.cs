using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class CreateEntertainmentViewModel
    {
        public Entertainment CurrentEntertainment { get; set; }
        public ApplicationUser Promoter { get; set; }
        public Event CurrentEvent { get; set; }
        public List<Venue> PreMadeVenues { get; set; }
        public int VenueId { get; set; }

    }
}