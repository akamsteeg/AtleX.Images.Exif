using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers
{
    public abstract class ExifDataReader : ExifReader
    {
        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public override ExifData ReadExif()
        {
            ExifData ed = null;
            using (FileStream stream = new FileStream(this._imageFileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader bReader = new BinaryReader(stream, new ASCIIEncoding()))
            {
                ed = this.ReadExifFromBinaryReader(bReader);
            }

            return ed;
        }

        /// <summary>
        /// Read the data from the file
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected abstract ExifData ReadExifFromBinaryReader(BinaryReader reader);
    }
}
