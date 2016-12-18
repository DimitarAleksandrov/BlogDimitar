using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogDimitar.Startup))]
namespace BlogDimitar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
