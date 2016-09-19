using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using SunIQ.Models;

namespace SunIQ.Controllers
{
    [Authorize]
    public class Calculation_SetupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calculation_Setup
        public ActionResult Index()
        {
            return View(db.Calculation_Setup.ToList());
        }

        // GET: Calculation_Setup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation_Setup calculation_Setup = db.Calculation_Setup.Find(id);
            if (calculation_Setup == null)
            {
                return HttpNotFound();
            }
            return View(calculation_Setup);
        }

        // GET: Calculation_Setup/Create
        public ActionResult Create()
        {
            Calculation_Setup c = new Calculation_Setup();
            return View(c);
        }

        // POST: Calculation_Setup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalculationId,Setup_Name,Rated_Power,Efficiency,Efficiency_Plus_Minus")] Calculation_Setup calculation_Setup)
        {
            if (ModelState.IsValid)
            {
                db.Calculation_Setup.Add(calculation_Setup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calculation_Setup);
        }

        // GET: Calculation_Setup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation_Setup calculation_Setup = db.Calculation_Setup.Find(id);
            if (calculation_Setup == null)
            {
                return HttpNotFound();
            }
            return View(calculation_Setup);
        }

        // POST: Calculation_Setup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalculationId,Setup_Name,Rated_Power,Efficiency,Efficiency_Plus_Minus")] Calculation_Setup calculation_Setup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calculation_Setup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calculation_Setup);
        }

        // GET: Calculation_Setup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation_Setup calculation_Setup = db.Calculation_Setup.Find(id);
            if (calculation_Setup == null)
            {
                return HttpNotFound();
            }
            return View(calculation_Setup);
        }

        // POST: Calculation_Setup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calculation_Setup calculation_Setup = db.Calculation_Setup.Find(id);
            db.Calculation_Setup.Remove(calculation_Setup);
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
