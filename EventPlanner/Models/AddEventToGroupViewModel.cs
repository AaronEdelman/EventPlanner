using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Models
{
    public class AddEventToGroupViewModel
    {
        public Group Group { get; set; }
        public SelectList Events { get; set; }
        public Event Event { get; set; }
    }
}