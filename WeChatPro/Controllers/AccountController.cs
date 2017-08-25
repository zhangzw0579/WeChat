using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatPro.Models;
using WeChatPro.App.Admin;
using WeChatPro.App.Common;

namespace WeChatPro.Controllers
{
    public class AccountController : BaseController
    {
        private DBEntities db = new DBEntities();
        // GET: Account
        public ActionResult Index()
        {
            AccountModel account = new AccountModel();
            account.Id = "admin";
            account.TrueName = "admin";
            Session["Account"] = account;
            return View();
        }


        public JsonResult Login(string UserName, string Password, string Code)
        {

            if (Session["Code"] == null)
                return Json(JsonHandler.CreateMessage(0, "请重新刷新验证码"), JsonRequestBehavior.AllowGet);

            if (Session["Code"].ToString().ToLower() != Code.ToLower())
                return Json(JsonHandler.CreateMessage(0, "验证码错误"), JsonRequestBehavior.AllowGet);

            string psd = ValueConvert.MD5(Password);

            SysUser user = db.SysUser.SingleOrDefault(a => a.UserName == UserName && a.Password == psd);


            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }
            else if (!Convert.ToBoolean(user.State))//被禁用
            {
                return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"), JsonRequestBehavior.AllowGet);
            }

            AccountModel account = new AccountModel();
            account.Id = user.Id;
            account.TrueName = user.TrueName;
            Session["Account"] = account;

            return Json(JsonHandler.CreateMessage(1, ""), JsonRequestBehavior.AllowGet);

        }


    }
}