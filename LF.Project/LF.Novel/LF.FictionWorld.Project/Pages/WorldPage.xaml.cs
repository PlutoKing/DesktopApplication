/*──────────────────────────────────────────────────────────────
 * FileName     : WorldPage.cs
 * Created      : 2021-05-25 17:01:20
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
    /// WorldPage.xaml 的交互逻辑
    /// </summary>
    public partial class WorldPage : UserControl
    {
        #region Fields

        #endregion

        #region Constructors
        public WorldPage()
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
            DataContext = World.Info;
            DtgEras.ItemsSource = World.Info.Calendar.Eras;
            DtgMonths.ItemsSource = World.Info.Calendar.Months;
            DtgWeeks.ItemsSource = World.Info.Calendar.Weeks;
            DtgMoons.ItemsSource = World.Info.Calendar.MoonList;
        }
    }
}
