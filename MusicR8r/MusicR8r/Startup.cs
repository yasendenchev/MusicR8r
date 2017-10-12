using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicR8r.Startup))]
namespace MusicR8r
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
