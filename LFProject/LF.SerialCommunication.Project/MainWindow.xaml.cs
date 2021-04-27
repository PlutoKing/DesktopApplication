/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-04-21 22:30:05
 * Author       : Xu Zhe
 * Description  : 主屏幕
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        private List<int> baudrates = new List<int>() { 4800, 9600, 19200, 38400, 57600, 115200 };
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = serialPort;
            CmbName.ItemsSource = serialPort.PortNames;
            CmbBaudrate.ItemsSource = baudrates;
            CmbBaudrate.SelectedIndex = 1;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 设置串口打开后控件样式
        /// </summary>
        private void SetPortOpen()
        {
            CmbName.IsEnabled = false;
            CmbBaudrate.IsEnabled = false;
            CmbDatabits.IsEnabled = false;
            CmbParity.IsEnabled = false;
            CmbStopbits.IsEnabled = false;
            BtnOpenClose.Content = "关闭串口";
        }

        /// <summary>
        /// 设置串口关闭后控件样式
        /// </summary>
        private void SetPortClose()
        {
            CmbName.IsEnabled = true;
            CmbBaudrate.IsEnabled = true;
            CmbDatabits.IsEnabled = true;
            CmbParity.IsEnabled = true;
            CmbStopbits.IsEnabled = true;
            BtnOpenClose.Content = "打开串口";
        }

        #endregion

        #region Events

        /// <summary>
        /// 打开串口与关闭串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenClose_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.Port.IsOpen)
            {
                SetPortClose();
            }
            else
            {
                if (serialPort.Config())    // 如果配置成功则打开串口
                {
                    serialPort.Open();
                    SetPortOpen();
                }
                else
                {
                    MessageBox.Show("请选择串口！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

        /// <summary>
        /// 修改名字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serialPort.Name = CmbName.SelectedItem.ToString() ;
        }

        /// <summary>
        /// 修改波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBaudrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serialPort.Baudrate = baudrates[CmbBaudrate.SelectedIndex];
        }

        /// <summary>
        /// 修改数据位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDatabits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDatabits.Text != "")
                serialPort.Databits = Convert.ToInt32(CmbDatabits.Text);
        }

        /// <summary>
        /// 修改停止位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbStopbits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CmbStopbits.SelectedIndex)
            {
                case 0:
                    serialPort.Stopbits = StopBits.One;
                    break;
                case 1:
                    serialPort.Stopbits = StopBits.OnePointFive;
                    break;
                case 2:
                    serialPort.Stopbits = StopBits.Two;
                    break;
                default:
                    serialPort.Stopbits = StopBits.One;
                    break;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbParity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CmbParity.SelectedIndex)
            {
                case 0:
                    serialPort.Parity = Parity.None;
                    break;
                case 1:
                    serialPort.Parity = Parity.Odd;
                    break;
                case 2:
                    serialPort.Parity = Parity.Even;
                    break;
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }
        }

        #endregion



        /// <summary>
        /// 清屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            RtbRece.Document.Blocks.Clear();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Scan();
            if (serialPort.PortNames.Count == 0)
            {
                MessageBox.Show("当前无可用串口，请检查串口设备！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
