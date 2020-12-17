namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double re;
        private readonly double im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => this.re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => this.im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));


        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(Real, Imaginary);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            return this.Real + " " + this.Imaginary + "i";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }

        public override bool Equals(object obj)
        {
            return obj is Complex complex &&
                   re.CompareTo(complex.re) == 0 &&
                   im.CompareTo(complex.im) == 0;
        }
    }
}
