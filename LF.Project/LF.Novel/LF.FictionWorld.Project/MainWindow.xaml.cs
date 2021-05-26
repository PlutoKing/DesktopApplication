/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-05-25 11:16:42
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

namespace LF.FictionWorld.Project
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            World.Open();
        }

        /// <summary>
        /// 菜单按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            /* 保存数据 */
            World.Save();
            /* 关闭应用 */
            Close();
        }


        /// <summary>
        /// 保存按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            World.Save();   // 保存世界
            itemPage.KeepObj();
        }

        /// <summary>
        /// 全屏切换按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFull_Click(object sender, RoutedEventArgs e)
        {
            if (WindowStyle != WindowStyle.None)
            {
                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Normal;

                Left = 0.0;
                Top = 0.0;
                Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                ResizeMode = ResizeMode.CanResize;
                WindowState = WindowState.Maximized;
            }
        }
        #endregion
    }
}
