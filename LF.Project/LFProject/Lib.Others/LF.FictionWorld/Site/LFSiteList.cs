/*──────────────────────────────────────────────────────────────
 * FileName     : LFSiteList
 * Created      : 2020-09-28 11:19:17
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 位置列表
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
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFSiteList(LFSiteList rhs)
        {
            foreach (LFSite obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSiteList Clone()
        {
            return new LFSiteList(this);
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

        #region Sort

        /// <summary>
        /// 按照Index排序
        /// </summary>
        public void Sort()
        {
            List<LFSite> list = new List<LFSite>();
            foreach (LFSite obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFSite O1, LFSite O2) { return O1.Index.CompareTo(O2.Index); });
            this.Clear();
            foreach (LFSite obj in list)
            {
                this.Add(obj);
            }
        }
        #endregion

        #region Sub

        public LFSiteList GetAreaSub(int area)
        {
            LFSiteList list = new LFSiteList();
            foreach(LFSite site in this)
            {
                if (site.Index/100 == area)
                {
                    list.Add(site);
                }
            }
            return list;
        }

        public LFSiteList GetSub(int add1)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area1.Index == add1)
                {
                    list.Add(site);
                }
            }
            return list;
        }
        public LFSiteList GetSub(int add1,int add2)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area2.Index == add2 && site.Area1.Index == add1)
                {
                    list.Add(site);
                }
            }
            return list;
        }

        public LFSiteList GetSub(int add1, int add2,int add3)
        {
            LFSiteList list = new LFSiteList();
            foreach (LFSite site in this)
            {
                if (site.Area2.Index == add2 && site.Area1.Index == add1 && site.Area3.Index == add3)
                {
                    list.Add(site);
                }
            }
            return list;
        }


        #endregion

        #region Search
        /// <summary>
        /// 按名称搜索ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的ID，如未搜索到，返回-1</returns>
        public int GetIndex(string name)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Name == name)
                {
                    return obj.Index;
                }
            }
            return -1;
        }

        /// <summary>
        /// 按ID搜索名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(int idx)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Index == idx)
                {
                    return obj.Name;
                }
            }
            return null;
        }


        /// <summary>
        /// 按名称搜索位置
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public LFSite GetLocation(string name)
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
        /// 按ID搜索位置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LFSite GetLocation(int idx)
        {
            foreach (LFSite obj in this)
            {
                if (obj.Index == idx)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// 按区域搜索子集
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public LFSiteList GetLocations(int add)
        {
            LFSiteList locs = new LFSiteList();
            foreach (LFSite loc in this)
            {
                if (loc.AreaIndex == add)
                {
                    locs.Add(loc);
                }
            }
            return locs;
        }
        #endregion

        #region File

        /// <summary>
        /// 保存位置列表
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path, string file)
        {
            Sort(); // 保存之前先排序

            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Locations");                    // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 开始写入 */
            if (this != null)
            {
                foreach (LFSite obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Location");
                    ele.SetAttribute("Index", obj.Index.ToString());
                    ele.SetAttribute("Name", obj.Name);
                    ele.SetAttribute("Brief", obj.Brief);

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

            /* 保存文件 */
            xmlDoc.Save(path + @"\" + file + ".xml");
        }



        /// <summary>
        /// 打开位置列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public void Open(string path, string file)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                     // 定义文件
            xmlDoc.Load(path + @"\" + file + ".xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                   // 读取根节点
            XmlNodeList nodes = root.ChildNodes;                        // 读取子节点

            /* 开始读取 */
            foreach (XmlNode node in nodes)
            {
                XmlElement ele = (XmlElement)node;

                int idx = Convert.ToInt32(ele.GetAttribute("Index"));
                string name = ele.GetAttribute("Name");
                LFSite obj = new LFSite(idx, name)
                {
                    Brief = ele.GetAttribute("Brief")
                };

                //XmlElement eleBrief = (XmlElement)ele.GetElementsByTagName("Brief")[0];
                //obj.Brief = eleBrief.InnerText;

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

                this.Add(obj);
            }
        }

        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
