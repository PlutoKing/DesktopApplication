/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-04-27 15:15:39
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

namespace LF.NotePad.Project
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
        #endregion

        private void CkbIsShowTool_Checked(object sender, RoutedEventArgs e)
        {
            GridTool.Height = new GridLength(30);
        }

        private void CkbIsShowTool_Unchecked(object sender, RoutedEventArgs e)
        {
            GridTool.Height = new GridLength(0);
        }

        private void CkbIsShowStatus_Checked(object sender, RoutedEventArgs e)
        {
            GridStatus.Height = new GridLength(30);
        }

        private void CkbIsShowStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            GridStatus.Height = new GridLength(0);
        }
    }
}
