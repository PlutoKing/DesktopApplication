/*──────────────────────────────────────────────────────────────
 * FileName     : LFParticle.cs
 * Created      : 2021-05-13 11:40:45
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Algorithm
{
    public class LFParticle:ICloneable
    {
        #region Fields
        private double[] _x;    // 状态
        private double[] _v;    // 速度
        private double _y;      // 值
        #endregion

        #region Properties
        /// <summary>
        /// 状态
        /// </summary>
        public double[] X { get => _x; set => _x = value; }

        /// <summary>
        /// 速度
        /// </summary>
        public double[] V { get => _v; set => _v = value; }

        /// <summary>
        /// 值
        /// </summary>
        public double Y { get => _y; set => _y = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFParticle()
        {
            _x = new double[1];
            _v = new double[1];
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="x"></param>
        public LFParticle(double x)
        {
            _x = new double[] { x };
            _v = new double[1];
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="n"></param>
        public LFParticle(int n)
        {
            _x = new double[n];
            _v = new double[n];
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="x"></param>
        public LFParticle(double[] x)
        {
            _x = x;
            _v = new double[x.Length];
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFParticle(LFParticle obj)
        {
            _x = obj._x;
            _v = obj._v;
            _y = obj._y;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFParticle Clone()
        {
            return new LFParticle(this);
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

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}