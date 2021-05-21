/*──────────────────────────────────────────────────────────────
 * FileName     : LFRoleList
 * Created      : 2020-09-28 11:28:13
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using LF.Algorithm;

namespace LF.FictionWorld
{
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
        /// <param name="rhs"></param>
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
        /// <returns></returns>
        public LFRoleList Clone()
        {
            return new LFRoleList(this);
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

        #region Sort and Add

        /// <summary>
        /// 按照ID排序
        /// </summary>
        public void Sort()
        {
            List<LFRole> list = new List<LFRole>();
            foreach (LFRole obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFRole O1, LFRole O2) { return O1.Index.CompareTo(O2.Index); });
            Clear();
            foreach (LFRole obj in list)
            {
                Add(obj);
            }
        }

        #endregion

        #region SubList

        /// <summary>
        /// 按照区域筛选
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public LFRoleList GetSub(LFSite site)
        {
            LFRoleList list = new LFRoleList();

            foreach(LFRole r in this)
            {
                if(r.Home == site)
                {
                    list.Add(r);
                }
            }

            return list;
        }

        public LFRoleList GetSub(LFRoleList sub)
        {
            LFRoleList list = this.Clone();

            foreach(LFRole r in sub)
            {
                list.Remove(r);
            }

            return list;
        }


        /// <summary>
        /// 筛选存活
        /// </summary>
        /// <returns></returns>
        public LFRoleList GetNowRole()
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

        #region Search
        /// <summary>
        /// 按名称搜索ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的ID，如未搜索到，返回-1</returns>
        public Int64 GetIndex(string name)
        {
            foreach (LFRole obj in this)
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
        /// <param name="idx"></param>
        /// <returns></returns>
        public string GetName(Int64 idx)
        {
            foreach (LFRole obj in this)
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
        /// 按ID搜索位置
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public LFRole GetRole(Int64 idx)
        {
            foreach (LFRole obj in this)
            {
                if (obj.Index == idx)
                {
                    return obj;
                }
            }
            return null;
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
            XmlElement root = xmlDoc.CreateElement("Roles");                    // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 开始写入 */
            if (this != null)
            {
                foreach (LFRole obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Role");
                    ele.SetAttribute("Index", obj.Index.ToString());
                    ele.SetAttribute("Name", obj.Name);
                    ele.SetAttribute("Talent", obj.Talent.ToString());
                    ele.SetAttribute("Attribute", obj.Attributes.Code.ToString());
                    ele.SetAttribute("Brief", obj.Brief);

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
        public LFRoleList Open(string path, string file)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                     // 定义文件
            xmlDoc.Load(path + @"\" + file + ".xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;                   // 读取根节点
            XmlNodeList nodes = root.ChildNodes;                        // 读取子节点

            LFRoleList errorList = new LFRoleList();
            /* 开始读取 */
            foreach (XmlNode node in nodes)
            {
                XmlElement ele = (XmlElement)node;

                long idx = Convert.ToInt64(ele.GetAttribute("Index"));
                string name = ele.GetAttribute("Name");
                LFRole obj = new LFRole(idx, name)
                {
                    Talent = Convert.ToSingle(ele.GetAttribute("Talent")),
                    Attributes = new LFAttribute(Convert.ToInt32(ele.GetAttribute("Attribute"))),
                    Brief = ele.GetAttribute("Brief")
                };

                Add(obj);
            }

            return errorList;
        }

        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
