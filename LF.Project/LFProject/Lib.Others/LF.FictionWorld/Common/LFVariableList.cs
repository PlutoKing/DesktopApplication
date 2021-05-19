/*──────────────────────────────────────────────────────────────
 * FileName     : LFVariableList
 * Created      : 2020-09-28 11:32:58
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 变量列表
    /// </summary>
    public class LFVariableList : ObservableCollection<LFVariable>, ICloneable
    {
        #region Fields

        private string _value = "";


        #endregion

        #region Properties

        public string Value { get => _value; set => _value = value; }


        #endregion

        #region Constructors
        public LFVariableList()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFVariableList(LFVariableList rhs)
        {
            _value = rhs._value;
            foreach (LFVariable obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFVariableList Clone()
        {
            return new LFVariableList(this);
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

        /// <summary>
        /// 计算当前值
        /// </summary>
        /// <param name="date"></param>
        public void GetValue(LFDate date)
        {
            foreach (LFVariable v in this)
            {
                if (v.Date.IsBefore(date))
                {
                    _value = v.Value;
                }
            }
        }

        /// <summary>
        /// 计算当变量
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public LFVariable GetVariable(long idx)
        {
            foreach (LFVariable v in this)
            {
                if (v.Index == idx)
                {
                    return v;
                }
            }
            return null;
        }

        #region Sort

        /// <summary>
        /// 按照ID排序
        /// </summary>
        public void Sort()
        {
            List<LFVariable> list = new List<LFVariable>();
            foreach (LFVariable obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFVariable O1, LFVariable O2) { return O1.Date.Code.CompareTo(O2.Date.Code); });
            this.Clear();
            foreach (LFVariable obj in list)
            {
                this.Add(obj);
            }
        }

        #endregion

        #region Add



        #endregion



        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
