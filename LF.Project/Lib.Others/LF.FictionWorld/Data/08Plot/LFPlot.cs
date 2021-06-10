/*──────────────────────────────────────────────────────────────
 * FileName     : LFPlot.cs
 * Created      : 2021-05-30 12:24:03
 * Author       : Xu Zhe
 * Description  : 情节
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 情节
    /// </summary>
    public class LFPlot : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _code;     // 编码
        private int _id;        // ID
        private string _name;   // 名称
        private string _brief;  // 简介

        private LFDate _date = new LFDate();    // 时间
        private LFSite _site = new LFSite();    // 地点

        private LFActionList _actionList = new LFActionList();  // 动作列表

        private LFPointerList _roleList = new LFPointerList();  // 角色列表
        #endregion

        #region Properties

        #region Basic Properties
        /// <summary>
        /// 编码
        /// </summary>
        public long Code
        {
            get => _code;
            set
            {
                _code = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Code"));
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get => _brief;
            set
            {
                _brief = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brief"));
            }
        }

        #endregion

        #region Info Properties
        /// <summary>
        /// 日期
        /// </summary>
        public LFDate Date
        {
            get => _date;
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public LFSite Site
        {
            get => _site;
            set
            {
                _site = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Site"));
            }
        }
        /// <summary>
        /// 动作列表
        /// </summary>
        public LFActionList ActionList
        {
            get => _actionList;
            set
            {
                _actionList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActionList"));
            }
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        public LFPointerList RoleList
        {
            get => _roleList;
            set
            {
                _roleList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoleList"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public LFPlot()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的情节实例。
        /// </summary>
        /// <param name="code">编码。</param>
        /// <param name="name">名称。</param>
        public LFPlot(long code, string name)
        {
            _code = code;
            _name = name;
            Decode();       // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFPlot(LFPlot rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _id = rhs._id;
            _brief = rhs._brief;

            _date = rhs._date;
            _site = rhs._site;

            _actionList = rhs._actionList.Clone();
            _roleList = rhs._roleList.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFPlot Clone()
        {
            return new LFPlot(this);
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

        #region Common Methods
        /// <summary>
        /// 编码方式
        /// </summary>
        public void Encode()
        {
            long code = _date.Code * 100000;
            code += _site.Code;

            /* 重复性检测 */
            _id = GetID(code);

            _code = code * 100 + _id;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode()
        {
            int date = (int)(_code / 10000000);
            _date = new LFDate(date);

            int loc = (int)(_code % 10000000 / 100);
            _site = World.Data.SiteList.GetSite(loc);

            _id = (int)(_code % 100);
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="id"></param>
        public bool SetID(int id)
        {
            bool res = false;
            long code = _date.Code * 100000;
            code += _site.Code;

            if (_id != id)
            {
                _id = id;
                res = true;
            }
            _code = code * 100 + _id;

            return res;
        }


        /// <summary>
        /// 计算ID
        /// </summary>
        /// <param name="idx">尾数为0的code</param>
        /// <returns></returns>
        public int GetID(long idx)
        {
            /* 找到同类 */
            LFPlotList tmp = World.Data.PlotList.GetPlotGroup(idx);

            int cnt = tmp.Count;
            if (cnt == 0)
            {
                return 1;
            }

            for (int i = 0; i < cnt; i++)
            {
                if (tmp[i].ID != i + 1)
                {
                    return i + 1;
                }
                else
                {
                    if (tmp[i].ID == _id)
                        return tmp[i].ID;
                }

            }

            return cnt + 1;
        }

        #endregion

        #endregion
    }
}