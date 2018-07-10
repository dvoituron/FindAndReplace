To create the NuGet tool package: 
   C:\> dotnet pack -c Release 

To install and use this package locally:
   Copy the generated package to C:\Program Files\dotnet\sdk\NuGetFallbackFolder\DVoituron.FindAndReplace
   C:\> dotnet tool install -g DVoituron.FindAndReplace

The application will be installed in the current user folder: %USERPROFILE%\.dotnet\tools

To display all tool installed:
  C:\> dotnet tool list -g

To uninstall the tool:
  C:\> dotnet tool uninstall -g DVoituron.FindAndReplace