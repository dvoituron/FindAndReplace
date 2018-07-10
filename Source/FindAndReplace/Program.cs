using System;
using System.Diagnostics;

namespace FindAndReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"FindAndReplace Command Line Tools (v{GetAssemblyVersion().ToString(3)})");
            Console.WriteLine($"Project on https://github.com/dvoituron/FindAndReplace");

            if (args == null || args.Length <= 0 || args[0].IsEqualTo("-h") || args[0].IsEqualTo("--help"))
            {
                Help.DisplayGeneralHelp();
                return;
            }

            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var arguments = new Arguments(args);

                Console.WriteLine($"  Searching files for {arguments.Pattern}");

                var manager = new FindAndReplaceManager(arguments);
                manager.Logger = (type, content) =>
                {
                    if (type == 'F')    // File Found => Display the filename
                        Console.WriteLine(content);
                };
                manager.Start();

                Console.WriteLine($"  Text found and replaced in {manager.FilesMatched.Count} files. {watch.Elapsed.TotalSeconds:0.00} seconds.");
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached) Debugger.Break();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine("Write FindAndReplace --help for more information.");
                Console.ResetColor();
            }

#if DEBUG
            Console.ReadLine();
#endif

        }

        private static Version GetAssemblyVersion()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(fvi.FileVersion);
        }
    }
}
