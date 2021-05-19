/*──────────────────────────────────────────────────────────────
 * FileName     : RoleErrorWindow.cs
 * Created      : 2021-05-19 16:41:32
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
    /// RoleErrorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleErrorWindow : Window
    {
        #region Fields

        #endregion

        #region Constructors
        public RoleErrorWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DtgRoles.ItemsSource = World.Data.ErrorRoleList;
        }
    }
}
