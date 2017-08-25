
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChatPro.Models;

namespace WeChatPro.App.IDAL
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        public SysUser Login(string username, string pwd)
        {
            using (DBEntities db = new DBEntities())
            {
                SysUser user = db.SysUser.SingleOrDefault(a => a.UserName == username && a.Password == pwd);
                return user;
            }
        }
        public void Dispose()
        {

        }
    }
}
