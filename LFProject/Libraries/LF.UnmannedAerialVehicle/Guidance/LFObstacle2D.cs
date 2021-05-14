/*──────────────────────────────────────────────────────────────
 * FileName     : LFObstacle2D.cs
 * Created      : 2021-05-13 15:32:00
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF.Mathematics;
using LF.Algorithm;

namespace LF.UnmannedAerialVehicle
{
    /// <summary>
    /// 二维障碍物描述
    /// </summary>
    public class LFObstacle2D : LFPolygon, ICloneable,INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// 定义属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _index;

        #endregion

        #region Properties
        public int Index { get => _index; set => _index = value; }

        #endregion

        #region Constructors
        public LFObstacle2D()
             : base()
        {

        }

        public LFObstacle2D(List<LFPoint2D> points)
            : base(points)
        {
        }

        public LFObstacle2D(LFObstacle2D rhs)
            : base(rhs)
        {

        }

        public LFObstacle2D Clone()
        {
            return new LFObstacle2D(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取修正路径
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public LFRoute2D GetRoute(LFLine2D line)
        {
            LFPolygon expand = this.Expand(1);
            if (expand.IsIntersectWith(line))
            {
                // 获取所有顶点
                List<LFPoint2D> points = new List<LFPoint2D>();
                points.Add(line.Start);
                points.Add(line.End);
                foreach (LFPoint2D p in expand.Points)
                {
                    points.Add(p);
                }

                int n = points.Count;

                // 构建图邻接矩阵
                LFGraph<LFPoint2D> graph = new LFGraph<LFPoint2D>(n);

                int cnt = 0;
                foreach (LFPoint2D p in points)
                {
                    LFNode<LFPoint2D> node = new LFNode<LFPoint2D>(p);
                    graph.SetNode(cnt, node);
                    cnt++;
                }

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        LFLine2D tmp = new LFLine2D(points[i], points[j]);
                        if (!this.IsIntersectWith(tmp))
                        {
                            double d = (points[j] - points[i]).Norm();
                            graph.SetEdge(i, j, d);
                        }
                    }
                }
                int[,] pathMatrix = new int[n, n];
                double[] shortPath = new double[n];
                graph.ShortestRoute(ref pathMatrix, ref shortPath, 0);

                List<LFPoint2D> route = new List<LFPoint2D>();

                for (int i = 0; i < n; i++)
                {
                    if (pathMatrix[1, i] != -1)
                    {
                        route.Add(points[pathMatrix[1, i]]);
                    }
                    else
                    {
                        break;
                    }
                }

                return new LFRoute2D(route);
            }
            else
                return new LFRoute2D(line.Start, line.End);
        }


        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}