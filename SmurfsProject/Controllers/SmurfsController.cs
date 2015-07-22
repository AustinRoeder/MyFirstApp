using SmurfsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmurfsProject.Controllers
{
    public class SmurfsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Smurfs
        public ActionResult Index()
        {
            return View(db.Smurfs);
        
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Smurf smurf)
        {
            if (ModelState.IsValid)
            {
                db.Smurfs.Add(smurf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smurf);
        }
    }
}