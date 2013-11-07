using AtleX.Images.Exif.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    public abstract class Reader
    {
        public static IExifReader OpenReader(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException("imageFileName");
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format("Can't find file '{0}'", imageFileName));

            return GetReaderByFileType(imageFileName);
        }

        protected static IExifReader GetReaderByFileType(string imageFileName)
        {
            IExifReader reader;

            using (FileStream stream = new FileStream(imageFileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader bReader = new BinaryReader(stream, new ASCIIEncoding()))
            {
                byte[] buffer = new byte[10];
                buffer = bReader.ReadBytes(10);

                // Check for JPEG header (FF D8 FF)
                if (buffer[0] == 255 // FF
                    && buffer[1] == 216  // D8
                    && buffer[2] == 255 // FF
                    )
                {
                    reader = new JpegExifReader(imageFileName);
                }
                else
                {
                    throw new FileLoadException(string.Format("File '{0}' is not a supported file", imageFileName));
                }

            }
            
             return reader;
        }
    }
}
