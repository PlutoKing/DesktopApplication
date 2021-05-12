/*──────────────────────────────────────────────────────────────
 * FileName     : Loft.cs
 * Created      : 2021-05-10 15:51:29
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
        /// 绘制多界面曲面：基于参考面
        /// </summary>
        /// <param name="s">参考面</param>
        /// <returns></returns>
        public Reference DrawLoft(Reference[] s)
        {
            int ns = s.Length;
            HybridShapeLoft shape = Shapes.AddNewLoft();
            shape.SectionCoupling = 1;
            shape.Relimitation = 1;
            shape.CanonicalDetection = 2;

            for (int i = 0; i < ns; i++)
            {
                shape.AddSectionToLoft(s[i], 1, null);
                Shapes.GSMVisibility(s[i], 0);
            }

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);

        }

        /// <summary>
        /// 绘制多截面曲面：基于参考面和引导线
        /// </summary>
        /// <param name="s">参考面</param>
        /// <param name="g">引导线</param>
        /// <returns></returns>
        public Reference DrawLoft(Reference[] s, Reference[] g)
        {
            int ns = s.Length;
            int ng = g.Length;
            HybridShapeLoft shape = Shapes.AddNewLoft();
            shape.SectionCoupling = 1;
            shape.Relimitation = 1;
            shape.CanonicalDetection = 2;

            for (int i = 0; i < ns; i++)
            {
                shape.AddSectionToLoft(s[i], 1, null);
                Shapes.GSMVisibility(s[i], 0);
            }

            for (int i = 0; i < ng; i++)
            {
                shape.AddGuide(g[i]);
                Shapes.GSMVisibility(g[i], 0);
            }
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);

        }
    }
}