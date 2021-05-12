/*──────────────────────────────────────────────────────────────
 * FileName     : LFCatia.cs
 * Created      : 2021-05-10 15:33:42
 * Author       : Xu Zhe
 * Description  : Catia管理类
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
    /// <summary>
    /// Catia管理类
    /// </summary>
    public partial class LFCatia
    {
        #region Fields
        public Application CatiaApp;            // Catia应用程序
        public PartDocument Doc;                // 零件文件
        public Part Part;                       // 零件
        public HybridBodies Bodies;             // 混合体集合
        public HybridBody Body;                 // 混合体
        public HybridShapeFactory Shapes;       // 混合形状

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFCatia()
        {
        }
        #endregion

        #region Methods

        #region 启动函数
        /// <summary>
        /// 启动CATIA
        /// </summary>
        public void Startup()
        {
            try
            {
                CatiaApp = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch
            {
                Type oType = System.Type.GetTypeFromProgID("CATIA.Application");
                CatiaApp = (INFITF.Application)Activator.CreateInstance(oType);
                CatiaApp.Visible = true;
            }

            try
            {
                Doc = (PartDocument)CatiaApp.ActiveDocument;
            }
            catch { }

            if (Doc == null)
            {
                Doc = (PartDocument)CatiaApp.Documents.Add("Part");
            }
            // 初始化
            Part = Doc.Part;
            Bodies = Part.HybridBodies;
            Body = Bodies.Add();
            Shapes = (HybridShapeFactory)Part.HybridShapeFactory;
        }
        #endregion

        

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}