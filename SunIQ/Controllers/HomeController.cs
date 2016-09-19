using IdentitySample.Models;
using SunIQ.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Index(int? id)
        {
            
            
            InverterandRange ir = new InverterandRange();
            ir.inv = db.Inverters.ToList();
            ir.calc = db.Calculation_Setup.Find(id);
            if (id == null || ir.calc == null)
                ir.calc = db.Calculation_Setup.First();

            var xaxis = new ArrayList();

            var yaxis = new ArrayList();

            Dictionary<String, double> xyaxis = new Dictionary<string, double>();

            foreach (var item in ir.inv)
            foreach(var j in ir.calc.Setup_percentage_Ranges.OrderBy(r=>r.Percentage))
            {

                    try
                    {

                        var jcec = item.inverter_files_datas.
                                Where(i => i.ETUK > (ir.calc.Efficiency - ir.calc.Efficiency_Plus_Minus)
                                && i.ETUK < (ir.calc.Efficiency + ir.calc.Efficiency_Plus_Minus)
                                && i.STPK > (ir.calc.Rated_Power * j.Min_Percentage_Range / 100)
                                && i.STPK < (ir.calc.Rated_Power * j.Max_Percentage_Range / 100)
                            ).Average(avg => avg.SFPK / avg.ETPK * 100);

                        if (xyaxis.ContainsKey(j.Percentage + "%"))
                            xyaxis[j.Percentage + "%"] += Math.Round(jcec / ir.inv.Count(),2);
                        else
                            xyaxis[j.Percentage + "%"] = jcec/ir.inv.Count();
                    }
                    catch (Exception e) { }

            }



            //            return View(ir);
            ViewBag.xyaxis = xyaxis;
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
