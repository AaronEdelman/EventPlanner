using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class VoteViewModel
    {
        public List<Entertainment> entertainments { get; set; }
        public Group group { get; set; }
        public bool pointsAccepted { get; set; }
        public Entertainment name { get; set; }

    }
}