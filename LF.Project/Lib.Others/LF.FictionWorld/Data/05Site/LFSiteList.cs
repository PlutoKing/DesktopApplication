/*──────────────────────────────────────────────────────────────
 * FileName     : LFSiteList.cs
 * Created      : 2021-05-28 10:16:37
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
    /// 地点列表，表示<see cref="LFSite"/>的列表。
    /// </summary>
    public class LFSiteList : ObservableCollection<LFSite>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFSiteList()
        {
        }

        /// <summary>
        /// 拷贝构造函数。
        /// </summary>
        /// <param name="rhs">源对象。</param>
        public LFSiteList(LFSiteList rhs)
        {
            foreach (LFSite obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数。
        /// </summary>
        /// <returns>与该地点列表相同的新实例。</returns>
        public LFSiteList Clone()
        {
            return new LFSiteList(this);
        }
        /// <summary>
        /// 拷贝函数。
        /// </summary>
        /// <returns>与该地点列表相同的新实例。</returns>
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
            List<LFSite> list = new List<LFSite>();
            foreach (LFSite obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFSite O1, LFSite O2) { return O1.Code.CompareTo(O2.Code); });
            this.Clear();
            foreach (LFSite obj in list)
            {
                this.Add(obj);
            }
        }

        /// <summary>
        /// 按Name搜索Code
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的Code；如未搜索到，返回0。</returns>
        public long GetCode(string name)
        {
            foreach (LFSite obj in this)
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
        /// <returns>搜索到的对象的名称；如未搜索到，返回null。</returns>
        public string GetName(long code)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Code == code)
                {
                    return obj.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// 按Name搜索Site
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>搜索到的对象；如未搜索到，返回null。</returns>
        public LFSite GetSite(string name)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// 按Code搜索Site
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns>搜索到的对象；如未搜索到，返回null。</returns>
        public LFSite GetSite(long code)
        {
            foreach (LFSite obj in this)
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
        public LFSiteList GetSiteGroup(long code)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite obj in this)
            {
                if (obj.Code / 100 == code)
                {
                    list.Add(obj);
                }
            }
            return list;
        }

        /// <summary>
        /// 减去列表
        /// </summary>
        /// <param name="items">被减列表</param>
        /// <returns>剩余列表</returns>
        public LFSiteList Minus(LFSiteList items)
        {
            LFSiteList res = new LFSiteList();

            foreach (LFSite item in this)
            {
                bool tmp = false;   // 是否在items中
                foreach (LFSite obj in items)
                {
                    if (item.Name == obj.Name)
                    {
                        tmp = true;
                        break;
                    }
                }
                if (!tmp)
                {
                    res.Add(item);
                }
            }

            return res;
        }

        /// <summary>
        /// 根据一级区划获取子集
        /// </summary>
        /// <param name="add1">一级区划</param>
        /// <returns></returns>
        public LFSiteList GetSubList(int add1)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area1.Code == add1)
                {
                    list.Add(site);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据二级区划获取子集
        /// </summary>
        /// <param name="add1"></param>
        /// <param name="add2"></param>
        /// <returns></returns>
        public LFSiteList GetSubList(int add1, int add2)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area2.Code == add2 && site.Area1.Code == add1)
                {
                    list.Add(site);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取同区域的地点列表
        /// </summary>
        /// <param name="add1">一级区域</param>
        /// <param name="add2">二级区域</param>
        /// <param name="add3">三级区域</param>
        /// <returns></returns>
        public LFSiteList GetSubList(int add1, int add2, int add3)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area2.Code == add2 && site.Area1.Code == add1 && site.Area3.Code == add3)
                {
                    list.Add(site);
                }
            }
            return list;
        }

        /// <summary>
        /// 使用Code和Name检测是否包含
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public bool IsContains(LFSite site)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Code == site.Code || obj.Name == site.Name)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Operating Methods

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="obj">待添加对象</param>
        public void AddSite(LFSite obj)
        {
            Add(obj);
            Sort();
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        /// <param name="idx">待添加对象的索引</param>
        /// <param name="obj"></param>
        public void EditSite(int idx, LFSite obj)
        {
            // 更新Site数据
            this[idx] = obj;
            Sort();
            // 更新Site相关信息

        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj">待删除对象</param>
        public void DeleteSite(LFSite obj)
        {
            if (Contains(obj))
            {
                Remove(obj);

                // 更新Site相关信息
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
            foreach (LFSite item in this)
            {
                if (tmp != item.Code - item.ID)
                {
                    tmp = item.Code - item.ID;
                    i = 1;
                }
                item.SetID(i);
                i++;
            }
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="obj"></param>
        public bool UpSite(LFSite obj)
        {
            int index = IndexOf(obj);

            // 最上方，无法上移
            if (obj.ID == 1)
                return false;

            // 将其上方的site先取出
            LFSite tmp = this[index - 1].Clone();
            // 更改ID
            tmp.SetID(tmp.ID + 1);
            obj.SetID(obj.ID - 1);
            // 交换
            this[index - 1] = obj.Clone();
            this[index] = tmp.Clone();

            // 对于其所对应的角色和势力中的地点

            return true;
        }

        /// <summary>
        /// 下移
        /// <code>在其所在区域范围内下移，改变ID</code>
        /// </summary>
        /// <param name="obj"></param>
        public bool DownSite(LFSite obj)
        {
            int index = IndexOf(obj);
            // 最下方，无法下移
            LFSiteList sites = GetSiteGroup(obj.AreaIndex);
            if (obj.ID >= sites.Count)
                return false;

            // 将其下方的site取出
            LFSite tmp = this[index + 1].Clone();
            // 更改ID
            tmp.SetID(tmp.ID - 1);
            obj.SetID(obj.ID + 1);
            // 交换
            this[index + 1] = obj.Clone();
            this[index] = tmp.Clone();
            return true;
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
            XmlElement root = xmlDoc.CreateElement("Sites");                        // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 写入数据
            if (this != null)
            {
                foreach (LFSite obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Site");
                    ele.SetAttribute("Code", obj.Code.ToString());
                    ele.SetAttribute("Name", obj.Name);
                    ele.SetAttribute("Brief", obj.Brief);

                    /* 子地点 */
                    foreach (LFSubSite sub in obj.SubSites)
                    {
                        XmlElement eleSub = xmlDoc.CreateElement("SubLocation");
                        eleSub.SetAttribute("ID", sub.ID.ToString());
                        eleSub.SetAttribute("Name", sub.Name);
                        eleSub.SetAttribute("Brief", sub.Brief);

                        ele.AppendChild(eleSub);
                    }

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
                LFSite obj = new LFSite(idx, name)
                {
                    Brief = ele.GetAttribute("Brief"),
                };
                // 读取子地点信息
                foreach (XmlNode nodeSubLoc in ele.ChildNodes)
                {
                    XmlElement eleSubLoc = (XmlElement)nodeSubLoc;
                    LFSubSite subloc = new LFSubSite
                    {
                        ID = Convert.ToInt32(eleSubLoc.GetAttribute("ID")),
                        Name = eleSubLoc.GetAttribute("Name"),
                        Brief = eleSubLoc.GetAttribute("Brief")
                    };
                    obj.SubSites.Add(subloc);
                }
                Add(obj);
            }
        }

        #endregion

        #endregion
    }
}