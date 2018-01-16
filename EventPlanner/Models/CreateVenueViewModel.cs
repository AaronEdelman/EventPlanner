using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class CreateVenueViewModel
    {
        public ApplicationUser Promoter { get; set; }
        public Entertainment CurrentEntertainment { get; set; }
    }
}