/*──────────────────────────────────────────────────────────────
 * FileName     : RoleDialog.cs
 * Created      : 2021-05-18 18:40:39
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
    /// RoleDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RoleDialog : Window
    {
        #region Fields

        public LFRole Role = new LFRole();  // 核心数据

        #endregion

        #region Constructors
        public RoleDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Role;
            //LbxAttribute.ItemsSource = World.Attributes;
            CmbRace.ItemsSource = World.Setting.Races;
            CmbAdd1.ItemsSource = World.Setting.Areas;

            CmbSects.ItemsSource = World.Data.SectList;

            DtgRanks.ItemsSource = Role.Ranks;
            DtgExps.ItemsSource = Role.Experiences;
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
            Role.Encode();
            Role.Age = World.Info.NowDate.GetAge(Role.Birthday);
            Role.Ranks.GetValue(World.Info.NowDate);
            Role.CheckRanks();
            Role.CheckExps();


            this.DialogResult = true;
            Close();
        }


        #endregion

        private void CmbRace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbGender.ItemsSource = ((LFType)CmbRace.SelectedItem).Childs;
        }

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

        private void BtnAutoRanks_Click(object sender, RoutedEventArgs e)
        {
            Role.GetRanks();
            DtgRanks.ItemsSource = Role.Ranks;
        }

        private void BtnCheckRanks_Click(object sender, RoutedEventArgs e)
        {
            Role.CheckRanks();
        }

        private void BtnAddExp_Click(object sender, RoutedEventArgs e)
        {
            LFVariable v = new LFVariable
            {
                Date = Role.Birthday.Clone()
            };
            Role.Experiences.Add(v);
        }

    }
}
