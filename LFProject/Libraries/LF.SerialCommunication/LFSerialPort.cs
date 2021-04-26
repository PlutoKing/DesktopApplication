/*──────────────────────────────────────────────────────────────
 * FileName     : LFSerialPort.cs
 * Created      : 2021-04-22 09:43:10
 * Author       : Xu Zhe
 * Description  : 管理串口的基本操作和通信方法
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LF.SerialCommunication
{
    /// <summary>
    /// 串口
    /// </summary>
    public class LFSerialPort : INotifyPropertyChanged
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;


        private SerialPort _port;   // 串口

        private List<string> _portNames;
        private string _name;       // 串口名
        private int _baudrate;      // 波特率
        private int _databits;      // 数据位
        private Parity _parity;     // 校验位
        private StopBits _stopbits; // 停止位

        private int _receCount;     // 接收量
        private int _sendCound;     // 发送量

        Thread _readThread;     // 读取线程
        bool _keepReading;      // 是否保持读取
        #endregion

        #region Properties

        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort Port { get => _port; set => _port = value; }

        /// <summary>
        /// 所有串口名
        /// </summary>
        public List<string> PortNames { get => _portNames; set => _portNames = value; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baudrate { get => _baudrate; set => _baudrate = value; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int Databits { get => _databits; set => _databits = value; }

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get => _parity; set => _parity = value; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits Stopbits { get => _stopbits; set => _stopbits = value; }

        /// <summary>
        /// 接收量
        /// </summary>
        public int ReceCount { get => _receCount; set => _receCount = value; }

        /// <summary>
        /// 发送量
        /// </summary>
        public int SendCound { get => _sendCound; set => _sendCound = value; }

        #endregion

        #region Constructors
        public LFSerialPort()
        {
            _port = new SerialPort();
            _port.DataReceived += Port_DataReceived;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
        #endregion

        #region Methods

        #region 基本操作

        /// <summary>
        /// 扫描可用串口
        /// </summary>
        /// <returns></returns>
        public void Scan()
        {
            string[] portNames = SerialPort.GetPortNames();
            _portNames = portNames.ToList();
        }

        /// <summary>
        /// 配置串口
        /// </summary>
        public void Config()
        {
            _port.PortName = _name;
            _port.BaudRate = _baudrate;
            _port.DataBits = _databits;
            _port.Parity = _parity;
            _port.StopBits = _stopbits;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            // 打开串口
            _port.Open();
            // 启动读取线程
            _keepReading = true;
            _readThread = new Thread(ReceiveMessage);
            _readThread.Start();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Clone()
        {
            _port.Close();
        }
        #endregion

        #region 通信

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void SendMessage(byte[] buffer)
        {
            _port.Read(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public void ReceiveMessage()
        {
            while (_keepReading)
            {
                if (_port.IsOpen)
                {
                    byte[] readBuffer = new byte[_port.ReadBufferSize + 1];
                    try
                    {
                        int count = _port.Read(readBuffer, 0, _port.ReadBufferSize);
                        string SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                        if (count != 0)
                        {
                        }
                    }
                    catch { }
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }    
}