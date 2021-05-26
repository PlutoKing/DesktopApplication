/*──────────────────────────────────────────────────────────────
 * FileName     : LFXiuLian.cs
 * Created      : 2021-05-25 15:59:27
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF.Mathematics;

namespace LF.FictionWorld
{
    /// <summary>
    /// 修炼模型
    /// </summary>
    public class LFXiuLian : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _levels;            // 登记数
        private double[] _ageLimit;     // 寿限
        private double _limitFactor;    // 限制因子
        private double[] _ranks;        // 等级
        private double[] _params;       // 修炼模型计算参数

        #endregion

        #region Properties

        /// <summary>
        /// 等级
        /// </summary>
        public int Levels
        {
            get => _levels;
            set
            {
                _levels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Levels"));
            }
        }

        /// <summary>
        /// 年龄限制
        /// </summary>
        public double[] AgeLimit
        {
            get => _ageLimit;
            set
            {
                _ageLimit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AgeLimit"));
            }
        }

        /// <summary>
        /// 限制因子
        /// </summary>
        public double LimitFactor
        {
            get => _limitFactor;
            set
            {
                _limitFactor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LimitFactor"));
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public double[] Ranks
        {
            get => _ranks;
            set
            {
                _ranks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ranks"));
            }
        }

        /// <summary>
        /// 修炼模型计算参数
        /// </summary>
        public double[] Params
        {
            get => _params;
            set
            {
                _params = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Params"));
            }
        }

        #endregion

        #region Constructors
        public LFXiuLian()
        {
            _levels = 7;
            _ageLimit = new double[_levels];
            _ranks = new double[_levels];
        }
        #endregion

        #region Methods

        /// <summary>
        /// 计算系数
        /// </summary>
        public void GetParams()
        {
            double[] g = _ageLimit;
            for (int i = 0; i < _levels; i++)
            {
                g[i] = Math.Log(_ageLimit[i] * _limitFactor) / _ranks[i];
            }

            // 多项式拟合，求解模型参数
            LFFitting fitting = new LFFitting();
            fitting.X = new LFVector(VectorType.Col, _ranks);
            fitting.Y = new LFVector(VectorType.Col, g);
            _params = fitting.PolynomialFitting(_levels);
        }

        /// <summary>
        /// 计算修炼年龄
        /// </summary>
        /// <param name="t">天赋值</param>
        /// <param name="l">等级</param>
        /// <returns>年龄</returns>
        public double GetAge(double t,double l)
        {
            // 计算天赋值中间量
            double tmp = 0;
            for(int i = 0; i < _levels; i++)
            {
                tmp += _params[i] * Math.Pow(t, _levels - i);
            }

            double age = 10 * Math.Exp(tmp * (l - 1));
            return age;
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}