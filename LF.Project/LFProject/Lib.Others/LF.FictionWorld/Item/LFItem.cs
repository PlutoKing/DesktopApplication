/*──────────────────────────────────────────────────────────────
 * FileName     : LFItem
 * Created      : 2020-09-28 10:45:20
 * Author       : Xu Zhe
 * Description  : 物品
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 物品
    /// </summary>
    public class LFItem : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _index;            // 索引
        private int _id;                // ID
        private string _name = "NaN";   // 名称
        private string _brief = "NaN";  // 简介

        private LFType _level = new LFType();      // 等级

        private LFType _type1 = new LFType();      // 类别1
        private LFType _type2 = new LFType();      // 类别2

        private LFAttribute _attributes = new LFAttribute();    // 属性

        private LFItemList _recipe = new LFItemList();          // 配方

        private LFSiteList _locations = new LFSiteList();       // 产地
        #endregion

        #region Properties

        /// <summary>
        /// 索引
        /// </summary>
        public long Index { get => _index; set => _index = value; }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }

        /// <summary>
        /// 等级
        /// </summary>
        public LFType Level { get => _level; set => _level = value; }

        /// <summary>
        /// 类别1
        /// </summary>
        public LFType Type1 { get => _type1; set => _type1 = value; }

        /// <summary>
        /// 类别2
        /// </summary>
        public LFType Type2 { get => _type2; set => _type2 = value; }

        /// <summary>
        /// 属性
        /// </summary>
        public LFAttribute Attributes { get => _attributes; set => _attributes = value; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Type
        {
            get { return _type1.Value + "-" + _type2.Value; }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public string Attribute
        {
            get
            {
                return _attributes.ToString();
            }
        }

        /// <summary>
        /// 配方
        /// </summary>
        public LFItemList Recipe { get => _recipe; set => _recipe = value; }
        public LFSiteList Locations { get => _locations; set => _locations = value; }

        #endregion

        #region Constructors
        public LFItem()
        {
        }

        public LFItem(Int64 index, string name)
        {
            _index = index;
            _name = name;

            Decode();   // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFItem(LFItem rhs)
        {
            this._index = rhs._index;
            this._name = rhs._name;
            this._id = rhs._id;
            this._brief = rhs._brief;

            this._level = rhs._level;
            this._type1 = rhs._type1;
            this._type2 = rhs._type2;

            this._attributes = rhs._attributes.Clone();

            _locations = rhs.Locations;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFItem Clone()
        {
            return new LFItem(this);
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
        /// 编码
        /// </summary>
        public void Encode()
        {
            long index = _level.Index * 1000000;
            index += _type1.Index * 100000;
            index += _type2.Index * 10000;
            index += _attributes.Code;

            /* ID 重复性检测 */
            if (_id == 0)
            {
                _id = 1;
                foreach (LFItem obj in World.Data.ItemList)
                {
                    if (obj.Index / 1000 == index)
                    {
                        _id++;
                    }
                }
            }

            _index = index * 1000 + _id;

        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode()
        {
            int l = (int)(_index / 1000000000);
            int t1 = (int)(_index % 1000000000) / 100000000;
            int t2 = (int)(_index % 100000000) / 10000000;
            int a = (int)(_index % 10000000) / 1000;

            _level = World.Setting.Levels.GetType(l);
            _type1 = World.Setting.Items.GetType(t1);
            _type2 = World.Setting.Items.GetChilds(t1).GetType(t2);
            _attributes = new LFAttribute(a);

            _id = (int)(_index % 1000);
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
