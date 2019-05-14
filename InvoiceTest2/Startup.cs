using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceTest2.Startup))]
namespace InvoiceTest2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
