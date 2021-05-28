/*──────────────────────────────────────────────────────────────
 * FileName     : ItemDialog.cs
 * Created      : 2021-05-25 21:51:59
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
    /// ItemDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ItemDialog : Window
    {
        #region Fields
        public LFItem Item = new LFItem();
        #endregion

        #region Constructors
        public ItemDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Item;

            CmbLevel.ItemsSource = World.Setting.Levels;
            CmbType1.ItemsSource = World.Setting.Items;

            ListAllItems.ItemsSource = World.Data.ItemList;
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
            Item.Encode();
            this.DialogResult = true;
            Close();
        }

        private void CmbType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbType2.ItemsSource = ((LFType)(CmbType1.SelectedItem)).Childs;
        }
        #endregion
    }
}
