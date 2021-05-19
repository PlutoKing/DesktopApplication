/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldInfo
 * Created      : 2020-09-28 10:10:40
 * Author       : Xu Zhe
 * Description  : 世界信息
 * ──────────────────────────────────────────────────────────────*/

using System.ComponentModel;

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

        private string _path = @"I:\DesktopApplication\LF.Project\LFProject\Projects\LF.FictionWorld.Project\bin\Debug\Files";

        private LFCalendar _Calendar = new LFCalendar();    // 历法
        private LFDate _nowDate = new LFDate();             // 当前日期

        #endregion

        #region Properties

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get => _path; set => _path = value; }

        /// <summary>
        /// 历法
        /// </summary>
        public LFCalendar Calendar { get => _Calendar; set => _Calendar = value; }
        
        /// <summary>
        /// 当前日期
        /// </summary>
        public LFDate NowDate { get => _nowDate; set => _nowDate = value; }

        
        #endregion

        #region Constructors
        public LFWorldInfo()
        {
        }

    #endregion

        #region Methods

        public void Open()
        {
            Calendar.Open(Path + @"\Common");

            NowDate = new LFDate(326280316);
        }

        
        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion
}
}
