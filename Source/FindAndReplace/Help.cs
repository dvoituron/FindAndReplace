using System;

namespace FindAndReplace
{
    internal class Help
    {
        public static void DisplayGeneralHelp()
        {
            Console.WriteLine(" Usage: FindAndReplace [options] ");
            Console.WriteLine();
            Console.WriteLine(" Options:");
            Console.WriteLine("   --Find      | -f    Required. Text to search. ");
            Console.WriteLine("                       Using regular expression syntax.");
            Console.WriteLine("   --Replace   | -r    Text to replace.");
            Console.WriteLine("   --Base      | -b    Base directory to start file searchs. Default is current folder.");
            Console.WriteLine("   --Pattern   | -p    List of files to include in the search. Default is *.*.");
            Console.WriteLine("                       Using the Minimatch syntax (**/*.config search in all sub-folders).");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  FindAndReplace -f='^version = \"\\d.\\d.\\d\"$' -r=\"version = '1.0.2'\" -d=**/*.config");
        }
    }
}
