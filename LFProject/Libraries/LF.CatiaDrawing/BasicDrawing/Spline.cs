/*──────────────────────────────────────────────────────────────
 * FileName     : Spline.cs
 * Created      : 2021-05-10 15:46:26
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
        /// 绘制样条线：多点无约束
        /// </summary>
        /// <param name="points">参考点</param>
        /// <returns></returns>
        public Reference DrawSpline(Reference[] points)
        {
            int n = points.Length;
            HybridShapeSpline shape = Shapes.AddNewSpline();
            shape.SetSplineType(0);
            shape.SetClosing(0);
            for (int i = 0; i < n; i++)
            {
                shape.AddPointWithConstraintExplicit(points[i], null, -1.0f, 1, null, 0);
                Shapes.GSMVisibility(points[i], 0);
            }
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();

            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 绘制样条线：与某一直线相切
        /// </summary>
        /// <param name="points">参考点</param>
        /// <param name="line">约束直线</param>
        /// <param name="d">相切方向</param>
        /// <returns></returns>
        public Reference DrawSpline(Reference[] points, Reference line, int d)
        {
            int n = points.Length;
            HybridShapeSpline shape = Shapes.AddNewSpline();
            shape.SetSplineType(0);
            shape.SetClosing(0);

            shape.AddPointWithConstraintFromCurve(points[0], line, 1.0f, d, 0);
            Shapes.GSMVisibility(points[0], 0);
            Shapes.GSMVisibility(line, 0);

            for (int i = 1; i < n; i++)
            {
                shape.AddPointWithConstraintExplicit(points[i], null, -1.0f, 1, null, 0);
                Shapes.GSMVisibility(points[i], 0);
            }
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();

            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 绘制样条线：与两条直线相切
        /// </summary>
        /// <param name="points">参考点</param>
        /// <param name="line1">直线1</param>
        /// <param name="d1">相切方向1</param>
        /// <param name="line2">直线2</param>
        /// <param name="d2">相切方向2</param>
        /// <returns></returns>
        public Reference DrawSpline(Reference[] points, Reference line1, int d1, Reference line2, int d2)
        {
            int n = points.Length;
            HybridShapeSpline shape = Shapes.AddNewSpline();
            shape.SetSplineType(0);
            shape.SetClosing(0);
            shape.AddPointWithConstraintFromCurve(points[0], line1, 1.0f, d1, 0);
            Shapes.GSMVisibility(points[0], 0);
            Shapes.GSMVisibility(line1, 0);
            if (n > 2)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    shape.AddPointWithConstraintExplicit(points[i], null, -1.0f, 1, null, 0);
                    Shapes.GSMVisibility(points[i], 0);
                }
            }
            shape.AddPointWithConstraintFromCurve(points[n - 1], line2, 1.0f, d2, 0);
            Shapes.GSMVisibility(points[n - 1], 0);
            Shapes.GSMVisibility(line2, 0);

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();

            return Part.CreateReferenceFromObject(shape);
        }
    }
}