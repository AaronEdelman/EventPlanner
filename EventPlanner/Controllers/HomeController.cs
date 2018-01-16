using EventPlanner.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserHomeModel = new HomeViewModel();
                var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                foreach (ApplicationUser user in _context.Users)
                {
                    if (user.Id == currentUserId)
                    {
                        UserHomeModel.User = user;
                        var roles = (from x in _context.Roles where x.Id == currentUserId select x.Name);
                        foreach(string roleName in roles)
                        {
                            UserHomeModel.Role = roleName;
                        }
                    }
                }
                
                return View(UserHomeModel);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}