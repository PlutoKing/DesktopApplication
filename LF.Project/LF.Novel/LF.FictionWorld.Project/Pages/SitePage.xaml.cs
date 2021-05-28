/*──────────────────────────────────────────────────────────────
 * FileName     : SitePage.cs
 * Created      : 2021-05-25 22:08:16
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

            DataContext = World.Data;

        }
        #endregion

        #region Methods

        /// <summary>
        /// 加载对象
        /// </summary>
        private void LoadObj()
        {
            DtgSites.ItemsSource = World.Data.SiteList;
            DtgSites.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载对象
        /// </summary>
        /// <param name="id"></param>
        private void LoadObj(int id)
        {
            DtgSites.ItemsSource = null;
            DtgSites.ItemsSource = World.Data.SiteList;
            DtgSites.SelectedIndex = id;
        }

        /// <summary>
        /// 选择具体对象后刷新界面
        /// </summary>
        private void RefreshObj()
        {
            World.Data.Site = (LFSite)DtgSites.SelectedItem;
            if (World.Data.Site != null)
            {
                DtgSubSites.ItemsSource = World.Data.Site.SubSites;
                ID = World.Data.SiteList.IndexOf(World.Data.Site);
                DataGridIndex = DtgSites.SelectedIndex;
                DtgSiteItems.ItemsSource = World.Data.Site.ItemList;
                DtgSiteSects.ItemsSource = World.Data.Site.SectList;
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
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
                //World.Data.Site = sd.Site.Clone();
                DtgSites.SelectedIndex = World.Data.SiteList.Count - 1;
            }
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
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
                World.Data.SiteList.EditSite(ID, sd.Site.Clone());

                //World.Data.Site = sd.Site.Clone();
                DtgSites.SelectedIndex = DataGridIndex;
                //World.Data.Site.CheckItems();
            }
        }

        private void DeleteObj()
        {
            MessageDialog msg = new MessageDialog
            {
                Title = "删除提示"
            };
            msg.TxbMsg.Text = "是否删除【地点】 " + World.Data.Site.Name + "?";
            msg.ShowDialog();

            if (msg.DialogResult == true)
            {
                World.Data.SiteList.DeleteSite(World.Data.Site);
                int tmp;
                if (ID < World.Data.ItemList.Count)
                {
                    tmp = ID;
                }
                else
                {
                    tmp = World.Data.ItemList.Count - 1;
                }
                LoadObj(tmp);
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
            DeleteObj();
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

        /// <summary>
        /// 上移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {

            bool tmp = World.Data.SiteList.UpSite(World.Data.Site);
            if (tmp)
                DtgSites.SelectedIndex = ID - 1;

        }

        /// <summary>
        /// 下移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            bool tmp = World.Data.SiteList.DownSite(World.Data.Site);
            if (tmp)
                DtgSites.SelectedIndex = ID + 1;
        }
        #endregion
    }
}
