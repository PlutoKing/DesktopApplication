/*──────────────────────────────────────────────────────────────
 * FileName     : LFRoleList.cs
 * Created      : 2021-05-29 11:35:26
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
    /// 角色列表，表示<see cref="LFRole"/>的列表
    /// </summary>
    public class LFRoleList : ObservableCollection<LFRole>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFRoleList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFRoleList(LFRoleList rhs)
        {
            foreach (LFRole obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该角色列表相同的新实例</returns>
        public LFRoleList Clone()
        {
            return new LFRoleList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该角色列表相同的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 按照编码Code排序
        /// </summary>
        public void Sort()
        {
            List<LFRole> list = new List<LFRole>();
            foreach (LFRole obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFRole O1, LFRole O2) { return O1.Code.CompareTo(O2.Code); });
            this.Clear();
            foreach (LFRole obj in list)
            {
                this.Add(obj);
            }
        }

        /// <summary>
        /// 按Name搜索Code
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的Code；如未搜索到，返回0</returns>
        public long GetCode(string name)
        {
            foreach (LFRole obj in this)
            {
                if (obj.Name == name)
                {
                    return obj.Code;
                }
            }
            return 0;
        }

        /// <summary>
        /// 按Code搜索Name
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns>搜索到的对象的名称；如未搜索到，返回null</returns>
        public string GetName(long code)
        {
            foreach (LFRole obj in this)
            {
                if (obj.Code == code)
                {
                    return obj.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// 按Name搜索Role
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>搜索到的对象；如未搜索到，返回null</returns>
        public LFRole GetRole(string name)
        {
            foreach (LFRole obj in this)
            {
                if (obj.Name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// 按Code搜索Role
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns>搜索到的对象；如未搜索到，返回null</returns>
        public LFRole GetRole(long code)
        {
            foreach (LFRole obj in this)
            {
                if (obj.Code == code)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取同类项
        /// </summary>
        /// <param name="code">无ID的Code</param>
        /// <returns>同类的对象列表</returns>
        internal LFRoleList GetRoleGroup(long code)
        {
            LFRoleList list = new LFRoleList();
            foreach (LFRole obj in this)
            {
                if (obj.Code / 1000 == code)
                {
                    list.Add(obj);
                }
            }
            return list;
        }

        /// <summary>
        /// 减去列表
        /// </summary>
        /// <param name="roles">被减列表</param>
        /// <returns>剩余列表</returns>
        public LFRoleList Minus(LFRoleList roles)
        {
            LFRoleList res = new LFRoleList();

            foreach (LFRole role in this)
            {
                bool tmp = false;   // 是否在roles中
                foreach (LFRole obj in roles)
                {
                    if (role.Name == obj.Name)
                    {
                        tmp = true;
                        break;
                    }
                }
                if (!tmp)
                {
                    res.Add(role);
                }
            }

            return res;
        }

        /// <summary>
        /// 筛选存活
        /// </summary>
        /// <returns></returns>
        public LFRoleList GetNowRoleList()
        {
            LFRoleList list = new LFRoleList();

            foreach (LFRole r in this)
            {
                if (r.Ranks.Value != "死亡")
                {
                    list.Add(r);
                }
            }

            return list;
        }
        #endregion

        #region Operating Methods

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="obj">待添加对象</param>
        public void AddRole(LFRole obj)
        {
            Add(obj);
            Sort();
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        /// <param name="idx">待添加对象的索引</param>
        /// <param name="obj"></param>
        public void EditRole(int idx, LFRole obj)
        {
            long oldCode = this[idx].Code;
            LFRole oldRole = this[idx];
            // 更新Role数据
            this[idx] = obj;

            // 更新Role相关信息
            

            // 排序
            Sort();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj">待删除对象</param>
        public void DeleteRole(LFRole obj)
        {
            if (Contains(obj))
            {
                Remove(obj);

                // 更新Role相关信息
                
            }
        }

        /// <summary>
        /// 刷新编号
        /// </summary>
        public void RefreshID()
        {
            Sort();
            long tmp = 0;
            int i = 1;
            foreach (LFRole role in this)
            {
                if (tmp != role.Code - role.ID)
                {
                    tmp = role.Code - role.ID;
                    i = 1;
                }
                role.SetID(i);
                i++;
            }
        }
        #endregion

        #region Data Methods

        /// <summary>
        /// 减去指针所指的对象
        /// </summary>
        /// <param name="pointers"></param>
        /// <returns></returns>
        public LFRoleList Minus(LFPointerList pointers)
        {
            LFRoleList list = new LFRoleList();
            foreach (LFRole role in this)
            {
                bool tmp = false;   // 是否在pointers中
                foreach (LFPointer p in pointers)
                {
                    if (role.Code == p.Code || role.Name == p.Name)
                    {
                        tmp = true; ;
                        break;
                    }
                }
                if (!tmp)
                {
                    list.Add(role);
                }
            }
            return list;
        }

        #endregion

        #region File Methods

        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="file">文件</param>
        public void Save(string path, string file)
        {
            Sort(); // 保存之前先排序

            // Xml基础操作
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Roles");                        // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 写入数据
            if (this != null)
            {
                foreach (LFRole obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Role");
                    ele.SetAttribute("Code", obj.Code.ToString());
                    ele.SetAttribute("Name", obj.Name);
                    ele.SetAttribute("Brief", obj.Brief);
                    root.AppendChild(ele);
                }
            }

            // 保存文件
            xmlDoc.Save(path + @"\" + file + ".xml");
        }


        /// <summary>
        /// 打开列表
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="file">文件</param>
        public void Open(string path, string file)
        {
            // Xml基础操作
            XmlDocument xmlDoc = new XmlDocument();     // 定义文件
            xmlDoc.Load(path + @"\" + file + ".xml");   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;   // 读取根节点
            XmlNodeList nodes = root.ChildNodes;        // 读取子节点

            // 读取数据
            foreach (XmlNode node in nodes)
            {
                XmlElement ele = (XmlElement)node;

                long idx = Convert.ToInt64(ele.GetAttribute("Code"));
                string name = ele.GetAttribute("Name");
                LFRole obj = new LFRole(idx, name)
                {
                    Brief = ele.GetAttribute("Brief"),
                };
                Add(obj);
            }
        }

        #endregion

        #endregion
    }
}