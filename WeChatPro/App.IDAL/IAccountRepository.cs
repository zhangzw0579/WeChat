using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChatPro.Models;

namespace WeChatPro.App.IDAL
{
    public interface IAccountRepository
    {
        SysUser Login(string username, string pwd);
    }
}
