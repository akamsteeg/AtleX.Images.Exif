using AtleX.Images.Exif.Data;
using AtleX.Images.Exif.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public class JpegExifReader : ExifReader
    {

        public JpegExifReader(Stream imageDataStream)
        {
            if (imageDataStream == null)
                throw new ArgumentNullException("imageDataStream");

            if (FileTypeHelper.DetermineFileType(imageDataStream) == ImageFileType.Jpeg)
            {
                this.ImageDataStream = imageDataStream;
                this.CanRead = true;
            }
            else
            {
                this.CanRead = false;
                throw new InvalidDataException(Strings.ExceptionImageInvalidJpeg);
            }
        }

        /// <summary>
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>A Dictionary with the tags and the values read from the image</returns>
        public override IEnumerable<ExifValue> GetExifData()
        {
            if (!this.CanRead)
                throw new InvalidOperationException(Strings.ExceptionReaderCanNotRead);

            List<ExifValue> values = new List<ExifValue>();

            BinaryReader bReader = new BinaryReader(this.ImageDataStream, new ASCIIEncoding());

            IEnumerable<RawJpegSegment> segments = JpegFileParser.ParseHeaderIntoSegments(bReader);

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
                    values.AddRange(parser.Parse(currentSegment));
            }

            bReader.Close();

            return values;
        }
    }
}
