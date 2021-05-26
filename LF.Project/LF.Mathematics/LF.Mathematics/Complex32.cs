/*──────────────────────────────────────────────────────────────
 * FileName     : Complex32.cs
 * Created      : 2021-05-26 21:51:12
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics
{
    /// <summary>
    /// 32位单精度复数类。
    /// </summary>
    public struct Complex32 : IEquatable<Complex32>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 实部。
        /// </summary>
        [DataMember(Order = 1)]
        readonly float _real;

        /// <summary>
        /// 虚部。
        /// </summary>
        [DataMember(Order = 2)]
        readonly float _imag;

        /// <summary>
        /// Returns a new <see cref="LF.Mathematics.Complex32"/> instance with a real number equal to zero
        /// and an imaginary number equal to one.
        /// </summary>
        public static readonly Complex32 ImaginaryOne = new Complex32(0, 1);

        /// <summary>
        /// Represents infinity as a complex32 number.
        /// </summary>
        public static readonly Complex32 Infinity = new Complex32(float.PositiveInfinity, float.PositiveInfinity);

        /// <summary>
        /// Represents a complex instance that is not a number (NaN).
        /// </summary>
        public static readonly Complex32 NaN = new Complex32(float.NaN, float.NaN);

        /// <summary>
        /// Returns a new <see cref="LF.Mathematics.Complex32"/> instance with a real number equal to one
        /// and an imaginary number equal to zero.
        /// </summary>
        public static readonly Complex32 One = new Complex32(1.0f, 0.0f);

        /// <summary>
        /// Returns a new <see cref="LF.Mathematics.Complex32"/> instance with a real number equal to zero
        /// and an imaginary number equal to zero.
        /// </summary>
        public static readonly Complex32 Zero = new Complex32(0.0f, 0.0f);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the real component of the current System.Numerics.Complex object.
        /// </summary>
        /// <value>The real component of a complex32 number.</value>
        public float Real
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get => _real;
        }

        /// <summary>
        /// Gets the imaginary component of the current System.Numerics.Complex object.
        /// </summary>
        /// <value> The imaginary component of a complex number.</value>
        public float Imaginary
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get => _imag;
        }

        /// <summary>
        /// Gets the phase of a complex number.
        /// </summary>
        /// <returns>The phase of a complex number, in radians.</returns>
        public float Phase
        {
            // NOTE: the special case for negative real numbers fixes negative-zero value behavior. Do not remove.
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get => _imag == 0f && _real < 0f ? (float)Math.PI : (float)Math.Atan2(_imag, _real);
        }

        /// <summary>
        ///  Gets the magnitude (or absolute value) of a complex number.
        /// </summary>
        /// <returns>The magnitude of the current instance.</returns>
        public float Magnitude
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                if (float.IsNaN(_real) || float.IsNaN(_imag))
                    return float.NaN;
                if (float.IsInfinity(_real) || float.IsInfinity(_imag))
                    return float.PositiveInfinity;
                float a = Math.Abs(_real);
                float b = Math.Abs(_imag);
                if (a > b)
                {
                    double tmp = b / a;
                    return a * (float)Math.Sqrt(1.0f + tmp * tmp);

                }
                if (a == 0.0f) // one can write a >= float.Epsilon here
                {
                    return b;
                }
                else
                {
                    double tmp = a / b;
                    return b * (float)Math.Sqrt(1.0f + tmp * tmp);
                }
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LF.Mathematics.Complex32 structure using the
        /// specified real and imaginary values.
        /// </summary>
        /// <param name="real">The real part of the complex32 number.</param>
        /// <param name="imaginary">The imaginary part of the complex32 number.</param>
        public Complex32(float real,float imaginary)
        {
            _real = real;
            _imag = imaginary;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the absolute value (or magnitude) of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The absolute value of value.</returns>
        public static double Abs(Complex32 value)
        {
            return value.Magnitude;
        }

        /// <summary>
        /// Returns the angle that is the arc cosine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number that represents a cosine.</param>
        /// <returns>The angle, measured in radians, which is the arc cosine of value.</returns>
        public static Complex32 Acos(Complex32 value)
        {
            return (Complex32)Trig.Acos(value.ToComplex());
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}