using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GitFirstApp.Models;
using GitFirstApp.Models.My_Models;
using System.Web.UI;

namespace GitFirstApp.Controllers
{

    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(string search, int? page)
        {
            var pagesize = 3;
            var skip = (page ?? 1) * pagesize - pagesize;
            if (search != null)
            {
                return View(db.Posts.Where(p => p.Title.Contains(search) || p.Slug.Contains(search) || p.Body.Contains(search) || p.Comments.Any(c => c.Body.Contains(search))).OrderByDescending(d => d.Created).Skip(skip).Take(pagesize).ToList());
            }
            ViewBag.ModelCount = db.Posts.Count();

            return View(db.Posts.OrderByDescending(d => d.Created).Skip(skip).Take(pagesize).ToList());
        }
       
        [Authorize(Roles="Admin")]
        public ActionResult AdminsIndex()
        {
            return View(db.Posts.ToList());
        }

        // GET: Blog/{slug}
        public ActionResult Details(string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Include(p=>p.Comments).FirstOrDefault(p=>p.Slug == slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var slug = StringUtilities.UrlFriendly(blogPost.Title);

                if (String.IsNullOrWhiteSpace(slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogPost);
                }
                if (db.Posts.Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blogPost);
                }
                else
                {
                    blogPost.Created = DateTimeOffset.Now;
                    blogPost.Slug = slug;

                    db.Posts.Add(blogPost);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5 
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Updated,Body,MediaURL")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(blogPost);
                db.Entry(blogPost).Property("Body").IsModified = true;
                blogPost.Updated = DateTimeOffset.Now;
                db.Entry(blogPost).Property("Updated").IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
