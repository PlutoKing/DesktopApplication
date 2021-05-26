/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldSetting.cs
 * Created      : 2021-05-25 11:18:43
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 世界配置
    /// </summary>
    public class LFWorldSetting : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 区域
        /// </summary>
        private LFTypeList _areas = new LFTypeList();
        /// <summary>
        /// 等级
        /// </summary>
        private LFTypeList _levels = new LFTypeList();
        /// <summary>
        /// 物品分类
        /// </summary>
        private LFTypeList _items = new LFTypeList();
        /// <summary>
        /// 属性
        /// </summary>
        private LFTypeList _attributes = new LFTypeList();
        /// <summary>
        /// 种族
        /// </summary>
        private LFTypeList _races = new LFTypeList();
        /// <summary>
        /// 等级
        /// </summary>
        private LFTypeList _ranks = new LFTypeList();
        /// <summary>
        /// 动作
        /// </summary>
        private LFTypeList _actions = new LFTypeList();
        /// <summary>
        /// 关系
        /// </summary>
        private LFTypeList _relations = new LFTypeList();
        #endregion

        #region Properties

        /// <summary>
        /// 区域分类
        /// </summary>
        public LFTypeList Areas
        {
            get => _areas;
            set
            {
                _areas = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }


        /// <summary>
        /// 属性分类
        /// </summary>
        public LFTypeList Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 等级
        /// </summary>
        public LFTypeList Levels
        {
            get => _levels;
            set
            {
                _levels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 物品分类
        /// </summary>
        public LFTypeList Items
        {
            get => _items;
            set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 种族分类
        /// </summary>
        public LFTypeList Races
        {
            get => _races;
            set
            {
                _races = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 修炼等级
        /// </summary>
        public LFTypeList Ranks
        {
            get => _ranks;
            set
            {
                _ranks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 动作分类
        /// </summary>
        public LFTypeList Actions
        {
            get => _actions;
            set
            {
                _actions = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }
        /// <summary>
        /// 关系类别
        /// </summary>
        public LFTypeList Relations
        {
            get => _relations;
            set
            {
                _relations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SiteList"));
            }
        }

        #endregion

        #region Constructors
        public LFWorldSetting()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 打开世界配置
        /// </summary>
        public void Open()
        {
            Areas.Open(World.Info.Path + @"\Setting", "Area");
            Races.Open(World.Info.Path + @"\Setting", "Race");
            Items.Open(World.Info.Path + @"\Setting", "Item");
            Attributes.Open(World.Info.Path + @"\Setting", "Attribute");
            Levels.Open(World.Info.Path + @"\Setting", "Level");
            Ranks.Open(World.Info.Path + @"\Setting", "Rank");
            Actions.Open(World.Info.Path + @"\Setting", "Action");
        }

        /// <summary>
        /// 保存世界配置
        /// </summary>
        public void Save()
        {
            Areas.Save(World.Info.Path + @"\Setting", "Area");
            Races.Save(World.Info.Path + @"\Setting", "Race");
            Items.Save(World.Info.Path + @"\Setting", "Item");
            Attributes.Save(World.Info.Path + @"\Setting", "Attribute");
            Levels.Save(World.Info.Path + @"\Setting", "Level");
            Ranks.Save(World.Info.Path + @"\Setting", "Rank");
            Actions.Save(World.Info.Path + @"\Setting", "Action");
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}