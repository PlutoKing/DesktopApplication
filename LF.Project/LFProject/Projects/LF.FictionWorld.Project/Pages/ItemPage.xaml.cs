/*──────────────────────────────────────────────────────────────
 * FileName     : ItemPage.cs
 * Created      : 2021-05-18 11:04:16
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
    /// ItemPage.xaml 的交互逻辑
    /// </summary>
    public partial class ItemPage : UserControl
    {
        #region Fields
        int DataGridIndex = 0;
        int ID = 0;

        #endregion

        #region Constructors
        public ItemPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DtgItems.ItemsSource = World.Data.ItemList;
            DtgItems.SelectedIndex = 0;

            this.DataContext = World.Data;
            //TxbCount.Text = World.Data.ItemList.Count.ToString();
        }

        private void DtgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            World.Data.Item = (LFItem)DtgItems.SelectedItem;
            if (World.Data.Item != null)
            {
                ID = World.Data.ItemList.IndexOf(World.Data.Item);
                DataGridIndex = DtgItems.SelectedIndex;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ItemDialog dialog = new ItemDialog();
            dialog.Title = "新建物品 - " + (World.Data.ItemList.Count + 1).ToString();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.ItemList.Add(dialog.Item.Clone());
                World.Data.Item = dialog.Item.Clone();
                DtgItems.SelectedIndex = World.Data.ItemList.Count - 1;
               

            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ItemDialog dialog = new ItemDialog();
            dialog.Item = World.Data.Item.Clone();
            dialog.Title = "编辑物品 - " + World.Data.Item.Name;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.ItemList[ID] = dialog.Item.Clone();
                World.Data.Item = dialog.Item.Clone();
                DtgItems.SelectedIndex = DataGridIndex;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否删除【物品：" + World.Data.Item.Name + "】", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                World.Data.ItemList.Remove(World.Data.Item);
                World.Data.Item = World.Data.ItemList.Last();
                DtgItems.SelectedIndex = World.Data.ItemList.Count - 1;
            }
        }

        #endregion
    }
}
