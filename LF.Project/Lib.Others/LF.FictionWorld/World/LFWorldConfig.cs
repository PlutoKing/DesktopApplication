/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldConfig.cs
 * Created      : 2021-05-30 21:25:45
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFWorldConfig : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private LFXiulian _xiulian;     // 修炼模型


        #endregion

        #region Properties
        /// <summary>
        /// 修炼模型
        /// </summary>
        public LFXiulian Xiulian
        {
            get => _xiulian;
            set
            {
                _xiulian = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Xiulian"));
            }
        }

        #endregion

        #region Constructors
        public LFWorldConfig()
        {
            _xiulian = new LFXiulian();
        }
        #endregion

        #region Methods

        public void Open()
        {
            _xiulian.Open(World.Info.Path + @"\Config");
            _xiulian.GetParams();

        }

        #endregion
    }
}