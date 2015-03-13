using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Represents an Exif/IPTC value
    /// </summary>
    [DebuggerDisplay("{Field} - {Value}")]
    public struct ExifValue
    {
        private ExifFieldType _field;
        /// <summary>
        /// the type of the field
        /// </summary>
        public ExifFieldType Field
        {
            get { return _field; }
        }

        private object _value;
        /// <summary>
        /// The value of the field
        /// </summary>
        public object Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ExifValue"/>
        /// </summary>
        /// <param name="field">The type of the field</param>
        /// <param name="value">The value of the field</param>
        public ExifValue(ExifFieldType field, object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            _field = field;
            _value = value;
        }

        /// <summary>
        /// Gets the value of the field
        /// </summary>
        /// <typeparam name="T">The type where to cast the value to</typeparam>
        /// <returns>The value of this <see cref="ExifValue"/> as T, or null if the cast fails</returns>
        public T GetValue<T>() where T: class, new()
        {
            return Value as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Field, Value as string);
        }
    }
}
