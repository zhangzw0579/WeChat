using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeChatPro.Startup))]
namespace WeChatPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
