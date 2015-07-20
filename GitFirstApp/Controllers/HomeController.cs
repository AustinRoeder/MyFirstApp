using GitFirstApp.Models.My_Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitFirstApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmailMessage contactMe)
        {
            if (!ModelState.IsValid) return View(contactMe);
            var emailer = new EmailService();
            ConfigurationManager.AppSettings["FromEmail"] = contactMe.Email;
            var mail = new IdentityMessage()
            {
                Subject = contactMe.Subject,
                Destination = ConfigurationManager.AppSettings["ContactEmail"],
                Body = "You have recieved a message: " + contactMe.Message
            };
            emailer.SendAsync(mail);

            TempData["MessageSent"] = "Your message has been successfully delievered.";

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
        public ActionResult Portfolio()
        {
            ViewBag.Message = "Welcome.";
            return View();
        }
        public ActionResult JavaScript()
        {
            ViewBag.Message = "JavaScript Exercises.";
            return View();
        }
    }
}