/*──────────────────────────────────────────────────────────────
 * FileName     : PlotPage.cs
 * Created      : 2021-05-25 22:08:49
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
    /// PlotPage.xaml 的交互逻辑
    /// </summary>
    public partial class PlotPage : UserControl
    {
        #region Fields
        int DataGridIndex = 0;
        int ID = 0;

        LFAction CurrentAction = new LFAction();

        int ListIndex = 0;

        int ActionID = 0;

        #endregion

        #region Constructors
        public PlotPage()
        {
            InitializeComponent();

            /* 绑定情节列表数据源 */
            DtgPlots.ItemsSource = World.Data.PlotList;
            /* 设置默认选择项 */
            DtgPlots.SelectedIndex = 0;
            /* 绑定数据内容 */
            this.DataContext = World.Data;

        }
        #endregion

        #region Methods
        private void LoadObj()
        {
            
        }

        /// <summary>
        /// 刷新对象
        /// </summary>
        public void RefreshObj()
        {
            World.Data.Plot = (LFPlot)DtgPlots.SelectedItem;
            if (World.Data.Plot != null)
            {
                DtgRoles.ItemsSource = World.Data.Plot.RoleList;
                ListAction.ItemsSource = World.Data.Plot.ActionList;
                if (ListAction.Items.Count != 0)
                {
                    ListAction.SelectedIndex = World.Data.Plot.ActionList.Count - 1;
                }
                ID = World.Data.PlotList.IndexOf(World.Data.Plot);
                DataGridIndex = DtgPlots.SelectedIndex;
                TxbActionCount.Text = World.Data.Plot.ActionList.Count.ToString();
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        public void AddObj()
        {
            PlotDialog dialog = new PlotDialog
            {
                Title = "新建情节 - " + (World.Data.PlotList.Count + 1).ToString()
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.PlotList.Add(dialog.Plot.Clone());
                World.Data.Plot = dialog.Plot.Clone();
                DtgPlots.SelectedIndex = World.Data.PlotList.Count - 1;
                TxbCount.Text = World.Data.PlotList.Count.ToString();
            }
        }
        /// <summary>
        /// 编辑对象
        /// </summary>
        public void EditObj()
        {
            PlotDialog dialog = new PlotDialog
            {
                Plot = World.Data.Plot.Clone(),
                Title = "编辑情节 - " + World.Data.Plot.Name
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                World.Data.PlotList[ID] = dialog.Plot.Clone();
                World.Data.Plot = dialog.Plot.Clone();
                DtgPlots.SelectedIndex = DataGridIndex;
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        public void DeleteObj()
        {
            World.Data.Plot = (LFPlot)DtgPlots.SelectedItem;
            //if (World.Data.Plot != null)
            //{
            //    DtgRoles.ItemsSource = World.Data.Plot.RoleList;
            //    ListAction.ItemsSource = World.Data.Plot.ActionList;
            //    ID = World.Data.PlotList.IndexOf(World.Data.Plot);
            //    DataGridIndex = DtgPlots.SelectedIndex;
            //}
        }

        /// <summary>
        /// 刷新动作
        /// </summary>
        public void RefreshAction()
        {
            CurrentAction = (LFAction)ListAction.SelectedItem;
            ActionID = World.Data.Plot.ActionList.IndexOf(CurrentAction);
            ListIndex = ListAction.SelectedIndex;
        }

        /// <summary>
        /// 添加动作
        /// </summary>
        public void AddAction()
        {
            ActionDialog actionDialog = new ActionDialog
            {
                Title = "新建动作 - " + (World.Data.Plot.ActionList.Count + 1).ToString(),
                Plot = World.Data.Plot
            };
            actionDialog.ShowDialog();
            if (actionDialog.DialogResult == true)
            {
                World.Data.Plot.ActionList.Add(actionDialog.Action.Clone());
                CurrentAction = actionDialog.Action.Clone();
                ListAction.SelectedIndex = ListAction.Items.Count - 1;
            }
        }

        /// <summary>
        /// 编辑动作
        /// </summary>
        public void EditAction()
        {
            if (CurrentAction != null)
            {
                ActionDialog actionDialog = new ActionDialog
                {
                    Title = "编辑动作 - " + CurrentAction.ID.ToString(),
                    Plot = World.Data.Plot,
                    Action = CurrentAction.Clone()
                };
                actionDialog.ShowDialog();
                if (actionDialog.DialogResult == true)
                {
                    World.Data.Plot.ActionList[ActionID] = actionDialog.Action.Clone();
                    CurrentAction = actionDialog.Action.Clone();
                    ListAction.SelectedIndex = ListIndex;
                }
            }
        }

        public void DeleteAction()
        {
            if (CurrentAction != null)
            {
                World.Data.Plot.ActionList.Remove(CurrentAction);
                try
                {
                    ListAction.SelectedIndex = ListIndex;
                }
                catch
                {
                    try
                    {
                        ListAction.SelectedIndex = ListAction.Items.Count - 1;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 上移动作
        /// </summary>
        public void UpAction()
        {
            LFAction action = (LFAction)ListAction.SelectedItem;
            if (action == null)
                return;
            int idx = World.Data.Plot.ActionList.IndexOf(action);
            if (idx != 0)
            {
                int idx2 = idx - 1;
                LFAction tmp = action.Clone();
                World.Data.Plot.ActionList[idx] = World.Data.Plot.ActionList[idx2].Clone();
                World.Data.Plot.ActionList[idx2] = tmp.Clone();
                ListAction.SelectedIndex = idx2;
            }
        }

        /// <summary>
        /// 下移动作
        /// </summary>
        public void DownAction()
        {
            LFAction action = (LFAction)ListAction.SelectedItem;
            if (action == null)
                return;
            int idx = World.Data.Plot.ActionList.IndexOf(action);
            if (idx != World.Data.Plot.ActionList.Count - 1)
            {
                int idx2 = idx + 1;
                LFAction tmp = action.Clone();
                World.Data.Plot.ActionList[idx] = World.Data.Plot.ActionList[idx2].Clone();
                World.Data.Plot.ActionList[idx2] = tmp.Clone();
                ListAction.SelectedIndex = idx2;
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
        /// 情节列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgPlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshObj();
        }

        /// <summary>
        /// 动作列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAction();
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
        /// 添加动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddAction_Click(object sender, RoutedEventArgs e)
        {
            AddAction();
        }

        /// <summary>
        /// 编辑动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditAction_Click(object sender, RoutedEventArgs e)
        {
            EditAction();
        }

        /// <summary>
        /// 删除动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteAction_Click(object sender, RoutedEventArgs e)
        {
            DeleteAction();
        }

        /// <summary>
        /// 上移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            UpAction();
        }

        /// <summary>
        /// 下移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            DownAction();
        }

        /// <summary>
        /// 动作列表双击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditAction();
        }

        /// <summary>
        /// 情节列表双击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgPlots_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditObj();
        }
        #endregion


    }
}
