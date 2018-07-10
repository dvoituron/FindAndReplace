using System;
using System.Collections.Generic;
using System.Text;

namespace FindAndReplace
{
    public class Arguments
    {
        public Arguments(string[] args)
        {
            var cmdLine = new CommandLine(args);

            // Read arguments
            this.IsDemoMode = cmdLine.ContainsKey("Demo", "d");
            this.Find = cmdLine.GetValue("Find", "f");
            this.Replace = cmdLine.GetValue("Replace", "r") ?? String.Empty;
            this.Pattern = cmdLine.GetValue("Pattern", "p") ?? "*.*";
            this.BaseFolder = cmdLine.GetValue("Base", "b") ?? System.IO.Directory.GetCurrentDirectory();

            if (!this.BaseFolder.EndsWith("\\")) this.BaseFolder += "\\";

            // Validation
            this.Validate();
        }

        private void Validate()
        {
            if (this.Find.IsNullOrEmpty())
            {
                throw new ArgumentException("The 'Find' argument must be set.");
            }
        }

        public bool IsDemoMode { get; set; }
        public string Find { get; private set; }
        public string Replace { get; private set; }
        public string Pattern { get; private set; }
        public string BaseFolder { get; private set; }
    }

}
