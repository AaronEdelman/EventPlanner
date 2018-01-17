using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class CreateVenueViewModel
    {
        public Venue CurrentVenue { get; set; }
        public int IdOfEvent { get; set; }
    }
}