/*──────────────────────────────────────────────────────────────
 * FileName     : LFType
 * Created      : 2020-09-28 10:03:06
 * Author       : Xu Zhe
 * Description  : 分类方法
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{

    /// <summary>
    /// 分类方法
    /// </summary>
    public class LFType : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _index = 0;         // 索引号
        private string _value = "NaN";  // 值
        private string _brief = "NaN";  // 简介
        private LFTypeList _childs;     // 子分类
        
        #endregion

        #region Properties

        /// <summary>
        /// 索引号
        /// </summary>
        public int Index { get => _index; set => _index = value; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get => _value; set => _value = value; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }

        /// <summary>
        /// 子分类
        /// </summary>
        public LFTypeList Childs { get => _childs; set => _childs = value; }

        #endregion

        #region Constructors
        public LFType()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <returns></returns>
        public LFType(LFType rhs)
        {
            this._index = rhs._index;
            this._value = rhs._value;
            this._brief = rhs._brief;
            if (rhs._childs != null)
            {
                this._childs = new LFTypeList();
                this._childs = this._childs.Clone();
            }

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFType Clone()
        {
            return new LFType(this);
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
