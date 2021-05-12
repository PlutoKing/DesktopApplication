/*──────────────────────────────────────────────────────────────
 * FileName     : Circle.cs
 * Created      : 2021-05-10 15:44:22
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
        /// 绘制圆：圆心半径
        /// </summary>
        /// <param name="plane">参考平面</param>
        /// <param name="c">圆心</param>
        /// <param name="r">半径</param>
        /// <returns></returns>
        public Reference DrawCircle(Reference plane, Reference c, double r)
        {
            HybridShapeCircleCtrRad shape = Shapes.AddNewCircleCtrRadWithAngles(c, plane, false, r, 0, 360);
            shape.SetLimitation(0);
            Body.AppendHybridShape(shape);
            Shapes.GSMVisibility(c, 0);
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 绘制圆弧：圆心半径和起止角度
        /// </summary>
        /// <param name="plane">参考平面</param>
        /// <param name="c">圆心</param>
        /// <param name="r">半径</param>
        /// <param name="a1">起始角度</param>
        /// <param name="a2">终止角度</param>
        /// <returns></returns>
        public Reference DrawCircle(Reference plane, Reference c, double r, double a1, double a2)
        {
            HybridShapeCircleCtrRad shape = Shapes.AddNewCircleCtrRadWithAngles(c, plane, false, r, a1, a2);
            shape.SetLimitation(0);
            Body.AppendHybridShape(shape);
            Shapes.GSMVisibility(plane, 0);
            Shapes.GSMVisibility(c, 0);
            return Part.CreateReferenceFromObject(shape);
        }
    }
}