using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class EntertainmentPreference
    {
        [Key]
        public int Id { get; set; }
        public int PreferenePoints { get; set; }

        public string  UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public string EntertainmentId { get; set; }
        [ForeignKey("EntertainmentId")]
        public Entertainment Entertainment { get; set; }
    }
}