/*──────────────────────────────────────────────────────────────
 * FileName     : HomePage.cs
 * Created      : 2021-05-12 21:14:08
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using LF.Figure;
using LF.UnmannedAerialVehicle;
using LF.Mathematics;

namespace LF.Swarm.Project.Pages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        #region Fields

        public static FigureControl MainMap;
        public static Overlay DroneOverlay;

        LFMap2D mainMap;
        LineItem startItem;
        LineItem endItem;

        #endregion

        #region Constructors
        public HomePage()
        {
            InitializeComponent();

            MainMap = map;
            map.Figure.Broder.IsVisible = true;
            map.Figure.Broder.Line.Color = Color.FromArgb(172, 172, 172);
            map.Figure.XAxis.Title.Text = "x/m";
            map.Figure.YAxis.Title.Text = "y/m";
            map.Figure.XAxis.Scale.Max = 1000;
            map.Figure.YAxis.Scale.Max = 1000;

            map.Figure.IsAxisEqual = true;
            map.Figure.XAxis.Scale.MajorStep = 100;
            map.Figure.XAxis.Scale.MajorStepAuto = false;
            map.Figure.YAxis.Scale.MajorStep = 100;
            map.Refresh();
        }
        #endregion

        #region Methods
        public void LoadMap(LFMap2D map)
        {

            mainMap = map;

            RefreshMap();

        }

        /// <summary>
        /// 更新地图
        /// </summary>
        public void RefreshMap()
        {
            foreach (Overlay o in MainMap.Figure.Overlays)
            {
                o.Items.Clear();
            }
            startItem = MainMap.Figure.PlotPoint(new List<LFPoint2D>() { mainMap.Start });
            startItem.Line.Color = Color.Red;
            startItem.Symbol.Type = SymbolType.Star;
            startItem.Symbol.Size = 14;
            endItem = MainMap.Figure.PlotPoint(new List<LFPoint2D>() { mainMap.End });
            endItem.Line.Color = Color.LimeGreen;
            endItem.Symbol.Type = SymbolType.Triangle;
            endItem.Symbol.Size = 14;
            LineItem item3 = MainMap.Figure.PlotPoint(mainMap.Waypoints);
            item3.Line.Color = Color.Blue;
            item3.Symbol.Type = SymbolType.Circle;
            foreach (LFObstacle2D ob in mainMap.Obstacle2DList)
            {
                MainMap.Figure.PlotPolygon(ob);
            }
            MainMap.Refresh();


        }
        #endregion

        #region Events
        #endregion
    }
}
