/*──────────────────────────────────────────────────────────────
 * FileName     : LFPointerList.cs
 * Created      : 2021-05-28 15:54:35
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
    /// 指针列表
    /// </summary>
    public class LFPointerList : ObservableCollection<LFPointer>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFPointerList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFPointerList(LFPointerList rhs)
        {
            foreach (LFPointer obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该指针列表相同的新实例</returns>
        public LFPointerList Clone()
        {
            return new LFPointerList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该指针列表相同的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 按照编码Code排序
        /// </summary>
        public void Sort()
        {
            List<LFPointer> list = new List<LFPointer>();
            foreach (LFPointer obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFPointer O1, LFPointer O2) { return O1.Code.CompareTo(O2.Code); });
            this.Clear();
            foreach (LFPointer obj in list)
            {
                this.Add(obj);
            }
        }

        public string GetName(long code)
        {
            foreach(LFPointer obj in this)
            {
                if (obj.Code == code)
                    return obj.Name;
            }
            return "无";
        }

        /// <summary>
        /// 判断是否包含编码为<paramref name="code"/>的对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public bool IsContains(long code)
        {
            foreach (LFPointer obj in this)
            {
                if (obj.Code == code)
                    return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 添加指针
        /// </summary>
        /// <param name="pointer"></param>
        public void AddObj(LFPointer pointer)
        {
            if (!IsContains(pointer.Code))
            {
                Add(pointer);
                Sort();
            }
        }

        /// <summary>
        /// 编辑指针
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pointer"></param>
        public void EditObj(long code, LFPointer pointer)
        {
            foreach (LFPointer obj in this)
            {
                if (obj.Code == code)
                {
                    Remove(obj);
                    Add(pointer);
                    Sort();
                    return;
                }
            }
        }


        /// <summary>
        /// 移除指针
        /// </summary>
        /// <param name="code"></param>
        public void RemoveObj(long code)
        {
            foreach (LFPointer obj in this)
            {
                if (obj.Code == code)
                {
                    Remove(obj);
                    return;
                }
            }
        }

        #endregion
    }
}