/*──────────────────────────────────────────────────────────────
 * FileName     : LFPlot
 * Created      : 2020/9/28 22:58:42
 * Author       : Xu Zhe
 * Description  : 情节
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Xml;


namespace LF.FictionWorld
{
    public class LFPlot : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _index;        // 索引
        private string _name;       // 名称
        private int _id;            // ID
        private string _brief;      // 简介

        private LFDate _date = new LFDate();    // 时间
        private LFSite _site = new LFSite();    // 地点

        private LFRoleList _roles = new LFRoleList();       // 角色列表
        private LFActionList _actions = new LFActionList(); // 动作列表
        #endregion

        #region Properties

        /// <summary>
        /// 索引
        /// </summary>
        public long Index { get => _index; set => _index = value; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => _name; set => _name = value; }
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }
        /// <summary>
        /// 日期
        /// </summary>
        public LFDate Date { get => _date; set => _date = value; }
        /// <summary>
        /// 地址
        /// </summary>
        public LFSite Site { get => _site; set => _site = value; }
        /// <summary>
        /// 角色
        /// </summary>
        public LFRoleList Roles { get => _roles; set => _roles = value; }
        /// <summary>
        /// 动作
        /// </summary>
        public LFActionList Actions { get => _actions; set => _actions = value; }
        #endregion

        #region Constructors
        public LFPlot()
        {

        }

        public LFPlot(long index,string name)
        {
            _index = index;
            _name = name;

            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFPlot(LFPlot rhs)
        {
            this._index = rhs._index;
            this._id = rhs._id;
            this._name = rhs._name;
            this._brief = rhs._brief;

            this._date = rhs._date;
            this._site = rhs._site;
            this._roles = rhs._roles.Clone();
            this._actions = rhs._actions.Clone();
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
            int date = (int)(_index / 10000000);
            _date = new LFDate(date);

            int loc = (int)((_index % 10000000) / 100);
            _site = World.Data.SiteList.GetLocation(loc);

            _id =(int)( _index % 100);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            long index = _date.Code * 100000;
            index += _site.Index;

            /* 重复性检测 */
            if (_id == 0)
            {
                _id = 1;
                foreach (LFPlot plot in World.Data.PlotList)
                {
                    if (plot.Index / 100 == index)
                    {
                        _id++;
                    }
                }
            }

            _index = index * 100 + _id;
        }
        #endregion

        #region Operations
        /// <summary>
        /// 添加角色并检查
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool AddRole(LFRole role)
        {
            foreach (LFRole r in _roles)
            {
                if (r == role)
                {
                    return false;
                }
            }

            _roles.Add(role);
            _roles.Sort();
            return true;
        }

        /// <summary>
        /// 移除角色
        /// </summary>
        /// <param name="role"></param>
        public void RemoveRole(LFRole role)
        {
            _roles.Remove(role);
        }
        #endregion

        #region File

        /// <summary>
        /// 保存情节内容
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Plot");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            root.SetAttribute("Index", _index.ToString());
            root.SetAttribute("Name", _name);

            XmlElement eleRoles = xmlDoc.CreateElement("Roles");
            foreach (LFRole role in _roles)
            {
                XmlElement eleRole = xmlDoc.CreateElement("Role");
                eleRole.SetAttribute("Index", role.Index.ToString());
                eleRoles.AppendChild(eleRole);
            }
            root.AppendChild(eleRoles);

            XmlElement eleActions = xmlDoc.CreateElement("Actions");

            foreach(LFAction a in _actions)
            {
                XmlElement ele = xmlDoc.CreateElement("Action");
                
                ele.SetAttribute("ID", a.ID.ToString());
                ele.SetAttribute("Name", a.Role.Name);
                ele.SetAttribute("Type", a.Type.Index.ToString());
                ele.SetAttribute("Content", a.Content);
                eleActions.AppendChild(ele);
            }
            root.AppendChild(eleActions);

            /* 保存文件 */
            xmlDoc.Save(path + @"\Plots\" + _index.ToString() + ".xml");
        }



        /// <summary>
        /// 打开情节内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                         // 定义文件
            xmlDoc.Load(path + @"\Plots\" + _index.ToString() + ".xml");    // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                       // 读取根节点

            XmlElement eleRoles = (XmlElement)root.GetElementsByTagName("Roles")[0];
            foreach (XmlNode node in eleRoles.ChildNodes)
            {
                XmlElement eleRole = (XmlElement)node;

                long idx = Convert.ToInt64(eleRole.GetAttribute("Index"));

                LFRole role = World.Data.RoleList.GetRole(idx);
                if (role != null)
                {
                    LFRole r = role.Clone();
                    r.Ranks.GetValue(_date);
                    _roles.Add(r);
                }
            }

            XmlElement eleActions = (XmlElement)root.GetElementsByTagName("Actions")[0];
            foreach(XmlNode node in eleActions.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;

                LFAction a = new LFAction
                {
                    ID = Convert.ToInt32(ele.GetAttribute("ID")),
                    Role = _roles.GetRole(ele.GetAttribute("Name")),
                    Type = World.Setting.Actions.GetType(Convert.ToInt32(ele.GetAttribute("Type"))),
                    Content = ele.GetAttribute("Content")
                };
                _actions.Add(a);
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
