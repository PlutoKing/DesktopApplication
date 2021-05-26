/*──────────────────────────────────────────────────────────────
 * FileName     : ItemPage.cs
 * Created      : 2021-05-25 21:48:50
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        /// <summary>
        /// 刷新对象
        /// </summary>
        public void LoadObj()
        {
            // 绑定数据
            DataContext = World.Data;

            // 绑定列表数据
            DtgItems.ItemsSource = World.Data.ItemList;
            DtgItems.SelectedIndex = 0;
        }

        /// <summary>
        /// 保持对象
        /// </summary>
        public void KeepObj()
        {
            DtgItems.SelectedIndex = ID;
        }

        /// <summary>
        /// 刷新对象
        /// </summary>
        public void RefreshObj()
        {
            World.Data.Item = (LFItem)DtgItems.SelectedItem;
            if (World.Data.Item != null)
            {
                ID = World.Data.ItemList.IndexOf(World.Data.Item);
                DataGridIndex = DtgItems.SelectedIndex;
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        public void AddObj()
        {
            ItemDialog dialog = new ItemDialog();
            dialog.Title = "新建物品 - " + (World.Data.ItemList.Count + 1).ToString();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.ItemList.Add(dialog.Item);
                World.Data.Item = dialog.Item;
                DtgItems.SelectedIndex = World.Data.ItemList.IndexOf(World.Data.Item);
            }
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        public void EditObj()
        {
            ItemDialog dialog = new ItemDialog
            {
                Item = World.Data.Item.Clone(),
                Title = "编辑物品 - " + World.Data.Item.Name
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                // 调用编辑物品函数
                World.Data.ItemList.EditItem(ID, dialog.Item);

                // 刷新列表
                DtgItems.SelectedIndex = DataGridIndex;
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        public void DeleteObj()
        {
            MessageDialog msg = new MessageDialog();
            msg.Title = "删除提示";
            msg.TxbMsg.Text = "是否删除【物品：" + World.Data.Item.Name + "】?";
            msg.ShowDialog();

            if (msg.DialogResult == true)
            {
                //调用删除物品函数
                    World.Data.ItemList.DeleteItem(World.Data.Item);

                // 刷新列表
                if (ID < World.Data.ItemList.Count)
                {
                    DtgItems.SelectedIndex = ID;
                }
                else
                {
                    DtgItems.SelectedIndex = World.Data.ItemList.Count - 1;
                }
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
        /// 列表选择项变化是发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();
        }

        /// <summary>
        /// 列表双击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }


        /// <summary>
        /// 添加Button单击时发生
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

        #endregion

    }
}
