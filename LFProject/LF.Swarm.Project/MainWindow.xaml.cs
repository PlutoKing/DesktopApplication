/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-05-12 21:00:05
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LF.UnmannedAerialVehicle;

namespace LF.Swarm.Project
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        public static LFMap2D map;
        Map2DFile file;

        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            file = new Map2DFile(AppDomain.CurrentDomain.BaseDirectory);
            map = file.ReadFile();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            homePage.LoadMap(map);
            spacePage.LoadMap(map);
        }
    }
}
