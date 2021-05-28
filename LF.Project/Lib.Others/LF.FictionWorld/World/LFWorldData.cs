/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldData.cs
 * Created      : 2021-05-25 11:18:58
 * Author       : Xu Zhe
 * Description  : 世界数据：历史、物品、文化、秘籍、地点、角色、势力、情节
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 世界数据
    /// </summary>
    public class LFWorldData : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private LFItemList _itemList = new LFItemList();    // 物品列表
        private LFBookList _bookList = new LFBookList();    // 秘籍列表
        private LFSiteList _siteList = new LFSiteList();    // 地点列表
        private LFSectList _sectList = new LFSectList();    // 势力列表

        private LFItem _item = new LFItem();                // 当前物品
        private LFBook _book = new LFBook();                // 当前秘籍
        private LFSite _site = new LFSite();                // 当前地点
        private LFSect _sect = new LFSect();                // 当前势力

        #endregion

        #region Properties
        /// <summary>
        /// 物品列表
        /// </summary>
        public LFItemList ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemList"));
            }
        }

        /// <summary>
        /// 秘籍列表
        /// </summary>
        public LFBookList BookList
        {
            get => _bookList;
            set
            {
                _bookList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BookList"));
            }
        }
        /// <summary>
        /// 地点列表
        /// </summary>
        public LFSiteList SiteList { get => _siteList;
            set
            {
                _siteList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 势力列表
        /// </summary>
        public LFSectList SectList
        {
            get => _sectList;
            set
            {
                _sectList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SectList"));
            }
        }

        /// <summary>
        /// 当前物品
        /// </summary>
        public LFItem Item
        {
            get => _item;
            set
            {
                _item = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
            }
        }


        /// <summary>
        /// 当前秘籍
        /// </summary>
        public LFBook Book
        {
            get => _book;
            set
            {
                _book = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Book"));
            }
        }
        /// <summary>
        /// 当前地点
        /// </summary>
        public LFSite Site
        {
            get => _site;
            set
            {
                _site = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Site"));
            }
        }
        /// <summary>
        /// 当前势力
        /// </summary>
        public LFSect Sect { get => _sect;
            set
            {
                _sect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sect"));
            }
        }
        #endregion

        #region Constructors
        public LFWorldData()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 打开世界数据
        /// </summary>
        public void Open()
        {
            ItemList.Open(World.Info.Path + @"\Data", "Items");
            BookList.Open(World.Info.Path + @"\Data", "Books");
            SiteList.Open(World.Info.Path + @"\Data", "Sites");
            SectList.Open(World.Info.Path + @"\Data", "Sects");

            foreach (LFItem obj in ItemList)
            {
                obj.Open(World.Info.Path + @"\Data");
            }

            foreach (LFBook obj in BookList)
            {
                obj.Open(World.Info.Path + @"\Data");
            }

            foreach(LFSite obj in SiteList)
            {
                obj.Open(World.Info.Path + @"\Data");
                obj.CheckItems();
            }
        }

        /// <summary>
        /// 保存世界数据
        /// </summary>
        public void Save()
        {
            ItemList.Save(World.Info.Path + @"\Data", "Items");
            BookList.Save(World.Info.Path + @"\Data", "Books");
            SiteList.Save(World.Info.Path + @"\Data", "Sites");
            SectList.Save(World.Info.Path + @"\Data", "Sects");

            DeleteFiles(World.Info.Path + @"\Data\Items");
            foreach (LFItem obj in ItemList)
            {
                obj.Save(World.Info.Path + @"\Data");
            }


            DeleteFiles(World.Info.Path + @"\Data\Books");
            foreach (LFBook obj in BookList)
            {
                obj.Save(World.Info.Path + @"\Data");
            }

            DeleteFiles(World.Info.Path + @"\Data\Sites");
            foreach (LFSite obj in SiteList)
            {
                obj.Save(World.Info.Path + @"\Data");
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public static void DeleteFiles(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}