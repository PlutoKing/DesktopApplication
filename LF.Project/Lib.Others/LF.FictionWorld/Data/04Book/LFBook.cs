/*──────────────────────────────────────────────────────────────
 * FileName     : LFBook.cs
 * Created      : 2021-05-27 16:36:53
 * Author       : Xu Zhe
 * Description  : 秘籍
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 秘籍
    /// </summary>
    public class LFBook : INotifyPropertyChanged, ICloneable
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
        private LFType _type = new LFType();        // 种类
        private LFAttribute _attributes = new LFAttribute();    // 属性

        private LFLevelList _content = new LFLevelList();       // 修炼内容

        private LFSkillList _skills = new LFSkillList();        // 包含技能
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
        /// 种类
        /// </summary>
        public LFType Type { get => _type;
            set
            {
                _type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
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
        /// 属性
        /// </summary>
        public string Attribute
        {
            get { return _attributes.ToString(); }
        }


        #endregion

        #region Content Properties

        /// <summary>
        /// 修炼内容
        /// </summary>
        public LFLevelList Content
        {
            get => _content;
            set
            {
                _content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }
        /// <summary>
        /// 包含技能
        /// </summary>
        public LFSkillList Skills
        {
            get => _skills;
            set
            {
                _skills = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
            }
        }


        #endregion

        #endregion

        #region Constructors
        public LFBook()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的秘籍实例
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        public LFBook(long code, string name)
        {
            _code = code;
            _name = name;
            Decode();       // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFBook(LFBook rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _id = rhs._id;
            _brief = rhs._brief;

            _level = rhs._level;
            _type = rhs._type;
            _attributes = rhs._attributes.Clone();

            _content = rhs._content.Clone();
            _skills = rhs._skills.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
        public LFBook Clone()
        {
            return new LFBook(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
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
            // 等级1位+种类1位+属性4位+编号3位=9位
            int l = (int)(_code / 100000000);
            int t = (int)(_code % 100000000)/ 10000000;
            int a = (int)(_code % 10000000) / 1000;
            _level = World.Setting.Levels.GetType(l);   // 调用配置信息
            _type = World.Setting.Books.GetType(t);
            _attributes = new LFAttribute(a);
            _id = (int)(_code % 1000);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            int index = _level.Index * 100000;
            index += _type.Index * 10000;
            index += _attributes.Code;

            /* 重复性检测 */
            _id = GetID(index);

            _code = index * 1000 + _id;
        }

        /// <summary>
        /// 计算ID
        /// </summary>
        /// <param name="idx">尾声为0的Index</param>
        /// <returns></returns>
        public int GetID(int idx)
        {
            /* 找到同类 */
            LFBookList tmp = World.Data.BookList.GetBookGroup(idx);

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

        public void SetID(int id)
        {
            int index = _level.Index * 100000;
            index += _type.Index * 10000;
            index += _attributes.Code;

            /* 重复性检测 */
            _id = id;

            _code = index * 1000 + _id;
        }
        #endregion

        #region Operating Methods

        public void AddLevel(LFLevel level)
        {

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
            XmlElement root = xmlDoc.CreateElement("Book");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            root.SetAttribute("Index", _code.ToString());
            root.SetAttribute("Name", _name);

            // 存入内容
            XmlElement eleContent = xmlDoc.CreateElement("Content");
            foreach(LFLevel lv in _content)
            {
                XmlElement ele = xmlDoc.CreateElement("Level");
                ele.SetAttribute("ID", lv.ID.ToString());
                ele.SetAttribute("Name", lv.Name);
                ele.SetAttribute("Brief", lv.Brief);
                eleContent.AppendChild(ele);
            }
            root.AppendChild(eleContent);

            XmlElement eleSkills = xmlDoc.CreateElement("Skills");
            foreach (LFSkill skill in _skills)
            {
                XmlElement ele = xmlDoc.CreateElement("Skill");
                ele.SetAttribute("ID", skill.ID.ToString());
                ele.SetAttribute("Name", skill.Name);
                ele.SetAttribute("Brief", skill.Brief);
                ele.SetAttribute("Level", skill.Level.ToString());
                ele.SetAttribute("Precondition", skill.Precondition.ToString());
                eleSkills.AppendChild(ele);
            }
            root.AppendChild(eleSkills);


            /* 保存文件 */
            xmlDoc.Save(path + @"\Books\" + _code.ToString() + ".xml");
        }



        /// <summary>
        /// 打开位置列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* 1. XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                         // 定义文件
            xmlDoc.Load(path + @"\Books\" + _code.ToString() + ".xml");    // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                       // 读取根节点

            /* 2. 读取内容 */
            XmlElement eleContent = (XmlElement)root.GetElementsByTagName("Content")[0];
            foreach (XmlNode node in eleContent.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFLevel lv = new LFLevel()
                {
                    ID = Convert.ToByte(ele.GetAttribute("ID")),
                    Name = ele.GetAttribute("Name"),
                    Brief = ele.GetAttribute("Brief")
                };
                _content.Add(lv);
            }

            XmlElement eleSkills = (XmlElement)root.GetElementsByTagName("Skills")[0];
            foreach (XmlNode node in eleSkills.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFSkill skill = new LFSkill()
                {
                    ID = Convert.ToByte(ele.GetAttribute("ID")),
                    Name = ele.GetAttribute("Name"),
                    Brief = ele.GetAttribute("Brief"),
                    Level = Convert.ToByte(ele.GetAttribute("Level")),
                    Precondition = Convert.ToByte(ele.GetAttribute("Precondition"))
                };
                _skills.Add(skill);
            }
        }
        #endregion

        #endregion
    }
}