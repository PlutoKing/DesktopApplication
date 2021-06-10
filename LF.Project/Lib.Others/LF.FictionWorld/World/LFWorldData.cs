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
        private LFRoleList _roleList = new LFRoleList();    // 角色列表
        private LFPlotList _plotList = new LFPlotList();    // 情节列表

        private LFItem _item = new LFItem();                // 当前物品
        private LFBook _book = new LFBook();                // 当前秘籍
        private LFSite _site = new LFSite();                // 当前地点
        private LFSect _sect = new LFSect();                // 当前势力
        private LFRole _role = new LFRole();                // 当前角色
        private LFPlot _plot = new LFPlot();                // 当前情节

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
        /// 角色列表
        /// </summary>
        public LFRoleList RoleList
        {
            get => _roleList;
            set
            {
                _roleList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoleList"));
            }
        }
        /// <summary>
        /// 情节列表
        /// </summary>
        public LFPlotList PlotList
        {
            get => _plotList;
            set
            {
                _plotList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlotList"));
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
        /// <summary>
        /// 当前角色
        /// </summary>
        public LFRole Role
        {
            get => _role;
            set
            {
                _role = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Role"));
            }
        }
        /// <summary>
        /// 当前情节
        /// </summary>
        public LFPlot Plot
        {
            get => _plot;
            set
            {
                _plot = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Plot"));
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
            RoleList.Open(World.Info.Path + @"\Data", "Roles");
            PlotList.Open(World.Info.Path + @"\Data", "Plots");

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
            foreach(LFRole obj in RoleList)
            {
                obj.Open(World.Info.Path + @"\Data");
                obj.Ranks.GetValue(World.Info.NowDate);
                obj.Experiences.GetValue(World.Info.NowDate);
                obj.ExpToSect();
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
            RoleList.Save(World.Info.Path + @"\Data", "Roles");
            PlotList.Save(World.Info.Path + @"\Data", "Plots");

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

            DeleteFiles(World.Info.Path + @"\Data\Roles");
            foreach (LFRole obj in RoleList)
            {
                obj.Save(World.Info.Path + @"\Data");
            }

            DeleteFiles(World.Info.Path + @"\Data\Sects");
            foreach (LFSect obj in SectList)
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