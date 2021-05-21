/*──────────────────────────────────────────────────────────────
 * FileName     : LFSect
 * Created      : 2020-09-28 16:30:19
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;
using System.Collections.ObjectModel;

namespace LF.FictionWorld
{
    public class LFSect : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _index;            // 索引号
        private int _id;                // ID
        private string _name = "NaN";   // 名称
        private string _brief = "NaN";  // 简介

        private LFDate _createdDate = new LFDate();     // 创建日期
        private string _createdRole = "未知";            // 创建者
        private LFSite _location = new LFSite();        // 地点
        private float _age;                             // 传承年数

        private LFDepartmentList _struct = new LFDepartmentList();  // 组织结构
        private LFVariableList _roles = new LFVariableList();       // 角色
        #endregion

        #region Properties
        /// <summary>
        /// 索引号
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
        /// 创建日期
        /// </summary>
        public LFDate CreatedDate { get => _createdDate; set => _createdDate = value; }
        /// <summary>
        /// 地点
        /// </summary>
        public LFSite Location { get => _location; set => _location = value; }
        /// <summary>
        /// 传承时间
        /// </summary>
        public float Age { get => _age; set => _age = value; }
        /// <summary>
        /// 组织结构
        /// </summary>
        public LFDepartmentList Struct { get => _struct; set => _struct = value; }
        public LFVariableList Roles { get => _roles; set => _roles = value; }
        public string CreatedRole { get => _createdRole; set => _createdRole = value; }

        /// <summary>
        /// 角色
        /// </summary>
        #endregion

        #region Constructors
        public LFSect()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public LFSect(long index, string name)
        {
            _index = index;
            _name = name;

            Decode();
        }



        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFSect(LFSect rhs)
        {
            this._index = rhs._index;
            this._id = rhs._id;
            this._name = rhs._name;
            this._brief = rhs._brief;
            this._createdDate = rhs._createdDate.Clone();
            this._location = rhs._location;
            this._age = rhs._age;

            this._struct = rhs._struct.Clone();
        }

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
            return this.Clone();
        }

        #endregion

        #region Methods

        #region Common
        public void Decode()
        {
            _createdDate = new LFDate((int)(_index / 10000000));
            int a = (int)(_index % 10000000);
            _age = World.Info.NowDate.GetAge(_createdDate);

            int loc = (int)(a / 100);
            _location = World.Data.SiteList.GetLocation(loc);

            _id = (int)(a % 100);
        }

        /// <summary>
        /// 生成索引
        /// </summary>
        public void Encode()
        {
            long index = _createdDate.Code * 100000;
            index += _location.Index;

            if (_id == 0)
            {
                _id = 1;
                foreach (LFSect sect in World.Data.SectList)
                {
                    if (sect.Index / 100 == index)
                    {
                        _id++;
                    }
                }

            }

            _index = index * 100 + _id;
        }
        
        /// <summary>
        /// 地点检测
        /// </summary>
        public void CheckSite()
        {
            if(_location != null)
            {
                // 将势力添加到地点中
                foreach (LFSect sect in _location.Sects)
                {
                    if (sect.Name == this.Name)
                        return;
                }
                _location.Sects.Add(this);
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
            XmlElement root = xmlDoc.CreateElement("Sect");                    // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            root.SetAttribute("Index", _index.ToString());
            root.SetAttribute("Name", _name);

            XmlElement eleStruct = xmlDoc.CreateElement("Struct");
            root.AppendChild(eleStruct);

            foreach(LFDepartment d in _struct)
            {
                XmlElement ele = xmlDoc.CreateElement("Department");

                WriteDepartment(xmlDoc,ele,d);
                eleStruct.AppendChild(ele);
            }

            xmlDoc.Save(path + @"\Sects\" + _index.ToString() + ".xml");                   // 加载文件
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
            xmlDoc.Load(path + @"\Sects\" + _index.ToString() + ".xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                   // 读取根节点


            XmlElement eleStuct = (XmlElement)root.GetElementsByTagName("Struct")[0];
            /* 开始读取 */
            foreach (XmlNode node in eleStuct.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFDepartment d = ReadDepartment(ele);
                this._struct.Add(d);
            }

            CheckSite();
        }

        #region Sect Struct

        public void WriteDepartment(XmlDocument xmlDoc, XmlElement root ,LFDepartment department)
        {
            root.SetAttribute("ID", department.ID.ToString());
            root.SetAttribute("Title", department.Title);
            root.SetAttribute("Level", department.Level.ToString());

            XmlElement eleLeader = xmlDoc.CreateElement("Leader");
            WriteLeader(xmlDoc, eleLeader, department.Leader);
            root.AppendChild(eleLeader);

            XmlElement eleMembers = xmlDoc.CreateElement("Members");
            WriteMembers(xmlDoc, eleMembers, department.Members);
            root.AppendChild(eleMembers);

            XmlElement eleDepartments = xmlDoc.CreateElement("Departments");
            WriteDepartments(xmlDoc, eleDepartments, department.Departments);
            root.AppendChild(eleDepartments);

        }

        public void WriteLeader(XmlDocument xmlDoc, XmlElement root, LFMember leader)
        {
            root.SetAttribute("ID", leader.ID.ToString());
            root.SetAttribute("Title", leader.Title);
            root.SetAttribute("Level", leader.Level.ToString());

            foreach(LFMember a in leader.Assistants)
            {
                XmlElement ele = xmlDoc.CreateElement("Assistant");
                ele.SetAttribute("ID", a.ID.ToString());
                ele.SetAttribute("Title", a.Title);
                ele.SetAttribute("Level", a.Level.ToString());
                root.AppendChild(ele);
            }
        }

        public void WriteMembers(XmlDocument xmlDoc, XmlElement root, LFMemberList members)
        {
            foreach(LFMember member in members)
            {
                XmlElement ele = xmlDoc.CreateElement("Member");
                WriteMember(ele, member);
                root.AppendChild(ele);
            }
        }

        public void WriteMember(XmlElement root,LFMember member)
        {
            root.SetAttribute("ID", member.ID.ToString());
            root.SetAttribute("Title", member.Title);
            root.SetAttribute("Level", member.Level.ToString());
            root.SetAttribute("Count", member.Count.ToString());
        }

        public void WriteDepartments(XmlDocument xmlDoc, XmlElement root, LFDepartmentList departments)
        {
            foreach(LFDepartment d in departments)
            {
                XmlElement ele = xmlDoc.CreateElement("Department");
                WriteDepartment(xmlDoc, ele, d);
                root.AppendChild(ele);
            }
        }
        /// <summary>
        /// 读取领导者
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public LFMember ReadLeader(XmlElement root)
        {
            LFMember leader = new LFMember
            {
                ID = Convert.ToInt32(root.GetAttribute("ID")),
                Title = root.GetAttribute("Title"),
                Level = Convert.ToInt32(root.GetAttribute("Level"))
            };

            XmlNodeList aNodes = root.ChildNodes;
            foreach (XmlNode aNode in aNodes)
            {
                XmlElement aEle = (XmlElement)aNode;
                LFMember a = new LFMember
                {
                    ID = Convert.ToInt32(aEle.GetAttribute("ID")),
                    Title = aEle.GetAttribute("Title"),
                    Level = Convert.ToInt32(aEle.GetAttribute("Level"))
                };
                leader.Assistants.Add(a);
            }
            return leader;
        }

        /// <summary>
        /// 读取成员列表
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public LFMemberList ReadMembers(XmlElement root)
        {
            LFMemberList members = new LFMemberList();

            foreach (XmlNode node in root.ChildNodes)
            {
                members.Add(ReadMember((XmlElement)node));
            }

            return members;
        }

        /// <summary>
        /// 读取成员
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public LFMember ReadMember(XmlElement root)
        {
            LFMember member = new LFMember
            {
                ID = Convert.ToInt32(root.GetAttribute("ID")),
                Title = root.GetAttribute("Title"),
                Level = Convert.ToInt32(root.GetAttribute("Level")),
                Count = Convert.ToInt32(root.GetAttribute("Count"))
            };

            return member;
        }

        /// <summary>
        /// 读取部门列表
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public LFDepartmentList ReadDepartments(XmlElement root)
        {
            LFDepartmentList departments = new LFDepartmentList();

            foreach (XmlNode node in root.ChildNodes)
            {
                departments.Add(ReadDepartment((XmlElement)node));
            }

            return departments;
        }

        /// <summary>
        /// 读取部门
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public LFDepartment ReadDepartment(XmlElement root)
        {
            LFDepartment department = new LFDepartment
            {
                ID = Convert.ToInt32(root.GetAttribute("ID")),
                Title = root.GetAttribute("Title"),
                Level = Convert.ToInt32(root.GetAttribute("Level"))
            };

            XmlElement leaderEle = (XmlElement)root.GetElementsByTagName("Leader")[0];
            department.Leader = ReadLeader(leaderEle);
            XmlElement membersEle = (XmlElement)root.GetElementsByTagName("Members")[0];
            department.Members = ReadMembers(membersEle);

            XmlElement departmentsEle = (XmlElement)root.GetElementsByTagName("Departments")[0];
            if (departmentsEle != null)
            {
                if (departmentsEle.ChildNodes != null)
                {
                    department.Departments = ReadDepartments(departmentsEle);
                }
            }

            return department;
        } 
        #endregion
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
