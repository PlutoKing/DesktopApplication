/*──────────────────────────────────────────────────────────────
 * FileName     : MainWindow.cs
 * Created      : 2021-06-11 11:07:07
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
using System.IO;
using System.Timers;
using System.IO.Ports;

namespace LF.SerialCommunication.Project
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        /// <summary>
        /// 串口
        /// </summary>
        private LFSerialPort SerialPort = new LFSerialPort();

        /// <summary>
        /// 常用波特率
        /// </summary>
        private List<int> baudrates = new List<int>() { 4800, 9600, 19200, 38400, 57600, 115200 };


        Paragraph paragraph = new Paragraph();

        /// <summary>
        /// 定时器
        /// </summary>
        private Timer Timer = new Timer();

        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            CmbName.ItemsSource = SerialPort.PortNames;
            CmbBaudrate.ItemsSource = baudrates;
            CmbBaudrate.SelectedIndex = 5;

            paragraph = (Paragraph)RtbRece.Document.Blocks.FirstBlock;
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
            Timer.Start();
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
            Timer.Stop();
        }

        /// <summary>
        /// 添加消息到接收框
        /// </summary>
        private void AddMessage(string msg)
        {
            // 如果显示时间
            if (SerialPort.IsShowTime)
            {
                string time = "[" + DateTime.Now.ToString() + "] ";
                Run runTime = new Run(time);
                runTime.Foreground = Brushes.Blue;
                paragraph.Inlines.Add(runTime);

            }
            // 消息内容
            Run run = new Run(msg);
            paragraph.Inlines.Add(run);
        }
        #endregion

        #region Events

        /// <summary>
        /// 刷新串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SerialPort.Scan();
            CmbName.ItemsSource = SerialPort.PortNames;

            if (SerialPort.PortNames.Count == 0)
            {
                MessageBox.Show("当前无可用串口，请检查串口设备！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 打开串口与关闭串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenClose_Click(object sender, RoutedEventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
                SetPortClose();
            }
            else
            {
                try
                {
                    SerialPort.Open();
                    SetPortOpen();
                }
                catch
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
            SerialPort.PortName = CmbName.SelectedItem.ToString();
        }

        /// <summary>
        /// 修改波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBaudrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SerialPort.Baudrate = baudrates[CmbBaudrate.SelectedIndex];
        }

        /// <summary>
        /// 修改数据位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDatabits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SerialPort.DataBits = 8 - CmbDatabits.SelectedIndex;
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
                    SerialPort.StopBits = StopBits.One;
                    break;
                case 1:
                    SerialPort.StopBits = StopBits.OnePointFive;
                    break;
                case 2:
                    SerialPort.StopBits = StopBits.Two;
                    break;
                default:
                    SerialPort.StopBits = StopBits.One;
                    break;
            }
        }

        /// <summary>
        /// 修改校验位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbParity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CmbParity.SelectedIndex)
            {
                case 0:
                    SerialPort.Parity = Parity.None;
                    break;
                case 1:
                    SerialPort.Parity = Parity.Odd;
                    break;
                case 2:
                    SerialPort.Parity = Parity.Even;
                    break;
                default:
                    SerialPort.Parity = Parity.None;
                    break;
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            paragraph.Inlines.Clear();
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
    }
}
