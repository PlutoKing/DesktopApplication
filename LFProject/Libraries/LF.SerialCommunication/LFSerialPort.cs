/*──────────────────────────────────────────────────────────────
 * FileName     : LFSerialPort.cs
 * Created      : 2021-04-22 09:43:10
 * Author       : Xu Zhe
 * Description  : 管理串口的基本操作和通信方法
 * ──────────────────────────────────────────────────────────────*/

using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;


namespace LF.SerialCommunication
{
    /// <summary>
    /// 串口
    /// </summary>
    public class LFSerialPort : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ReceiveMessageHandler();

        public event ReceiveMessageHandler OnReceiveMessage;

        private SerialPort _port;   // 串口

        private List<string> _portNames;
        private string _name;       // 串口名
        private int _baudrate = 115200;      // 波特率
        private int _databits;      // 数据位
        private Parity _parity;     // 校验位
        private StopBits _stopbits; // 停止位

        private int _receCount = 0;     // 接收量
        private int _sendCount = 0;     // 发送量

        private string _receContent;    // 接收内容
        private string _sendContent;    // 发送内容

        private bool _isSendHex = true;
        private bool _isReceHex = true;

        private bool _isShowTime = false;
        private bool _isShowSend = true;

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
        public int SendCount
        {
            get { return _sendCount; }
            set
            {
                _sendCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SendCount"));
                }
            }
        }

        /// <summary>
        /// 接收内容
        /// </summary>
        public string ReceContent { get => _receContent; set => _receContent = value; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent { get => _sendContent; set => _sendContent = value; }


        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool IsShowTime { get => _isShowTime; set => _isShowTime = value; }

        /// <summary>
        /// 是否按照16进制发送
        /// </summary>
        public bool IsSendHex { get => _isSendHex; set => _isSendHex = value; }

        /// <summary>
        /// 是否按照16进制接收
        /// </summary>
        public bool IsReceHex { get => _isReceHex; set => _isReceHex = value; }

        /// <summary>
        /// 是否显示发送
        /// </summary>
        public bool IsShowSend { get => _isShowSend; set => _isShowSend = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFSerialPort()
        {
            _port = new SerialPort();
            _port.DataReceived += Port_DataReceived;
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
        public bool Config()
        {
            if(_name == null)
            {
                // 如果没有串口名，则范围false，表示配置失败
                return false;
            }
            else
            {
                // 配置串口
                _port.PortName = _name;
                _port.BaudRate = _baudrate;
                _port.DataBits = _databits;
                _port.Parity = _parity;
                _port.StopBits = _stopbits;
                return true;
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            // 打开串口
            _port.Open();
            // 计数器清零
            _receCount = 0;
            _sendCount = 0;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            _port.Close();
        }
        #endregion

        #region 通信
  
        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMessage()
        {
            if (_isSendHex)
            {
                /* 十六进制 */
                MatchCollection mc = Regex.Matches(_sendContent, @"(?i)[\dA-F]{2}");    // 正则表达式，检测0-9，A-F，两位
                List<byte> buf = new List<byte>();
                foreach (Match m in mc)
                {
                    buf.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));
                }
                SendMessage(buf.ToArray());
                SendCount += buf.Count;     // 使用Sendcount而不是_sendCount，是为了监测属性变化
            }
            else
            {
                /* 字符串 */
                char[] buf = _sendContent.ToCharArray();
                SendMessage(buf);
                SendCount += buf.Length;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer">byte型数据</param>
        private void SendMessage(byte[] buffer)
        {
            if (_port.IsOpen)
                _port.Read(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer">char型数据</param>
        private void SendMessage(char[] buffer)
        {
            if (_port.IsOpen)
                _port.Read(buffer, 0, buffer.Length);
        }

        #endregion

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// 接收消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            OnReceiveMessage?.Invoke();
        }

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