using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoManagement.Models;

namespace PhotoManagement.Controllers
{
    public class UserController : Controller
    {
        private PhotoManagementContext db = new PhotoManagementContext();

        //
        // GET: /User/Index

        public ActionResult Index()
        {
                int UserId = (int)Session["UserId"];
                var Photos = db.Photos
                            .Where(Photo => Photo.ClientId == UserId)
                            .ToList();
                if ((bool)Session["Admin"] == true) return PartialView("Index", Photos);
                else return View(Photos);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Add(photo);
                db.SaveChanges();
                // File upload occurs now
                if (!Directory.Exists("~/App_Data/" + photo.ClientId))
                {
                    Directory.CreateDirectory(Server.MapPath("~/App_Data/") + photo.ClientId);
                }
                var FileExt = Path.GetExtension(photo.File.FileName);
                var FilePath = Path.Combine(Server.MapPath("~/App_Data/" + photo.ClientId), photo.PhotoId.ToString()) + FileExt;
                photo.File.SaveAs(FilePath);
                return RedirectToAction("Create");
            }
            else return View();
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}