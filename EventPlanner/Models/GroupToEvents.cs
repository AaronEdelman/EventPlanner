using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class GroupToEvents
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
}