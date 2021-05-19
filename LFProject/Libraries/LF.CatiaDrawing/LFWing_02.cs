﻿/*──────────────────────────────────────────────────────────────
 * FileName     : LFWing_02.cs
 * Created      : 2021-05-13 20:51:46
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
    public class LFWing_02
    {
        #region Fields
        /// <summary>
        /// Catia相关
        /// </summary>
        public LFCatia Catia = new LFCatia();

        /// <summary>
        /// 参考面ZX
        /// </summary>
        private Reference refZX;


        #region 几何参数
        /// <summary>
        /// 机翼翼型
        /// </summary>
        public LFAirfoil AirfoilW;

        /// <summary>
        /// 机翼各段宽度
        /// </summary>
        public double[] BW;

        /// <summary>
        /// 机翼各截面弦长
        /// </summary>
        public double[] CW;

        /// <summary>
        /// 机翼各段后掠角
        /// </summary>
        public double[] AsW;

        /// <summary>
        /// 机翼各段上反角
        /// </summary>
        public double[] AhW;

        /// <summary>
        /// 机翼各段扭转角
        /// </summary>
        public double[] ThetaW;

        /// <summary>
        /// 机翼各段外倾角
        /// </summary>
        public double[] PhiW;

        /// <summary>
        /// 机翼各段偏航角
        /// </summary>
        public double[] PsiW;
        #endregion


        #region 控制点定位参数

        /// <summary>
        /// 机翼各截面前缘点X坐标
        /// </summary>
        public double[] XW;

        /// <summary>
        /// 机翼各截面前缘点Y坐标
        /// </summary>
        public double[] YW;

        /// <summary>
        /// 机翼各截面前缘点Z坐标
        /// </summary>
        public double[] ZW;

        public int[] LinePointX = new int[] { 18, 25, 31, 49 };

        public int Ns = 3;
        #endregion

        #region 控制点

        /// <summary>
        /// 机翼上表面控制点
        /// Upper
        /// </summary>
        Reference[][] PointWU;
        /// <summary>
        /// 机翼下表面控制点
        /// Down(不用Lower防止与Left混淆)
        /// </summary>
        Reference[][] PointWD;

        /// <summary>
        /// 机翼前缘左侧控制点
        /// </summary>
        Reference[] PointWLeL = new Reference[2];
        Reference[][] PointWUL = new Reference[2][];
        Reference[][] PointWDL = new Reference[2][];


        #endregion

        #region 控制线

        /// <summary>
        /// 机翼上表面线条
        /// </summary>
        Reference[] SectionWU = new Reference[3];

        /// <summary>
        /// 机翼下表面线条
        /// </summary>
        Reference[] SectionWD = new Reference[3];

        /// <summary>
        /// 机翼后缘线
        /// </summary>
        Reference[] SectionWTe = new Reference[3];

        /// <summary>
        /// 机翼界面线
        /// </summary>
        Reference[] SectionW = new Reference[3];

        /// <summary>
        /// 机翼上表面线条（左侧）
        /// </summary>
        Reference[] SectionWUL = new Reference[2];
        Reference[] SectionWDL = new Reference[2];
        Reference[] SectionWL = new Reference[2];


        Reference[][] LineW;

        Reference[] GuideW;

        #endregion

        #region 面
        Reference SurfaceWR;
        Reference BlendWR;
        Reference FillWR;
        Reference WingR;
        Reference WingL;
        Reference Wing;
        #endregion


        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFWing_02()
        {
        }
        #endregion

        #region Methods

        public void Draw()
        {
            Init();
            refZX = Catia.SetPlaneZX();

            DrawWing();
        }


        public void DrawWing()
        {
            // 绘制机翼截面
            for (int i = 0; i < Ns; i++)
            {
                PointWU[i] = Catia.DrawAirfoilPoints(AirfoilW, CW[i], XW[i], YW[i], ZW[i], ThetaW[i], PhiW[i], PsiW[i], 1, true);
                SectionWU[i] = Catia.DrawSpline(PointWU[i]);
                PointWD[i] = Catia.DrawAirfoilPoints(AirfoilW, CW[i], XW[i], YW[i], ZW[i], ThetaW[i], PhiW[i], PsiW[i], 1, false);
                SectionWD[i] = Catia.DrawSpline(PointWD[i]);
                SectionWTe[i] = Catia.DrawLine(PointWU[i].Last(), PointWD[i].Last());
                SectionW[i] = Catia.Join(new Reference[] { SectionWU[i], SectionWD[i], SectionWTe[i] });
            }

            LineW[0] = new Reference[2];
            LineW[1] = new Reference[2];
            LineW[2] = new Reference[2];
            for (int j = 0; j < Ns - 1; j++)
            {
                LineW[0][j] = Catia.DrawLine(PointWU[j][0], PointWU[j + 1][0]);
                LineW[1][j] = Catia.DrawLine(PointWU[j].Last(), PointWU[j + 1].Last());
                LineW[2][j] = Catia.DrawLine(PointWD[j].Last(), PointWD[j + 1].Last());
            }

            GuideW[0] = Catia.Join(LineW[0]);
            GuideW[1] = Catia.Join(LineW[1]);
            GuideW[2] = Catia.Join(LineW[2]);

            SurfaceWR = Catia.DrawLoft(SectionW, GuideW);

            HybridShapeBlend BlendWRtmp = Catia.GetBlend(SurfaceWR, SectionWU[Ns - 1], -1, SurfaceWR, SectionWD[Ns - 1], 2);
            BlendWR = Catia.Part.CreateReferenceFromObject(BlendWRtmp);

            Reference tmp = Catia.Doc.Part.CreateReferenceFromBRepName("BorderREdge:(BEdge:(Brp:(GSMBlend.1;(Brp:(GSMCurve.6;2);Brp:(GSMCurve.5;2)));None:(Limits1:();Limits2:();+1);Cf11:());WithPermanentBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", BlendWRtmp);
            FillWR = Catia.Fill(new Reference[] { tmp, SectionWTe[Ns - 1] });

            // 缝合右侧机翼
            WingR = Catia.Join(new Reference[] { SurfaceWR, BlendWR, FillWR });
            // 对称处左侧机翼
            WingL = Catia.GetSymmetry(WingR, refZX);

            // 缝合机翼
            Wing = Catia.Join(new Reference[] { WingR, WingL });
        }
        #endregion

        #region Initialization

        /// <summary>
        /// 参数初始化
        /// </summary>
        public void Init()
        {
            // 配置翼型
            AirfoilW = new LFAirfoil("S3010");
            AirfoilW.Zt = 0.1;

            // 配置机翼形状
            BW = new double[2] { 219.5, 425.7 };
            CW = new double[3] { 225.5, 225.5, 168.6 };
            AsW = new double[2] { 0, 0 };
            AhW = new double[2] { 0, 5 };

            XW = new double[3];
            XW[0] = 0; //-cw[i] * 0.5
            for (int i = 1; i < 3; i++)
            {
                XW[i] = XW[i - 1] + BW[i - 1] * Math.Tan(AsW[i - 1] * Math.PI / 180);
            }

            YW = new double[Ns];
            YW[0] = 0;
            for (int i = 1; i < 3; i++)
            {
                YW[i] = YW[i - 1] + BW[i - 1];
            }

            ZW = new double[Ns];
            ZW[0] = 0;
            for (int i = 1; i < 3; i++)
            {
                ZW[i] = ZW[i - 1] + BW[i - 1] * Math.Tan(AhW[i - 1] * Math.PI / 180);
            }

            ThetaW = new double[Ns];
            PhiW = new double[Ns];
            PsiW = new double[Ns];


            PointWU = new Reference[Ns][];
            PointWD = new Reference[Ns][];

            LineW = new Reference[3][];
            GuideW = new Reference[3];
        }
        #endregion

    }
}