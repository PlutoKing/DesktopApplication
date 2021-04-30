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
using System.Timers;
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

        /// <summary>
        /// 串口
        /// </summary>
        private LFSerialPort serialPort = new LFSerialPort();

        /// <summary>
        /// 常用波特率
        /// </summary>
        private List<int> baudrates = new List<int>() { 4800, 9600, 19200, 38400, 57600, 115200 };

        /// <summary>
        /// 定时器
        /// </summary>
        private Timer Timer = new Timer();

        Paragraph paragraph = new Paragraph();
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();


            CmbName.ItemsSource = serialPort.PortNames;
            CmbBaudrate.ItemsSource = baudrates;
            CmbBaudrate.SelectedIndex = 5;

            serialPort.OnReceiveMessage += SerialPort_OnReceiveMessage;

            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = 100;

            this.DataContext = serialPort;      // 设定数据内容


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
            if (serialPort.IsShowTime)
            {
                string time = "["+ DateTime.Now.ToString()+"] ";
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
        /// 串口接收消息事件
        /// </summary>
        private void SerialPort_OnReceiveMessage()
        {
            if (serialPort.Port.IsOpen)
            {
                byte[] readBuffer = new byte[serialPort.Port.ReadBufferSize + 1];
                try
                {
                    int count = serialPort.Port.Read(readBuffer, 0, serialPort.Port.ReadBufferSize);
                    string SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                    if (count != 0)
                    {
                        serialPort.ReceContent = readBuffer.ToString();
                        Paragraph p = new Paragraph();
                        Run run = new Run() { Text = serialPort.ReceContent };
                        p.Inlines.Add(run);
                        RtbRece.Document.Blocks.Add(p);
                        serialPort.ReceCount += count;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (serialPort.Port.IsOpen)
            {
                byte[] readBuffer = new byte[serialPort.Port.ReadBufferSize + 1];
                try
                {
                    int count = serialPort.Port.Read(readBuffer, 0, serialPort.Port.ReadBufferSize);
                    string SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                    if (count != 0)
                    {
                        serialPort.ReceContent = readBuffer.ToString();
                        Paragraph p = new Paragraph();
                        Run run = new Run() { Text = serialPort.ReceContent };
                        p.Inlines.Add(run);
                        RtbRece.Document.Blocks.Add(p);
                        serialPort.ReceCount += count;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 刷新串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Scan();
            CmbName.ItemsSource = serialPort.PortNames;

            if (serialPort.PortNames.Count == 0)
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
            if (serialPort.Port.IsOpen)
            {
                serialPort.Close();
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

            serialPort.Databits = 8 - CmbDatabits.SelectedIndex;
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
        /// 修改校验位
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
            paragraph.Inlines.Clear();
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            /* 1. 将RichTextBox中内容转化为发送内容 */
            TextRange tr = new TextRange(RtbSend.Document.ContentStart, RtbSend.Document.ContentEnd);
            serialPort.SendContent = tr.Text;
            /* 2. 将发送内容写入串口 */
            serialPort.SendMessage();
            /* 3. 判断是否需要显示发送内容 */
            if (serialPort.IsShowSend)
            {
                AddMessage(tr.Text);
            }
            //if (serialPort.Port.IsOpen)
            //{
                

            //}
            //else
            //{
            //    MessageBox.Show("串口未打开，无法发送！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

    }
}
