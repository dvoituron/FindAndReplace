using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FindAndReplace
{
    public class FindAndReplaceManager
    {
        public FindAndReplaceManager(Arguments args)
        {
            this.Arguments = args;
        }

        public void Start()
        {
            var filesMinimatched = new List<string>();
            var baseFolderLength = Arguments.BaseFolder.Length;
            var relativeFiles = Directory.EnumerateFiles(Arguments.BaseFolder, "*.*", SearchOption.AllDirectories)
                                 .Select(f => f.Substring(baseFolderLength));

            // Search files matching the Pattern
            foreach (string file in relativeFiles)
            {
                if (Minimatcher.IsMatch(file))
                {
                    filesMinimatched.Add(file);
                }
            }

            // Read and replace
            foreach (var relativeFile in filesMinimatched)
            {
                var filename = Arguments.BaseFolder + relativeFile;
                string content = File.ReadAllText(filename);

                string newContent = Regex.Replace(content, Arguments.Find, Arguments.Replace, RegexOptions.IgnoreCase);

                if (!Arguments.IsDemoMode)
                {
                    File.WriteAllText(filename, newContent);
                }

                if (newContent != content)
                {
                    this.FilesMatched.Add(relativeFile);
                    Logger?.Invoke(relativeFile, newContent); 
                }
            }
        }

        public Action<string, string> Logger;     // FileName, Content

        public Arguments Arguments { get; private set; }

        public List<string> FilesMatched { get; set; } = new List<string>();

        private Minimatch.Minimatcher _minimatcher = null;
        public Minimatch.Minimatcher Minimatcher
        {
            get
            {
                if (_minimatcher == null)
                {
                    var options = new Minimatch.Options()
                    {
                        AllowWindowsPaths = true,
                        Dot = true,
                        IgnoreCase = true,
                    };
                    _minimatcher = new Minimatch.Minimatcher(Arguments.Pattern, options);
                }
                return _minimatcher;
            }
        }
    }
}
