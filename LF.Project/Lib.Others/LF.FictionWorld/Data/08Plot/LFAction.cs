/*──────────────────────────────────────────────────────────────
 * FileName     : LFAction.cs
 * Created      : 2021-05-30 12:34:42
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
    /// <summary>
    /// 动作
    /// </summary>
    public class LFAction : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;            // ID

        private LFType _type = new LFType();    // 类别

        private string _content;    // 内容

        #endregion

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get => _id; set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }

        

        /// <summary>
        /// 类型
        /// </summary>
        public LFType Type
        {
            get => _type; set
            {
                _type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get => _content; set
            {
                _content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }

        #endregion

        #region Constructors
        public LFAction()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFAction(LFAction rhs)
        {
            _id = rhs._id;

           _type = rhs._type;

            _content = rhs._content;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFAction Clone()
        {
            return new LFAction(this);
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Methods

        #endregion
    }
}