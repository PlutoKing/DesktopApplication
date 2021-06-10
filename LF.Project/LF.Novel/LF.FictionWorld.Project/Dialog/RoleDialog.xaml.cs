/*──────────────────────────────────────────────────────────────
 * FileName     : RoleDialog.cs
 * Created      : 2021-05-29 11:49:43
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// 一级区域更新时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAdd1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 二级区域数据绑定
            CmbAdd2.ItemsSource = ((LFType)CmbAdd1.SelectedItem).Childs;
        }

        private void CmbAdd2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbAdd3.ItemsSource = ((LFType)CmbAdd2.SelectedItem).Childs;
        }

        /// <summary>
        /// 三级区域选项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAdd3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbAdd3.SelectedItem != null)
            {
                int add1 = ((LFType)CmbAdd1.SelectedItem).Code;
                int add2 = ((LFType)CmbAdd2.SelectedItem).Code;
                int add3 = ((LFType)CmbAdd3.SelectedItem).Code;

                int add = add1 * 100 + add2 * 10 + add3;
                CmbAdd4.ItemsSource = World.Data.SiteList.GetSiteGroup(add);
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
