
namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// String value stored in the EXIF data
    /// </summary>
    public class ExifStringValue : ExifValue<string>
    {
        public ExifStringValue(ExifTag tag, string value)
        {
            this.Tag = tag;
            this.Value = value;
        }
    }
}
