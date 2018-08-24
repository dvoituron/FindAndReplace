using FindAndReplace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace FindAndReplaceTests
{
    [TestClass]
    public class FileTests
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
        public void File_Encoding_Default()
        {
            var file = new FindAndReplace.File($"{BASE_FOLDER}/First.json");

            Assert.AreEqual(Encoding.UTF8, file.Encoding);      // No BOM, so UTF8
            Assert.AreEqual(false, file.HasBom);
        }

        [TestMethod]
        public void File_Encoding_SavedDefault()
        {
            new FindAndReplace.File($"{BASE_FOLDER}/First.json").WriteAllText("New Content");

            var file = new FindAndReplace.File($"{BASE_FOLDER}/First.json");
            Assert.AreEqual("New Content", file.Content);
            Assert.AreEqual(Encoding.UTF8, file.Encoding);      // No BOM, so UTF8
            Assert.AreEqual(false, file.HasBom);
        }

        [TestMethod]
        [DataRow("Encoding-UTF8.txt", "UTF-8", true)]
        [DataRow("Encoding-UTF32.txt", "UTF-32", true)]
        [DataRow("Encoding-Unicode.txt", "Unicode", true)]
        [DataRow("Encoding-UTF7.txt", "UTF-8", false)]      // No BOM, so UTF8
        [DataRow("Encoding-ASCII.txt", "UTF-8", false)]     // No BOM, so UTF8
        public void File_WithBom_MultipleEncoding(string filename, string encoding, bool hasBom)
        {
            var file = new FindAndReplace.File($"{BASE_FOLDER}/{filename}");

            Assert.AreEqual(Encoding.GetEncoding(encoding), file.Encoding);
            Assert.AreEqual(hasBom, file.HasBom);
        }

        [TestMethod]
        [DataRow("Encoding-UTF8.txt", "UTF-8", true)]
        [DataRow("Encoding-UTF32.txt", "UTF-32", true)]
        [DataRow("Encoding-Unicode.txt", "Unicode", true)]
        [DataRow("Encoding-UTF7.txt", "UTF-8", false)]      // No BOM, so UTF8
        [DataRow("Encoding-ASCII.txt", "UTF-8", false)]     // No BOM, so UTF8
        public void File_Save_WithBom_MultipleEncoding(string filename, string encoding, bool hasBom)
        {
            // Save the content
            new FindAndReplace.File($"{BASE_FOLDER}/{filename}").WriteAllText("New Content");

            var file = new FindAndReplace.File($"{BASE_FOLDER}/{filename}");
            Assert.AreEqual("New Content", file.Content);
            Assert.AreEqual(Encoding.GetEncoding(encoding), file.Encoding);
            Assert.AreEqual(hasBom, file.HasBom);
        }

    }
}
