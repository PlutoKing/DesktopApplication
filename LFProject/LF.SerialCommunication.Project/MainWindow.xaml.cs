/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-04-21 22:30:05
 * Author       : Xu Zhe
 * Description  : 主屏幕
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

namespace LF.SerialCommunication.Project
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private LFSerialPort serialPort = new LFSerialPort();
        
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = serialPort;
            //CmbPorts.ItemsSource = serialPort.PortNames;
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion


        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Scan();
            if (serialPort.PortNames.Count == 0)
            {
                MessageBox.Show("当前无可用串口，请检查串口设备！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
