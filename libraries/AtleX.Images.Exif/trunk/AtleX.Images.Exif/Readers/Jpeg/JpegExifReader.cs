using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers
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
            if (!imageFileName.EndsWith("jpeg") 
                && !imageFileName.EndsWith("jpg")
                && FileTypeHelper.DetermineFileType(imageFileName) != FileType.Jpeg
                )
            {
                throw new FileLoadException(string.Format("File '{0}' is not a JPEG file", this._imageFileName));
            }
        }

        protected override ExifData ReadExifFromBinaryReader(BinaryReader reader)
        {
            ExifData ed = new ExifData();
            if (this.HasApp1(reader))
            {
                Byte[] app1Data = this.ReadApp1Block(reader);
            }
            //if (this.HasApp2

            return ed;
        }

        protected bool HasApp1(BinaryReader reader)
        {
            bool result = false;
            /*
             * Reset the stream to just after the JPEG magic numbers at 
             * the beginning of the file
             */
            reader.BaseStream.Seek(2, SeekOrigin.Begin);

            byte[] exifMarker = reader.ReadBytes(8);

            // An APP1 segment is indicated by FF E1 (255 225)
            if (exifMarker[0] == 255 && exifMarker[1] == 225) // We have a JFIF/EXIF segment
            {
                // And EXIF APP1 segment starts with "Exif" in ASCII
                if (exifMarker[4] == 69 // E
                    && exifMarker[5] == 120 // x
                    && exifMarker[6] == 105 // i
                    && exifMarker[7] == 102 // f
                    )
                { 
                    // Ladies & gentlemen, we have found EXIF info!
                    result = true;
                }
            }

            return result;
        }

        protected Byte[] ReadApp1Block(BinaryReader reader)
        {
            /*
             * Reset the stream to just after the beginning of the block after
             * the "FF E1" block marker
             */
            reader.BaseStream.Seek(12, SeekOrigin.Begin);

            throw new NotImplementedException();
        }

        protected class AppBlock
        {
            public int Length { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
