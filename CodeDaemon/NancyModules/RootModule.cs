using CodeDaemon.Fake;
using CodeDaemon.Hubs;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDaemon.NancyModules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = x =>
            {
                return "CodeDaemon ON!!!";
            };
        }
    }

    public class CmdExecModule : NancyModule
    {
        public CmdExecModule()
        {
            Post["/CmdExec"] = x =>
            {
                var miocomando = this.Request.Form.MyCommand;
                MemoryPersistenceLayer.Instance.Status = DaemonStatus.Running;

                Command.LongRunning();
                MemoryPersistenceLayer.Instance.Status = DaemonStatus.Terminated;
                return "Eseguito!!!";
            };

            Get["/CmdExec"] = x =>
            {
                ThreadPool.QueueUserWorkItem((i) => Command.LongRunning());
                NotifyHub notifyHub = new NotifyHub();

                notifyHub.SendToClient("messaggio dal server");

                return "Eseguito!!!";
            };
        }
    }
}
