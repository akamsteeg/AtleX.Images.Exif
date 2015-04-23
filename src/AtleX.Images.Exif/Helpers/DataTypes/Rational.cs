using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Helpers.DataTypes
{
    public struct Rational
    {
        private readonly long _numerator;
        public long Numerator
        {
            get { return this._numerator; }
        }

        private readonly long _denominator;
        public long Denominator
        {
            get { return this._denominator; }
        }

        public Rational(long nominator, long denominator)
        {
            this._numerator = nominator;
            this._denominator = denominator;
        }

        public static explicit operator long(Rational r)
        {
            long result;

            result = r.Numerator / r.Denominator;

            return result;
        }
    }
}
