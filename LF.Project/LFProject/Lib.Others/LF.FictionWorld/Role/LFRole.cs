/*──────────────────────────────────────────────────────────────
 * FileName     : LFRole
 * Created      : 2020-09-28 11:02:26
 * Author       : Xu Zhe
 * Description  : 角色
 * ──────────────────────────────────────────────────────────────*/

using System;
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
        readonly double[] p1 = new double[] { 3.75000000000000, -14.7222222222222, 50.4166666666663, 19.1269841269850, 100.238095238094 };
        readonly double[] p2 = new double[] { -2.46778121540625e-05, 0.000431909586119701, -0.00312240270874285, 0.0125046074065508, -0.0349999386505692, 0.119450631281870 };

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _index;        // 索引
        private int _id;            // ID，用于区别双胞胎
        private string _name;       // 姓名
        private string _brief;      // 简介

        private LFDate _birthday = new LFDate();    // 生日
        private LFSite _home = new LFSite();        // 籍贯
        private LFType _race = new LFType();        // 种族
        private LFType _gender = new LFType();      // 性别
        private float _age;         // 年龄


        private float _talent;      // 天赋值

        private LFAttribute _attributes = new LFAttribute();    // 属性
        private LFVariableList _ranks = new LFVariableList();           // 修炼等级
        private LFVariableList _experiences = new LFVariableList();     // 经历
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
        /// 姓名
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }

        /// <summary>
        /// 生日
        /// </summary>
        public LFDate Birthday { get => _birthday; set => _birthday = value; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public LFSite Home { get => _home; set => _home = value; }

        /// <summary>
        /// 年龄
        /// </summary>
        public float Age { get => _age; set => _age = value; }

        /// <summary>
        /// 种族
        /// </summary>
        public LFType Race { get => _race; set => _race = value; }

        /// <summary>
        /// 性别
        /// </summary>
        public LFType Gender { get => _gender; set => _gender = value; }

        /// <summary>
        /// 天赋
        /// </summary>
        public float Talent { get => _talent; set => _talent = value; }

        /// <summary>
        /// 属性
        /// </summary>
        public LFAttribute Attributes { get => _attributes; set => _attributes = value; }
        public LFVariableList Ranks { get => _ranks; set => _ranks = value; }
        public string Attribute
        {
            get
            {
                return _attributes.ToString();
            }
        }

        public LFVariableList Experiences { get => _experiences; set => _experiences = value; }
        #endregion

        #region Constructors
        public LFRole()
        {
        }
        public LFRole(long index, string name)
        {
            _index = index;       // 生日9+地点5+种族1+性别1+编号1=17位
            _name = name;
            Decode();
        }


        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFRole(LFRole rhs)
        {

            _index = rhs._index;
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
            return this.Clone();
        }
        #endregion

        #region Methods

        #region Constructing Methods
        
        /// <summary>
        /// 解码
        /// </summary>
        public void Decode()
        {
            int date = (int)(_index / 100000000);
            _birthday = new LFDate(date);
            _age = World.Info.NowDate.GetAge(_birthday);


            int loc = (int)((_index % 100000000) / 1000);
            _home = World.Data.SiteList.GetLocation(loc);


            int tmp = (int)(_index % 1000);
            int r = tmp / 100;
            int g = (tmp % 100) / 10;
            _id = tmp % 10;
            _race = World.Setting.Races.GetType(r);
            _gender = World.Setting.Races.GetChilds(r).GetType(g);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            long index = _birthday.Code * 10000000;
            index += _home.Index * 100;
            index += _race.Index * 10;
            index += _gender.Index * 1;

            if (_id == 0)
            {
                _id = 1;
                foreach (LFRole role in World.Data.RoleList)
                {
                    if (role.Index / 10 == index)
                    {
                        _id++;
                    }
                }
            }

            _index = index * 10 + _id;
        }
        #endregion

        #region Core Methods

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
                double t = 0;
                for (int j = 0; j < p2.Length; j++)
                {
                    t += p2[j] * Math.Pow(_talent, p2.Length - j - 1);
                }

                double y = 10 * Math.Exp(t * (Math.Pow(i, 2) - 1));

                int day = (int)(y * 360);

                LFDate date = _birthday.Add(day);

                LFVariable v = new LFVariable
                {
                    Date = date,
                    Index = i,
                    Value = World.Setting.Ranks.GetValue(i),
                    Age = (float)y
                };
                _ranks.Add(v);
            }


            double life = 0;
            for (int i = 0; i < p1.Length; i++)
            {
                life += p1[i] * Math.Pow(talentLevel, p1.Length - i - 1);
            }

            LFDate deadday = _birthday.Add((int)(life * 360));
            LFVariable vD = new LFVariable
            {
                Date = deadday,
                Index = -1,
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
                v.Index = World.Data.SectList.GetIndex(v.Value);
            }
            _experiences.GetValue(World.Info.NowDate);
        }

        #endregion

        #region Operating Methods

        /// <summary>
        /// 被杀：添加死亡
        /// </summary>
        /// <param name="date">被杀时间</param>
        public void BeKilled(LFDate date)
        {
            LFVariable v = new LFVariable
            {
                Date = date,
                Index = -1
            };
            _ranks.Add(v);
            CheckRanks();
            
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
            XmlElement root = xmlDoc.CreateElement("Role");                         // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            root.SetAttribute("Index", _index.ToString());
            root.SetAttribute("Name", _name);

            XmlElement eleRanks = xmlDoc.CreateElement("Ranks");
            foreach (LFVariable v in _ranks)
            {
                XmlElement ele = xmlDoc.CreateElement("Rank");
                ele.SetAttribute("Date", v.Date.Code.ToString());
                ele.SetAttribute("Index", v.Index.ToString());
                eleRanks.AppendChild(ele);
            }
            root.AppendChild(eleRanks);

            XmlElement eleExp = xmlDoc.CreateElement("Experiences");
            foreach (LFVariable v in _experiences)
            {
                XmlElement ele = xmlDoc.CreateElement("Experience");
                ele.SetAttribute("Date", v.Date.Code.ToString());
                ele.SetAttribute("Index", v.Index.ToString());
                eleExp.AppendChild(ele);
            }
            root.AppendChild(eleExp);

            /* 保存文件 */
            xmlDoc.Save(path + @"\Roles\" + _index.ToString() + ".xml");
        }



        /// <summary>
        /// 打开位置列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                         // 定义文件
            xmlDoc.Load(path + @"\Roles\" + _index.ToString() + ".xml");    // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                       // 读取根节点

            /* 读取修炼历程 */
            XmlElement eleRanks = (XmlElement)root.GetElementsByTagName("Ranks")[0];
            foreach (XmlNode node in eleRanks.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFVariable v = new LFVariable
                {
                    Date = new LFDate(Convert.ToInt32(ele.GetAttribute("Date"))),
                    Index = Convert.ToInt64(ele.GetAttribute("Index"))
                };
                v.Value = World.Setting.Ranks.GetValue(v.Index);
                v.Age = v.Date.GetAge(_birthday);
                _ranks.Add(v);
            }

            /* 读取经历 */
            XmlElement eleExps = (XmlElement)root.GetElementsByTagName("Experiences")[0];
            foreach (XmlNode node in eleExps.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFVariable v = new LFVariable
                {
                    Date = new LFDate(Convert.ToInt32(ele.GetAttribute("Date"))),
                    Index = Convert.ToInt64(ele.GetAttribute("Index"))
                };
                if (v.Index == 0)
                {
                    v.Value = "无";
                }
                else
                {
                    v.Value = World.Data.SectList.GetSect(v.Index).Name;
                }
                v.Age = v.Date.GetAge(_birthday);
                _experiences.Add(v);
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
