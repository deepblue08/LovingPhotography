using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoManagement.Models;

namespace PhotoManagement.Controllers
{
    public class AdminController : Controller
    {
        private PhotoManagementContext db = new PhotoManagementContext();

        //
        // GET: /Admin/Login

        public ActionResult Login()
        {
            return View();
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                Session["UserId"] = client.ClientId;
                Session["Name"] = client.ClientName;
                return RedirectToAction("Create","User");
            }
            return View(client);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        //Get /Admin/AddPhoto/5

        public ActionResult AddPhoto(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            Session["UserId"] = client.ClientId;
            Session["Name"] = client.ClientName;
            return RedirectToAction("Create", "User");
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Client client)
        {
            var user = db.Clients
            .Where(u => u.UserName == client.UserName)
            .FirstOrDefault();
            if (user!=null)
            {
                if (user.Password == client.Password)
                {
                    Session["Name"] = user.ClientName;
                    Session["UserId"] = user.ClientId;
                    if (user.ClientName == "Administrator")
                    {
                        Session["Admin"] = true;
                        return RedirectToAction("Index", "Admin");
                    }
                    
                    else
                    {
                        Session["Admin"] = false;
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ViewBag.Title = "Login failed, Password was entered incorrectly";
                    return View();
                }
            }
            else
            {
                ViewBag.Title = "Login failed, could not find User Name";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}