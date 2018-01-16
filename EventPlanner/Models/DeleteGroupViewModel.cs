using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class DeleteGroupViewModel
    {
        public Group Group { get; set; }
        public List<Group> GroupsToDelete { get; set; }
        public List<GroupToEvents> GroupToEventsToDelete { get; set; }
        public List<UserToGroup> UserToGroupsToDelete { get; set; }
    }
}