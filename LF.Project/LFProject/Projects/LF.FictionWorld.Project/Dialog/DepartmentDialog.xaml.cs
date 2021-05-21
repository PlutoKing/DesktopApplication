/*──────────────────────────────────────────────────────────────
 * FileName     : DepartmentDialog.cs
 * Created      : 2021-05-20 15:58:10
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
    /// DepartmentDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DepartmentDialog : Window
    {
        #region Fields
        public LFDepartment Department = new LFDepartment();
        #endregion

        #region Constructors
        public DepartmentDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private void LoadObj()
        {
            this.DataContext = Department;

            DtgAssistants.ItemsSource = Department.Leader.Assistants;
            DtgMembers.ItemsSource = Department.Members;
        }

        #endregion

        #region Events
        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            LoadObj();

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

        }


        #endregion
    }
}
