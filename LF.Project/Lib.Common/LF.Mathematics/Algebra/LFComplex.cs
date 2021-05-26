/*──────────────────────────────────────────────────────────────
 * FileName     : LFComplex.cs
 * Created      : 2021-05-19 20:36:23
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics
{
    /// <summary>
    /// 复数
    /// </summary>
    public class LFComplex:ICloneable
    {
        #region Fields
        private double _real;       // 实部
        private double _imag;       // 虚部
        #endregion

        #region Properties
        /// <summary>
        /// 实部
        /// </summary>
        public double Real { get => _real; set => _real = value; }
        /// <summary>
        /// 虚部
        /// </summary>
        public double Imag { get => _imag; set => _imag = value; }

        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFComplex()
        {
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="real">实部</param>
        /// <param name="imag">虚部</param>
        public LFComplex(double real,double imag)
        {
            _real = real;
            _imag = imag;
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="s">字符串</param>
        public LFComplex(string s)
        {
            StringToValue(s);
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj">源对象</param>
        public LFComplex(LFComplex obj)
        {
            _real = obj._real;
            _imag = obj._imag;
        }


        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFComplex Clone()
        {
            return new LFComplex(this);
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Methods

        #region Constructing Methods

        /// <summary>
        /// 字符串转复数
        /// </summary>
        /// <param name="s">字符串</param>
        public void StringToValue(string s)
        {

        }
        #endregion

        #region Operator Methods

        /// <summary>
        /// 原复数
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static LFComplex operator +(LFComplex c)
        {
            return new LFComplex(c);
        }

        /// <summary>
        /// 负复数
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static LFComplex operator -(LFComplex c)
        {
            return new LFComplex(-c._real, -c._imag) ;
        }

        /// <summary>
        /// 复数加法
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static LFComplex operator +(LFComplex c1,LFComplex c2)
        {
            return new LFComplex(c1._real + c2._real, c1._imag + c2._imag);
        }

        /// <summary>
        /// 复数减法
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static LFComplex operator -(LFComplex c1, LFComplex c2)
        {
            return new LFComplex(c1._real - c2._real, c1._imag - c2._imag);
        }

        /// <summary>
        /// 复数乘法
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static LFComplex operator *(LFComplex c1, LFComplex c2)
        {
            double x = c1._real * c2._real - c1._imag * c2._imag;
            double y = c1._real * c2._imag + c1._imag * c2._real;
            return new LFComplex(x, y);
        }

        /// <summary>
        /// 复数除法
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static LFComplex operator /(LFComplex c1, LFComplex c2)
        {
            double e, f, x, y;

            if(Math.Abs(c2._real)>=Math.Abs(c2._imag))
            {
                e = c2._imag / c2._real;
                f = c2._real + e * c2._imag;
                x = (c1._real + c1._imag * e) / f;
                y = (c1._imag - c1._real * e) / f;
            }
            else
            {
                e = c2._real / c2._imag;
                f = c2._imag + e * c2._real;
                x = (c1._real * e + c1._imag) / f;
                y = (c1._imag * e - c1._real) / f;
            }

            return new LFComplex(x, y);
        }
        #endregion

        #region Computing Methods

        /// <summary>
        /// 模
        /// </summary>
        /// <returns></returns>
        public double Norm()
        {
            /* 这种方式可以防止溢出 */
            double x = Math.Abs(_real); 
            double y = Math.Abs(_imag);

            if (_real == 0)
                return y;
            if (_imag == 0)
                return x;

            if (x > y)
                return x * Math.Sqrt(1 + (y / x) * (1 + y / x));

            return y * Math.Sqrt(1 + (x / y) * (1 + x / y));
        }

        /// <summary>
        /// 根
        /// </summary>
        /// <param name="n"></param>
        /// <param name="root"></param>
        public LFComplex[] Root(int n)
        {
            LFComplex[] root;
            if (n < 1)
                return null;

            double q = Math.Atan2(_imag, _real);
            double r = Math.Sqrt(_real * _real + _imag * _imag);

            if (r != 0)
            {
                r = (1.0 / n) * Math.Log(r);
                r = Math.Exp(r);
            }

            root = new LFComplex[n];
            for (int k = 0; k <n; k++)
            {
                double t = (2.0 * k * Math.PI + q) / n;
                root[k] = new LFComplex(r * Math.Cos(t), r * Math.Sin(t));
            }

            return root;
        }

        /// <summary>
        /// 实幂指数
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public LFComplex Pow(double w)
        {
            double r, t;

            if (_real == 0 && _imag == 0)
                return new LFComplex(0, 0);

            if(_real == 0)
            {
                if (_imag > 0)
                    t = Math.PI / 2;
                else
                    t = -Math.PI / 2;
            }
            else
            {
                if (_real > 0)
                    t = Math.Atan2(_imag, _real)+Math.PI;
                else
                    t = Math.Atan2(_imag, _real)-Math.PI;
            }

            r = Math.Exp(w * Math.Log(Math.Sqrt(_real * _real + _imag * _imag)));

            return new LFComplex(r * Math.Cos(w * t), r * Math.Sin(w * t));
        }


        #endregion

        public override string ToString()
        {
            if (_imag == 0)
                return _real.ToString();
            else if(_imag >0)
            {
                return _real.ToString()+"+"+_imag.ToString()+"i";
            }
            else
            {
                return _real.ToString() + "-" + _imag.ToString() + "i";
            }
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}