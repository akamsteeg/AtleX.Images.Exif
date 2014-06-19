
namespace AtleX.Images.Exif
{
    /// <summary>
    /// The possible fields in the EXIF data
    /// </summary>
    /// <remarks>
    /// Enum names are taken directly from 
    /// the EXIF standard v.2
    /// </remarks>
    public enum ExifFieldType
    {
        /// <summary>
        /// Image title
        /// </summary>
        ImageDescription = 270,
        /// <summary>
        /// Camera maker
        /// </summary>
        CameraMake = 271,
        /// <summary>
        /// Camera model
        /// </summary>
        CameraModel = 272,

        /// <summary>
        /// The editing software used
        /// </summary>
        SoftwareUsed = 305,
        /// <summary>
        /// Capture date/time
        /// </summary>
        DateTime = 306,
        /// <summary>
        /// Photographer
        /// </summary>
        Artist = 315,
        /// <summary>
        /// Copyright
        /// </summary>
        Copyright = 33432,
    }
}
