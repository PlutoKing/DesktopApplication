/*──────────────────────────────────────────────────────────────
 * FileName     : Point.cs
 * Created      : 2021-05-10 15:41:15
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using INFITF;
using MECMOD;
using HybridShapeTypeLib;

namespace LF.CatiaDrawing
{
    public partial class LFCatia
    {
        
        /// <summary>
        /// 绘制点：由三维坐标x,y,z绘制点
        /// </summary>
        /// <param name="x">坐标x</param>
        /// <param name="y">坐标y</param>
        /// <param name="z">坐标z</param>
        /// <returns>点的参考</returns>
        public Reference DrawPoint(double x, double y, double z)
        {
            HybridShapePointCoord shape = Shapes.AddNewPointCoord(x, y, z);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 绘制点：基于参考点绘制相对坐标点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Reference DrawPoint(Reference p, double x, double y, double z)
        {
            HybridShapePointCoord shape = Shapes.AddNewPointCoord(x, y, z);
            shape.PtRef = p;
            Shapes.GSMVisibility(p, 0);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 绘制点：取两点中点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Reference DrawPoint(Reference p1, Reference p2)
        {
            HybridShapePointBetween shape = Shapes.AddNewPointBetween(p1, p2, 0.5, 1);
            Shapes.GSMVisibility(p1, 0);
            Shapes.GSMVisibility(p2, 0);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }
    }
}