using Microsoft.Owin;
using Owin;
using BlogDimitar.Migrations;
using BlogDimitar.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(BlogDimitar.Startup))]
namespace BlogDimitar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
