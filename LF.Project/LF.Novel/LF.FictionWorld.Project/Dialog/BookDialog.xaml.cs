/*──────────────────────────────────────────────────────────────
 * FileName     : BookDialog.cs
 * Created      : 2021-05-27 19:51:39
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
    /// BookDialog.xaml 的交互逻辑
    /// </summary>
    public partial class BookDialog : Window
    {
        #region Fields

        public LFBook Book = new LFBook();

        public string SubTitle = "";
        #endregion

        #region Constructors
        public BookDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private void AddLevel()
        {

        }

        #endregion

        #region Events
        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Book;

            CmbLevel.ItemsSource = World.Setting.Levels;
            CmbType.ItemsSource = World.Setting.Books;

            DtgContent.ItemsSource = Book.Content;
            DtgSkills.ItemsSource = Book.Skills;

            TxbSubTitle.Text = SubTitle;
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
            Book.Encode();
            this.DialogResult = true;
            Close();
        }


        #endregion

        private void BtnAddLevel_Click(object sender, RoutedEventArgs e)
        {
            LFLevel lv = new LFLevel
            {
                ID = (byte)(Book.Content.Count + 1)
            };
            Book.Content.Add(lv);
        }

        private void BtnDeleteLevel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddSkill_Click(object sender, RoutedEventArgs e)
        {
            LFSkill skill = new LFSkill()
            {
                ID = (byte)(Book.Skills.Count + 1),
            };
            Book.Skills.Add(skill);
        }
    }
}
