/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldData.cs
 * Created      : 2021-05-19 10:56:17
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        /// <summary>
        /// 时间列表
        /// </summary>
        private LFEventList eventList = new LFEventList();

        /// <summary>
        /// 地点列表
        /// </summary>
        private LFSiteList siteList = new LFSiteList();

        /// <summary>
        /// 物品列表
        /// </summary>
        private LFItemList itemList = new LFItemList();

        private LFBookList bookList = new LFBookList();

        private LFSectList sectList = new LFSectList();
        /// <summary>
        /// 角色列表
        /// </summary>
        private LFRoleList roleList = new LFRoleList();

        /// <summary>
        /// 情节列表
        /// </summary>
        private LFPlotList plotList = new LFPlotList();

        private LFSite _site = new LFSite();                // 当前地点
        private LFItem _item = new LFItem();                // 当前物品
        private LFBook _book = new LFBook();                // 当前秘籍
        private LFSect _sect = new LFSect();                // 当前势力
        private LFRole _role = new LFRole();                // 当前角色
        private LFPlot _plot = new LFPlot();                // 当前情节

        #endregion

        #region Properties

        #region 数据列表
        /// <summary>
        /// 事件列表
        /// </summary>
        public LFEventList EventList { get => eventList; set => eventList = value; }
        /// <summary>
        /// 地点列表
        /// </summary>
        public LFSiteList SiteList { get => siteList; set => siteList = value; }
        /// <summary>
        /// 物品列表
        /// </summary>
        public LFItemList ItemList { get => itemList; set => itemList = value; }
        /// <summary>
        /// 书籍列表
        /// </summary>
        public LFBookList BookList { get => bookList; set => bookList = value; }
        /// <summary>
        /// 势力列表
        /// </summary>
        public LFSectList SectList { get => sectList; set => sectList = value; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public LFRoleList RoleList { get => roleList; set => roleList = value; }
        /// <summary>
        /// 情节列表
        /// </summary>
        public LFPlotList PlotList { get => plotList; set => plotList = value; } 
        #endregion

        #region 当前数据
        /// <summary>
        /// 地点
        /// </summary>
        public LFSite Site
        {
            get { return _site; }
            set
            {
                _site = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Site"));
            }
        }

        /// <summary>
        /// 物品
        /// </summary>
        public LFItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
            }
        }
        public LFBook Book
        {
            get { return _book; }
            set
            {
                _book = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Book"));
            }
        }

        public LFSect Sect
        {
            get { return _sect; }
            set
            {
                _sect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sect"));
            }
        }

        public LFRole Role
        {
            get { return _role; }
            set
            {
                _role = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Role"));
            }
        }

        public LFPlot Plot
        {
            get { return _plot; }
            set
            {
                _plot = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Plot"));
            }
        }

        #endregion

        #endregion

        #region Constructors
        public LFWorldData()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 加载数据
        /// </summary>
        public void Open()
        {
            EventList.Open(World.Info.Path + @"\Datas", "Events");
            SiteList.Open(World.Info.Path + @"\Datas", "Sites");
            ItemList.Open(World.Info.Path + @"\Datas", "Items");
            BookList.Open(World.Info.Path + @"\Datas", "Books");
            SectList.Open(World.Info.Path + @"\Datas", "Sects");
            RoleList.Open(World.Info.Path + @"\Datas", "Roles");
            PlotList.Open(World.Info.Path + @"\Datas", "Plots");

            foreach (LFSite site in SiteList)
            {
                try
                {
                    site.Open(World.Info.Path + @"\Datas");
                }
                catch { }
            }

            foreach (LFSect sect in SectList)
            {
                try
                {
                    sect.Open(World.Info.Path + @"\Datas");
                }
                catch { }
            }

            /* 解析角色 */
            foreach (LFRole role in RoleList)
            {
                try
                {
                    role.Open(World.Info.Path + @"\Datas");
                    role.Ranks.GetValue(World.Info.NowDate);
                    role.Experiences.GetValue(World.Info.NowDate);
                    role.ExpToSect();
                }
                catch { }
            }

            foreach (LFPlot plot in PlotList)
            {
                plot.Open(World.Info.Path + @"\Datas");
            }

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            SiteList.Save(World.Info.Path + @"\Datas", "Sites");
            ItemList.Save(World.Info.Path + @"\Datas", "Items");
            BookList.Save(World.Info.Path + @"\Datas", "Books");
            SectList.Save(World.Info.Path + @"\Datas", "Sects");
            RoleList.Save(World.Info.Path + @"\Datas", "Roles");
            PlotList.Save(World.Info.Path + @"\Datas", "Plots");

            DeleteFiles(World.Info.Path + @"\Datas\Sites");
            foreach (LFSite site in SiteList)
            {
                site.Save(World.Info.Path + @"\Datas");
            }

            DeleteFiles(World.Info.Path + @"\Datas\Sects");
            foreach (LFSect sect in SectList)
            {
                sect.Save(World.Info.Path + @"\Datas");
            }

            DeleteFiles(World.Info.Path + @"\Datas\Roles");
            foreach (LFRole role in RoleList)
            {
                role.Save(World.Info.Path + @"\Datas");
            }


            DeleteFiles(World.Info.Path + @"\Datas\Plots");

            foreach (LFPlot plot in PlotList)
            {
                plot.Save(World.Info.Path + @"\Datas");
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

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion
    }
}