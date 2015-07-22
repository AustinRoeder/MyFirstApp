using GitFirstApp.Models;
using GitFirstApp.Models.My_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace GitFirstApp.Controllers
{
    public class CommentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(Comment comment)
        {
            BlogPost post = db.Posts.Find(comment.PostID);
            if (ModelState.IsValid)
            {
                comment.AuthorID = User.Identity.GetUserId();
                comment.Created = DateTimeOffset.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { slug = post.Slug });
            }
            return RedirectToAction("Index","BlogPosts");
        }

        // POST: BlogPosts/Delete/5
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            BlogPost post = db.Posts.Find(comment.PostID);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { slug = post.Slug });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Updated,Body,PostID")] Comment comment)
        {
            BlogPost post = db.Posts.Find(comment.PostID);
            if (ModelState.IsValid)
            {
                db.Comments.Attach(comment);
                db.Entry(comment).Property("Body").IsModified = true;
                comment.Updated = DateTimeOffset.Now;
                db.Entry(comment).Property("Updated").IsModified = true;
                db.SaveChanges();
            }
            return RedirectToAction("Details", "BlogPosts", new { slug = post.Slug });
        }
    }
}