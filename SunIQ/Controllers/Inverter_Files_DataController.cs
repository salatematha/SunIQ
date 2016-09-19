using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IdentitySample.Models;
using SunIQ.Models;
using PagedList;

namespace SunIQ.Controllers
{
    public class Inverter_Files_DataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Autocomplete(string term)
        {
            var inverter_Files_Datas = db.Inverter_Files_Datas.Where(r => term == null || r.Inverter.Serial_Number.StartsWith(term)).Select(r => new { label = r.Inverter.Serial_Number }).Distinct().Take(5) ;

            return Json(inverter_Files_Datas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test()
        {

            var inverter_Files_Datas = db.Inverter_Files_Datas.Select(r => new { label = r.Inverter.Serial_Number, etuk=r.ETUK, etpk = r.ETPK, stuk=r.STUK  }).Distinct().Take(5);

            return Json(inverter_Files_Datas, JsonRequestBehavior.AllowGet);
        }

        // GET: Inverter_Files_Data
        public ActionResult Index(string searchTerm = null, int page=1)
        {
            ViewBag.CurrentFilter = searchTerm;
            var inverter_Files_Datas = db.Inverter_Files_Datas.Where(r=>searchTerm==null || r.Inverter.Serial_Number.StartsWith(searchTerm)).OrderBy(r=>r.Serial_No);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Inverter_Data", inverter_Files_Datas.ToPagedList(page,100));
            }
            return View(inverter_Files_Datas.ToPagedList(page, 100));
        }

        // GET: Inverter_Files_Data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter_Files_Data inverter_Files_Data = db.Inverter_Files_Datas.Find(id);
            if (inverter_Files_Data == null)
            {
                return HttpNotFound();
            }
            return View(inverter_Files_Data);
        }

        // GET: Inverter_Files_Data/Create
        public ActionResult Create()
        {
            ViewBag.InverterId = new SelectList(db.Inverters, "InverterId", "Serial_Number");
            return View();
        }

        // POST: Inverter_Files_Data/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InverterId,Serial_No,ETUK,ETPK,STUK,STPK,SFPK")] Inverter_Files_Data inverter_Files_Data)
        {
            if (ModelState.IsValid)
            {
                db.Inverter_Files_Datas.Add(inverter_Files_Data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InverterId = new SelectList(db.Inverters, "InverterId", "Serial_Number", inverter_Files_Data.InverterId);
            return View(inverter_Files_Data);
        }

        // GET: Inverter_Files_Data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter_Files_Data inverter_Files_Data = db.Inverter_Files_Datas.Find(id);
            if (inverter_Files_Data == null)
            {
                return HttpNotFound();
            }
            ViewBag.InverterId = new SelectList(db.Inverters, "InverterId", "Serial_Number", inverter_Files_Data.InverterId);
            return View(inverter_Files_Data);
        }

        // POST: Inverter_Files_Data/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InverterId,Serial_No,ETUK,ETPK,STUK,STPK,SFPK")] Inverter_Files_Data inverter_Files_Data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inverter_Files_Data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InverterId = new SelectList(db.Inverters, "InverterId", "Serial_Number", inverter_Files_Data.InverterId);
            return View(inverter_Files_Data);
        }

        // GET: Inverter_Files_Data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter_Files_Data inverter_Files_Data = db.Inverter_Files_Datas.Find(id);
            if (inverter_Files_Data == null)
            {
                return HttpNotFound();
            }
            return View(inverter_Files_Data);
        }

        // POST: Inverter_Files_Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inverter_Files_Data inverter_Files_Data = db.Inverter_Files_Datas.Find(id);
            db.Inverter_Files_Datas.Remove(inverter_Files_Data);
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
