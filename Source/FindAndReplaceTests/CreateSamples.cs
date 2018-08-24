
using System;
using System.IO;
using System.Text;

namespace FindAndReplaceTests
{
    public class CreateSamples
    {
        private string _baseFolder;

        public CreateSamples(string baseFolder)
        {
            _baseFolder = baseFolder;

            CreateSampleFile("First.json", "1.0.0");
            CreateSampleFile("Encoding-UTF8.txt", "1.0.0", Encoding.UTF8);
            CreateSampleFile("Encoding-UTF7.txt", "1.0.0", Encoding.UTF7);
            CreateSampleFile("Encoding-UTF32.txt", "1.0.0", Encoding.UTF32);
            CreateSampleFile("Encoding-ASCII.txt", "1.0.0", Encoding.ASCII);
            CreateSampleFile("Encoding-BigEndianUnicode.txt", "1.0.0", Encoding.BigEndianUnicode);
            CreateSampleFile("Encoding-Unicode.txt", "1.0.0", Encoding.Unicode);
            CreateSampleFile("SubFolder\\Second.json", "2.0.0");
            CreateSampleFile("SubFolder\\Third.json", "A.B.C");     // No digits
            CreateSampleFile("SubFolder\\Fourth.txt", "4.0.0");     // No JSON file
            CreateAppSettingsJsonFile("AppSettings.json");
        }

        public void CreateSampleFile(string filename, string sampleVersion)
        {
            CreateSampleFile(filename, sampleVersion, null);
        }

        public void CreateSampleFile(string filename, string sampleVersion, Encoding encoding)
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

            if (encoding == null)
                File.WriteAllText(file.FullName, content.Replace("__VERSION__", sampleVersion));
            else
                File.WriteAllText(file.FullName, content.Replace("__VERSION__", sampleVersion), encoding);
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
