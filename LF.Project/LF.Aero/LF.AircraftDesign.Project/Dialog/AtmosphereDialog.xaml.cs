/*──────────────────────────────────────────────────────────────
 * FileName     : AtmosphereDialog.cs
 * Created      : 2021-06-09 21:28:25
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

namespace LF.AircraftDesign.Project.Dialog
{
    /// <summary>
    /// AtmosphereDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AtmosphereDialog : Window
    {
        #region Fields

        #endregion

        #region Constructors
        public AtmosphereDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
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
        #endregion
    }
}
