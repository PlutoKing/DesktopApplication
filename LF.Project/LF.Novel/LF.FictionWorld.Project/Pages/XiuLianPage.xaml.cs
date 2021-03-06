/*──────────────────────────────────────────────────────────────
 * FileName     : XiuLianPage.cs
 * Created      : 2021-05-30 20:37:27
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LF.FictionWorld.Project.Pages
{
    /// <summary>
    /// XiuLianPage.xaml 的交互逻辑
    /// </summary>
    public partial class XiuLianPage : UserControl
    {
        #region Fields

        #endregion

        #region Constructors
        public XiuLianPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DtgRanks.ItemsSource = World.Config.Xiulian.RankList;
        }
    }
}
