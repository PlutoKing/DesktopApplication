/*──────────────────────────────────────────────────────────────
 * FileName     : LFSectList
 * Created      : 2020-09-28 16:32:01
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Collections.ObjectModel;

namespace LF.FictionWorld
{
    public class LFSectList : ObservableCollection<LFSect>, ICloneable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public LFSectList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFSectList(LFSectList rhs)
        {
            foreach (LFSect obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSectList Clone()
        {
            return new LFSectList(this);
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
        /// 按照ID排序
        /// </summary>
        public void Sort()
        {
            List<LFSect
                > list = new List<LFSect>();
            foreach (LFSect obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFSect O1, LFSect O2) { return O1.Index.CompareTo(O2.Index); });
            this.Clear();
            foreach (LFSect obj in list)
            {
                this.Add(obj);
            }
        }

        #region Search
        /// <summary>
        /// 按名称搜索ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的ID，如未搜索到，返回-1</returns>
        public Int64 GetIndex(string name)
        {
            foreach (LFSect obj in this)
            {
                if (obj.Name == name)
                {
                    return obj.Index;
                }
            }
            return 0;
        }

        /// <summary>
        /// 按ID搜索名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(long idx)
        {
            foreach (LFSect obj in this)
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
        public LFSect GetSect(string name)
        {
            foreach (LFSect obj in this)
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
        public LFSect GetSect(long idx)
        {
            foreach (LFSect obj in this)
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
            XmlElement root = xmlDoc.CreateElement("Sects");                    // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 开始写入 */
            if (this != null)
            {
                foreach (LFSect obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Sect");
                    ele.SetAttribute("Index", obj.Index.ToString());
                    ele.SetAttribute("Name", obj.Name);
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

                long idx = Convert.ToInt64(ele.GetAttribute("Index"));
                string name = ele.GetAttribute("Name");
                LFSect obj = new LFSect(idx, name);
                obj.Brief = ele.GetAttribute("Brief");

                this.Add(obj);
            }
        }

        #endregion

        #endregion

        #region 筛选
        /// <summary>
        /// 按照地点选择
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public LFSectList GetSub(LFSite site)
        {
            LFSectList list = new LFSectList();

            foreach (LFSect sect in this)
            {
                if (sect.Location == site)
                {
                    list.Add(sect.Clone());
                }
            }

            return list;
        } 
        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
