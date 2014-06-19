
namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// String value stored in the EXIF data
    /// </summary>
    public class ExifStringValue : ExifValue<string>
    {
        public ExifStringValue(ExifFieldType field, string value)
        {
            this.Field = field;
            this.Value = value;
        }
    }
}
