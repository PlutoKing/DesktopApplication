/*──────────────────────────────────────────────────────────────
 * FileName     : Operating.cs
 * Created      : 2021-05-10 15:54:19
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
    /// <summary>
    /// 基础操作
    /// </summary>
    public partial class LFCatia
    {
        /// <summary>
        /// 设定参考平面XY
        /// </summary>
        /// <returns></returns>
        public Reference SetPlaneXY()
        {
            OriginElements element = Part.OriginElements;
            HybridShapePlaneExplicit shape = (HybridShapePlaneExplicit)element.PlaneXY;
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 设定参考平面YZ
        /// </summary>
        /// <returns></returns>
        public Reference SetPlaneYZ()
        {
            OriginElements element = Part.OriginElements;
            HybridShapePlaneExplicit shape = (HybridShapePlaneExplicit)element.PlaneYZ;
            return Part.CreateReferenceFromObject(shape);
        }
        /// <summary>
        /// 设定参考平面ZX
        /// </summary>
        /// <returns></returns>
        public Reference SetPlaneZX()
        {
            OriginElements element = Part.OriginElements;
            HybridShapePlaneExplicit shape = (HybridShapePlaneExplicit)element.PlaneZX;
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 设定参考平面：与参考面相距d
        /// </summary>
        /// <param name="plane">一直参考面</param>
        /// <param name="d">距离</param>
        /// <returns></returns>
        public Reference SetPlane(Reference plane, double d)
        {
            HybridShapePlaneOffset shape = Shapes.AddNewPlaneOffset(plane, d, false);
            Shapes.GSMVisibility(plane, 0);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();

            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 缝合
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Reference Join(Reference[] obj)
        {
            int n = obj.Length;
            HybridShapeAssemble shape = Shapes.AddNewJoin(obj[0], obj[1]);

            shape.SetConnex(true);
            shape.SetManifold(true);
            shape.SetSimplify(false);
            shape.SetSuppressMode(false);
            shape.SetDeviation(0.001);
            shape.SetAngularToleranceMode(false);
            shape.SetAngularTolerance(0.5);
            shape.SetFederationPropagation(0);
            Shapes.GSMVisibility(obj[0], 0);
            Shapes.GSMVisibility(obj[1], 0);
            if (n > 2)
            {
                for (int i = 2; i < n; i++)
                {
                    shape.AddElement(obj[i]);
                    Shapes.GSMVisibility(obj[i], 0);
                }
            }

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();

            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 对称
        /// </summary>
        /// <param name="refshape"></param>
        /// <param name="refPlane"></param>
        /// <returns></returns>
        public Reference GetSymmetry(Reference refshape, Reference refPlane)
        {
            HybridShapeSymmetry shape = Shapes.AddNewSymmetry(refshape, refPlane);
            shape.VolumeResult = false;
            Shapes.GSMVisibility(refPlane, 0);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 裁剪
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cutObj"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public Reference Cut(Reference obj, Reference cutObj, int d)
        {
            HybridShapeSplit shape = Shapes.AddNewHybridSplit(obj, cutObj, d);
            Shapes.GSMVisibility(obj, 0);
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 倒圆角
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="d1"></param>
        /// <param name="l2"></param>
        /// <param name="d2"></param>
        /// <param name="r"></param>
        /// <param name="boc"></param>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public Reference GetCorner(Reference l1, int d1, Reference l2, int d2, double r, int boc, int o1, int o2)
        {
            HybridShapeDirection hybridShapeDirection1 = Shapes.AddNewDirectionByCoord(0, 0, 0);
            HybridShapeCorner shape = Shapes.AddNew3DCorner(l1, l2, hybridShapeDirection1, r, d1, d2, false);
            shape.DiscriminationIndex = 1;

            shape.BeginOfCorner = boc;
            shape.FirstTangentOrientation = o1;
            shape.SecondTangentOrientation = o2;

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 相交
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <param name="near"></param>
        /// <returns></returns>
        public Reference GetIntersection(Reference s1, Reference s2, Reference near)
        {
            HybridShapeIntersection shape = Shapes.AddNewIntersection(s1, s2);
            shape.PointType = 0;
            Reference r = Part.CreateReferenceFromObject(shape);

            HybridShapeNear shape1 = Shapes.AddNewNear(r, near);

            Shapes.GSMVisibility(s1, 0);
            Shapes.GSMVisibility(s2, 0);
            Shapes.GSMVisibility(near, 0);

            Body.AppendHybridShape(shape1);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape1);
        }

        /// <summary>
        /// 填充
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Reference Fill(Reference s)
        {
            HybridShapeFill shape = Shapes.AddNewFill();
            shape.AddBound(s);
            Shapes.GSMVisibility(s, 0);
            shape.Continuity = 0;
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 填充
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Reference Fill(Reference[] s)
        {
            int n = s.Length;
            HybridShapeFill shape = Shapes.AddNewFill();
            for (int i = 0; i < n; i++)
            {
                shape.AddBound(s[i]);
                Shapes.GSMVisibility(s[i], 0);
            }
            shape.Continuity = 0;
            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        /// <summary>
        /// 桥接
        /// </summary>
        /// <param name="s1">参考面1</param>
        /// <param name="l1">参考线1</param>
        /// <param name="d1">方向1</param>
        /// <param name="s2">参考面2</param>
        /// <param name="l2">参考线2</param>
        /// <param name="d2">方向2</param>
        /// <returns></returns>
        public Reference Blend(Reference s1, Reference l1, int d1, Reference s2, Reference l2, int d2)
        {
            HybridShapeBlend shape = Shapes.AddNewBlend();
            shape.Coupling = 1;

            shape.SetCurve(1, l1);
            shape.SetOrientation(1, 1);
            shape.SetSupport(1, s1);
            shape.SetTransition(1, d1);
            shape.SetContinuity(1, 1);
            shape.SetTrimSupport(1, 1);
            shape.SetBorderMode(1, 1);
            shape.SetTensionInDouble(1, 1, 0, 0);


            shape.SetCurve(2, l2);

            shape.SetOrientation(2, 1);
            shape.SetSupport(2, s2);
            shape.SetTransition(2, d2);
            shape.SetContinuity(2, 1);
            shape.SetTrimSupport(2, 1);
            shape.SetBorderMode(2, 1);
            shape.SetTensionInDouble(2, 1, 0, 0);

            shape.SmoothAngleThresholdActivity = false;
            shape.SmoothDeviationActivity = false;
            shape.RuledDevelopableSurface = false;

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return Part.CreateReferenceFromObject(shape);
        }

        public HybridShapeBlend GetBlend(Reference s1, Reference l1, int d1, Reference s2, Reference l2, int d2)
        {
            HybridShapeBlend shape = Shapes.AddNewBlend();
            shape.Coupling = 1;

            shape.SetCurve(1, l1);
            shape.SetOrientation(1, 1);
            shape.SetSupport(1, s1);
            shape.SetTransition(1, d1);
            shape.SetContinuity(1, 1);
            shape.SetTrimSupport(1, 1);
            shape.SetBorderMode(1, 1);
            shape.SetTensionInDouble(1, 1, 0, 0);


            shape.SetCurve(2, l2);

            shape.SetOrientation(2, 1);
            shape.SetSupport(2, s2);
            shape.SetTransition(2, d2);
            shape.SetContinuity(2, 1);
            shape.SetTrimSupport(2, 1);
            shape.SetBorderMode(2, 1);
            shape.SetTensionInDouble(2, 1, 0, 0);

            shape.SmoothAngleThresholdActivity = false;
            shape.SmoothDeviationActivity = false;
            shape.RuledDevelopableSurface = false;

            Body.AppendHybridShape(shape);
            Part.InWorkObject = shape;
            Part.Update();
            return shape;
        }

    }
}