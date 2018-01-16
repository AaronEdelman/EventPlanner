using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class GroupCreateViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

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