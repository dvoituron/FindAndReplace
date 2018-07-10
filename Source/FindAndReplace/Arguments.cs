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
            this.Mode = cmdLine.GetValue("Mode", "m") ?? String.Empty;

            if (!this.BaseFolder.EndsWith("\\")) this.BaseFolder += "\\";

            // JSON Mode
            if (Mode.IsEqualTo("json"))
            {
                string key = this.Find;
                string value = this.Replace;
                this.Find = $"\"{key}\"(\\s)*:(\\s)*\".+\"";
                this.Replace = $"\"{key}\": \"{value}\"";
            }

            // Validation
            this.Validate();
        }

        private void Validate()
        {
            if (this.Find.IsNullOrEmpty())
            {
                throw new ArgumentException("The 'Find' argument must be set.");
            }

            if (this.Mode.IsNotEqualTo("json") && this.Mode.IsNotNullOrEmpty())
            {
                throw new ArgumentException("The 'Mode' argument cas be empty or JSON only.");
            }
        }

        public bool IsDemoMode { get; set; }
        public string Find { get; private set; }
        public string Replace { get; private set; }
        public string Pattern { get; private set; }
        public string BaseFolder { get; private set; }
        public string Mode { get; private set; }
    }

}
