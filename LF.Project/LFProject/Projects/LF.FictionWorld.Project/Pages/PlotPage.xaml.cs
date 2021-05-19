/*──────────────────────────────────────────────────────────────
 * FileName     : PlotPage.cs
 * Created      : 2021-05-18 11:14:45
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
        }
        #endregion

        #region Methods

        #endregion

        #region Events

        /// <summary>
        /// 控件加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            /* 绑定情节列表数据源 */
            DtgPlots.ItemsSource = World.Data.PlotList;
            /* 设置默认选择项 */
            DtgPlots.SelectedIndex = 0;
            /* 绑定数据内容 */
            this.DataContext = World.Data;

            /* 显示计数 */
            TxbCount.Text = World.Data.PlotList.Count.ToString();
        }

        /// <summary>
        /// 情节列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgPlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            World.Data.Plot = (LFPlot)DtgPlots.SelectedItem;
            if (World.Data.Plot != null)
            {
                DtgRoles.ItemsSource = World.Data.Plot.Roles;
                ListAction.ItemsSource = World.Data.Plot.Actions;
                if (ListAction.Items.Count != 0)
                {
                    ListAction.SelectedIndex = World.Data.Plot.Actions.Count - 1;
                }
                ID = World.Data.PlotList.IndexOf(World.Data.Plot);
                DataGridIndex = DtgPlots.SelectedIndex;
                TxbActionCount.Text = World.Data.Plot.Actions.Count.ToString();
            }
        }

        /// <summary>
        /// 动作列表选择项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentAction = (LFAction)ListAction.SelectedItem;
            ActionID = World.Data.Plot.Actions.IndexOf(CurrentAction);
            ListIndex = ListAction.SelectedIndex;
        }

        /// <summary>
        /// 添加按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
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
        /// 编辑按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
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
        /// 删除按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            World.Data.Plot = (LFPlot)DtgPlots.SelectedItem;
            if (World.Data.Plot != null)
            {
                DtgRoles.ItemsSource = World.Data.Plot.Roles;
                ListAction.ItemsSource = World.Data.Plot.Actions;
                ID = World.Data.PlotList.IndexOf(World.Data.Plot);
                DataGridIndex = DtgPlots.SelectedIndex;
            }
        }

        /// <summary>
        /// 添加动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddAction_Click(object sender, RoutedEventArgs e)
        {
            ActionDialog actionDialog = new ActionDialog
            {
                Title = "新建动作 - " + (World.Data.Plot.Actions.Count + 1).ToString(),
                Plot = World.Data.Plot
            };
            actionDialog.ShowDialog();
            if (actionDialog.DialogResult == true)
            {
                World.Data.Plot.Actions.Add(actionDialog.Action.Clone());
                CurrentAction = actionDialog.Action.Clone();
                ListAction.SelectedIndex = ListAction.Items.Count - 1;
            }
        }

        /// <summary>
        /// 编辑动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditAction_Click(object sender, RoutedEventArgs e)
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
                    World.Data.Plot.Actions[ActionID] = actionDialog.Action.Clone();
                    CurrentAction = actionDialog.Action.Clone();
                    ListAction.SelectedIndex = ListIndex;
                }
            }
        }

        /// <summary>
        /// 删除动作按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteAction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAction != null)
            {
                World.Data.Plot.Actions.Remove(CurrentAction);
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
        /// 上移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            LFAction action = (LFAction)ListAction.SelectedItem;
            int idx = World.Data.Plot.Actions.IndexOf(action);
            if (idx != 0)
            {
                int idx2 = idx - 1;
                LFAction tmp = action.Clone();
                World.Data.Plot.Actions[idx] = World.Data.Plot.Actions[idx2].Clone();
                World.Data.Plot.Actions[idx2] = tmp.Clone();
                ListAction.SelectedIndex = idx2;
            }
        }

        /// <summary>
        /// 下移按钮单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            LFAction action = (LFAction)ListAction.SelectedItem;
            int idx = World.Data.Plot.Actions.IndexOf(action);
            if (idx != World.Data.Plot.Actions.Count - 1)
            {
                int idx2 = idx + 1;
                LFAction tmp = action.Clone();
                World.Data.Plot.Actions[idx] = World.Data.Plot.Actions[idx2].Clone();
                World.Data.Plot.Actions[idx2] = tmp.Clone();
                ListAction.SelectedIndex = idx2;
            }
        }

        #endregion

        private void ListAction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
                    World.Data.Plot.Actions[ActionID] = actionDialog.Action.Clone();
                    CurrentAction = actionDialog.Action.Clone();
                    ListAction.SelectedIndex = ListIndex;
                }
            }
        }

        private void DtgPlots_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
    }
}
