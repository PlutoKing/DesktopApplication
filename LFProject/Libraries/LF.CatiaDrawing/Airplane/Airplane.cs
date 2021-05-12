/*──────────────────────────────────────────────────────────────
 * FileName     : Airplane.cs
 * Created      : 2021-05-10 16:01:52
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using MECMOD;
using HybridShapeTypeLib;

namespace LF.CatiaDrawing
{
    public partial class LFCatia
    {
        /// <summary>
        /// 绘制翼型点
        /// </summary>
        /// <param name="airfoil">翼型</param>
        /// <param name="c">弦长</param>
        /// <param name="x">位置x</param>
        /// <param name="y">位置y</param>
        /// <param name="z">位置z</param>
        /// <param name="theta">扭转角</param>
        /// <param name="phi">外倾角</param>
        /// <param name="psi">后掠角</param>
        /// <param name="scale">比例</param>
        /// <param name="isUp">是否上翼面</param>
        /// <returns></returns>
        public Reference[] DrawAirfoilPoints(LFAirfoil airfoil, double c, double x, double y, double z, double theta, double phi, double psi, double scale, bool isUp)
        {
            Reference[] r = new Reference[airfoil.Nc];

            double S;   // 形函数

            double x1;
            double y1;
            double z1;

            double x2;
            double y2;
            double z2;

            double ctheta = Math.Cos(theta * Math.PI / 180);
            double stheta = Math.Sin(theta * Math.PI / 180);
            double cphi = Math.Cos(phi * Math.PI / 180);
            double sphi = Math.Sin(phi * Math.PI / 180);
            double cpsi = Math.Cos(psi * Math.PI / 180);
            double spsi = Math.Sin(psi * Math.PI / 180);

            double a11 = ctheta * cpsi;
            double a12 = ctheta * spsi;
            double a13 = -stheta;
            double a21 = stheta * sphi * cpsi - cphi * spsi;
            double a22 = stheta * sphi * spsi + cphi * cpsi;
            double a23 = sphi * ctheta;
            double a31 = stheta * cphi * cpsi + sphi * spsi;
            double a32 = stheta * cphi * spsi - sphi * cpsi;
            double a33 = cphi * ctheta;

            // 上翼面
            if (isUp)
            {
                for (int i = 0; i < airfoil.Nc; i++)
                {
                    S = 0;
                    if (airfoil.A[0].Length == 1)
                    {
                        S = airfoil.A[0][0];
                        x1 = c * airfoil.Psi[i];
                        y1 = 0;
                        z1 = c * Math.Pow(airfoil.Psi[i], 0.7) * Math.Pow((1 - airfoil.Psi[i]), 1) * S * scale + airfoil.Psi[i] * airfoil.Zt;

                    }
                    else
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            S = S + airfoil.A[0][j] * Math.Pow(airfoil.Psi[i], j);
                        }
                        x1 = c * airfoil.Psi[i];
                        y1 = 0;
                        z1 = c * Math.Pow(airfoil.Psi[i], 0.5) * (1 - airfoil.Psi[i]) * S * scale + airfoil.Psi[i] * airfoil.Zt;

                    }



                    x2 = a11 * x1 + a12 * y1 + a13 * z1;
                    y2 = a21 * x1 + a22 * y1 + a23 * z1;
                    z2 = a31 * x1 + a32 * y1 + a33 * z1;

                    r[i] = DrawPoint(x + x2, y + y2, z + z2);
                }
            }
            // 下翼面
            else
            {
                for (int i = 0; i < airfoil.Nc; i++)
                {
                    S = 0;
                    if (airfoil.A[0].Length == 1)
                    {
                        S = airfoil.A[1][0];
                        x1 = c * airfoil.Psi[i];
                        y1 = 0;
                        z1 = c * Math.Pow(airfoil.Psi[i], 0.7) * Math.Pow((1 - airfoil.Psi[i]), 1) * S * scale - airfoil.Psi[i] * airfoil.Zt;
                    }
                    else
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            S = S + airfoil.A[1][j] * Math.Pow(airfoil.Psi[i], j);
                        }
                        x1 = c * airfoil.Psi[i];
                        y1 = 0;
                        z1 = c * Math.Pow(airfoil.Psi[i], 0.5) * (1 - airfoil.Psi[i]) * S * scale - airfoil.Psi[i] * airfoil.Zt;
                    }



                    x2 = a11 * x1 + a12 * y1 + a13 * z1;
                    y2 = a21 * x1 + a22 * y1 + a23 * z1;
                    z2 = a31 * x1 + a32 * y1 + a33 * z1;

                    r[i] = DrawPoint(x + x2, y + y2, z + z2);
                }
            }


            return r;
        }
    }
}