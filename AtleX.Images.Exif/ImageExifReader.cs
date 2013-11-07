using AtleX.Images.Exif.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Reads EXIF from a supported file
    /// <remarks>This reader acts as a factory for file-specific readers</remarks>
    /// </summary>
    public class ImageExifReader : ExifReader
    {
        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public override ExifData ReadExif()
        {
            IExifReader reader;
            FileType type = FileTypeHelper.DetermineFileType(this._imageFileName);

            switch (type)
            {
                case FileType.Jpeg:
                    reader = new JpegExifReader();
                    reader.Open(this._imageFileName);
                    break;
                default:
                    throw new FileLoadException(string.Format("File '{0}' is not a supported file", this._imageFileName));
            }
            
            return reader.ReadExif();
        }
    }
}
