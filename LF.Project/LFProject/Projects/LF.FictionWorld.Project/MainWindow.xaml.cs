/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-05-17 17:46:41
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
            /* 加载数据 */
            World.Open();

            this.DataContext = World.Info;

            
        }
        #endregion

        #region Methods

        #endregion

        #region Events

        /// <summary>
        /// 菜单按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            /* 保存数据 */
            World.Save();
            /* 关闭应用 */
            this.Close();
        }

        #endregion

    }
}
