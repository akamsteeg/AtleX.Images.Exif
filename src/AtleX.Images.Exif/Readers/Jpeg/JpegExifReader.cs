﻿using AtleX.Images.Exif.Data;
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
                throw new InvalidDataException("Data is not from a valid JPEG");
            }
        }

        public override Dictionary<ExifTag, ExifValue> GetExifData()
        {
            if (!this.CanRead)
                throw new InvalidOperationException("Can't read from invalid or unopened data. Have you instantiated this reader with valid data?");

            Dictionary<ExifTag, ExifValue> ed = new Dictionary<ExifTag, ExifValue>();

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
                {
                    ed = parser.Parse(currentSegment);
                }
            }

            bReader.Close();

            return ed;
        }
    }
}
