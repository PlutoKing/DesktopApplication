/*──────────────────────────────────────────────────────────────
 * FileName     : Line.cs
 * Created      : 2021-05-10 15:43:17
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
        /// 绘制直线：两点连线
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Reference DrawLine(Reference p1, Reference p2)
        {
            HybridShapeLinePtPt shape = Shapes.AddNewLinePtPt(p1, p2);
            Body.AppendHybridShape(shape);

            /* 隐藏两点 */
            Shapes.GSMVisibility(p1, 0);
            Shapes.GSMVisibility(p2, 0);

            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }
    }
}