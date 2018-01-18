using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class EventDetialsViewModel
    {
        public Event CurrentEvent { get; set; }
        public List <Venue> EventVenues { get; set; }
    }
}