using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class Venue_Entertainment
    {
        public List<Venue> venues { get; set; }
        public List<Entertainment> entertainment { get; set; }
    }
}