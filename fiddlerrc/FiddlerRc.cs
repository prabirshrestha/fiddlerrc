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
            if (this.lua != null)
            {
                this.lua.Dispose();
                this.lua = null;
            }
        }

        public void OnLoad()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            if (this.lua != null)
            {
                this.lua.Dispose();
            }

            this.lua = new Lua();
            this.lua.NewTable("fiddler");

            var rcPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "fiddler.lua");
            if (!File.Exists(rcPath))
            {
                FiddlerApplication.Log.LogString("File not found: " + rcPath);
                return;
            }

            this.lua["fiddler.MYRC"] = rcPath;

            this.lua.RegisterFunction("print", FiddlerApplication.Log, typeof(Logger).GetMethod("LogString"));

            this.lua.DoString(@"
local oldPrint = print

function print( ... )
	-- redefine the print to use erc._log
	local arguments = {...}
	local printResult = ''
	local first = true
	for i,v in ipairs(arguments) do
		if not first then
			printResult = '\t' .. printResult
		end
		printResult = printResult .. tostring(v)
	end
	printResult = printResult
	oldPrint(printResult)
end

print('loading ' .. fiddler.MYRC)

dofile(fiddler.MYRC)

print('loaded ' .. fiddler.MYRC)

");
        }

        private void Log(string message)
        {
            FiddlerApplication.Log.LogString(message);
        }
    }
}
