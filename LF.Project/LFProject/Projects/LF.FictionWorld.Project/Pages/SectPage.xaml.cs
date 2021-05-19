/*──────────────────────────────────────────────────────────────
 * FileName     : SectPage.cs
 * Created      : 2021-05-18 11:09:13
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
    /// SectPage.xaml 的交互逻辑
    /// </summary>
    public partial class SectPage : UserControl
    {
        #region Fields

        int DataGridIndex = 0;
        int ID = 0;

        #endregion

        #region Constructors
        public SectPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = World.Data;
            DtgSects.ItemsSource = World.Data.SectList;
            DtgSects.SelectedIndex = 0;

            TxbCount.Text = World.Data.SectList.Count.ToString();

            TrvSectStruct.ItemsSource = World.Data.Sect.Struct;
        }

        /// <summary>
        /// 选择势力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgSects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            World.Data.Sect = (LFSect)DtgSects.SelectedItem;
            if (World.Data.Sect != null)
            {
                ID = World.Data.SectList.IndexOf(World.Data.Sect);
                DataGridIndex = DtgSects.SelectedIndex;

                TrvSectStruct.ItemsSource = World.Data.Sect.Struct;
            }
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SectDialog form = new SectDialog
            {
                Title = "新建势力 - " + (World.Data.SectList.Count + 1).ToString()
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList.Add(form.Sect.Clone());
                World.Data.Sect = form.Sect.Clone();
                DtgSects.SelectedIndex = World.Data.SectList.Count - 1;
                TxbCount.Text = World.Data.SectList.Count.ToString();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            SectDialog form = new SectDialog
            {
                Sect = World.Data.Sect.Clone(),
                Title = "编辑势力 - " + World.Data.Sect.Name
            };
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                World.Data.SectList[ID] = form.Sect.Clone();
                World.Data.Sect = form.Sect.Clone();
                DtgSects.SelectedIndex = DataGridIndex;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
