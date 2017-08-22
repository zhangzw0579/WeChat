using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeChatPro;
using WeChatPro.Models;

namespace WeChatPro.Controllers
{
    public class SysSamplesController : Controller
    {
        private Model1 db = new Model1();
       
        // GET: SysSamples
        public ActionResult Index()
        {
            return View(db.SysSample.ToList());
        }

        // GET: SysSamples/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysSample sysSample = db.SysSample.Find(id);
            if (sysSample == null)
            {
                return HttpNotFound();
            }
            return View(sysSample);
        }

        // GET: SysSamples/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysSamples/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Age,Bir,Photo,Note,CreateTime")] SysSample sysSample)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SysSample.Add(sysSample);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sysSample);
        //}

        [HttpPost]
        public JsonResult Create(SysSample model)
        {
           // if (ModelState.IsValid)
           //{
                db.SysSample.Add(model);
                if (db.SaveChanges() > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            else
            {

                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
          
        }



        // GET: SysSamples/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysSample sysSample = db.SysSample.Find(id);
            if (sysSample == null)
            {
                return HttpNotFound();
            }
            return View(sysSample);
        }

        // POST: SysSamples/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Bir,Photo,Note,CreateTime")] SysSample sysSample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysSample).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysSample);
        }

        // GET: SysSamples/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysSample sysSample = db.SysSample.Find(id);
            if (sysSample == null)
            {
                return HttpNotFound();
            }
            return View(sysSample);
        }

        // POST: SysSamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SysSample sysSample = db.SysSample.Find(id);
            db.SysSample.Remove(sysSample);
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

        [HttpPost]
        public JsonResult GetList()
        {
            List<SysSample> list = db.SysSample.ToList();
            var json = new
            {
                total = list.Count,
                rows = (from r in list
                        select new SysSample()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public string GetList1()
        {
            return "fdafdasjlfasfldsjfljdsa ";
        }
    }
}
