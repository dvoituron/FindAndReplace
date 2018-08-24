using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FindAndReplace
{
    public class File
    {
        public File(string filename)
        {
            this.ReadAllText(filename);
        }

        public string FileName { get; private set; }

        public Encoding Encoding { get; private set; }

        public bool HasBom { get; private set; }

        public string Content { get; private set; }

        public string ReadAllText(string filename)
        {
            this.FileName = filename;

            using (var stream = new StreamReader(filename, detectEncodingFromByteOrderMarks: true))
            {
                this.Encoding = GetEncoding(filename);
                this.Content = stream.ReadToEnd();
                this.HasBom = true;     // Default

                if (this.Encoding == null)
                {
                    this.Encoding = Encoding.UTF8;
                    this.HasBom = false;
                }

                return this.Content;
            }
        }

        public void WriteAllText(string text)
        {
            this.WriteAllText(this.FileName, text);
        }

        public void WriteAllText(string filename, string text)
        {
            if (this.HasBom)
            {
                System.IO.File.WriteAllText(filename, text, this.Encoding);
            }
            else
            {
                System.IO.File.WriteAllText(filename, text);
            }
        }

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding
        /// </remarks>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            return null;
        }
    }
}
