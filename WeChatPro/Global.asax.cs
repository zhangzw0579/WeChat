using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeChatPro.Controllers;
using WeChatPro.Models;

namespace WeChatPro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }



        /// <summary>
        /// 全局的异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            string s = HttpContext.Current.Request.Url.ToString();
            HttpServerUtility server = HttpContext.Current.Server;
            if (server.GetLastError() != null)
            {
               // Exception lastError = server.GetLastError().GetBaseException();
                Exception lastError = server.GetLastError();
                // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。

                //  ExceptionHandler.WriteException(lastError);
                 new SysExceptionModelsController().WriteException(lastError);

 


                Application["LastError"] = lastError;
                
                int statusCode = HttpContext.Current.Response.StatusCode;




                string exceptionOperator = "/SysExceptionModels/Error";
                try
                {
                    if (!String.IsNullOrEmpty(exceptionOperator))
                    {
                        exceptionOperator = new System.Web.UI.Control().ResolveUrl(exceptionOperator);
                        string url = string.Format("{0}?ErrorUrl={1}", exceptionOperator, server.UrlEncode(s));
                        string script = String.Format("<script language='javascript' type='text/javascript'>window.top.location='{0}';</script>", url);
                        Response.Write(script);
                     
                        Response.End();
                    }
                }
                catch { }
            }
        }
    }
}
