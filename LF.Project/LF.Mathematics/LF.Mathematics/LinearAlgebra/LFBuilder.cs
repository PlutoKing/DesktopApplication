/*──────────────────────────────────────────────────────────────
 * FileName     : LFBuilder.cs
 * Created      : 2021-05-27 15:22:33
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace LF.Mathematics.LinearAlgebra
{

    internal static class LFBuilder<T>
        where T:struct, IEquatable<T>, IFormattable
    {
        #region Methods
        static Lazy<Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>> _singleton = new Lazy<Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>>(Create);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static Tuple <LFMatrixBuilder<T>,LFVectorBuilder<T>> Create()
        {
            // 复数
            if(typeof(T) == typeof(System.Numerics.Complex))
            {
                return new Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>(
                    (LFMatrixBuilder<T>)(object)new LFComplexMatrixBuilder(),
                    (LFVectorBuilder<T>)(object)new LFComplexVectorBuilder());
            }

            // 双精度
            if (typeof(T) == typeof(double))
            {
                return new Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>(
                    (LFMatrixBuilder<T>)(object)new LFDoubleMatrixBuilder(),
                    (LFVectorBuilder<T>)(object)new LFDoubleVectorBuilder());
            }

            throw new NotSupportedException(FormattableString.Invariant($"{typeof(T).Name}类型的矩阵暂不支持。目前仅支持实数double和复数complex。"));

        }

        public static void Register(LFMatrixBuilder<T> matrixBuilder, LFVectorBuilder<T> vectorBuilder)
        {
            _singleton = new Lazy<Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>>(() => new Tuple<LFMatrixBuilder<T>, LFVectorBuilder<T>>(matrixBuilder, vectorBuilder));
        }

        public static LFMatrixBuilder<T> Matrix => _singleton.Value.Item1;

        public static LFVectorBuilder<T> Vector => _singleton.Value.Item2;


        #endregion
    }

    #region Builder Class
    public abstract class LFMatrixBuilder<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// T类型的零元素。
        /// </summary>
        public abstract T Zero { get; }

        /// <summary>
        /// T类型的1元素。
        /// </summary>
        public abstract T One { get; }

        #endregion

        #region Constructors
        #endregion

        #region Methods

        internal abstract T Add(T x, T y);
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }

    public abstract class LFVectorBuilder<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields


        #endregion


        #region Properties

        /// <summary>
        /// T类型的零元素。
        /// </summary>
        public abstract T Zero { get; }

        /// <summary>
        /// T类型的1元素。
        /// </summary>
        public abstract T One { get; }
        #endregion
    }
    #endregion

    #region Complex Builder Classes
    internal class LFComplexMatrixBuilder : LFMatrixBuilder<Complex>
    {
        public override Complex Zero => Complex.Zero;

        public override Complex One => Complex.One;

        internal override Complex Add(Complex x, Complex y)
        {
            return x + y;
        }
    }

    internal class LFComplexVectorBuilder : LFVectorBuilder<Complex>
    {
        public override Complex Zero => Complex.Zero;

        public override Complex One => Complex.One;
    }


    #endregion

    #region Double Builder Classes
    internal class LFDoubleMatrixBuilder : LFMatrixBuilder<double>
    {
        public override double Zero => 0.0d;

        public override double One => 1.0d;

        internal override double Add(double x, double y)
        {
            return x + y;
        }
    }

    internal class LFDoubleVectorBuilder : LFVectorBuilder<double>
    {
        public override double Zero => 0.0d;

        public override double One => 1.0d;
    }

    #endregion

}

