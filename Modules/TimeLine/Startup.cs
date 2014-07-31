using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AIMS.Modules.TimeLine.Startup))]

namespace AIMS.Modules.TimeLine
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); 
        }
    }
}
