/*──────────────────────────────────────────────────────────────
 * FileName     : LFSite
 * Created      : 2020-09-28 11:04:11
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFSite : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _index;             // 索引号
        private int _id;                // ID
        private string _name = "NaN";   // 名称
        private string _brief = "NaN";  // 简介

        private LFType _area1 = new LFType();   // 所属一级区域
        private LFType _area2 = new LFType();   // 所属二级区域
        private LFType _area3 = new LFType();   // 所属三级区域

        private LFSubSiteList _subSites = new LFSubSiteList();  // 子地点

        private LFItemList _items = new LFItemList();               // 物品列表
        private LFSectList _sects = new LFSectList();               // 势力列表
        private LFVariableList _masterSect = new LFVariableList();  // 所属势力（随时间变化）
        #endregion

        #region Properties

        /// <summary>
        /// 所属区域
        /// </summary>
        public string Area
        {
            get { return _area1.Value + "-" + _area2.Value + "-" + _area3.Value; }
        }

        /// <summary>
        /// 索取区域索引
        /// </summary>
        public int AreaIndex
        {
            get { return _area1.Index * 100 + _area2.Index * 10 + _area3.Index; }
        }

        /// <summary>
        /// 地名全称
        /// </summary>
        public string FullAddress
        {
            get { return _area1.Value + "-" + _area2.Value + "-" + _area3.Value + "-" + _name; }
        }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get => _index; set => _index = value; }

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
        /// 1级区划
        /// </summary>
        public LFType Area1 { get => _area1; set => _area1 = value; }

        /// <summary>
        /// 2级区划
        /// </summary>
        public LFType Area2 { get => _area2; set => _area2 = value; }

        /// <summary>
        /// 3级区划
        /// </summary>
        public LFType Area3 { get => _area3; set => _area3 = value; }

        /// <summary>
        /// 子地点
        /// </summary>
        public LFSubSiteList SubSites { get => _subSites; set => _subSites = value; }

        /// <summary>
        /// 物品
        /// </summary>
        public LFItemList Items { get => _items; set => _items = value; }
        /// <summary>
        /// 势力
        /// </summary>
        public LFSectList Sects { get => _sects; set => _sects = value; }
        /// <summary>
        /// 所属势力（随时间变化）
        /// </summary>
        public LFVariableList MasterSect { get => _masterSect; set => _masterSect = value; }
        #endregion

        #region Constructors
        public LFSite()
        {
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFSite(int index, string name)
        {
            _index = index;
            _name = name;

            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <returns></returns>
        public LFSite(LFSite rhs)
        {
            this._index = rhs._index;
            this._name = rhs._name;
            this._id = rhs._id;
            this._brief = rhs._brief;

            this._area1 = rhs._area1;
            this._area2 = rhs._area2;
            this._area3 = rhs._area3;

            this._subSites = rhs._subSites.Clone();
            this._items = rhs._items.Clone();
            this._sects = rhs._sects.Clone();
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
            return this.Clone();
        }
        #endregion

        #region Methods

        #region Common
        /// <summary>
        /// 解码
        /// </summary>
        public void Decode()
        {
            int add1 = _index / 10000;
            int add2 = (_index % 10000) / 1000;
            int add3 = (_index % 1000) / 100;
            _id = _index % 100;

            _area1 = World.Setting.Areas.GetType(add1);
            _area2 = World.Setting.Areas.GetChilds(add1).GetType(add2);
            _area3 = World.Setting.Areas.GetChilds(add1).GetChilds(add2).GetType(add3);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            int index = _area1.Index * 100;
            index += _area2.Index * 10;
            index += _area3.Index;

            _id = 1;
            foreach (LFSite site in World.Data.SiteList.GetAreaSub(index))
            {
                if (site.Index / 100 == index)
                {
                    if (site.Name != Name)
                        _id++;
                    else
                        break;
                }
            }

            _index = index * 100 + _id;
        }

        /// <summary>
        /// 检测物品
        /// </summary>
        public void CheckItems()
        {
            foreach(LFItem item in this.Items)
            {
                if(item != null)
                {
                    bool tmp = false;
                    foreach (LFSite site in item.Locations)
                    {
                        if (site.Name == _name)
                        {
                            tmp = true;
                            break;
                        }
                    }
                    if (!tmp)
                    {
                        item.Locations.Add(this);
                    }

                }
            }
        }
        #endregion

        #region File

        /// <summary>
        /// 保存位置列表
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Site");                    // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            root.SetAttribute("Index", _index.ToString());
            root.SetAttribute("Name", _name);

            XmlElement eleRanks = xmlDoc.CreateElement("Items");
            foreach (LFItem obj in _items)
            {
                XmlElement ele = xmlDoc.CreateElement("Item");
                ele.SetAttribute("Index", obj.Index.ToString());
                eleRanks.AppendChild(ele);
            }
            root.AppendChild(eleRanks);

            XmlElement eleSects = xmlDoc.CreateElement("Sects");
            foreach (LFSect obj in _sects)
            {
                if(obj != null)
                {
                    XmlElement ele = xmlDoc.CreateElement("Sect");
                    ele.SetAttribute("Index", obj.Index.ToString());
                    eleSects.AppendChild(ele);
                }
            }
            root.AppendChild(eleSects);

            /* 保存文件 */
            xmlDoc.Save(path + @"\Sites\" + _index.ToString() + ".xml");
        }

        /// <summary>
        /// 打开位置列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                     // 定义文件
            xmlDoc.Load(path + @"\Sites\" + _index.ToString() + ".xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                   // 读取根节点

            XmlElement eleItmes = (XmlElement)root.GetElementsByTagName("Items")[0];
            foreach (XmlNode node in eleItmes.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                long index = Convert.ToInt64(ele.GetAttribute("Index"));
                
                LFItem item = World.Data.ItemList.GetItem(index);
                if (item != null)
                    _items.Add(item);
            }
            CheckItems();

            XmlElement eleSects = (XmlElement)root.GetElementsByTagName("Sects")[0];
            foreach (XmlNode node in eleSects.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                long index = Convert.ToInt64(ele.GetAttribute("Index"));
                LFSect obj = World.Data.SectList.GetSect(index);
                _sects.Add(obj);
            }

            
        }

        #endregion

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
