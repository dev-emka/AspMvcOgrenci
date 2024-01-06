using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciMvcAsp.Models.EntityFramework;

namespace OgrenciMvcAsp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrenciler = db.TblOgrenciler.ToList();
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenciEkle()
        {
            List<SelectListItem> degerler = (from i in db.TblKulupler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniOgrenciEkle(TblOgrenciler P)
        {
            var klp = db.TblKulupler.Where(m => m.KULUPID ==
            P.TblKulupler.KULUPID).FirstOrDefault();
            P.TblKulupler = klp;
            db.TblOgrenciler.Add(P);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int ID)
        {
            var ogr = db.TblOgrenciler.Find(ID);
            db.TblOgrenciler.Remove(ogr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OgrGetir(int ID)
        {
            var ogr = db.TblOgrenciler.Find(ID);
            List<SelectListItem> degerler = (from i in db.TblKulupler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("OgrGetir", ogr);
        }
        public ActionResult Guncelle(TblOgrenciler p)
        {
            var ogr = db.TblOgrenciler.Find(p.OGRID);
            ogr.OGRAD = p.OGRAD;
            ogr.OGRSOYAD= p.OGRSOYAD;
            ogr.OGRCİNSİYET= p.OGRCİNSİYET;
            ogr.OGRKULUP= p.OGRKULUP;
            ogr.OGRFOTO= p.OGRFOTO;
            db.SaveChanges();
            return RedirectToAction("Index","Default");
        }
    }
}