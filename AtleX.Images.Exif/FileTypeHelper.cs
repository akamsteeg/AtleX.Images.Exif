using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    public enum FileType
    {
        Unknown,
        Jpeg
    }

    public abstract class FileTypeHelper
    {
        /// <summary>
        /// Determines and returns the file type of the specified file
        /// </summary>
        /// <remarks>This is not a lazy extension-check but it reads the magic numbers at the beginning of the file</remarks>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileType DetermineFileType(string fileName)
        {
            FileType result = FileType.Unknown;

            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader bReader = new BinaryReader(stream, new ASCIIEncoding()))
            {
                byte[] buffer = new byte[10];
                buffer = bReader.ReadBytes(10);

                // Check for JPEG header (FF D8)
                if (buffer[0] == 255 // FF
                    && buffer[1] == 216  // D8
                    )
                {
                    result = FileType.Jpeg;
                }
            }

            return result;
        }
    }
}
