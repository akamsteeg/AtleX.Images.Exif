using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace AtleX.Images.Exif.Helpers
{
    public enum ImageFileType
    {
        Unknown,
        Jpeg
    }

    public static class FileTypeHelper
    {
        /// <summary>
        /// Determines and returns the file type of the specified file.
        /// </summary>
        /// <see cref="DetermineFileType(Stream fileContents)"/>
        /// 
        /// <param name="path">
        /// A relative or absolute path for the file
        /// </param>
        /// <returns>
        /// </returns>
        public static ImageFileType DetermineFileType(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("fileName");
            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.ExceptionFileNotFound, path));

            ImageFileType result = ImageFileType.Unknown;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                result = FileTypeHelper.DetermineFileType(stream);
            }

            return result;
        }

        /// <summary>
        /// Determines and returns the file type of the specified file
        /// </summary>
        /// <remarks>
        /// This is not a lazy extension check but it reads the magic numbers at
        /// the beginning of the file. It currently only supports JPEG, but
        /// that's enough for the this library and for the time being.
        /// </remarks>
        /// <param name="fileContents">
        /// </param>
        /// <returns>
        /// </returns>
        public static ImageFileType DetermineFileType(Stream fileContents)
        {
            if (fileContents == null)
                throw new ArgumentNullException("fileContents");
            if (!fileContents.CanRead)
                throw new ArgumentException(Strings.ExceptionCanNotReadFromStream, "fileContents");

            long originalPosition = fileContents.Position;
            if (originalPosition != 0)
            {
                /*
                 * The magic bytes are located in at the first n bytes of the file, so we need to 
                 * reset the stream when something already read from it.
                 */
                fileContents.Seek(0, SeekOrigin.Begin);
            }

            ImageFileType result = ImageFileType.Unknown;

            /*
             * We want to dispose the reader when we're done with it, but we have to leave the
             * stream open because it was instantiated and most likely used by one of the calling
             * methods
             */
            using (BinaryReader bReader = new BinaryReader(fileContents, new ASCIIEncoding(), true))
            {

                /*
                 * According to Wikipedia (http://en.wikipedia.org/wiki/List_of_file_signatures)
                 * the longest magic number is 30 bytes and is used by the
                 * Flexible Image Transport System (FITS)
                 */
                const int magicNumberLength = 30;
                var buffer = bReader.ReadBytes(magicNumberLength);

                // Check for JPEG header (FF D8)
                if (buffer[0] == 255 // FF
                    && buffer[1] == 216  // D8
                    )
                {
                    result = ImageFileType.Jpeg;
                }
            }

            /*
             * Reset the position of the stream to the same position it was
             * in when it was passed to this method. This avoids errors and
             * unexpected results in calling methods because they do not
             * expect the position to change
             */
            fileContents.Seek(originalPosition, SeekOrigin.Begin);

            return result;
        }
    }
}