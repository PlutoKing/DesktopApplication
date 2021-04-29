/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-04-29 10:35:01
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

namespace EX01DataContext
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private LFRole _role = new LFRole();

        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _role;
        }

       
        #endregion

        #region Methods



        #endregion

        #region Events
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _role.Name = "改变";

            _role.Tmp.Add(100);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = role;
            //list.ItemsSource = _role.Tmp;
        }
    }
}
