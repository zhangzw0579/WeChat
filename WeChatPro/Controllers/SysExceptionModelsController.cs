using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeChatPro.App.Common;
using WeChatPro.Models;
using WeChatPro.App.Admin;

namespace WeChatPro.Controllers
{
    public class SysExceptionModelsController : BaseController
    {
        private DBEntities db = new DBEntities();

        // GET: SysExceptionModels
        public ActionResult Index()
        {
            return View(db.SysException.ToList());
        }

        // GET: SysExceptionModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysException sysExceptionModel = db.SysException.Find(id);
            if (sysExceptionModel == null)
            {
                return HttpNotFound();
            }
            return View(sysExceptionModel);
        }

        // GET: SysExceptionModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysExceptionModels/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HelpLink,Message,Source,StackTrace,TargetSite,Data,CreateTime")] SysException sysExceptionModel)
        {
            if (ModelState.IsValid)
            {
                db.SysException.Add(sysExceptionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysExceptionModel);
        }


        public   void WriteException(Exception ex)
        {


            SysException sysE = new SysException();
            sysE.Id = Guid.NewGuid().ToString();
            sysE.HelpLink =ex.HelpLink;
            sysE.Message = ex.Message;
            sysE.Source = ex.Source;
            sysE.StackTrace = ex.StackTrace;
            sysE.TargetSite = ex.TargetSite.ToString();
            sysE.Data = ex.Data.ToString();
            sysE.CreateTime = DateTime.Now;


            if (ModelState.IsValid)
            {
                db.SysException.Add(sysE);
                db.SaveChanges();
               
            }

             
        }
        

        // GET: SysExceptionModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysException sysExceptionModel = db.SysException.Find(id);
            if (sysExceptionModel == null)
            {
                return HttpNotFound();
            }
            return View(sysExceptionModel);
        }

        // POST: SysExceptionModels/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HelpLink,Message,Source,StackTrace,TargetSite,Data,CreateTime")] SysException sysExceptionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysExceptionModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysExceptionModel);
        }

        // GET: SysExceptionModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysException sysExceptionModel = db.SysException.Find(id);
            if (sysExceptionModel == null)
            {
                return HttpNotFound();
            }
            return View(sysExceptionModel);
        }

        // POST: SysExceptionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SysException sysExceptionModel = db.SysException.Find(id);
            db.SysException.Remove(sysExceptionModel);
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




        public JsonResult GetList(GridPager pager, string queryStr)
        {


           


            List<SysException> query = null;
            // IQueryable<SysLog> list = db.SysLog.AsQueryable();
            List<SysException> list = db.SysException.ToList();


            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr)).ToList();
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }

            if (pager.order == "desc")
            {
                query = list.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }
            else
            {
                query = list.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }



            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysException()
                        {
                            Id = r.Id,
                            HelpLink = r.HelpLink,
                            Message = r.Message,
                            Source = r.Source,
                            StackTrace = r.StackTrace,
                            TargetSite = r.TargetSite,
                            Data = r.Data,
                            CreateTime = r.CreateTime
                        }).ToArray()

            };
            return Json(json);
        }


        public ActionResult Error()
        {

            BaseException ex = new BaseException();
            return View(ex);
        }

        

        
    }




}
