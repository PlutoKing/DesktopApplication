/*──────────────────────────────────────────────────────────────
 * FileName     : LFAirfoil.cs
 * Created      : 2021-05-10 15:58:52
 * Author       : Xu Zhe
 * Description  : 翼型（后期应将其归为飞行器命名空间）
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.CatiaDrawing
{
    /// <summary>
    /// 翼型
    /// </summary>
    public class LFAirfoil
    {
        #region Fields

        /// <summary>
        /// 上下翼面控制参数
        /// </summary>
        public readonly double[][] A = new double[2][];

        /// <summary>
        /// 控制点数目
        /// </summary>
        public int Nc = 50;

        /// <summary>
        /// 控制点相对位置
        /// </summary>
        public readonly double[] Psi;

        /// <summary>
        /// 后缘厚度
        /// </summary>
        public double Zt;
        public double S;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFAirfoil(string name):this(name,0.0d)
        {

        }

        public LFAirfoil(string name,double zt)
        {
            switch (name)
            {
                case "S3010":
                    A = Default.AS3010;
                    break;
                case "S5010":
                    A = Default.AS5010;
                    break;
                case "NACA0009":
                    A = Default.ANACA0009;
                    break;
                case "Fuselage":
                    A = Default.F;
                    break;
                default:
                    A = Default.AS5010;
                    break;
            }
            Psi = new double[Nc];
            for (int i = 0; i < Nc; i++)
            {
                Psi[i] = (Math.Sin(i * 1.0 / (Nc - 1) * Math.PI - Math.PI / 2) + 1) / 2;
            }
            Zt = zt;
        }
        #endregion

        #region Methods

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        public struct Default
        {
            public static double[][] AS3010 = new double[][]
            {
                new double[]{ 0.1748,0.7124,-5.3408,19.4486,-38.8055,42.1055,-23.055,4.8424 },
                new double[]{ -0.1135, 0.4416,-2.4782 ,10.0096,-23.3451,30.8517,-21.4175,6.0904}
            };

            public static double[][] AS5010 = new double[][]
            {
                new double[]{ 0.1508,1.0305,-9.1979,40.1532,-96.9707,129.0178,-88.9312,24.7596 },
                new double[]{ -0.0953, 0.3206,-2.5837 ,12.7844,-35.4655,54.1816,-42.3824,13.1829}
            };

            public static double[][] ANACA0009 = new double[][]
            {
                new double[]{ 0.1313,-0.2014,1.5808,-6.361,12.2112,-10.4493,2.3374,0.9155 },
                new double[]{-0.1313, 0.2014,-1.5808 ,6.361,-12.2112,10.4493,-2.3374,-0.9155 }
            };

            public static double[][] F = new double[][]
            {
                new double[]{0.12 },
                new double[]{-0.12 }
            };
        }
        #endregion
    }
}