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
    public class Setup_Percentage_RangeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Setup_Percentage_Range
        public ActionResult Index()
        {
            var setup_Percentage_Range = db.Setup_Percentage_Range.Include(s => s.Calculation_Setup);
            return View(setup_Percentage_Range.ToList());
        }

        // GET: Setup_Percentage_Range/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setup_Percentage_Range setup_Percentage_Range = db.Setup_Percentage_Range.Find(id);
            if (setup_Percentage_Range == null)
            {
                return HttpNotFound();
            }
            return View(setup_Percentage_Range);
        }

        // GET: Setup_Percentage_Range/Create
        public ActionResult Create()
        {
            ViewBag.CalculationId = new SelectList(db.Calculation_Setup, "CalculationId", "Setup_Name");
            return View();
        }

        // POST: Setup_Percentage_Range/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Setup_PercentageId,CalculationId,Percentage,Min_Percentage_Range,Max_Percentage_Range,Weightage")] Setup_Percentage_Range setup_Percentage_Range)
        {
            if (ModelState.IsValid)
            {
                db.Setup_Percentage_Range.Add(setup_Percentage_Range);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CalculationId = new SelectList(db.Calculation_Setup, "CalculationId", "Setup_Name", setup_Percentage_Range.CalculationId);
            return View(setup_Percentage_Range);
        }

        // GET: Setup_Percentage_Range/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setup_Percentage_Range setup_Percentage_Range = db.Setup_Percentage_Range.Find(id);
            if (setup_Percentage_Range == null)
            {
                return HttpNotFound();
            }
            ViewBag.CalculationId = new SelectList(db.Calculation_Setup, "CalculationId", "Setup_Name", setup_Percentage_Range.CalculationId);
            return View(setup_Percentage_Range);
        }

        // POST: Setup_Percentage_Range/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Setup_PercentageId,CalculationId,Percentage,Min_Percentage_Range,Max_Percentage_Range,Weightage")] Setup_Percentage_Range setup_Percentage_Range)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setup_Percentage_Range).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CalculationId = new SelectList(db.Calculation_Setup, "CalculationId", "Setup_Name", setup_Percentage_Range.CalculationId);
            return View(setup_Percentage_Range);
        }

        // GET: Setup_Percentage_Range/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setup_Percentage_Range setup_Percentage_Range = db.Setup_Percentage_Range.Find(id);
            if (setup_Percentage_Range == null)
            {
                return HttpNotFound();
            }
            return View(setup_Percentage_Range);
        }

        // POST: Setup_Percentage_Range/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Setup_Percentage_Range setup_Percentage_Range = db.Setup_Percentage_Range.Find(id);
            db.Setup_Percentage_Range.Remove(setup_Percentage_Range);
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
