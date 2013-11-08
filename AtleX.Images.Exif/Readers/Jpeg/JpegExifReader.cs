using System;
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
            base.Open(imageFileName);

            // Lazy extension check first, before doing the expensive Magic Numbers check
            if (!imageFileName.ToLower().EndsWith("jpeg") 
                && !imageFileName.ToLower().EndsWith("jpg")
                && FileTypeHelper.DetermineFileType(imageFileName) != FileType.Jpeg
                )
            {
                this._canRead = false;
                throw new FileLoadException(string.Format("File '{0}' is not a JPEG file", this._imageFileName));
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

            IEnumerable<JpegSegment> segments = jfp.ParseHeaderIntoSegments(reader);


            return ed;
        }
    }
}
