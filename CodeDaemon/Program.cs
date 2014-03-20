using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CodeDaemon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //Install service
                if (args[0].Trim().ToLower() == "/i")
                {
                    Console.WriteLine("Installazione in corso...");
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { "/i", Assembly.GetExecutingAssembly().Location });
                    Console.WriteLine("Installato correttamante!");
                }

                //Uninstall service                 
                else if (args[0].Trim().ToLower() == "/u")
                {
                    Console.WriteLine("Disinstallazione in corso...");
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                    Console.WriteLine("Disinstallato correttamante!");
                }
            }
            else
            {
                using(var implementation = new ServiceImplementation())
                {
                    // if started from console, file explorer, etc, run as console app.
                    if (Environment.UserInteractive)
                    {
                        DaemonConsole.Run(args, implementation);
                    }
    
                    // otherwise run as a windows service
                    else
                    {
                        ServiceBase.Run(new CodeDaemonService(implementation));
                    }
                }
            }

            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new CodeDaemonService() };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
    }
}
