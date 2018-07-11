using FindAndReplace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FindAndReplaceTests
{
    [TestClass]
    public class FindAndReplaceTests
    {
        private const string BASE_FOLDER = @"C:\Temp\FindAndReplaceTests";

        [TestInitialize]
        public void Initialize()
        {
            var samples = new CreateSamples(BASE_FOLDER);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(BASE_FOLDER, recursive: true);
        }

        [TestMethod]
        public void CommandLine_CheckArguments_Tests()
        {
            string[] args = new string[]
            {
                @"-f=Microsoft"
            };

            var arguments = new Arguments(args);

            Assert.AreEqual(arguments.Find, "Microsoft");
            Assert.AreEqual(arguments.Replace, "");
            Assert.AreEqual(arguments.Pattern, "*.*");
        }

        [TestMethod]
        public void CommandLine_Simple_OneJsonFile_Tests()
        {
            string[] args = new string[]
            {
                @"-f=Microsoft",
                @"-r=Denis",
                @"-p=*.json",
                $"-b={BASE_FOLDER}",                    // From base folder
                @"-d"                                   // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Start();

            Assert.AreEqual(1, manager.FilesMatched.Count);
        }

        [TestMethod]
        public void CommandLine_AppSettingsJsonFile_Tests()
        {
            string[] args = new string[]
            {
                $"-f=\"ApiGlobalPrefix\"",
                $"-r=Denis",
                $"-p=*.json",
                $"-b={BASE_FOLDER}",                    // From base folder
                $"-d"                                   // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Logger = (file, content) =>
            {
                Assert.IsTrue(content.Contains("Denis"));
            };
            manager.Start();

            Assert.AreEqual(1, manager.FilesMatched.Count);
        }

        [TestMethod]
        public void CommandLine_FindCaseSensitive_Tests()
        {
            string[] args = new string[]
            {
                @"-f=MICROSOFT",
                @"-r=Denis",
                @"-p=*.json",
                $"-b={BASE_FOLDER}",                    // From base folder
                @"-d"                                   // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Start();

            Assert.AreEqual(1, manager.FilesMatched.Count);
        }

        [TestMethod]
        public void CommandLine_Simple_AllJsonFiles_Tests()
        {
            string[] args = new string[]
            {
                @"-f=Microsoft",
                @"-r=Denis",
                @"-p=**/*.json",                        // All json in current and subfolders
                $"-b={BASE_FOLDER}",                    // From base folder
                @"-d"                                   // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Logger = (file, content) =>
            {
                Assert.IsTrue(content.Contains("Denis"));
            };
            manager.Start();

            Assert.AreEqual(3, manager.FilesMatched.Count);
        }

        [TestMethod]
        public void CommandLine_RegExVersion_AllJsonFiles_Tests()
        {
            string[] args = new string[]
            {
                @"-f=""""version"": ""\d.\d.\d\""""",       // 'version': 'x.x.x'
                @"-r=""""version"": ""9.1.2""""",           // 'version': '9.1.2'
                @"-p=**/*.json",                        // All json in current and subfolders
                $"-b={BASE_FOLDER}",                    // From base folder
                //@"-d"                                 // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Logger = (file, content) =>
            {
                Assert.IsTrue(content.Contains("9.1.2"));
            };
            manager.Start();

            Assert.AreEqual(2, manager.FilesMatched.Count);
        }

        [TestMethod]
        public void CommandLine_ModeJson_Tests()
        {
            string[] args = new string[]
            {
                @"-f=Version",
                @"-r=9.1.3",
                @"-p=**/*.json",
                $"-b={BASE_FOLDER}",                    // From base folder
                $"-m=JSON",                             // JSON Mode => Key / Value
                @"-d"                                   // In demo mode (no write file)
            };

            var arguments = new Arguments(args);
            var manager = new FindAndReplaceManager(arguments);
            manager.Logger = (file, content) =>
            {
                Assert.IsTrue(content.Contains("\"Version\": \"9.1.3\""));
            };
            manager.Start();

            Assert.AreEqual(3, manager.FilesMatched.Count);
        }
    }
}
