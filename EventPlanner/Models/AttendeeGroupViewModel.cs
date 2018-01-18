using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class AttendeeGroupViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Group> CurrentGroups { get; set; }
        public List<Group> Invites { get; set; }

    }
}