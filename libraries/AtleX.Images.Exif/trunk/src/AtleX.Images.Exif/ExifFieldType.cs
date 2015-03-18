namespace AtleX.Images.Exif
{
    /// <summary>
    /// The possible fields in the EXIF data
    /// </summary>
    /// <remarks>
    /// Enum names are taken directly from the EXIF standard v.2
    /// </remarks>
    public enum ExifFieldType
    {
        /// <summary>
        /// Image width
        /// </summary>
        /// <remarks>
        /// The type is Long
        /// </remarks>
        ImageWidth = 256,

        /// <summary>
        /// Image height
        /// </summary>
        /// <remarks>
        /// The type is Long
        /// </remarks>
        ImageHeight = 257,

        /// <summary>
        /// The number of bits per image component
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        BitsPerSample = 258,

        /// <summary>
        /// The pixel composition
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        PhotometricInterpretation = 262,

        /// <summary>
        /// Image title
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        ImageDescription = 270,

        /// <summary>
        /// Camera maker
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        CameraMake = 271,

        /// <summary>
        /// Camera model
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        CameraModel = 272,

        /// <summary>
        /// The image orientation
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        Orientation = 274,

        /// <summary>
        /// The number of components per pixel
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        SamplesPerPixel = 277,

        /// <summary>
        /// The number of pixels per <see cref="ResolutionUnit"/> in
        /// the <see cref="ImageWidth"> direction 
        /// </summary>
        /// <remarks>
        /// The type is Rational
        /// </remarks>
        XResolution = 282,

        /// <summary>
        /// The number of pixels per <see cref="ResolutionUnit"/> in
        /// the <see cref="ImageHeight"> direction 
        /// </summary>
        /// <remarks>
        /// The type is Rational
        /// </remarks>
        YResolution = 283,

        /// <summary>
        /// Unit of X and Y resolution
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        ResolutionUnit = 296,

        /// <summary>
        /// The editing software used
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        SoftwareUsed = 305,

        /// <summary>
        /// Capture date/time
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        DateTime = 306,

        /// <summary>
        /// Photographer
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        Artist = 315,

        /// <summary>
        /// The position of chrominance components in relation to the luminance component
        /// </summary>
        /// <remarks>
        /// The type is Short
        /// </remarks>
        YCbCrPositioning = 531,

        /// <summary>
        /// Copyright
        /// </summary>
        /// <remarks>
        /// The type is ASCII
        /// </remarks>
        Copyright = 33432,
    }
}