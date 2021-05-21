/*──────────────────────────────────────────────────────────────
 * FileName     : LFRelation.cs
 * Created      : 2021-05-20 11:13:55
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 关系
    /// </summary>
    public class LFRelation:INotifyPropertyChanged,ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;        // ID
        private LFType _type;   // 类型
        private long _index;    // 索引
        private LFDate _date;   // 起效时间

        private string _name;   // 姓名
        private string _value;  // 值
        #endregion

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }
        /// <summary>
        /// 类型
        /// </summary>
        public LFType Type { get => _type; set => _type = value; }
        /// <summary>
        /// 索引
        /// </summary>
        public long Index { get => _index; set => _index = value; }
        /// <summary>
        /// 起效日期
        /// </summary>
        public LFDate Date { get => _date; set => _date = value; }

        #endregion

        #region Constructors
        public LFRelation()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFRelation(LFRelation obj)
        {
            _id = obj._id;
            _type = obj._type;
            _index = obj._index;
            _date = obj._date;
            _name = obj._name;
            _value = obj._value;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFRelation Clone()
        {
            return new LFRelation(this);
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

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }

    
}