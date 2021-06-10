/*──────────────────────────────────────────────────────────────
 * FileName     : SiteDialog.cs
 * Created      : 2021-05-28 10:37:35
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
    /// SiteDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SiteDialog : Window
    {
        #region Fields

        public LFSite Site = new LFSite();

        LFItemList items;
        #endregion

        #region Constructors
        public SiteDialog()
        {
            InitializeComponent();


        }
        #endregion

        #region Methods

        /// <summary>
        /// 添加物品
        /// </summary>
        public void AddItem()
        {
            LFItem item = (LFItem)ListAllItems.SelectedItem;
            if (item != null)
            {
                items.Remove(item);
                Site.ItemList.Add(new LFPointer(item));
            }
        }

        /// <summary>
        /// 移除物品
        /// </summary>
        private void RemoveItem()
        {
            LFPointer p = (LFPointer)ListItems.SelectedItem;
            if (p != null)
            {
                items.Add(p.GetItem());
                Site.ItemList.Remove(p);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 窗口加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /* 绑定数据 */
            DataContext = Site;

            DtgSubSites.ItemsSource = Site.SubSites;

            CmbAdd1.ItemsSource = World.Setting.Areas;

            items = World.Data.ItemList.Minus(Site.ItemList);
            ListAllItems.ItemsSource = items;
            ListItems.ItemsSource = Site.ItemList;

            //ListAllSects.ItemsSource = sects;
            //ListSects.ItemsSource = Site.Sects;
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
            Site.Encode();
            this.DialogResult = true;
            Close();
        }

        private void CmbAdd1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbAdd2.ItemsSource = ((LFType)CmbAdd1.SelectedItem).Childs;

        }

        /// <summary>
        /// 二级区域Combo选项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAdd2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbAdd2.SelectedItem != null)
            {
                CmbAdd3.ItemsSource = ((LFType)CmbAdd2.SelectedItem).Childs;
            }
            else
            {
                CmbAdd3.ItemsSource = null;
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem();
        }

        private void BtnAddSect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveSect_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void BtnAddSub_Click(object sender, RoutedEventArgs e)
        {
            LFSubSite sub = new LFSubSite
            {
                ID = Site.SubSites.Count + 1
            };
            Site.SubSites.Add(sub);
        }

        private void BtnDeleteSub_Click(object sender, RoutedEventArgs e)
        {
            LFSubSite sub = (LFSubSite)DtgSubSites.SelectedItem;
            if (Site.SubSites.Contains(sub))
            {
                Site.SubSites.Remove(sub);
            }
        }
        #endregion

    }
}
