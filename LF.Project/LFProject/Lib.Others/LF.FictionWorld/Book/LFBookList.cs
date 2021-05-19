/*──────────────────────────────────────────────────────────────
 * FileName     : LFBookList
 * Created      : 2020-09-28 18:24:16
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
    /// 秘籍列表
    /// </summary>
    public class LFBookList : ObservableCollection<LFBook>, ICloneable
    {
        #region Fields
        #endregion

        #region Properties



        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFBookList()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFBookList(LFBookList rhs)
        {
            foreach (LFBook obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFBookList Clone()
        {
            return new LFBookList(this);
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

        #region Sort Methods

        /// <summary>
        /// 按照ID排序
        /// </summary>
        public void Sort()
        {
            List<LFBook> list = new List<LFBook>();
            foreach (LFBook obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFBook O1, LFBook O2) { return O1.Index.CompareTo(O2.Index); });
            this.Clear();
            foreach (LFBook obj in list)
            {
                this.Add(obj);
            }
        }
        #endregion

        #region Operating Methods



        #endregion

        #region Search Methods
        /// <summary>
        /// 按名称搜索ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的ID，如未搜索到，返回-1</returns>
        public Int64 GetIndex(string name)
        {
            foreach (LFBook obj in this)
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
        public string GetName(Int64 index)
        {
            foreach (LFBook obj in this)
            {
                if (obj.Index == index)
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
        public LFBook GetLocation(string name)
        {
            foreach (LFBook obj in this)
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
        public LFBook GetLocation(Int64 index)
        {
            foreach (LFBook obj in this)
            {
                if (obj.Index == index)
                {
                    return obj;
                }
            }
            return null;
        }

        #endregion

        #region File

        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path, string file)
        {
            Sort(); // 保存之前先排序

            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Books");                        // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 开始写入 */
            if (this != null)
            {
                foreach (LFBook obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Book");
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
        /// 打开列表
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
                LFBook obj = new LFBook(idx, name)
                {
                    Brief = ele.GetAttribute("Brief")
                };

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
