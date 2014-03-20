using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CodeDaemon
{
    // the new service base implementation
    public partial class CodeDaemonService : ServiceBase
    {
        private IWindowsService _implementer;
 
        // default constructor to satisfy builder
        internal CodeDaemonService()
        {
            InitializeComponent();
        }
 
        // public constructor to take in the implementation to delegate to
        public CodeDaemonService(IWindowsService implementation)
            : this()
        {
            _implementer = implementation;
        }
 
        // because all of these available overrides are protected, we can't
        // call them directly from our console harness, so instead we will
        // just delegate to the IWindowsService interface which is public.
        protected override void OnStart(string[] args)
        {
            _implementer.OnStart(args);
        }
 
        protected override void OnStop()
        {
            _implementer.OnStop();
        }
 
        protected override void OnPause()
        {
            _implementer.OnPause();
        }
 
        protected override void OnContinue()
        {
            _implementer.OnContinue();
        }
 
        protected override void OnShutdown()
        {
            _implementer.OnShutdown();
        }
    }
}
