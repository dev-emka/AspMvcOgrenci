using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciMvcAsp.Models.EntityFramework;

namespace OgrenciMvcAsp.Controllers
{
    public class KuluplerController : Controller
    {
        // GET: Kulupler
        DbMvcOkulEntities db=new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var kulupler=db.TblKulupler.ToList();
            return View(kulupler);
        }
        [HttpGet]
        public ActionResult YeniKulup() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKulup(TblKulupler P)
        {
            db.TblKulupler.Add(P);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int ID)
        {
            var klp = db.TblKulupler.Find(ID);
            db.TblKulupler.Remove(klp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KulupGetir(int ID)
        {
            var kulup = db.TblKulupler.Find(ID);
            return View("KulupGetir",kulup);
        }
        public ActionResult Guncelle(TblKulupler P)
        {
            var klp = db.TblKulupler.Find(P.KULUPID);
            klp.KULUPAD= P.KULUPAD;
            db.SaveChanges();
            return RedirectToAction("Index","Kulupler");
        }
    }
}