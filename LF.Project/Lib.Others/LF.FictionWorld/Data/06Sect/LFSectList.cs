/*──────────────────────────────────────────────────────────────
 * FileName     : LFSectList.cs
 * Created      : 2021-05-28 17:21:48
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
    /// 势力列表，表示<see cref="LFSect"/>的列表
    /// </summary>
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
        /// <param name="rhs">源对象</param>
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
        /// <returns>与该势力列表相同的新实例</returns>
        public LFSectList Clone()
        {
            return new LFSectList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该势力列表相同的新实例</returns>
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
            List<LFSect> list = new List<LFSect>();
            foreach (LFSect obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFSect O1, LFSect O2) { return O1.Code.CompareTo(O2.Code); });
            this.Clear();
            foreach (LFSect obj in list)
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
            foreach (LFSect obj in this)
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
            foreach (LFSect obj in this)
            {
                if (obj.Code == code)
                {
                    return obj.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// 按Name搜索Sect
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>搜索到的对象；如未搜索到，返回null</returns>
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
        /// 按Code搜索Sect
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns>搜索到的对象；如未搜索到，返回null</returns>
        public LFSect GetSect(long code)
        {
            foreach (LFSect obj in this)
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
        internal LFSectList GetSectGroup(long code)
        {
            LFSectList list = new LFSectList();
            foreach (LFSect obj in this)
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
        /// <param name="items">被减列表</param>
        /// <returns>剩余列表</returns>
        public LFSectList Minus(LFSectList items)
        {
            LFSectList res = new LFSectList();

            foreach (LFSect item in this)
            {
                bool tmp = false;   // 是否在items中
                foreach (LFSect obj in items)
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

        #endregion

        #region Operating Methods

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="obj">待添加对象</param>
        public void AddSect(LFSect obj)
        {
            Add(obj);
            Sort();
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        /// <param name="idx">待添加对象的索引</param>
        /// <param name="obj"></param>
        public void EditSect(int idx, LFSect obj)
        {
            long oldCode = this[idx].Code;
            LFSect oldSect = this[idx];
            // 更新Sect数据
            this[idx] = obj;

            // 更新Sect相关信息
            

            // 排序
            Sort();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj">待删除对象</param>
        public void DeleteSect(LFSect obj)
        {
            if (Contains(obj))
            {
                Remove(obj);

                // 更新Sect相关信息
                
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
            foreach (LFSect item in this)
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
        #endregion

        #region Data Methods

        /// <summary>
        /// 减去指针所指的对象
        /// </summary>
        /// <param name="pointers"></param>
        /// <returns></returns>
        public LFSectList Minus(LFPointerList pointers)
        {
            LFSectList list = new LFSectList();
            foreach (LFSect item in this)
            {
                bool tmp = false;   // 是否在pointers中
                foreach (LFPointer p in pointers)
                {
                    if (item.Code == p.Code || item.Name == p.Name)
                    {
                        tmp = true; ;
                        break;
                    }
                }
                if (!tmp)
                {
                    list.Add(item);
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
            XmlElement root = xmlDoc.CreateElement("Sects");                        // 定义根节点
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 写入数据
            if (this != null)
            {
                foreach (LFSect obj in this)
                {
                    XmlElement ele = xmlDoc.CreateElement("Sect");
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
                LFSect obj = new LFSect(idx, name)
                {
                    Brief = ele.GetAttribute("Brief"),
                };
                Add(obj);
                obj.CheckSite();
            }
        }

        #endregion

        #endregion
    }
}