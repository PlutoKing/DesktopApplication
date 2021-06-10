/*──────────────────────────────────────────────────────────────
 * FileName     : SectPage.cs
 * Created      : 2021-05-25 22:08:35
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
    /// SectPage.xaml 的交互逻辑
    /// </summary>
    public partial class SectPage : UserControl
    {
        #region Fields

        int DataGridIndex = 0;
        int ID = 0;

        #endregion

        #region Constructors
        public SectPage()
        {
            InitializeComponent();

            /* 绑定数据 */
            DataContext = World.Data;
           
        }
        #endregion

        #region Methods

        public void LoadObj()
        {
            DtgSects.ItemsSource = World.Data.SectList;
            DtgSects.SelectedIndex = 0;
        }

        /// <summary>
        /// 刷新对象
        /// </summary>
        public void RefreshObj()
        {
            World.Data.Sect = (LFSect)DtgSects.SelectedItem;
            if (World.Data.Sect != null)
            {
                ID = World.Data.SectList.IndexOf(World.Data.Sect);
                DataGridIndex = DtgSects.SelectedIndex;

                DtgRoles.ItemsSource = World.Data.Sect.RoleList;
            }
        }


        /// <summary>
        /// 添加对象
        /// </summary>
        public void AddObj()
        {
            SectDialog form = new SectDialog
            {
                Title = "新建势力",
                SubTitle = (World.Data.SectList.Count + 1).ToString()
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList.AddSect(form.Sect.Clone());
                World.Data.Sect = form.Sect.Clone();
                DtgSects.SelectedIndex = World.Data.SectList.Count - 1;
            }
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        private void EditObj()
        {
            SectDialog form = new SectDialog
            {
                Sect = World.Data.Sect.Clone(),
                Title = "编辑势力",
                SubTitle = World.Data.Sect.Name
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList.EditSect(ID, form.Sect);

                DtgSects.SelectedIndex = DataGridIndex;
            }
        }

        private void DeleteObj()
        {
            MessageDialog msg = new MessageDialog();
            msg.Title = "删除提示";
            msg.TxbMsg.Text = "是否删除【势力：" + World.Data.Sect.Name + "】?";
            msg.ShowDialog();

            if (msg.DialogResult == true)
            {
                World.Data.SectList.DeleteSect(World.Data.Sect);
                World.Data.Sect = World.Data.SectList.Last();
                DtgSects.SelectedIndex = World.Data.SectList.Count - 1;
            }
        }

        /// <summary>
        /// 刷新部门
        /// </summary>
        public void RefreshDep()
        {
            
        }

        /// <summary>
        /// 新建部门
        /// </summary>
        public void AddDep()
        {
            
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        public void EditDep()
        {
            
        }
        #endregion

        #region Events

        /// <summary>
        /// 空间加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadObj();
        }

        /// <summary>
        /// 列表选择项变化是发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgSects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();  // 加载对象
        }

        /// <summary>
        /// 添加Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddObj();   // 添加对象
        }

        /// <summary>
        /// 编辑Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditObj();  // 编辑对象
        }

        /// <summary>
        /// 删除Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteObj();    // 删除对象
        }

        /// <summary>
        /// 添加部门Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDep_Click(object sender, RoutedEventArgs e)
        {
            AddDep();       // 添加部门
        }

        /// <summary>
        /// 编辑部门Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditDep_Click(object sender, RoutedEventArgs e)
        {
            EditDep();      // 编辑部门
        }

        /// <summary>
        /// 删除部门Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteDep_Click(object sender, RoutedEventArgs e)
        {
            // 删除部门
        }

        /// <summary>
        /// 组织结构TreeView选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrvSectStruct_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RefreshDep();  // 重新加载部门
        }

        private void DtgSects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }
        #endregion


    }
}
