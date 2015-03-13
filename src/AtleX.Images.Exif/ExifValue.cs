﻿using System;
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
    public struct ExifValue : IEquatable<ExifValue>
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
        /// <returns>The value of this <see cref="ExifValue"/> as T</returns>
        public T GetValue<T>()
        {
            T result = default(T);

            if (typeof(T) == typeof(string))
            {
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
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Field, this.GetValue<string>());
        }

        /// <summary>
        /// Compares this <see cref="ExifValue"/> with the specified one for equality
        /// </summary>
        /// <param name="other">The other <see cref="ExifValue"/> to compare this one with</param>
        /// <returns>True if the other <see cref="ExifValue"/> is equal to this one, false otherwise</returns>
        public bool Equals(ExifValue other)
        {
            bool result = false;

            if (this.Field == other.Field && this.Value.Equals(other.Value))
            {
                result = true;
            }

            return result;
        }
    }
}
