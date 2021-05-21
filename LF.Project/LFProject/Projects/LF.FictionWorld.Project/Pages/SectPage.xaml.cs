/*──────────────────────────────────────────────────────────────
 * FileName     : SectPage.cs
 * Created      : 2021-05-18 11:09:13
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

        LFDepartment CurrentDepartment = new LFDepartment();
        #endregion

        #region Constructors
        public SectPage()
        {
            InitializeComponent();

            /* 绑定数据 */
            DataContext = World.Data;
            DtgSects.ItemsSource = World.Data.SectList;
            DtgSects.SelectedIndex = 0;
            TxbCount.Text = World.Data.SectList.Count.ToString();
            TrvSectStruct.ItemsSource = World.Data.Sect.Struct;
        }
        #endregion

        #region Methods

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
                // 角色列表数据绑定
                DtgRoles.ItemsSource = World.Data.Sect.Roles;
                // 组织结构数据绑定
                TrvSectStruct.ItemsSource = World.Data.Sect.Struct;
            }
        }


        /// <summary>
        /// 添加对象
        /// </summary>
        public void AddObj()
        {
            SectDialog form = new SectDialog
            {
                Title = "新建势力 - " + (World.Data.SectList.Count + 1).ToString()
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList.Add(form.Sect.Clone());
                World.Data.Sect = form.Sect.Clone();
                DtgSects.SelectedIndex = World.Data.SectList.Count - 1;
                TxbCount.Text = World.Data.SectList.Count.ToString();
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
                Title = "编辑势力 - " + World.Data.Sect.Name
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList[ID] = form.Sect.Clone();
                World.Data.Sect = form.Sect.Clone();
                DtgSects.SelectedIndex = DataGridIndex;
            }
        }

        /// <summary>
        /// 刷新部门
        /// </summary>
        public void RefreshDep()
        {
            CurrentDepartment = (LFDepartment)TrvSectStruct.SelectedItem;
        }

        /// <summary>
        /// 新建部门
        /// </summary>
        public void AddDep()
        {
            DepartmentDialog form = new DepartmentDialog
            {
                Title = "新建部门"
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
            }
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        public void EditDep()
        {
            DepartmentDialog form = new DepartmentDialog
            {
                Department = CurrentDepartment,
                Title = "编辑部门 - " + CurrentDepartment.Title
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {

            }
        }
        #endregion

        #region Events
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
            // 删除对象
        }

        /// <summary>
        /// 添加部门Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDep_Click(object sender, RoutedEventArgs e)
        {
            AddDep();   // 添加部门
        }

        /// <summary>
        /// 编辑部门Button单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditDep_Click(object sender, RoutedEventArgs e)
        {
            EditDep();  // 编辑部门
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
        #endregion


    }
}
