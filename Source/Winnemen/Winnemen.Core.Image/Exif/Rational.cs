using System;

namespace Winnemen.Core.Image.Exif
{
    internal class Rational
    {
        private readonly int _d;
        private readonly int _n;

        public Rational(int n, int d)
        {
            this._n = n;
            this._d = d;
            this.Simplify(ref this._n, ref this._d);
        }

        public Rational(uint n, uint d)
        {
            this._n = Convert.ToInt32(n);
            this._d = Convert.ToInt32(d);
            this.Simplify(ref this._n, ref this._d);
        }

        public Rational()
        {
            this._n = this._d = 0;
        }

        public string ToString(string sp)
        {
            if (sp == null)
                sp = "/";
            return (string)(object)this._n + (object)sp + (string)(object)this._d;
        }

        public double ToDouble()
        {
            if (this._d == 0)
                return 0.0;
            else
                return Math.Round(Convert.ToDouble(this._n) / Convert.ToDouble(this._d), 2);
        }

        private void Simplify(ref int a, ref int b)
        {
            if (a == 0 || b == 0)
                return;
            int num = this.euclid(a, b);
            a /= num;
            b /= num;
        }

        private int euclid(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return this.euclid(b, a % b);
        }
    }
}
