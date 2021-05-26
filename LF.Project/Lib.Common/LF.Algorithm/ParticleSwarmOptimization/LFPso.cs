/*──────────────────────────────────────────────────────────────
 * FileName     : LFPso.cs
 * Created      : 2021-05-19 21:36:13
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
    public delegate double PsoTargetFunctionHangle(double[] x);
    public delegate double TargetFunctionHandler(double x);

    public class LFPso
    {
        #region Fields
        private int _pn = 100;    // 粒子数量
        private int _iter = 100;
        private LFParticle[] _p;    // 粒子数组
        private LFParticle[] _pBest;  // 粒子最优解
        private LFParticle _gBest;    // 全局最优解

        private double _vmax = 0.2;   // 最大速度
        private int _c1 = 3;        // 学习参数
        private int _c2 = 3;        // 学习参数
        private double _w = 0.3;
        private double _xRangeMin = 0;
        private double _xRangeMax = 100;

        Random random = new Random();

        public event TargetFunctionHandler TargetFunction;  // 目标函数
        public event PsoTargetFunctionHangle PsoTargetFunction;

        #endregion

        #region Properties
        /// <summary>
        /// 粒子数量
        /// </summary>
        public int Pn { get => _pn; set => _pn = value; }
        public double XRangeMin { get => _xRangeMin; set => _xRangeMin = value; }
        public double XRangeMax { get => _xRangeMax; set => _xRangeMax = value; }
        /// <summary>
        /// 迭代次数
        /// </summary>
        public int Iter { get => _iter; set => _iter = value; }
        /// <summary>
        /// 最大速度
        /// </summary>
        public double Vmax { get => _vmax; set => _vmax = value; }
        /// <summary>
        /// 学习参数
        /// </summary>
        public int C1 { get => _c1; set => _c1 = value; }
        /// <summary>
        /// 学习参数
        /// </summary>
        public int C2 { get => _c2; set => _c2 = value; }
        /// <summary>
        /// 权重
        /// </summary>
        public double W { get => _w; set => _w = value; }

        #endregion

        #region Constructors
        public LFPso()
        {
        }
        #endregion

        #region Methods

        #region 标准粒子群算法
        /// <summary>
        /// 粒子群算法优化一般函数
        /// </summary>
        /// <param name="xOpt"></param>
        /// <param name="yOpt"></param>
        /// <param name="history"></param>
        public void Optimize(out double xOpt, out double yOpt, out double[] history)
        {
            InitializePSO();
            List<double> record = new List<double>();
            double oldBest = 0;
            int holdCnt = 0;    // 优化结果保持不变 计数


            // 开始迭代
            for (int i = 0; i < _iter; i++)
            {
                double w = 0.3; // 惯性权重
                for (int j = 0; j < _pn; j++)
                {
                    // 更新粒子速度
                    double vx = w * _p[j].V[0] + _c1 * random.NextDouble() * (_pBest[j].X[0] - _p[j].X[0]) + _c2 * random.NextDouble() * (_gBest.X[0] - _p[j].X[0]);
                    if (vx > _vmax)
                        vx = _vmax;

                    _p[j].V[0] = vx;

                    // 更新粒子位置，像好的方向进化
                    _p[j].X[0] += _p[j].V[0];

                    if (_p[j].X[0] > _xRangeMax)
                        _p[j].X[0] = _xRangeMax;
                    if (_p[j].X[0] < _xRangeMin)
                        _p[j].X[0] = _xRangeMin;
                }

                for (int k = 0; k < _pn; k++)
                {
                    _p[k].Y = FitnessFunction(_p[k].X[0]);
                }

                oldBest = _gBest.Y;
                for (int j = 0; j < _pn; j++)
                {
                    if (_p[j].Y > _pBest[j].Y)
                    {
                        _pBest[j] = _p[j];
                    }
                    if (_p[j].Y > _gBest.Y)
                    {
                        _gBest = _p[j];
                    }
                }

                if (oldBest == _gBest.Y)
                    holdCnt++;
                else
                    holdCnt = 0;

                if (holdCnt >= 100)
                    break;

                record.Add(_gBest.Y);
            }

            xOpt = _gBest.X[0];
            yOpt = _gBest.Y;
            history = record.ToArray();

        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializePSO()
        {
            _p = new LFParticle[_pn];
            _pBest = new LFParticle[_pn];
            _gBest = new LFParticle();

            for (int i = 0; i < _pn; i++)
            {
                _p[i] = new LFParticle(random.NextDouble() * (_xRangeMax - _xRangeMin) + _xRangeMin);
                _p[i].V[0] = random.NextDouble() * _vmax;
                _p[i].Y = FitnessFunction(_p[i].X[0]);
            }

            _gBest.Y = double.MinValue;
            for (int i = 0; i < _pn; i++)
            {
                _pBest[i] = _p[i];
                if (_p[i].Y > _gBest.Y)
                {
                    _gBest = _p[i];
                }
            }

        }

        public double FitnessFunction(double x)
        {
            if (TargetFunction != null)
            {
                return TargetFunction(x);
            }

            return x * Math.Sin(10 * Math.PI * x) + 2;
        }
        #endregion

        #region 旅行商问题

        List<List<SO>> SOList = new List<List<SO>>();
        private void InitializeSOList(int city)
        {
            for (int i = 0; i < _pn; i++)
            {
                List<SO> list = new List<SO>();
                int n = random.Next(0, city);
                for (int j = 0; j < n; j++)
                {
                    int a = random.Next(0, city);
                    int b = random.Next(0, city);
                    while (b == a)
                    {
                        b = random.Next(0, city);
                    }

                    SO so = new SO(a, b);
                    list.Add(so);
                }
                SOList.Add(list);
            }
        }

        public List<SO> Minus(double[] a, double[] b)
        {
            double[] tmp = (double[])a.Clone();

            List<SO> list = new List<SO>();
            int index = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (tmp[i] != b[i])
                {
                    //在tmp中找到和b[i]相等的值，将下标存储起来
                    for (int j = i + 1; j < tmp.Length; j++)
                    {
                        if (tmp[j] == b[i])
                        {
                            index = j;
                            break;
                        }
                    }
                    SO so = new SO(i, index);
                    list.Add(so);
                }
            }
            return list;
        }

        public void SolveTsp(int city, out int[] xOpt, out double yOpt, out double[] history)
        {
            InitializeTSP(city);

            List<double> record = new List<double>();
            double oldBest = 0;
            int holdCnt = 0;    // 优化结果保持不变 计数


            // 开始迭代
            for (int i = 0; i < _iter; i++)
            {
                InitializeSOList(city);
                // 评估
                oldBest = _gBest.Y;
                for (int j = 0; j < _pn; j++)
                {
                    if (_p[j].Y < _pBest[j].Y)
                    {
                        _pBest[j] = _p[j];  // 局部最优
                    }
                    if (_p[j].Y < _gBest.Y)
                    {
                        _gBest = _p[j];     // 全局最优
                    }
                }

                if (oldBest == _gBest.Y)
                    holdCnt++;
                else
                    holdCnt = 0;

                if (holdCnt >= 100)
                    break;

                record.Add(_gBest.Y);

                // 更新粒子位置

                double w = 0.3; // 惯性权重
                for (int j = 0; j < _pn; j++)
                {
                    List<SO> vii = new List<SO>();
                    int len = (int)(w * SOList[j].Count);// 交换长度
                    for (int k = 0; k < len; k++)
                    {
                        vii.Add(SOList[j][k]);
                    }


                    List<SO> a = Minus(_p[j].X, _pBest[j].X);
                    double ra = random.NextDouble();
                    len = (int)(ra * a.Count);
                    for (int k = 0; k < len; k++)
                    {
                        vii.Add(a[k]);
                    }

                    List<SO> b = Minus(_p[j].X, _gBest.X);
                    double rb = random.NextDouble();
                    len = (int)(rb * b.Count);
                    for (int k = 0; k < len; k++)
                    {
                        vii.Add(b[k]);
                    }

                    SOList.RemoveAt(0);
                    SOList.Add(vii);

                    Exchange(_p[j].X, vii);
                }

                for (int k = 0; k < _pn; k++)
                {
                    _p[k].Y = PsoTargetFunction(_p[k].X);
                }
            }


            xOpt = new int[_gBest.X.Length];
            for (int i = 0; i < city; i++)
            {
                xOpt[i] = (int)_gBest.X[i];
            }
            yOpt = _gBest.Y;
            history = record.ToArray();

        }

        public void Exchange(double[] path, List<SO> vii)
        {
            double tmp;
            foreach (SO so in vii)
            {
                tmp = path[so.a];
                path[so.a] = path[so.b];
                path[so.b] = tmp;
            }
        }

        public void InitializeTSP(int city)
        {

            _p = new LFParticle[_pn];
            _pBest = new LFParticle[_pn];
            _gBest = new LFParticle();

            // 随机初始解
            for (int i = 0; i < _pn; i++)
            {
                _p[i] = new LFParticle(city);
                int[] codes = LF<int>.RandArray(city);
                for (int j = 0; j < city; j++)
                {
                    _p[i].X[j] = (double)codes[j];
                }
                _p[i].V[0] = random.NextDouble() * _vmax;
                _p[i].Y = PsoTargetFunction(_p[i].X);
            }

            _gBest.Y = double.MinValue;
            for (int i = 0; i < _pn; i++)
            {
                _pBest[i] = _p[i];
                if (_p[i].Y > _gBest.Y)
                {
                    _gBest = _p[i];
                }
            }

        }

        public struct SO
        {
            public int a;
            public int b;

            public SO(int a, int b)
            {
                this.a = a;
                this.b = b;
            }
        }
        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}