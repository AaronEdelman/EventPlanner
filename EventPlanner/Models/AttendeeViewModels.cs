using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Models
{
    public class GroupCreateViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string CurrentUserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group Name")]
        public Group Group{ get; set; }

        [Required]
        [Display(Name = "Event")]
        public Event Event { get; set; }

        [Display(Name = "Events")]
        public SelectList Events { get; set; }
    }

    public class GroupEditViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }
    }
}