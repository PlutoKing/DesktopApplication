/*──────────────────────────────────────────────────────────────
 * FileName     : LFSerialPort.cs
 * Created      : 2021-06-11 11:08:39
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

        private SerialPort _port;   // 串口
        private List<string> _portNames;    // 所有可用串口名称

        private int _receiveCount;  // 接收量
        private int _sendCount;     // 发送量

        private bool _isShowTime;
        #endregion

        #region Properties

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName
        {
            get { return _port.PortName; }
            set { _port.PortName = value; }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baudrate
        {
            get { return _port.BaudRate; }
            set { _port.BaudRate = value; }
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return _port.DataBits; }
            set { _port.DataBits = value; }
        }

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity
        {
            get { return _port.Parity; }
            set { _port.Parity = value; }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits
        {
            get { return _port.StopBits; }
            set { _port.StopBits = value; }
        }

        public bool IsOpen
        {
            get { return _port.IsOpen; }
        }
        /// <summary>
        /// 接收数据量
        /// </summary>
        public int ReceiveCount { get => _receiveCount; set => _receiveCount = value; }
        
        /// <summary>
        /// 发送数据量
        /// </summary>
        public int SendCount { get => _sendCount; set => _sendCount = value; }
        /// <summary>
        /// 所以可用串口名称
        /// </summary>
        public List<string> PortNames { get => _portNames; set => _portNames = value; }
        public bool IsShowTime { get => _isShowTime; set => _isShowTime = value; }


        #endregion

        #region Constructors
        public LFSerialPort()
        {
            _port = new SerialPort();
            _port.DataReceived += Port_DataReceived;
        }


        #endregion

        #region Methods

        #region Commom Methods

        /// <summary>
        /// 扫描串口
        /// </summary>
        public void Scan()
        {
            string[] portNames = SerialPort.GetPortNames();
            _portNames = portNames.ToList();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            
            _port.Open();

            // 计数器清零
            _receiveCount = 0;
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

        #endregion

        #region Events

        /// <summary>
        /// 接收数据是发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }
        #endregion
    }
}