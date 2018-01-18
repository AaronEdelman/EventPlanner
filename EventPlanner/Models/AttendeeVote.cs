
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner
{
    public class AttendeeVote
    {
        public string show;
        public int points;

        public AttendeeVote(string show, int points)
        {
            this.show = show;
            this.points = points;

        }
    }
}
