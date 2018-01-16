using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class GroupToEventsViewModel
    {
        public Group Group { get; set; }
        public List<ApplicationUser> CurrentUsers { get; set; }
        public List<Event> Events { get; set; }
    }
}