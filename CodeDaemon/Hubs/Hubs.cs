using CodeDaemon.Fake;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDaemon.Hubs
{
    public class ClientPushHub : Hub
    {
    }

    [HubName("NotifyHub")]
    public class NotifyHub : Hub
    {
        private IHubContext SRContext;

        public NotifyHub()
        {
            SRContext = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
        }

        [HubMethodName("sendMessage")]
        public void SendMessage(string message)
        {
            //Clients.All.addMessage(message);
            if (MemoryPersistenceLayer.Instance.Status == DaemonStatus.Untouched ||
                MemoryPersistenceLayer.Instance.Status == DaemonStatus.Terminated)
            {
                switch (message)
                {
                    case "a":
                        ThreadPool.QueueUserWorkItem((i) => Command.LongRunning());

                        //notifyHub.SendToClient("messaggio dal server");
                        break;
                    case "b":
                        break;
                    case "c":
                        break;
                    case "d":
                        break;

                }
            }
            else
            {
                
            }
        }

        public void SendToClient(string pushMsg)
        {
            SRContext.Clients.All.addMessage(pushMsg);
        }

        public override Task OnConnected()
        {
            var version = Context.QueryString["version"];
            if (version != "1.0")
            {
                Clients.Caller.notifyWrongVersion();
            }
            return base.OnConnected();
        }
    }
}
