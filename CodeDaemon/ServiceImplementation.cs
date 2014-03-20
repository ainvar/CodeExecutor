using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Net;
using Microsoft.AspNet.SignalR;
using System.Web.Http;
using Owin;
using Microsoft.Owin.Cors;

namespace CodeDaemon
{
    public class ServiceImplementation : IWindowsService
    {
        public void SignalRConfig(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJSONP = true
                };

                map.RunSignalR(hubConfiguration);
            });
        } 

        public void OnStart(string[] args)
        {
            //var webApp = WebApp.Start<SignalRStartup>("http://localhost:999/");
            
            var options = new StartOptions();
            options.Urls.Add("http://localhost:999/");
            //using (WebApp.Start(options, (IAppBuilder builder) =>
            //{
            //    builder.Properties["host.AppName"] = "CodeDaemon";
            //    SignalRConfig(builder);
            //    builder.UseNancy();
            //}))
            //{
                
            //}


            WebApp.Start(options, (IAppBuilder builder) =>
            {
                builder.Properties["host.AppName"] = "CodeDaemon";
                SignalRConfig(builder);
                builder.UseNancy();
            });

            // start del servizio
            //new XSockets.Windows.Service.Host.Instance();

            //Console.ReadKey();
            
        }
 
        public void OnStop()
        {
        }
 
        public void OnPause()
        {
        }
 
        public void OnContinue()
        {
        }

        public void OnShutdown()
        {
        }

        public void Dispose()
        {
        }
    }
}
