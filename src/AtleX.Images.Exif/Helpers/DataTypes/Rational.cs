using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Helpers.DataTypes
{
    public struct Rational
    {
        private readonly int _numerator;
        public int Numerator
        {
            get { return this._numerator; }
        }

        private readonly int _denominator;
        public int Denominator
        {
            get { return this._denominator; }
        }

        public Rational(int nominator, int denominator)
        {
            this._numerator = nominator;
            this._denominator = denominator;
        }

        public static explicit operator int(Rational r)
        {
            int result;

            result = r.Numerator / r.Denominator;

            return result;
        }
    }
}
