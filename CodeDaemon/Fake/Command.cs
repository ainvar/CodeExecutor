using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDaemon.Fake
{
    public class Command
    {
        public static void LongRunning()
        {
            for (var i = 0; i < 20000; ++i)
            {
                MemoryPersistenceLayer.Instance.BroadcastMessageToAllClient("ciclo n." + i.ToString());
                Thread.Sleep(200);
            }
        }
    }
}
