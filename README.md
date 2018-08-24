# FindAndReplace
Simple command line tool to find and replace regular expression, in multiple texts (json, xml, config files).

```
Usage: FindAndReplace [options] 

 Options:
   --Find      | -f    Required. Text to search. 
                       If Mode is undefined: regular expression syntax, with case insensitive.
                       If Mode=Json: Find is the JSON key and Replace is the value to set.
   --Replace   | -r    Text to replace.
   --Base      | -b    Base directory to start file searchs. Default is current folder.
   --Pattern   | -p    List of files to include in the search. Default is *.*.
                       Using the Minimatch syntax (**/*.config search in all sub-folders).
   --Demo      | -d    Run the tool in Preview mode (without update files).
   --Mode      | -m    Execution mode of the tool.
                       By default, undefined.
                       JSon, find the key to replace by his value.

Examples:
  FindAndReplace --find="Version" --replace="1.0.2" --pattern=**/*.json --mode=json
  FindAndReplace --find="([A-Z])\w+" --replace="XXX" --pattern=**/*.txt
```

## Installation

First, install [.Net Core 2.1](https://www.microsoft.com/net/download).
Next, execute this command:

`$> dotnet tool install -g dvoituron.FindAndReplace`

To display all .Net tools installed:

`$> dotnet tool list -g`

To uninstall the tool:

`$> dotnet tool uninstall -g dvoituron.FindAndReplace`

More detail via [docs.microsoft.com](https://docs.microsoft.com/dotnet/core/tools/dotnet-tool-install).

## Release Notes

### Version 1.1

First version with basic features (Find, Replace, Pattern, Demo and Mode).

### Version 1.2

- FIX: Don't overwrite files if nothing has changed (Thanks [quittenkaes](https://github.com/quittenkaes)).
- NEW: Detect the BOM (Byte order mark) of the file and save it with the same encoding format (UTF8, UTF16, Unicode, ...) (Thanks [quittenkaes](https://github.com/quittenkaes)).