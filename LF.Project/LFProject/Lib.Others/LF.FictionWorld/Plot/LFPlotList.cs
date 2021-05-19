/*──────────────────────────────────────────────────────────────
 * FileName     : LFPlotList
 * Created      : 2020/9/28 23:16:36
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFPlotList : ObservableCollection<LFPlot>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFPlotList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFPlotList(LFPlotList rhs)
        {
            foreach (LFPlot obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFPlotList Clone()
        {
            return new LFPlotList(this);
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
            List<LFPlot> list = new List<LFPlot>();
            foreach (LFPlot obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFPlot O1, LFPlot O2) { return O1.Index.CompareTo(O2.Index); });
            this.Clear();
            foreach (LFPlot obj in list)
            {
                this.Add(obj);
            }
        }
        #endregion

        #region Add



        #endregion

        #region Search
        /// <summary>
        /// 按名称搜索ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回搜索到的ID，如未搜索到，返回-1</returns>
        public Int64 GetIndex(string name)
        {
            foreach (LFPlot obj in this)
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
            foreach (LFPlot obj in this)
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
        public LFPlot GetPlot(string name)
        {
            foreach (LFPlot obj in this)
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
        public LFPlot GetPlot(Int64 idx)
        {
            foreach (LFPlot obj in this)
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
        /// 保存情节列表
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path, string file)
        {
            Sort(); // 保存之前先排序

            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Plots");                        // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            /* 开始写入 */
            if (this != null)
            {
                foreach (LFPlot obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Plot");
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
        /// 打开情节列表
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

                Int64 idx = Convert.ToInt64(ele.GetAttribute("Index"));
                string name = ele.GetAttribute("Name");
                LFPlot obj = new LFPlot(idx, name)
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
