/*──────────────────────────────────────────────────────────────
 * FileName     : BookPage.cs
 * Created      : 2021-05-18 11:12:23
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
    /// BookPage.xaml 的交互逻辑
    /// </summary>
    public partial class BookPage : UserControl
    {
        #region Fields
        int DataGridIndex = 0;
        int ID = 0;

        #endregion

        #region Constructors
        public BookPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events

        /// <summary>
        /// 控件加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DtgBooks.ItemsSource = World.Data.BookList;
            this.DataContext = World.Data;

            DtgBooks.SelectedIndex = 0;

        }

        /// <summary>
        /// 列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            World.Data.Book = (LFBook)DtgBooks.SelectedItem;
            if (World.Data.Book != null)
            {
                ID = World.Data.BookList.IndexOf(World.Data.Book);
                DataGridIndex = DtgBooks.SelectedIndex;
            }
        }

        /// <summary>
        /// 添加按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookDialog dialog = new BookDialog
            {
                Title = "新建功法 - " + (World.Data.BookList.Count + 1).ToString()
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.BookList.Add(dialog.Book.Clone());
                World.Data.Book = dialog.Book.Clone();
                DtgBooks.SelectedIndex = World.Data.BookList.Count - 1;
            }
        }

        /// <summary>
        /// 编辑按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            BookDialog dialog = new BookDialog
            {
                Book = World.Data.Book.Clone(),
                Title = "编辑功法 - " + World.Data.Book.Name
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.BookList[ID] = dialog.Book.Clone();
                World.Data.Book = dialog.Book.Clone();
                DtgBooks.SelectedIndex = DataGridIndex;
            }
        }

        /// <summary>
        /// 删除按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否删除【功法：" + World.Data.Book.Name + "】", "删除提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                World.Data.BookList.Remove(World.Data.Book);
                World.Data.Book = World.Data.BookList.Last();
                DtgBooks.SelectedIndex = World.Data.BookList.Count - 1;
                TxbCount.Text = World.Data.BookList.Count.ToString();
            }
        }
        #endregion


    }
}
