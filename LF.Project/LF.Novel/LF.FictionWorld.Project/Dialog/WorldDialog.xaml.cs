/*──────────────────────────────────────────────────────────────
 * FileName     : WorldDialog.cs
 * Created      : 2021-05-29 15:59:20
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

namespace LF.FictionWorld.Project.Dialog
{
    /// <summary>
    /// WorldDialog.xaml 的交互逻辑
    /// </summary>
    public partial class WorldDialog : Window
    {
        #region Fields

        #endregion

        #region Constructors
        public WorldDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events

        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// 确认编辑 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
        #endregion
    }
}
