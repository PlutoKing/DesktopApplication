/*──────────────────────────────────────────────────────────────
 * FileName     : LFSect.cs
 * Created      : 2021-05-28 17:13:11
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
    /// 势力
    /// </summary>
    public class LFSect : INotifyPropertyChanged, ICloneable
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

        private LFDate _createdDate = new LFDate();    // 创建时间
        private LFDate _endDate = new LFDate();        // 灭亡时间
        private LFSite _location = new LFSite();       // 地理位置
        private double _age;             // 传承年数

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
        /// 建立时间
        /// </summary>
        public LFDate CreatedDate
        {
            get => _createdDate;
            set
            {
                _createdDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CreatedDate"));
            }
        }
        /// <summary>
        /// 灭亡时间
        /// </summary>
        public LFDate EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EndDate"));
            }
        }
        /// <summary>
        /// 地点
        /// </summary>
        public LFSite Location
        {
            get => _location;
            set
            {
                _location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Location"));
            }
        }
        /// <summary>
        /// 传承年数
        /// </summary>
        public double Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Age"));
            }
        }

        #endregion

        #endregion

        #region Constructors
        public LFSect()
        {

        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="code">索引</param>
        /// <param name="name">名称</param>
        public LFSect(long code, string name)
        {
            _code = code;
            _name = name;

            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFSect(LFSect obj)
        {
            _code = obj._code;
            _name = obj._name;
            _id = obj._id;
            _brief = obj._brief;

            _createdDate = obj._createdDate.Clone();
            _endDate = obj._endDate.Clone();
            _location = obj._location;
            _age = obj._age;

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSect Clone()
        {
            return new LFSect(this);
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
        public void Decode()
        {
            _createdDate = new LFDate((int)(_code / 10000000));
            int a = (int)(_code % 10000000);
            _age = World.Info.NowDate.GetAge(_createdDate);

            int loc = (int)(a / 100);
            _location = World.Data.SiteList.GetSite(loc);

            _id = (int)(a % 100);
        }

        /// <summary>
        /// 生成索引
        /// </summary>
        public void Encode()
        {
            long code = _createdDate.Code * 100000;
            code += _location.Code;

            

            _code = code * 100 + _id;
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="id"></param>
        public bool SetID(int id)
        {
            bool res = false;
            long code = _createdDate.Code * 100000;
            code += _location.Code;


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
        /// <param name="idx">尾声为0的code</param>
        /// <returns></returns>
        public int GetID(long idx)
        {
            /* 找到同类 */
            LFSectList tmp = World.Data.SectList.GetSectGroup(idx);

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

        #region Data Methods

        public void CheckSite()
        {
            Location.SectList.AddObj(new LFPointer(this));
        }

        #endregion

        #endregion
    }
}