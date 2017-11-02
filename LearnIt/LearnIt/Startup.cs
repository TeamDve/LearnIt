using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnIt.Startup))]
namespace LearnIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);//dwqd
        }
    }
}
