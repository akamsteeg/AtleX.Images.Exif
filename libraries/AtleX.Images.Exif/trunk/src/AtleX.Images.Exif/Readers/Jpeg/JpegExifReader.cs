using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public class JpegExifReader : ExifReader
    {

        public JpegExifReader(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException(imageFileName);
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, "Can't find file '{0}'", imageFileName));

            if (FileTypeHelper.DetermineFileType(imageFileName) == FileType.Jpeg)
            {
                this.ImageFileName = imageFileName;
                this.CanRead = true;
            }
            else
            {
                this.CanRead = false;
                throw new FileLoadException(string.Format(CultureInfo.InvariantCulture, "File '{0}' is not a JPEG file", this.ImageFileName));
            }
        }

        public override ExifData GetExifData()
        {
            if (this.CanRead)
            {
                ExifData ed = new ExifData();
                using (FileStream stream = new FileStream(this.ImageFileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader bReader = new BinaryReader(stream, new ASCIIEncoding());

                    JpegFileParser jfp = new JpegFileParser();

                    IEnumerable<RawJpegSegment> segments = jfp.ParseHeaderIntoSegments(bReader);

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

                    bReader.Close();
                }

                return ed;
            }
            else
            {
                throw new InvalidOperationException("Can't read EXIF because the reader isn't ready (have you called Open()?)");
            }
        }
    }
}
