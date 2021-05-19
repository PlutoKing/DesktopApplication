/*──────────────────────────────────────────────────────────────
 * FileName     : SectDialog.cs
 * Created      : 2021-05-19 15:47:01
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
    /// SectDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SectDialog : Window
    {
        #region Fields
        public LFSect Sect = new LFSect();
        #endregion

        #region Constructors
        public SectDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Sect;
            CmbAdd1.ItemsSource = World.Setting.Areas;
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
            Sect.Encode();
            Sect.Age = World.Info.NowDate.GetAge(Sect.CreatedDate);
            this.DialogResult = true;
            Close();
        }


        #endregion


        private void CmbAdd1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbAdd2.ItemsSource = ((LFType)CmbAdd1.SelectedItem).Childs;
        }

        private void CmbAdd2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbAdd3.ItemsSource = ((LFType)CmbAdd2.SelectedItem).Childs;
        }

        private void CmbAdd3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbAdd3.SelectedItem != null)
            {
                int add1 = ((LFType)CmbAdd1.SelectedItem).Index;
                int add2 = ((LFType)CmbAdd2.SelectedItem).Index;
                int add3 = ((LFType)CmbAdd3.SelectedItem).Index;

                int add = add1 * 100 + add2 * 10 + add3;
                CmbAdd4.ItemsSource = World.Data.SiteList.GetLocations(add);
            }
        }

    }
}
