using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using NLua;

[assembly: Fiddler.RequiredVersion("2.0.0.0")]


namespace fiddlerrc
{
    public class FiddlerRc : IAutoTamper
    {

        private Lua lua;

        public FiddlerRc()
        {
            this.lua = new Lua();
        }

        public void AutoTamperRequestAfter(Session oSession)
        {
        }

        public void AutoTamperRequestBefore(Session oSession)
        {
        }

        public void AutoTamperResponseAfter(Session oSession)
        {
        }

        public void AutoTamperResponseBefore(Session oSession)
        {
        }

        public void OnBeforeReturningError(Session oSession)
        {
        }

        public void OnBeforeUnload()
        {
        }

        public void OnLoad()
        {
            FiddlerApplication.Log.LogString("fiddlerrc loaded");

            var rcPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fiddler2", "Scripts", "fiddler.lua");

            if (!File.Exists(rcPath))
            {
                FiddlerApplication.Log.LogString("Didn't find " + rcPath);
                return;
            }

            try
            {
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogString("Failed to load " + rcPath);
                FiddlerApplication.Log.LogString(ex.StackTrace);
            }
        }
    }
}
