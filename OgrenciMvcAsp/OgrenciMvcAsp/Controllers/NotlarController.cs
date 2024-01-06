using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciMvcAsp.Models.EntityFramework;
using OgrenciMvcAsp.Models;

namespace OgrenciMvcAsp.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db=new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var notlar=db.TblNotlar.ToList();
            return View(notlar);
        }

        [HttpGet]
        public ActionResult YeniNot() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniNot(TblNotlar P)
        {
            db.TblNotlar.Add(P);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int ID)
        {
            var not=db.TblNotlar.Find(ID);
            return View("NotGetir",not);
        }

        [HttpPost]
        public ActionResult NotGetir(Class1 class1,TblNotlar p,int sinav1=0,int sinav2=0,int sinav3=0,int proje=0)
        {
            if (class1.islem == "HESAPLA")
            {
                int ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
                ViewBag.ort=ortalama;

            }
            if(class1.islem == "GUNCELLE")
            {
                var not = db.TblNotlar.Find(p.NOTID);
                not.SINAV1 = p.SINAV1;
                not.SINAV2 = p.SINAV2;
                not.SINAV3 = p.SINAV3;
                not.PROJE = p.PROJE;
                not.ORTALAMA=(p.SINAV1 + p.SINAV2+p.SINAV3+p.PROJE)/4;
                not.DURUM=(not.ORTALAMA>=50)? true : false;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
            return View();
        }
    }
}