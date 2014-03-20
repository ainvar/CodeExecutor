using CodeDaemon.Hubs;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ROSlyn = Roslyn.Scripting;
using ServiceStack;
using Newtonsoft.Json;
using KatService.DTOs;


namespace CodeDaemon.NancyModules
{
    public class ReplModule : NancyModule
    {
        public ReplModule()
        {

            Post["/repljson"] = x =>
            {
                //var mycode = this.Context.Request.Query.Keys["code"];
                string myCode = Context.Request.Query["code"].ToString();

                try
                {
                    var codeFragment = JsonConvert.DeserializeObject<CodeFragment>(myCode);
                    var res = MemoryPersistenceLayer.Instance.PersistenceRoslynSession.Execute(codeFragment.Fragment);
                    return res.ToString();
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            };

            Get["/repl"] = x =>
            {
                //var mycode = this.Context.Request.Query.Keys["code"];
                string myCode = Context.Request.Query["code"].ToString();

                try
                {
                    var res = MemoryPersistenceLayer.Instance.PersistenceRoslynSession.Execute(myCode);

                    if (res == null)
                        return "code received without errors!";
                    else
                        return res.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            };

            Get["/Load"] = x =>
                {
                    string path = Context.Request.Query["path"].ToString();
                    if (string.IsNullOrEmpty(path))
                        return "nothing to do!";
                    else
                    {
                        try
                        {
                            MemoryPersistenceLayer.Instance.PersistenceRoslynSession.ExecuteFile(path);
                            return "File loaded!!";
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }

                    }
                };

        }
    }
}
