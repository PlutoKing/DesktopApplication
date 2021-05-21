/*──────────────────────────────────────────────────────────────
 * FileName     : SitePage.cs
 * Created      : 2021-05-17 15:59:56
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
    /// SitePage.xaml 的交互逻辑
    /// </summary>
    public partial class SitePage : UserControl
    {
        #region Fields
        int DataGridIndex = 0;      // 列表选择索引号
        int ID = 0;                 // 对象ID
        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SitePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void LoadObj()
        {
            this.DataContext = World.Data;

            DtgSites.ItemsSource = World.Data.SiteList;
            DtgSites.SelectedIndex = 0;
        }

        private void RefreshObj()
        {
            World.Data.Site = (LFSite)DtgSites.SelectedItem;
            if (World.Data.Site != null)
            {
                DtgSubSites.ItemsSource = World.Data.Site.SubSites;
                ID = World.Data.SiteList.IndexOf(World.Data.Site);
                DataGridIndex = DtgSites.SelectedIndex;
                DtgSiteItems.ItemsSource = World.Data.Site.Items;
                DtgSiteSects.ItemsSource = World.Data.Site.Sects;
            }
        }
        
        public void AddObj()
        {
            /* 打开地点编辑对话框 */
            SiteDialog sd = new SiteDialog
            {
                Title = "新建地点 - " + (World.Data.SiteList.Count + 1).ToString()
            };
            sd.ShowDialog();

            /* 回传数据 */
            if (sd.DialogResult == true)
            {
                World.Data.SiteList.Add(sd.Site.Clone());
                World.Data.Site = sd.Site.Clone();
                DtgSites.SelectedIndex = World.Data.SiteList.Count - 1;
                TxbCount.Text = World.Data.SiteList.Count.ToString();
            }
        }

        public void EditObj()
        {
            /* 打开地点编辑对话框，并载入数据 */
            SiteDialog sd = new SiteDialog
            {
                Site = World.Data.Site.Clone(),
                Title = "编辑势力 - " + World.Data.Site.Name
            };
            sd.ShowDialog();

            /* 回传数据 */
            if (sd.DialogResult == true)
            {
                World.Data.SiteList[ID] = sd.Site.Clone();
                World.Data.Site = sd.Site.Clone();
                DtgSites.SelectedIndex = DataGridIndex;
                World.Data.Site.CheckItems();
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
        /// 地点列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgSites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();
        }

        /// <summary>
        /// 添加按钮点击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddObj();
        }

        /// <summary>
        /// 编辑按钮点击时发生
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

        }

        /// <summary>
        /// 双击列表时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgSites_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }

        #endregion

    }
}
