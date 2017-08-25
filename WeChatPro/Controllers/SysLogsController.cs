using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeChatPro.App.Admin;
using WeChatPro.App.Common;
using WeChatPro.Models;

namespace WeChatPro.Controllers
{
    public class SysLogsController : BaseController
    {
        private DBEntities db = new DBEntities();

        // GET: SysLogs
        public ActionResult Index()
        {
            return View(db.SysLog.ToList());
        }




        public JsonResult GetList(GridPager pager, string queryStr)
        {



 
            List<SysLog> query = null;
           // IQueryable<SysLog> list = db.SysLog.AsQueryable();

            List<SysLog> list = db.SysLog.ToList();


            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr)).ToList();
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

           // query = LinqHelper.DataSorting(query, pager.sort, pager.order);

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysLog()
                        {

                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime

                        }).ToArray()

            };

            return Json(json);
        }




        // GET: SysLogs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLog.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // GET: SysLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysLogs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Operator,Message,Result,Type,Module,CreateTime")] SysLog sysLog)
        {
            if (ModelState.IsValid)
            {
                db.SysLog.Add(sysLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysLog);
        }

        // GET: SysLogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLog.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // POST: SysLogs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Operator,Message,Result,Type,Module,CreateTime")] SysLog sysLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysLog).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysLog);
        }

        // GET: SysLogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLog.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // POST: SysLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SysLog sysLog = db.SysLog.Find(id);
            db.SysLog.Remove(sysLog);
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
