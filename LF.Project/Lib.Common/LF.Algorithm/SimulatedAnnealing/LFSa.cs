/*──────────────────────────────────────────────────────────────
 * FileName     : LFSa.cs
 * Created      : 2021-05-19 21:46:22
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
    public delegate double EnergyFunctionHandler(int[] x);

    public class LFSa
    {
        #region Fields
        private double _t0 = 100;       // 初始温度
        private int _k = 100;
        private double _tmin = 1e-8;
        private double _delta = 0.98;   // 变化化率一般取0.95~0.99
        Random random = new Random();
        public event TargetFunctionHandler TargetFunction;
        public event EnergyFunctionHandler EnergyFunction;

        private double _xRangeMin = 0;
        private double _xRangeMax = 100;
        #endregion

        #region Properties
        /// <summary>
        /// 初始温度
        /// </summary>
        public double T0 { get => _t0; set => _t0 = value; }

        /// <summary>
        /// 迭代次数
        /// </summary>
        public int K { get => _k; set => _k = value; }

        /// <summary>
        /// 变化率
        /// </summary>
        public double Delta { get => _delta; set => _delta = value; }

        /// <summary>
        /// 最低温度
        /// </summary>
        public double Tmin { get => _tmin; set => _tmin = value; }
        public double XRangeMin { get => _xRangeMin; set => _xRangeMin = value; }
        public double XRangeMax { get => _xRangeMax; set => _xRangeMax = value; }

        #endregion

        #region Constructors
        public LFSa()
        {
        }
        #endregion

        #region Methods

        #region 标准模拟退火算法
        /// <summary>
        /// 优化
        /// </summary>
        /// <param name="xopt"></param>
        /// <param name="y"></param>
        /// <param name="history"></param>
        public void Optimize(out double xopt, out double y, out double[] history)
        {
            double t = _t0;
            double[] x = new double[_k];
            double[] yArray = new double[_k];
            List<double> record = new List<double>();

            for (int i = 0; i < _k; i++)
            {
                x[i] = random.NextDouble() * (_xRangeMax - _xRangeMax) + _xRangeMin;
            }

            double ymin = double.MaxValue;
            int index = 0;
            double xResult = 0; ;

            double oldBest = 0;
            int holdCnt = 0;    // 优化结果保持不变 计数


            while (t > _tmin)
            {
                for (int i = 0; i < _k; i++)
                {
                    yArray[i] = GetFunctionResult(x[i]);
                    double xnew = x[i] + (random.NextDouble() * 2 - 1) * t;
                    if (xnew >= _xRangeMin && xnew <= _xRangeMax)    // 范围
                    {
                        double funTmpNew = GetFunctionResult(xnew);
                        double p = Metropolis(yArray[i], funTmpNew, T0);
                        if (random.NextDouble() <= p)   // 按概率替换
                        {
                            x[i] = xnew;
                            yArray[i] = funTmpNew;
                        }
                    }
                }

                t = t * _delta;
                double yTmpMin;
                oldBest = ymin;
                LF<double>.GetMin(yArray, out yTmpMin, out index);
                if (ymin > yTmpMin)
                {
                    xResult = x[index];
                    ymin = yTmpMin;
                }

                if (oldBest == ymin)
                    holdCnt++;
                else
                    holdCnt = 0;

                if (holdCnt >= 100)
                    break;

                record.Add(ymin);
            }
            xopt = xResult;
            history = record.ToArray();
            y = ymin;
        }

        public double GetFunctionResult(double x)
        {
            if (TargetFunction != null)
            {
                return TargetFunction(x);
            }

            double result = 6 * Math.Pow(x, 7) + 8 * Math.Pow(x, 6) + 7
               * Math.Pow(x, 3) + 5 * Math.Pow(x, 2) - x * 1;

            return result;
        }
        /// <summary>
        /// Metroplis准则
        /// 状态转移概率
        /// </summary>
        /// <param name="Eold"></param>
        /// <param name="Enew"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public double Metropolis(double Eold, double Enew, double T)
        {
            if (Enew < Eold)
            {
                return 1;
            }
            else
            {
                return Math.Exp(-(Enew - Eold) / T);
            }
        }

        /// <summary>
        /// 冷却过程
        /// </summary>
        /// <param name="T0"></param>
        /// <param name="t"></param>
        /// <param name="isFast"></param>
        /// <returns></returns>
        public double Cooling(double T0, double t, bool isFast)
        {
            if (isFast)
            {
                return T0 / (1 + t);
            }
            else
            {
                return T0 / (Math.Log10(1 + t));
            }
        }
        #endregion

        #region 旅行商问题

        public void SolveTsp(int city, out int[] xOpt, out double yOpt, out double[] history)
        {
            double t = _t0; // 令当前温度为初始温度
            int[][] x = new int[_k][];    // 记录变量
            double[] yArray = new double[_k];
            List<double> record = new List<double>();

            for (int i = 0; i < _k; i++)
            {
                // 随机生成
                x[i] = LF<int>.RandArray(city);
            }

            double ymin = double.MaxValue;
            int index = 0;
            int[] xResult = new int[] { 0 };

            double oldBest = 0;
            int holdCnt = 0;    // 优化结果保持不变 计数


            while (t > _tmin)
            {
                for (int i = 0; i < _k; i++)
                {
                    yArray[i] = EnergyFunction(x[i]);


                    // 更新变量，随机交换连个城市位置
                    int index1 = random.Next(0, x[i].Length);
                    int index2 = random.Next(0, x[i].Length);

                    int[] xnew = LF<int>.ArraySwap(x[i], index1, index2);
                    double funTmpNew = EnergyFunction(xnew);
                    double p = Metropolis(yArray[i], funTmpNew, T0);
                    if (random.NextDouble() <= p)   // 按概率替换
                    {
                        x[i] = xnew;
                        yArray[i] = funTmpNew;
                    }
                }

                t = t * _delta;
                double yTmpMin;
                oldBest = ymin;
                LF<double>.GetMin(yArray, out yTmpMin, out index);
                if (ymin > yTmpMin)
                {
                    xResult = x[index];
                    ymin = yTmpMin;
                }

                if (oldBest == ymin)
                    holdCnt++;
                else
                    holdCnt = 0;

                if (holdCnt >= 100)
                    break;

                record.Add(ymin);
            }
            xOpt = xResult;
            history = record.ToArray();
            yOpt = ymin;
        }

        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}