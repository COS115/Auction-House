using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auction.Models;
using System.Timers;
using System.Web.Security;
namespace Auction.Controllers
{
    public class AuctController : Controller
    {
        private ItemDBContext db = new ItemDBContext();

        // GET: /Auct/
        public ActionResult Index()
        {
            return View(db.Aucts.ToList());
            ViewBag.remaining = new List<DateTime>();
        }

        // GET: /Auct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auct auct = db.Aucts.Find(id);
            if (auct == null)
            {
                return HttpNotFound();
            }
            return View(auct);
        }

        // GET: /Auct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Auct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Category,Price,Rating,startTime")] Auct auct)
        {
            if (ModelState.IsValid)
            {
                db.Aucts.Add(auct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auct);
        }

        // GET: /Auct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auct auct = db.Aucts.Find(id);
            if (auct == null)
            {
                return HttpNotFound();
            }
            return View(auct);
        }
        public ActionResult Bid(int? id)
        {
            var it = from b in db.Aucts
                     where b.ID == id
                     select b;
            var itm = it.FirstOrDefault();
            Auct au = new Auct();
            au.ID = itm.ID;
            au.Name = itm.Name;
            au.Category = itm.Category;
            au.Price = itm.Price;
            au.Rating = itm.Rating;
            au.startTime = itm.startTime;
            au.highestBid = itm.highestBid;
            au.highestUser = itm.highestUser;
            return View(au);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(Auct act)
        {
            //Console.WriteLine(act.Name); 
            var it = from a in db.Aucts
                     where a.ID == act.ID
                     select a;
            var item = it.FirstOrDefault();
            item.highestBid = act.highestBid;
            db.SaveChanges();
            /*aucti.ID = item.ID;
            aucti.Name = item.Name;
            aucti.Category = item.Category;
            aucti.Price = item.Price;
            aucti.Rating = item.Rating;
            aucti.startTime = item.startTime;
            aucti.highestBid = act.highestBid;
            int uId = 3;
            aucti.highestUser = uId;
            db.Aucts.Remove(item);
            db.Aucts.Add(aucti);
            db.SaveChanges();*/
            /*if (ModelState.IsValid)
            {
                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();
            }*/
            return View(item);
        }
        // POST: /Auct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Category,Price,Rating,startTime")] Auct auct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auct);
        }

        // GET: /Auct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auct auct = db.Aucts.Find(id);
            if (auct == null)
            {
                return HttpNotFound();
            }
            return View(auct);
        }

        // POST: /Auct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auct auct = db.Aucts.Find(id);
            db.Aucts.Remove(auct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult addAuction(int id)
        {
            var lstone = new List<Item>();
            var items = from d in db.Items
                        where d.ID == id
                        select d;
            lstone.AddRange(items.Distinct());
            foreach (var it in lstone)
            {
                Auct ac = new Auct();
                ac.ID = it.ID;
                ac.Name = it.Name;
                ac.Price = it.Price;
                ac.Rating = it.Rating;
                ac.Category = it.Category;
                ac.startTime = DateTime.Now.AddHours(1);
                ac.highestUser = 0;
                ac.highestBid = 30;
                db.Aucts.Add(ac);
                db.SaveChanges();
            }
            return View("~/Views/Home/Index.cshtml");
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
