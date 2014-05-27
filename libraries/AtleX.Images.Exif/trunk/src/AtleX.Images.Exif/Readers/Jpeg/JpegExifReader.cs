using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public class JpegExifReader : ExifDataReader
    {
        /// <summary>
        /// Open the image
        /// </summary>
        /// <param name="imageFileName"></param>
        public override void Open(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException(imageFileName);
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format("Can't find file '{0}'", imageFileName));

            if (FileTypeHelper.DetermineFileType(imageFileName) == FileType.Jpeg)
            {
                this.ImageFileName = imageFileName;
                this.CanRead = true;
            }
            else
            {
                this.CanRead = false;
                throw new FileLoadException(string.Format("File '{0}' is not a JPEG file", this.ImageFileName));
            }
        }

        /// <summary>
        /// Read the data from the file
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override ExifData ReadExifFromBinaryReader(BinaryReader reader)
        {
            ExifData ed = new ExifData();
            
            JpegFileParser jfp = new JpegFileParser();

            IEnumerable<RawJpegSegment> segments = jfp.ParseHeaderIntoSegments(reader);

            JpegSegmentParser parser = null;
            foreach (RawJpegSegment currentSegment in segments)
            {
                switch (currentSegment.Type)
                {
                    case JpegSegmentType.App1:
                        parser = new JpegSegmentParserApp1();
                        break;
                }

                if (parser != null)
                {
                    foreach (KeyValuePair<string, string> keyValue in parser.Parse(currentSegment))
                    {
                    }
                }
            }
            return ed;
        }
    }
}
