/*──────────────────────────────────────────────────────────────
 * FileName     : LFLink.cs
 * Created      : 2021-04-26 11:18:21
 * Author       : Xu Zhe
 * Description  : 蓝风通信协议
 *                Index | Content | Value | Brief
 *                ------|---------|-------|----------
 *                0     | STA     | 0xAA  | 帧头
 *                1     | STA     | 0xB5  | 帧头
 *                2     | LNG     | 0~255 | 消息长度
 *                3     | SEQ     | 0~255 | 消息序列
 *                4     | SID     | 0~255 | 发送端ID
 *                5     | RID     | 0~255 | 接收端ID
 *                6     | MID     | 0~255 | 消息类型
 *                7~7+n | MSG     | 0~255 | 消息内容
 *                8+n   | CKA     | 0~255 | 校验和高位
 *                9+n   | CKA     | 0~255 | 校验和低位
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SerialCommunication
{
    /// <summary>
    /// 蓝风通信协议
    /// </summary>
    public class LFLink
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFLink()
        {
        }
        #endregion

        #region Methods

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}