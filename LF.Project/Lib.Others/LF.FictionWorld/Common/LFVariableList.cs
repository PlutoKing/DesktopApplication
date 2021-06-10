/*──────────────────────────────────────────────────────────────
 * FileName     : LFVariableList.cs
 * Created      : 2021-05-28 17:07:46
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
            foreach (LFVariable v in rhs)
            {
                Add(v.Clone());
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
            return Clone();
        }
        #endregion

        #region Methods

        #region Basic Methods
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
                if (v.Code == idx)
                {
                    return v;
                }
            }
            return null;
        }
        #endregion

        #region Sort

        /// <summary>
        /// 按照日期排序
        /// </summary>
        public void Sort()
        {
            List<LFVariable> list = new List<LFVariable>();
            foreach (LFVariable obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFVariable O1, LFVariable O2) { return O1.Date.Code.CompareTo(O2.Date.Code); });
            Clear();
            foreach (LFVariable obj in list)
            {
                Add(obj);
            }
        }

        public string GetValue(long code)
        {
            foreach(LFVariable obj in this)
            {
                if (obj.Code == code)
                    return obj.Value;
            }
            return "无";
        }
        
        #endregion

        #region Add
        public bool IsContains(LFVariable obj)
        {
            foreach(LFVariable v in this)
            {
                if (v.Equals(obj))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 添加变量
        /// </summary>
        public void AddObj(LFVariable obj)
        {
            /* 重复性检测 */
            if(!IsContains(obj))
            {
                /* 添加新变量 */
                Add(obj);
                Sort();
            }
        }

        #endregion

        #endregion
    }
}