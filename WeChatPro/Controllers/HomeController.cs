using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatPro.Models;
using WeChatPro.App.Admin;

namespace WeChatPro.Controllers
{
    public class HomeController : BaseController
    {
        private DBEntities db = new DBEntities();
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

            // Convert.ToInt16("dddd"); 

            if (Session["Account"] != null)
            {

                AccountModel account = (AccountModel)Session["Account"];
                // List<SysModule> menus = homeBLL.GetMenuByPersonId(account.Id, id);

                var menus =
                 (
                     from m in db.SysModule
                     join rl in db.SysRight
                     on m.Id equals rl.ModuleId
                     join r in
                         (from r in db.SysRole
                          from u in r.SysUser
                          where u.Id == account.Id
                          select r)
                     on rl.RoleId equals r.Id
                     where rl.Rightflag == true
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
            }
            else {

                return Json("0", JsonRequestBehavior.AllowGet);

            }



        }

    }
}