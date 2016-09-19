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
    public class InvertersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inverters
        public ActionResult Index(int?id)
        {
            InverterandRange ir = new InverterandRange();
            ir.inv = db.Inverters.ToList();
            ir.calc = db.Calculation_Setup.Find(id);
            if (id == null || ir.calc ==null)
                ir.calc = db.Calculation_Setup.First();

            return View(ir);
            //return View(db.Inverters.ToList());
        }

        // GET: Inverters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter inverter = db.Inverters.Find(id);
            if (inverter == null)
            {
                return HttpNotFound();
            }
            return View(inverter);
        }

        // GET: Inverters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inverters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InverterId,Serial_Number,File_Name,Last_Modified")] Inverter inverter, HttpPostedFileBase FileData = null)
        {
            if (ModelState.IsValid)
            {
                byte[] ltext = new byte[4];
                inverter.Last_Modified = DateTime.Now;
                if (FileData != null)
                {
                    ltext = new byte[FileData.ContentLength];
                    FileData.InputStream.Read(ltext, 0, ltext.Length);
                    inverter.File_Name = FileData.FileName;
                }
                if (inverter.inverter_files_datas!=null)
                        inverter.inverter_files_datas.Clear();
                db.Inverters.Add(inverter);
                 db.SaveChanges();



                String ltextextract = System.Text.Encoding.UTF8.GetString(ltext);
                string[] lines = ltextextract.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                int l_insertNext3 = 0;
                String l_elementorsigma = "";
                Console.WriteLine(lines.Length);
               // photo.ImageData = "";
                for (int i = 38270; i < lines.Length && i < 63062; i++)
                {
                    bool l_docalc = false;
                    bool l_doreplace = false;
                    if ((i - 38270) % 35 == 0)
                    {
                        l_docalc = true;
                        l_doreplace = true;
                    }
                    else if (l_insertNext3 > 1)
                    {
                        l_doreplace = true;
                    }
                    String l_insertStr = "";

                    if (l_doreplace)
                        l_insertStr = lines[i].Replace("\"", "");

                    if (l_docalc)
                        if (lines[i].Contains("Element4"))
                        {
                            l_elementorsigma = "";
                            l_insertNext3 = 12;
                        }
                    if (l_insertNext3 > 1)
                    {
                        l_insertNext3--;
                        if (l_insertNext3 == 9 || l_insertNext3 == 7 || l_insertNext3 == 1 || l_insertNext3 == 3)
                        {
                            if (l_insertNext3 != 1)
                                l_insertStr = l_insertStr.Substring(0, l_insertStr.IndexOf(",", l_insertStr.IndexOf(",", 3) + 1));
                            l_elementorsigma = l_elementorsigma + l_insertStr;
                        }
                        if (l_insertNext3 < 2)
                        {
                            string tmp = l_elementorsigma.Replace(",P(k)", "");
                                tmp = tmp.Replace(",U(k)", "");
                                tmp = tmp.Replace("\"","") + "," + (i + 1);
                            string[] intarr = tmp.Split(',');
                            double a = Double.Parse(intarr[1]);
                            Inverter_Files_Data ex = new Inverter_Files_Data();
                            ex.InverterId = inverter.InverterId;
                            ex.ETUK = Double.Parse(intarr[1]);
                            ex.ETPK = Double.Parse(intarr[2]);
                            ex.STUK = Double.Parse(intarr[3]);
                            ex.STPK = Double.Parse(intarr[4]);
                            ex.SFPK = Double.Parse(intarr[5]);
                            ex.Serial_No = int.Parse(intarr[6]);
                            db.Inverter_Files_Datas.Add(ex);
                            //  Console.WriteLine(tmp);
                        }
                        // photo.ImageData += ":" + l_elementorsigma.Replace(",P(k)", "").Replace(",U(k)", "") + "," + (i + 1);

                    }
                }




                //ex.ETPK = 1;
                //ex.ETUK = 1;
                //ex.Serial_No = 1;
                //ex.InverterId = inverter.InverterId;


                //db.Inverter_Files_Datas.add(ex);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(inverter);
        }

        // GET: Inverters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter inverter = db.Inverters.Find(id);
            if (inverter == null)
            {
                return HttpNotFound();
            }
            return View(inverter);
        }

        // POST: Inverters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InverterId,Serial_Number,File_Name,Last_Modified")] Inverter inverter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inverter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inverter);
        }

        // GET: Inverters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inverter inverter = db.Inverters.Find(id);
            if (inverter == null)
            {
                return HttpNotFound();
            }
            return View(inverter);
        }

        // POST: Inverters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inverter inverter = db.Inverters.Find(id);
            db.Inverters.Remove(inverter);
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
