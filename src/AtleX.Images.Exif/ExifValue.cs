using System;
using System.Diagnostics;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Represents an Exif/IPTC value
    /// </summary>
    [DebuggerDisplay("{Field} - {Value}")]
    public class ExifValue : IEquatable<ExifValue>
    {
        private readonly ExifFieldType _field;
        private readonly object _value;

        /// <summary>
        /// The type of the field
        /// </summary>
        public ExifFieldType Field
        {
            get { return _field; }
        }

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
        /// <param name="field">
        /// The type of the field
        /// </param>
        /// <param name="value">
        /// The value of the field
        /// </param>
        public ExifValue(ExifFieldType field, object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            _field = field;
            _value = value;
        }

        public static bool operator ==(ExifValue left, ExifValue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ExifValue left, ExifValue right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Gets the value of the field
        /// </summary>
        /// <typeparam name="T">
        /// The type where to cast the value to
        /// </typeparam>
        /// <returns>
        /// The value of this <see cref="ExifValue"/> as T
        /// </returns>
        /// <remarks>
        /// When called with String as T, it does a ToString() on the value internally
        /// </remarks>
        public T GetValue<T>()
        {
            T result = default(T);

            if (typeof(T) == typeof(string))
            {
                /*
                 * Apparently converting a String to T-as-string is
                 * not possible, so we convert the string back to
                 * an object so we can convert the object back
                 * to a string. Yes, my head was spinning too.
                 */
                result = (T)(object)this.Value.ToString(); // HACK!
            }
            else
            {
                result = (T)this.Value;
            }

            return result;
        }

        /// <summary>
        /// Returns a string representation of this <see cref="ExifValue"/>
        /// </summary>
        /// <returns>
        /// </returns>
        public override string ToString()
        {
            const string format = "{0} - {1}";

            return string.Format(format, Field, this.GetValue<string>());
        }

        /// <summary>
        /// Compares this <see cref="ExifValue"/> with the specified one for equality
        /// </summary>
        /// <param name="other">
        /// The other <see cref="ExifValue"/> to compare this one with
        /// </param>
        /// <returns>
        /// True if the other <see cref="ExifValue"/> is equal to this one,
        /// false otherwise
        /// </returns>
        public bool Equals(ExifValue other)
        {
            bool result = false;

            if (this.Field == other.Field && this.Value.Equals(other.Value))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Indicates whether this <see cref="ExifValue"/> and a specified
        /// object are equal
        /// </summary>
        /// <param name="obj">
        /// Another object to compare to
        /// </param>
        /// <returns>
        /// True if the other object is equal to this one, false otherwise
        /// </returns>
        public override bool Equals(object obj)
        {
            bool result = (obj is ExifValue && this.Equals(obj));
            return result;
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance
        /// </returns>
        public override int GetHashCode()
        {
            int result = this.Field.GetHashCode() ^ this.Value.GetHashCode();
            return result;
        }
    }
}