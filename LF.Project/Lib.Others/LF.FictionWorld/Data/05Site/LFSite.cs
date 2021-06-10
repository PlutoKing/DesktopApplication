/*──────────────────────────────────────────────────────────────
 * FileName     : LFSite.cs
 * Created      : 2021-05-28 10:16:29
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 地点
    /// </summary>
    public class LFSite : INotifyPropertyChanged, ICloneable
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

        private LFType _area1 = new LFType();   // 所属一级区域
        private LFType _area2 = new LFType();   // 所属二级区域
        private LFType _area3 = new LFType();   // 所属三级区域
        private LFSubSiteList _subSites = new LFSubSiteList();  // 子地点

        private LFPointerList _itemList = new LFPointerList();  // 物产
        private LFPointerList _sectList = new LFPointerList();  // 势力

        private LFVariableList _masterSect = new LFVariableList();
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
        /// 所属区域
        /// </summary>
        public string Area
        {
            get { return _area1.Value + " - " + _area2.Value + " - " + _area3.Value; }
        }

        /// <summary>
        /// 索取区域索引
        /// </summary>
        public int AreaIndex
        {
            get { return _area1.Code * 100 + _area2.Code * 10 + _area3.Code; }
        }

        /// <summary>
        /// 地名全称
        /// </summary>
        public string FullAddress
        {
            get { return _area1.Value + " - " + _area2.Value + " - " + _area3.Value + " - " + _name; }
        }

        /// <summary>
        /// 1级区划
        /// </summary>
        public LFType Area1
        {
            get => _area1;
            set
            {
                _area1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area1"));
            }
        }

        /// <summary>
        /// 2级区划
        /// </summary>
        public LFType Area2
        {
            get => _area2;
            set
            {
                _area2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area2"));
            }
        }

        /// <summary>
        /// 3级区划
        /// </summary>
        public LFType Area3
        {
            get => _area3;
            set
            {
                _area3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area3"));
            }
        }
        /// <summary>
        /// 子地点
        /// </summary>
        public LFSubSiteList SubSites
        {
            get => _subSites;
            set
            {
                _subSites = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SubSites"));
            }
        }

        #endregion

        #region Data Properties
        /// <summary>
        /// 物产指针列表
        /// </summary>
        public LFPointerList ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemList"));
            }
        }
        /// <summary>
        /// 势力指针列表
        /// </summary>
        public LFPointerList SectList
        {
            get => _sectList;
            set
            {
                _sectList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SectList"));
            }
        }

        /// <summary>
        /// 所属势力
        /// </summary>
        public LFVariableList MasterSect
        {
            get => _masterSect;
            set
            {
                _masterSect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MasterSect"));
            }
        }


        #endregion

        #endregion

        #region Constructors
        public LFSite()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的地点实例。
        /// </summary>
        /// <param name="code">编码。</param>
        /// <param name="name">名称。</param>
        public LFSite(long code, string name)
        {
            _code = code;
            _name = name;
            Decode();       // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFSite(LFSite rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _id = rhs._id;
            _brief = rhs._brief;

            _area1 = rhs._area1;
            _area2 = rhs._area2;
            _area3 = rhs._area3;
            _subSites = rhs._subSites.Clone();

            _itemList = rhs._itemList.Clone();
            _sectList = rhs._sectList.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSite Clone()
        {
            return new LFSite(this);
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
        /// 解码
        /// </summary>
        public void Decode()
        {
            int add1 = (int)(_code / 10000);
            int add2 = (int)((_code % 10000) / 1000);
            int add3 = (int)((_code % 1000) / 100);
            _id = (int)(_code % 100);

            _area1 = World.Setting.Areas.GetType(add1);
            _area2 = World.Setting.Areas.GetChilds(add1).GetType(add2);
            _area3 = World.Setting.Areas.GetChilds(add1).GetChilds(add2).GetType(add3);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            int index = _area1.Code * 100;
            index += _area2.Code * 10;
            index += _area3.Code;

            _id = GetID(index);

            _code = index * 100 + ID;
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="id"></param>
        public bool SetID(int id)
        {
            bool res = false;
            int code = _area1.Code * 100;
            code += _area2.Code * 10;
            code += _area3.Code;


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
            LFSiteList tmp = World.Data.SiteList.GetSiteGroup(idx);

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

        /// <summary>
        /// 检测物品
        /// </summary>
        public void CheckItems()
        {
            foreach(LFPointer obj in _itemList)
            {
                LFItem item = obj.GetItem();
                if (!item.SiteList.IsContains(this.Code))
                    item.SiteList.Add(new LFPointer(this));
            }
        }

        #endregion

        #region File Methods

        /// <summary>
        /// 保存地点文件
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            /* 1. 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Site");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 2. 开始写入 */
            root.SetAttribute("Code", _code.ToString());
            root.SetAttribute("Name", _name);

            XmlElement eleItems = xmlDoc.CreateElement("Items");
            foreach (LFPointer obj in _itemList)
            {
                XmlElement ele = xmlDoc.CreateElement("Item");
                ele.SetAttribute("Code", obj.Code.ToString());
                eleItems.AppendChild(ele);
            }
            root.AppendChild(eleItems);


            /* 3. 保存文件 */
            xmlDoc.Save(path + @"\Sites\" + _code.ToString() + ".xml");
        }

        /// <summary>
        /// 打开地点文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* 1. XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                             // 定义文件
            xmlDoc.Load(path + @"\Sites\" + _code.ToString() + ".xml");         // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                           // 读取根节点

            /* 2. 开始读取
             * 2.1 读取物品指针 */
            XmlElement eleItmes = (XmlElement)root.GetElementsByTagName("Items")[0];
            foreach (XmlNode node in eleItmes.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                long index = Convert.ToInt64(ele.GetAttribute("Code"));

                LFItem item = World.Data.ItemList.GetItem(index);
                if (item != null)
                {
                    LFPointer p = new LFPointer(item);
                    _itemList.Add(p);
                }
            }
        }
        #endregion

        #endregion
    }
}