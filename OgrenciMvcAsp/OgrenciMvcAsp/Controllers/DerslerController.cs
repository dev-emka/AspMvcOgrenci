using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciMvcAsp.Models.EntityFramework;

namespace OgrenciMvcAsp.Controllers
{
    public class DerslerController : Controller
    {
        // GET: Dersler
        DbMvcOkulEntities db=new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var dersler=db.TblDersler.ToList();
            return View(dersler);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(TblDersler P)
        {
            db.TblDersler.Add(P);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int ID)
        {
            var ders = db.TblDersler.Find(ID);
            db.TblDersler.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DersGetir(int ID)
        {
            var ders = db.TblDersler.Find(ID);

            return View("DersGetir",ders);
        }
        public ActionResult Guncelle(TblDersler p)
        {
            var ders = db.TblDersler.Find(p.DERSID);
            ders.DERSAD = p.DERSAD;
            db.SaveChanges();
            return RedirectToAction("Index","Dersler");
        }
    }
}