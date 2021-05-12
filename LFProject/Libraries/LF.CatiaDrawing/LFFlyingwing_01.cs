/*──────────────────────────────────────────────────────────────
 * FileName     : LFFlyingwing_01.cs
 * Created      : 2021-05-10 16:08:06
 * Author       : Xu Zhe
 * Description  : 飞翼01型号，外形来自Z-84小飞翼
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
    /// 飞翼01型号
    /// 单位为mm
    /// </summary>
    public class LFFlyingwing_01
    {
        #region Fields

        public LFCatia Catia = new LFCatia();

        #region 参考面或参考方向
        private Reference refXY;
        private Reference refYZ;
        private Reference refZX;

        private Reference PlaneU;
        private Reference PlaneD;
        #endregion

        #region 几何参数
        /// <summary>
        /// 机翼翼型
        /// </summary>
        public LFAirfoil[] AirfoilW;

        /// <summary>
        /// 翼型缩放比例
        /// </summary>
        public double[] AirfoilWUScale;
        public double[] AirfoilWDScale;



        /// <summary>
        /// 机翼各段宽度
        /// </summary>
        public double[] BW;

        /// <summary>
        /// 机翼截面弦长
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

        /// <summary>
        /// 垂尾翼型
        /// </summary>
        public LFAirfoil AirfoilV;

        /// <summary>
        /// 后缘默认厚度
        /// </summary>
        public double[] ZtW;
        /// <summary>
        /// 电机半径（mm）
        /// </summary>
        public double RE = 15; 
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

        #endregion

        #region 控制点
        /// <summary>
        /// 机翼上表面控制点
        /// Upper
        /// </summary>
        Reference[][] PointWU = new Reference[6][];
        /// <summary>
        /// 机翼下表面控制点
        /// Down(不用Lower防止与Left混淆)
        /// </summary>
        Reference[][] PointWD = new Reference[6][];

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
        Reference[] SectionWU = new Reference[6];

        /// <summary>
        /// 机翼下表面线条
        /// </summary>
        Reference[] SectionWD = new Reference[6];
        
        /// <summary>
        /// 机翼后缘线
        /// </summary>
        Reference[] SectionWTe = new Reference[6];

        /// <summary>
        /// 机翼界面线
        /// </summary>
        Reference[] SectionW = new Reference[6];

        /// <summary>
        /// 机翼上表面线条（左侧）
        /// </summary>
        Reference[] SectionWUL = new Reference[2];
        Reference[] SectionWDL = new Reference[2];
        Reference[] SectionWL = new Reference[2];



        #endregion
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFFlyingwing_01()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 绘制
        /// </summary>
        public void Draw()
        {
            Init();

            /* 参考面 */
            refXY = Catia.SetPlaneXY();
            refYZ = Catia.SetPlaneYZ();
            refZX = Catia.SetPlaneZX();



            DrawWingSection();
            DrawWingSurface();
        }


        /// <summary>
        /// 绘制机翼截面
        /// </summary>
        public void DrawWingSection()
        {
           for(int i = 0; i < 6; i++)
            {
                PointWU[i] = Catia.DrawAirfoilPoints(AirfoilW[i], CW[i], XW[i], YW[i], ZW[i], ThetaW[i], PhiW[i], PsiW[i], AirfoilWUScale[i], true);
                SectionWU[i] = Catia.DrawSpline(PointWU[i]);
                PointWD[i] = Catia.DrawAirfoilPoints(AirfoilW[i], CW[i], XW[i], YW[i], ZW[i], ThetaW[i], PhiW[i], PsiW[i], AirfoilWDScale[i], false);
                SectionWD[i] = Catia.DrawSpline(PointWD[i]);
                SectionWTe[i] = Catia.DrawLine(PointWU[i].Last<Reference>(), PointWD[i].Last<Reference>());
                SectionW[i] = Catia.Join(new Reference[] { SectionWU[i], SectionWD[i], SectionWTe[i] });
            }

            SectionWUL[0] = Catia.GetSymmetry(SectionWU[1], refZX);
            SectionWDL[0] = Catia.GetSymmetry(SectionWD[1], refZX);
            for (int i = 0; i < 2; i++)
            {
                PointWLeL[i] = Catia.GetSymmetry(PointWU[i + 1][0], refZX);
                PointWUL[i] = new Reference[4];
                PointWDL[i] = new Reference[4];

                for (int j = 0; j < 3; j++)
                {
                    PointWUL[i][j] = Catia.GetSymmetry(PointWU[i + 1][LinePointX[j]], refZX);
                    PointWDL[i][j] = Catia.GetSymmetry(PointWD[i + 1][LinePointX[j]], refZX);
                }
            }

        }

        public void DrawWingSurface()
        {

        }

        #endregion

        #region Initialization

        /// <summary>
        /// 参数初始化
        /// </summary>
        public void Init()
        {
            AirfoilW = new LFAirfoil[6] { new LFAirfoil("S3010",RE), new LFAirfoil("S3010",0.1), new LFAirfoil("S3010", 0.1), new LFAirfoil("S3010", 0.1), new LFAirfoil("S3010", 0.1), new LFAirfoil("S3010", 0.1) };
            AirfoilWUScale = new double[6] { 1.5, 1.2, 1, 1, 1, 1 };
            AirfoilWDScale = new double[6] { 2.5, 1.2, 1, 1, 1, 1 }; 

            AirfoilV = new LFAirfoil("NACA0009");   // 垂尾翼型

            BW = new double[5] { 50, 50, 200, 0, 0 };
            CW = new double[6] { 360, 280, 200, 100, 30, 20 };
            AsW = new double[7] { 50, 50, 35, 60, 80, 24, 38 };
            AhW = new double[5] { 0, 0, 2, 15, 30 };
            
            BW[3] = (CW[3] - CW[4]) / ((Math.Tan(AsW[3] * Math.PI / 180)) - (Math.Tan(AsW[5] * Math.PI / 180)));
            BW[4] = (CW[4] - CW[5]) / ((Math.Tan(AsW[4] * Math.PI / 180)) - (Math.Tan(AsW[6] * Math.PI / 180)));

            XW = new double[6];
            XW[0] = 0; //-cw[i] * 0.5
            for (int i = 1; i < 6; i++)
            {
                XW[i] = XW[i - 1] + BW[i - 1] * Math.Tan(AsW[i - 1] * Math.PI / 180);
            }

            YW = new double[6];
            YW[0] = 0;
            for (int i = 1; i < 6; i++)
            {
                YW[i] = YW[i - 1] + BW[i - 1];
            }

            ZW = new double[6];
            ZW[0] = 0;
            for (int i = 1; i < 6; i++)
            {
                ZW[i] = ZW[i - 1] + BW[i - 1] * Math.Tan(AhW[i - 1] * Math.PI / 180);
            }

            ThetaW = new double[6];
            PhiW = new double[6];
            PsiW = new double[6];

            PhiW[4] = -30;
            PhiW[5] = -30;

        }
        #endregion

        #region Defaults
        #endregion
    }
}