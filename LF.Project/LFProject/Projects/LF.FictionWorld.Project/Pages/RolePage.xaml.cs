/*──────────────────────────────────────────────────────────────
 * FileName     : RolePage.cs
 * Created      : 2021-05-16 12:13:21
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
using LF.FictionWorld.Project.Dialog;

namespace LF.FictionWorld.Project.Pages
{
    /// <summary>
    /// RolePage.xaml 的交互逻辑
    /// </summary>
    public partial class RolePage : UserControl
    {
        #region Fields
        int DataGridIndex = 0;
        int ID = 0;

        #endregion

        #region Constructors
        public RolePage()
        {
            InitializeComponent();

            /* 绑定数据 */
            this.DataContext = World.Data;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 加载
        /// </summary>
        private void LoadObj()
        {
            /* 绑定角色列表数据源 */
            DtgRoles.ItemsSource = World.Data.RoleList;
            /* 设置默认选择项 */
            DtgRoles.SelectedIndex = 0;

            TxbNowCount.Text = "";
        }

        /// <summary>
        /// 加载部分
        /// </summary>
        private void LoadSubObj()
        {
            DtgRoles.ItemsSource = World.Data.RoleList.GetNowRole();
            TxbNowCount.Text = World.Data.RoleList.GetNowRole().Count + " /";
            /* 设置默认选择项 */
            DtgRoles.SelectedIndex = 0;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void RefreshObj()
        {
            /* 选择项发生变化时，更新当前角色 */
            World.Data.Role = (LFRole)DtgRoles.SelectedItem;
            /* 当前角色不为空时，加载角色信息 */
            if (World.Data.Role != null)
            {
                /* 升级信息 */
                DtgRanks.ItemsSource = World.Data.Role.Ranks;
                DtgExps.ItemsSource = World.Data.Role.Experiences;

                ID = World.Data.RoleList.IndexOf(World.Data.Role);
                DataGridIndex = DtgRoles.SelectedIndex;
            }
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        private void AddObj()
        {
            RoleDialog dialog = new RoleDialog
            {
                Title = "新建角色 - " + (World.Data.RoleList.Count + 1).ToString()
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                /* 数据中添加 */
                World.Data.RoleList.Add(dialog.Role.Clone());
                /* 刷新当前 */
                World.Data.Role = dialog.Role.Clone();
                /* 列表选项变化 */
                DtgRoles.SelectedIndex = World.Data.RoleList.Count - 1;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void EditObj()
        {
            RoleDialog dialog = new RoleDialog
            {
                Role = World.Data.Role.Clone(),
                Title = "编辑角色 - " + World.Data.Role.Name
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.RoleList[ID] = dialog.Role.Clone();
                World.Data.Role = dialog.Role.Clone();
                DtgRoles.SelectedIndex = DataGridIndex;
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        private void DeleteObj()
        {
            if (MessageBox.Show("是否删除【角色：" + World.Data.Role.Name + "】", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                World.Data.RoleList.Remove(World.Data.Role);
                World.Data.Role = World.Data.RoleList.Last();
                DtgRoles.SelectedIndex = World.Data.RoleList.Count - 1;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 控件加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadObj();
        }

        /// <summary>
        /// 角色列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();
        }

        /// <summary>
        /// 添加按钮点击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddObj();
        }

        /// <summary>
        /// 编辑按钮点击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditObj();
        }

        /// <summary>
        /// 删除按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteObj();
        }

        /// <summary>
        /// 列表双击时发生，调用编辑函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }

        /// <summary>
        /// 显示当前CheckBox选中时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkbShowNow_Checked(object sender, RoutedEventArgs e)
        {
            LoadSubObj();
        }

        /// <summary>
        /// 显示当前CheckBox未选中时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkbShowNow_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadObj();
        }
        #endregion


    }
}
