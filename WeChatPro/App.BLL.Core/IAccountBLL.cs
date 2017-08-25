using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChatPro.Models;

namespace WeChatPro.App.BLL
{
    public interface IAccountBLL
    {
        SysUser Login(string username, string pwd);
    }
}

