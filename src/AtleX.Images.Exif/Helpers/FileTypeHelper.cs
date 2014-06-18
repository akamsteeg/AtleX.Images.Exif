using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Helpers
{
    internal enum ImageFileType
    {
        Unknown,
        Jpeg
    }

    internal abstract class FileTypeHelper
    {
        /// <summary>
        /// Determines and returns the file type of the specified file
        /// </summary>
        /// <remarks>This is not a lazy extension-check but it reads the magic numbers at the beginning of the file</remarks>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ImageFileType DetermineFileType(string fileName)
        {
            ImageFileType result = ImageFileType.Unknown;
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                result = FileTypeHelper.DetermineFileType(stream);
            }

            return result;
        }

        public static ImageFileType DetermineFileType(Stream fileContents)
        {
            ImageFileType result = ImageFileType.Unknown;
            BinaryReader bReader = new BinaryReader(fileContents, new ASCIIEncoding());

            /*
             * According to Wikipedia (http://en.wikipedia.org/wiki/List_of_file_signatures)
             * the longest magic number is 30 bytes and is used by the
             * Flexible Image Transport System (FITS)
             */
            const int magicNumberLength = 30;

            byte[] buffer = new byte[magicNumberLength];
            buffer = bReader.ReadBytes(magicNumberLength);

            // Check for JPEG header (FF D8)
            if (buffer[0] == 255 // FF
                && buffer[1] == 216  // D8
                )
            {
                result = ImageFileType.Jpeg;
            }

            fileContents.Seek(0, SeekOrigin.Begin); // Reset the stream to avoid problems in calling methods

            return result;
        }
    }
}
