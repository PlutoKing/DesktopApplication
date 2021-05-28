/*──────────────────────────────────────────────────────────────
 * FileName     : BookPage.cs
 * Created      : 2021-05-25 22:08:06
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

        /// <summary>
        /// 加载对象
        /// </summary>
        private void LoadObj()
        {
            DtgBooks.ItemsSource = World.Data.BookList;
            DataContext = World.Data;

            DtgBooks.SelectedIndex = 0;
        }

        /// <summary>
        /// 刷新对象
        /// </summary>
        private void RefreshObj()
        {
            World.Data.Book = (LFBook)DtgBooks.SelectedItem;
            if (World.Data.Book != null)
            {
                ID = World.Data.BookList.IndexOf(World.Data.Book);
                DataGridIndex = DtgBooks.SelectedIndex;

                DtgContent.ItemsSource = World.Data.Book.Content;
                //DtgSkills.ItemsSource = World.Data.Book.Skills;
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        public void AddObj()
        {
            BookDialog dialog = new BookDialog
            {
                Title = "新建秘籍",
                SubTitle = (World.Data.BookList.Count + 1).ToString()
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
        /// 编辑对象
        /// </summary>
        public void EditObj()
        {
            BookDialog dialog = new BookDialog
            {
                Book = World.Data.Book.Clone(),
                Title = "编辑秘籍",
                SubTitle = World.Data.Book.Name
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.BookList.EditBook(ID,dialog.Book);
                World.Data.Book = dialog.Book.Clone();
                DtgBooks.SelectedIndex = DataGridIndex;
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        private void DeleteObj()
        {
            MessageDialog msg = new MessageDialog();
            msg.Title = "删除提示";
            msg.TxbMsg.Text = "是否删除【秘籍：" + World.Data.Book.Name + "】?";
            msg.ShowDialog();

            if (msg.DialogResult == true)
            {
                World.Data.BookList.Remove(World.Data.Book);
                World.Data.Book = World.Data.BookList.Last();
                DtgBooks.SelectedIndex = World.Data.BookList.Count - 1;
                TxbCount.Text = World.Data.BookList.Count.ToString();
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
        /// 列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();
        }

        /// <summary>
        /// 添加按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddObj();
        }

        /// <summary>
        /// 编辑按钮单击时发生
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
        /// 双击列表时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }
        #endregion

    }
}
