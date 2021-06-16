# 串口通信助手

## 一、基本信息

- 项目名称：`LF.SerialCommunication.Project`
- 创建日期：2021-06-11
- 版　　本：V1.0

## 二、串口通信

串口通信（Serial Communications）的概念非常简单，串口按位（bit）发送和接收字节。尽管比按字节（byte）的并行通信慢，但是串口可以在使用一根线发送数据的同时用另一根线接收数据。它很简单并且能够实现远距离通信。比如IEEE488定义并行通行状态时，规定设备线总长不得超过20米，并且任意两个设备间的长度不得超过2米；而对于串口而言，长度可达1200米。典型地，串口用于ASCII码字符的传输。通信使用3根线完成，分别是地线、发送、接收。由于串口通信是异步的，端口能够在一根线上发送数据同时在另一根线上接收数据。其他线用于握手，但不是必须的。串口通信最重要的参数是**波特率**、**数据位**、**停止位**和**奇偶校验**。对于两个进行通信的端口，这些参数必须匹配。

### 2.1 波特率

这是一个衡量符号传输速率的参数。指的是信号被调制以后在单位时间内的变化，即单位时间内载波参数变化的次数，如每秒钟传送240个字符，而每个字符格式包含10位（1个起始位，1个停止位，8个数据位），这时的波特率为240Bd，比特率为10位*240个/秒=2400bps。一般调制速率大于波特率，比如曼彻斯特编码）。通常电话线的波特率为14400，28800和36600。波特率可以远远大于这些值，但是波特率和距离成反比。高波特率常常用于放置的很近的仪器间的通信，典型的例子就是GPIB设备的通信。

### 2.2 数据位

这是衡量通信中实际数据位的参数。当计算机发送一个信息包，实际的数据往往不会是8位的，标准的值是6、7和8位。如何设置取决于你想传送的信息。比如，标准的ASCII码是0～127（7位）。扩展的ASCII码是0～255（8位）。如果数据使用简单的文本（标准 ASCII码），那么每个数据包使用7位数据。每个包是指一个字节，包括开始/停止位，数据位和奇偶校验位。由于实际数据位取决于通信协议的选取，术语“包”指任何通信的情况。

### 2.3 停止位

用于表示单个包的最后一位。典型的值为1，1.5和2位。由于数据是在传输线上定时的，并且每一个设备有其自己的时钟，很可能在通信中两台设备间出现了小小的不同步。因此停止位不仅仅是表示传输的结束，并且提供计算机校正时钟同步的机会。适用于停止位的位数越多，不同时钟同步的容忍程度越大，但是数据传输率同时也越慢。

### 2.4 奇偶校验位

在串口通信中一种简单的检错方式。有四种检错方式：偶、奇、高和低。当然没有校验位也是可以的。对于偶和奇校验的情况，串口会设置校验位（数据位后面的一位），用一个值确保传输的数据有偶个或者奇个逻辑高位。例如，如果数据是011，那么对于偶校验，校验位为0，保证逻辑高的位数是偶数个。如果是奇校验，校验位为1，这样就有3个逻辑高位。高位和低位不真正的检查数据，简单置位逻辑高或者逻辑低校验。这样使得接收设备能够知道一个位的状态，有机会判断是否有噪声干扰了通信或者是否传输和接收数据是否不同步。

## 三、C#中的串口

命名空间：
```c#
using System.IO.Ports;
```

基本事件：

```c#
//
// 摘要:
//     指示由 System.IO.Ports.SerialPort 对象表示的端口上发生了错误。
[MonitoringDescription("SerialErrorReceived")]
public event SerialErrorReceivedEventHandler ErrorReceived;
//
// 摘要:
//     指示已通过由 System.IO.Ports.SerialPort 对象表示的端口接收了数据。
[MonitoringDescription("SerialDataReceived")]
public event SerialDataReceivedEventHandler DataReceived;
//
// 摘要:
//     指示由 System.IO.Ports.SerialPort 对象表示的端口上发生了非数据信号事件。
[MonitoringDescription("SerialPinChanged")]
public event SerialPinChangedEventHandler PinChanged;
```
## 四、软件界面

![界面](Projects/LF.Communication/LF.SerialCommunication.Project/demo.png)
