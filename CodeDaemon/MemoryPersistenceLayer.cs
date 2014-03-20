using CodeDaemon.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using ROSlyn = Roslyn.Scripting;
using ServiceStack;
using Roslyn.Compilers.CSharp;


namespace CodeDaemon
{
    public enum DaemonStatus
    {
        Untouched,
        PreRun,
        Motionless,
        Running,
        Terminated,
        Stopped,
        Error
        
    }

    public sealed class MemoryPersistenceLayer
    {
        private static readonly MemoryPersistenceLayer instance = new MemoryPersistenceLayer();
        private DaemonStatus _status = DaemonStatus.Running;
        private ROSlyn.Session _codeSession;
        private ROSlyn.CSharp.ScriptEngine _scriptEngine;
        //private ROSlyn.CSharp.ScriptEngine _scriptEngine;

        static MemoryPersistenceLayer()
	    {

	    }

        private MemoryPersistenceLayer()
        {
            _scriptEngine = new ROSlyn.CSharp.ScriptEngine();

            _scriptEngine.AddReference("System");
            _scriptEngine.AddReference("System.Core");
            _scriptEngine.AddReference("System.Linq");
            _scriptEngine.AddReference("System.Xml");
            _scriptEngine.AddReference("System.Xml.Linq");
            _scriptEngine.AddReference("System.Data");
            _scriptEngine.AddReference("System.Data.DataSetExtensions");

            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/ServiceStack.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/ServiceStack.Client.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/ServiceStack.Common.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/ServiceStack.Interfaces.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/ServiceStack.Text.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/Newtonsoft.Json.dll");
            _scriptEngine.AddReference(_scriptEngine.BaseDirectory + "/Sprache.dll");

            _scriptEngine.AddReference("System.IO");
            _scriptEngine.AddReference("System.Net.Http");
            _scriptEngine.AddReference("System.Reflection");
            _scriptEngine.AddReference("System.Management");

            _codeSession = _scriptEngine.CreateSession();
            // direttive using
            _codeSession.Execute(@" using System;
                                    using System.Collections.Generic;
                                    using System.Linq;
                                    using System.Text;
                                    using System.Threading.Tasks;
                                    using System.Threading;
                                    using Newtonsoft.Json;
                                    using System.IO;
                                    using System.Reflection;
                                    using ServiceStack;     

                                    using System.Data.SqlClient;
                                    using System.Data;
                                    using System.Data.Common;         
            ");

        }

        public static MemoryPersistenceLayer Instance
        {
            get
            {
                return instance;
            }
        }

        public DaemonStatus Status
        {
            get 
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public ROSlyn.Session PersistenceRoslynSession
        {
            get 
            {
                return _codeSession;
            }
        }

        public void BroadcastMessageToAllClient(string message)
        {
            NotifyHub notifyHub = new NotifyHub();

            notifyHub.SendToClient(message);
        }

    }
}
