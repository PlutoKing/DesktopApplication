/*──────────────────────────────────────────────────────────────
 * FileName     : SiteDialog.cs
 * Created      : 2021-05-18 15:51:18
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
        LFSectList sects;
        #endregion

        #region Constructors
        public SiteDialog()
        {
            InitializeComponent();

            items = World.Data.ItemList.Clone();
            sects = World.Data.SectList.Clone();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Site;
            DtgSubSites.ItemsSource = Site.SubSites;

            CmbAdd1.ItemsSource = World.Setting.Areas;

            items = items.Minus(Site.Items).Clone();
            ListAllItems.ItemsSource = items;
            ListItems.ItemsSource = Site.Items;

            ListAllSects.ItemsSource = sects;
            ListSects.ItemsSource = Site.Sects;
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
            LFItem item = (LFItem)ListAllItems.SelectedItem;
            if (item != null)
            {
                items.Remove(item);
                Site.Items.Add(item);
            }
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            LFItem item = (LFItem)ListItems.SelectedItem;
            if (item != null)
            {
                items.Add(item);
                Site.Items.Remove(item);
            }
        }

        private void BtnAddSect_Click(object sender, RoutedEventArgs e)
        {
            LFSect obj = (LFSect)ListAllSects.SelectedItem;
            if (obj != null)
            {
                sects.Remove(obj);
                Site.Sects.Add(obj);
            }
        }

        private void BtnRemoveSect_Click(object sender, RoutedEventArgs e)
        {
            LFSect obj = (LFSect)ListSects.SelectedItem;
            if (obj != null)
            {
                sects.Add(obj);
                Site.Sects.Remove(obj);
            }
        }


        private void BtnAddSub_Click(object sender, RoutedEventArgs e)
        {
            LFSubSite sub = new LFSubSite();
            sub.ID = Site.SubSites.Count + 1;
            Site.SubSites.Add(sub);
        }

        private void BtnDeleteSub_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
