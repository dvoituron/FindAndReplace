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
            Console.WriteLine("                       If Mode is undefined: regular expression syntax, with case insensitive.");
            Console.WriteLine("                       If Mode=Json: Find is the JSON key and Replace is the value to set.");
            Console.WriteLine("   --Replace   | -r    Text to replace.");
            Console.WriteLine("   --Base      | -b    Base directory to start file searchs. Default is current folder.");
            Console.WriteLine("   --Pattern   | -p    List of files to include in the search. Default is *.*.");
            Console.WriteLine("                       Using the Minimatch syntax (**/*.config search in all sub-folders).");
            Console.WriteLine("   --Demo      | -d    Run the tool in Preview mode (without update files).");
            Console.WriteLine("   --Mode      | -m    Execution mode of the tool.");
            Console.WriteLine("                       By default, undefined.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  FindAndReplace --find=\"Version\" --replace=\"1.0.2\" --pattern=**/*.json --mode=json");
            
        }
    }
}
