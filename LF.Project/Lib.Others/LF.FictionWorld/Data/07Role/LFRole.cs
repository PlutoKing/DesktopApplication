/*──────────────────────────────────────────────────────────────
 * FileName     : LFRole.cs
 * Created      : 2021-05-27 18:30:03
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
    /// 角色
    /// </summary>
    public class LFRole : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        //readonly double[] p1 = new double[] { 3.75000000000000, -14.7222222222222, 50.4166666666663, 19.1269841269850, 100.238095238094 };
        //readonly double[] p2 = new double[] { -2.46778121540625e-05, 0.000431909586119701, -0.00312240270874285, 0.0125046074065508, -0.0349999386505692, 0.119450631281870 };

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _code;     // 编码
        private int _id;        // ID
        private string _name;   // 名称
        private string _brief;  // 简介

        private LFDate _birthday = new LFDate();        // 生日
        private LFSite _home = new LFSite();            // 籍贯
        private LFType _race = new LFType();            // 种族
        private LFType _gender = new LFType();          // 性别
        private double _age;                            // 年龄
        private double _talent;                         // 天赋值
        private LFAttribute _attributes = new LFAttribute();        // 属性
        private LFVariableList _ranks = new LFVariableList();       // 修炼等级
        private LFVariableList _experiences = new LFVariableList(); // 经历

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
        /// 生日
        /// </summary>
        public LFDate Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Birthday"));
            }
        }

        /// <summary>
        /// 籍贯
        /// </summary>
        public LFSite Home
        {
            get => _home;
            set
            {
                _home = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Home"));
            }
        }

        /// <summary>
        /// 年龄
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

        /// <summary>
        /// 种族
        /// </summary>
        public LFType Race
        {
            get => _race;
            set
            {
                _race = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Race"));
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public LFType Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gender"));
            }
        }

        /// <summary>
        /// 天赋
        /// </summary>
        public double Talent
        {
            get => _talent;
            set
            {
                _talent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Talent"));
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
        public LFVariableList Ranks
        {
            get => _ranks;
            set
            {
                _ranks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ranks"));
            }
        }

        /// <summary>
        /// 经历
        /// </summary>
        public LFVariableList Experiences
        {
            get => _experiences;
            set
            {
                _experiences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Experiences"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public LFRole()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的角色实例。
        /// </summary>
        /// <param name="code">编码。</param>
        /// <param name="name">名称。</param>
        public LFRole(long code, string name)
        {
            _code = code;
            _name = name;
            Decode();       // 解码
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFRole(LFRole rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _id = rhs._id;
            _brief = rhs._brief;

            _birthday = rhs._birthday;
            _home = rhs._home;
            _age = rhs._age;

            _race = rhs._race;
            _gender = rhs._gender;

            _talent = rhs._talent;
            _attributes = rhs._attributes.Clone();

            _ranks = rhs._ranks.Clone();
            _experiences = rhs._experiences.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFRole Clone()
        {
            return new LFRole(this);
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
            long code = _birthday.Code * 10000000;
            code += _home.Code * 100;
            code += _race.Code * 10;
            code += _gender.Code * 1;

            ID = GetID(code);

            Code = code * 10 + _id;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode()
        {
            int date = (int)(_code / 100000000);
            _birthday = new LFDate(date);
            _age = World.Info.NowDate.GetAge(_birthday);

            int loc = (int)((_code % 100000000) / 1000);
            _home = World.Data.SiteList.GetSite(loc);

            int tmp = (int)(_code % 1000);
            int r = tmp / 100;
            int g = (tmp % 100) / 10;
            _id = tmp % 10;
            _race = World.Setting.Races.GetType(r);
            _gender = World.Setting.Races.GetChilds(r).GetType(g);
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="id"></param>
        public bool SetID(int id)
        {
            bool res = false;
            long code = _birthday.Code * 10000000;
            code += _home.Code * 100;
            code += _race.Code * 10;
            code += _gender.Code * 1;

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
            LFRoleList tmp = World.Data.RoleList.GetRoleGroup(idx);

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

        #region Info Methods
        /// <summary>
        /// 计算修炼历程
        /// </summary>
        public void GetRanks()
        {
            _ranks.Clear();
            int talentLevel = (int)Math.Floor(_talent);
            // 分析等级总数
            int ranks = talentLevel + 4;

            // 计算到达每一等级的年龄
            for (int i = 1; i <= ranks; i++)
            {


                double y = World.Config.Xiulian.GetAge(_talent, i);

                int day = (int)(y * World.Info.Calendar.DaysInYear);

                LFDate date = _birthday.Add(day);

                LFVariable v = new LFVariable
                {
                    Date = date,
                    Code = i,
                    Value = World.Setting.Ranks.GetValue(i),
                    Age = (float)y
                };
                _ranks.Add(v);
            }
            double life = World.Config.Xiulian.RankList[(int)Math.Floor(_talent)].AgeLimit;


            LFDate deadday = _birthday.Add((int)(life * World.Info.Calendar.DaysInYear));
            LFVariable vD = new LFVariable
            {
                Date = deadday,
                Code = -1,
                Value = World.Setting.Ranks.GetValue(-1),
                Age = (float)life
            };
            _ranks.Add(vD);
        }

        /// <summary>
        /// 检查修炼等级
        /// </summary>
        public void CheckRanks()
        {
            _ranks.Sort();
            LFVariable vd = _ranks.GetVariable(-1);
            int idx = _ranks.IndexOf(vd);
            while (idx != _ranks.Count - 1)
            {
                _ranks.RemoveAt(idx + 1);
            }
            foreach (LFVariable v in _ranks)
            {
                v.Age = v.Date.GetAge(_birthday);
            }
        }

        /// <summary>
        /// 检查经历
        /// </summary>
        public void CheckExps()
        {
            _experiences.Sort();
            foreach (LFVariable v in _experiences)
            {
                v.Age = v.Date.GetAge(_birthday);
                v.Code = World.Data.SectList.GetCode(v.Value);
            }
            _experiences.GetValue(World.Info.NowDate);
            ExpToSect();
        }

        /// <summary>
        /// 将经历添加到势力中
        /// </summary>
        public void ExpToSect()
        {
            int n = _experiences.Count;

            if (n == 0)
                return;

            LFSect sect;
            LFVariable tmp;
            for (int i = 0; i < n - 1; i++)
            {
                sect = World.Data.SectList.GetSect(_experiences[i].Code);
                if (sect != null)
                {
                    tmp = new LFVariable
                    {
                        Code = _code,
                        Date = _experiences[i].Date,
                        EndDate = _experiences[i + 1].Date,
                        Value = _name,
                    };
                    sect.RoleList.AddObj(tmp);
                }
            }

            sect = World.Data.SectList.GetSect(_experiences[n - 1].Code);
            if (sect != null)
            {
                tmp = new LFVariable
                {
                    Code = _code,
                    Date = _experiences[n - 1].Date,
                    EndDate = _ranks[_ranks.Count - 1].Date,
                    Value = _name,

                };
                sect.RoleList.AddObj(tmp);
            }
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
            XmlElement root = xmlDoc.CreateElement("Role");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 存入内容
            root.SetAttribute("Code", _code.ToString());
            root.SetAttribute("Name", _name);
            root.SetAttribute("Talent", _talent.ToString());
            root.SetAttribute("Attribute", _attributes.Code.ToString());
            
            XmlElement eleRanks = xmlDoc.CreateElement("Ranks");
            foreach (LFVariable v in _ranks)
            {
                XmlElement ele = xmlDoc.CreateElement("Rank");
                ele.SetAttribute("Date", v.Date.Code.ToString());
                ele.SetAttribute("Code", v.Code.ToString());
                eleRanks.AppendChild(ele);
            }
            root.AppendChild(eleRanks);

            XmlElement eleExp = xmlDoc.CreateElement("Experiences");
            foreach (LFVariable v in _experiences)
            {
                XmlElement ele = xmlDoc.CreateElement("Experience");
                ele.SetAttribute("Date", v.Date.Code.ToString());
                ele.SetAttribute("Code", v.Code.ToString());
                eleExp.AppendChild(ele);
            }
            root.AppendChild(eleExp);

            /* 保存文件 */
            xmlDoc.Save(path + @"\Roles\" + _code.ToString() + ".xml");
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
            xmlDoc.Load(path + @"\Roles\" + _code.ToString() + ".xml");     // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                       // 读取根节点

            Talent = Convert.ToDouble(root.GetAttribute("Talent"));
            Attributes = new LFAttribute(Convert.ToInt32(root.GetAttribute("Attribute")));
            /* 2. 读取内容 */
            /*2.1 读取修炼历程 */
            XmlElement eleRanks = (XmlElement)root.GetElementsByTagName("Ranks")[0];
            foreach (XmlNode node in eleRanks.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFVariable v = new LFVariable
                {
                    Date = new LFDate(Convert.ToInt32(ele.GetAttribute("Date"))),
                    Code = Convert.ToInt64(ele.GetAttribute("Code"))
                };
                v.Value = World.Setting.Ranks.GetValue(v.Code);
                v.Age = v.Date.GetAge(_birthday);
                _ranks.Add(v);
            }

            /* 2.2 读取经历 */
            XmlElement eleExps = (XmlElement)root.GetElementsByTagName("Experiences")[0];
            foreach (XmlNode node in eleExps.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFVariable v = new LFVariable
                {
                    Date = new LFDate(Convert.ToInt32(ele.GetAttribute("Date"))),
                    Code = Convert.ToInt64(ele.GetAttribute("Code"))
                };
                if (v.Code == 0)
                {
                    v.Value = "无";
                }
                else
                {
                    LFSect tmp = World.Data.SectList.GetSect(v.Code);
                    if (tmp != null)
                    {
                        v.Value = tmp.Name;
                    }
                }
                v.Age = v.Date.GetAge(_birthday);
                _experiences.Add(v);
            }
        }
        #endregion

        #endregion
    }
}