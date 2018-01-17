using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class ViewVenuesViewModel
    {
        public List <Venue> UserVenues { get; set; }
        public List<Entertainment> UserEntertainment { get; set; }
    }
}