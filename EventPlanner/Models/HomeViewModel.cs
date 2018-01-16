using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class HomeViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
    }
}