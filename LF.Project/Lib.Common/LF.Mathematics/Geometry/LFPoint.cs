/*──────────────────────────────────────────────────────────────
 * FileName     : LFPoint.cs
 * Created      : 2021-05-15 14:36:49
 * Author       : Xu Zhe
 * Description  : 二维点
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics
{
    /// <summary>
    /// 二维点
    /// </summary>
    public class LFPoint:LFVector, ICloneable
    {
        #region Fields
        #endregion

        #region Properties

        /// <summary>
        /// 坐标X
        /// </summary>
        public double X
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// 坐标Y
        /// </summary>
        public double Y
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        #endregion

        #region Constructors
        public LFPoint()
            : base(VectorType.Col, 2)
        {
        }

        public LFPoint(double x, double y)
            : base(VectorType.Col, new double[2] { x, y })
        {

        }

        public LFPoint(LFPoint rhs)
            : base(rhs.Type, (double[])rhs.Elements.Clone())
        {

        }

        public new LFPoint Clone()
        {
            return new LFPoint(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Methods
        public static LFPoint operator +(LFPoint v1, LFPoint v2)
        {
            LFPoint r = new LFPoint();
            r[0] = v1[0] + v2[0];
            r[1] = v1[1] + v2[1];
            return r;
        }

        public static LFPoint operator -(LFPoint v1, LFPoint v2)
        {
            LFPoint r = new LFPoint();
            r[0] = v1[0] - v2[0];
            r[1] = v1[1] - v2[1];
            return r;
        }

        public static LFPoint operator *(double k, LFPoint v)
        {
            LFPoint r = new LFPoint();
            r[0] = k * v[0];
            r[1] = k * v[1];
            return r;
        }

        public static double Cross(LFPoint p1, LFPoint p2)
        {
            return p1.X * p2.Y - p2.X * p1.Y;
        }

        /// <summary>
        /// 叉乘
        /// >0为逆时针
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static double Cross(LFPoint p1, LFPoint p2, LFPoint p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
        }

        public static double Distance(LFPoint p1, LFPoint p2)
        {
            return (p1 - p2).Norm();
        }

        /// <summary>
        /// 凸包算法
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<LFPoint> GetConvhull(List<LFPoint> points)
        {
            List<LFPoint> hullPoints = new List<LFPoint>();
            int top = 2;
            LFPoint tmp;
            int n = points.Count;

            int k = 0;
            // 选取points中y坐标最小的点，如果这样的点由多个，则取最左边的一个
            for (int i = 0; i < n; i++)
            {
                if ((points[i].Y <= points[k].Y) && (points[i].X < points[k].X))
                {
                    k = i;
                }
            }

            tmp = points[0];
            points[0] = points[k];
            points[k] = tmp;

            for (int i = 1; i < n - 1; i++)
            {
                k = i;
                for (int j = i + 1; j < n; j++)
                {
                    if ((Cross(points[j], points[k], points[0]) >= 0)
                        && (Distance(points[0], points[j]) < (Distance(points[0], points[k]))))
                    {
                        k = j;
                    }
                }
                tmp = points[i];
                points[i] = points[k];
                points[k] = tmp;
            }

            hullPoints.Add(points[0]);
            hullPoints.Add(points[1]);
            hullPoints.Add(points[2]);

            for (int i = 3; i < n; i++)
            {
                while (top > 1 && Cross(points[i], hullPoints[top], hullPoints[top - 1]) >= 0)
                    top--;
                hullPoints.Add(points[i]);
                top++;
            }

            return hullPoints;
        }

        #endregion


        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}