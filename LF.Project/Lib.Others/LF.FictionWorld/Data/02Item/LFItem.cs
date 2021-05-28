/*──────────────────────────────────────────────────────────────
 * FileName     : LFItem.cs
 * Created      : 2021-05-25 20:48:36
 * Author       : Xu Zhe
 * Description  : 物品
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Xml;

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

        private long _code;     // 编码
        private int _id;        // ID
        private string _name;   // 名称
        private string _brief;  // 简介

        private LFType _level = new LFType();      // 等级

        private LFType _type1 = new LFType();      // 类别1
        private LFType _type2 = new LFType();      // 类别2

        private LFAttribute _attributes = new LFAttribute();    // 属性

        private float _price;   // 价格
        private int _count;     // 数量

        private LFPointerList _siteList = new LFPointerList();  // 产地
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
        /// 等级
        /// </summary>
        public LFType Level
        {
            get => _level;
            set
            {
                _level = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
            }
        }

        /// <summary>
        /// 类别1
        /// </summary>
        public LFType Type1
        {
            get => _type1;
            set
            {
                _type1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type1"));
            }
        }

        /// <summary>
        /// 类别2
        /// </summary>
        public LFType Type2
        {
            get => _type2;
            set
            {
                _type2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type2"));
            }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public LFAttribute Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Attributes"));
            }
        }

        /// <summary>
        /// 类别
        /// </summary>
        public string Type
        {
            get { return _type1.Value + " - " + _type2.Value; }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public string Attribute
        {
            get { return _attributes.ToString(); }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get => _price;
            set
            {
                _price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count {
            get => _count;
            set
            {
                _count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }

        #endregion

        #region Data Properties
        /// <summary>
        /// 产地
        /// </summary>
        public LFPointerList SiteList
        {
            get => _siteList;
            set
            {
                _siteList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public LFItem()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的物品实例。
        /// </summary>
        /// <param name="code">编码。</param>
        /// <param name="name">名称。</param>
        public LFItem(long code, string name)
        {
            _code = code;
            _name = name;
            Decode();       // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFItem(LFItem rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _id = rhs._id;
            _brief = rhs._brief;

            _level = rhs._level;
            _type1 = rhs._type1;
            _type2 = rhs._type2;

            _attributes = rhs._attributes.Clone();
            _price = rhs._price;
            _count = rhs._count;
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
            long code = _level.Index * 1000000;
            code += _type1.Index * 100000;
            code += _type2.Index * 10000;
            code += _attributes.Code;

            _id = GetID(code);

            _code = code * 1000 + _id;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode()
        {
            int l = (int)(_code / 1000000000);
            int t1 = (int)(_code % 1000000000) / 100000000;
            int t2 = (int)(_code % 100000000) / 10000000;
            int a = (int)(_code % 10000000) / 1000;

            _level = World.Setting.Levels.GetType(l);
            _type1 = World.Setting.Items.GetType(t1);
            _type2 = World.Setting.Items.GetChilds(t1).GetType(t2);
            _attributes = new LFAttribute(a);

            _id = (int)(_code % 1000);
        } 

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="id"></param>
        public bool SetID(int id)
        {
            bool res = false;
            long code = _level.Index * 1000000;
            code += _type1.Index * 100000;
            code += _type2.Index * 10000;
            code += _attributes.Code;

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
            LFItemList tmp = World.Data.ItemList.GetItemGroup(idx);

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

        #region File Methods

        /// <summary>
        /// 保存秘籍
        /// </summary>
        /// <param name="path">路径</param>
        public void Save(string path)
        {
            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Item");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 存入内容
            root.SetAttribute("Index", _code.ToString());
            root.SetAttribute("Name", _name);
            root.SetAttribute("Price", _price.ToString());
            root.SetAttribute("Count", _count.ToString());

            XmlElement eleSites = xmlDoc.CreateElement("Sites");
            foreach (LFPointer obj in _siteList)
            {
                XmlElement ele = xmlDoc.CreateElement("Site");
                ele.SetAttribute("Code", obj.Code.ToString());
                eleSites.AppendChild(ele);
            }
            root.AppendChild(eleSites);


            /* 保存文件 */
            xmlDoc.Save(path + @"\Items\" + _code.ToString() + ".xml");
        }



        /// <summary>
        /// 打开物品
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* 1. XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                         // 定义文件
            xmlDoc.Load(path + @"\Items\" + _code.ToString() + ".xml");    // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                       // 读取根节点

            /* 2. 读取内容 */
            _price = Convert.ToSingle(root.GetAttribute("Price"));
            _count = Convert.ToInt32(root.GetAttribute("Count"));

            XmlElement eleSites = (XmlElement)root.GetElementsByTagName("Sites")[0];
            foreach (XmlNode node in eleSites.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                long code = Convert.ToInt64(ele.GetAttribute("Code"));

                LFSite site = World.Data.SiteList.GetSite(code);
                if (site != null)
                {
                    LFPointer p = new LFPointer(site);
                    _siteList.Add(p);
                }
            }
        }
        #endregion

        #endregion
    }
}