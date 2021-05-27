/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldInfo.cs
 * Created      : 2021-05-25 11:18:29
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 世界信息
    /// </summary>
    public class LFWorldInfo : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private string _path;

        private LFCalendar _calendar;   // 历法

        private LFDate _nowDate;        // 当前日期

        private LFXiuLian _xiuLian;     // 修炼模型

        #endregion

        #region Properties

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
            }
        }

        /// <summary>
        /// 历法
        /// </summary>
        public LFCalendar Calendar
        {
            get => _calendar;
            set
            {
                _calendar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Calendar"));
            }
        }

        /// <summary>
        /// 当前日期
        /// </summary>
        public LFDate NowDate
        {
            get => _nowDate;
            set
            {
                _nowDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NowDate"));
            }
        }
        /// <summary>
        /// 修炼模型
        /// </summary>
        public LFXiuLian XiuLian
        {
            get => _xiuLian;
            set
            {
                _xiuLian = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XiuLian"));
            }
        }


        #endregion

        #region Constructors
        public LFWorldInfo()
        {
            _path = @"E:\DesktopApplication\LF.Project\LF.Novel\LF.FictionWorld.Project\bin\Debug\File";
            _calendar = new LFCalendar();
        }

        #endregion

        #region Methods
        /// <summary>
        /// 打开世界信息
        /// </summary>
        public void Open()
        {
            _calendar.Open(Path + @"\Info");
        }

        /// <summary>
        /// 保存世界信息
        /// </summary>
        public void Save()
        {

        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}