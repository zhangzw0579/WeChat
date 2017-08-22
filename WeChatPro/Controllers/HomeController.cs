using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatPro.Models;

namespace WeChatPro.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {


            var menus =
                (
                    from m in db.SysModule
                    where m.ParentId == id
                    where m.Id != "0"
                    select m
                          ).Distinct().OrderBy(a => a.Sort).ToList();



            var jsonData = (
                    from m in menus
                    select new
                    {
                        id = m.Id,
                        text = m.Name,
                        value = m.Url,
                        showcheck = false,
                        complete = false,
                        isexpand = false,
                        checkstate = 0,
                        hasChildren = m.IsLast ? false : true,
                        Icon = m.Iconic
                    }
                ).ToArray();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
            //return null;
        }

    }
}