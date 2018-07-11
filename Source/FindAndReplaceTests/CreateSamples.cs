
using System;
using System.IO;

namespace FindAndReplaceTests
{
    public class CreateSamples
    {
        private string _baseFolder;

        public CreateSamples(string baseFolder)
        {
            _baseFolder = baseFolder;

            CreateSampleFile("First.json", "1.0.0");
            CreateSampleFile("SubFolder\\Second.json", "2.0.0");
            CreateSampleFile("SubFolder\\Third.json", "A.B.C");     // No digits
            CreateSampleFile("SubFolder\\Fourth.txt", "4.0.0");     // No JSON file
            CreateAppSettingsJsonFile("AppSettings.json");
        }

        public void CreateSampleFile(string filename, string sampleVersion)
        {
            string content =
@"{
  ""runtimeOptions"": {
    ""framework"": {
      ""name"": ""Microsoft.NETCore.App"",
      ""version"": ""__VERSION__"",
      ""info"": ""Hello"",
    }
  }
}";

            FileInfo file = new FileInfo(Path.Combine(_baseFolder, filename));

            if (!file.Directory.Exists)
                file.Directory.Create();

            if (file.Exists)
                file.Delete();

            File.WriteAllText(file.FullName, content.Replace("__VERSION__", sampleVersion));
        }

        public void CreateAppSettingsJsonFile(string filename)
        {
            string content = @"
{
  ""AppSettings"": {
    ""Key"": ""value"",
    ""ApiGlobalPrefix"": """",
    ""SwaggerEndpoint"": ""/DossierUnique/swagger/v1/swagger.json"",
    ""SwaggerBasePath"": ""/DossierUnique"",
    ""SwaggerRoutePrefix"": ""swagger"",
    ""JwtExpirationMinutes"": 60
  }
}
";

            FileInfo file = new FileInfo(Path.Combine(_baseFolder, filename));

            if (!file.Directory.Exists)
                file.Directory.Create();

            if (file.Exists)
                file.Delete();

            File.WriteAllText(file.FullName, content);
        }
    }
}
