/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldSetting.cs
 * Created      : 2021-05-19 10:56:52
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
        private LFTypeList areas = new LFTypeList();

        /// <summary>
        /// 等级
        /// </summary>
        private LFTypeList levels = new LFTypeList();

        /// <summary>
        /// 物品分类
        /// </summary>
        private LFTypeList items = new LFTypeList();

        /// <summary>
        /// 属性
        /// </summary>
        private LFTypeList attributes = new LFTypeList();

        /// <summary>
        /// 种族
        /// </summary>
        private LFTypeList races = new LFTypeList();

        /// <summary>
        /// 等级
        /// </summary>
        private LFTypeList ranks = new LFTypeList();

        /// <summary>
        /// 动作
        /// </summary>
        private LFTypeList actions = new LFTypeList();

        /// <summary>
        /// 关系
        /// </summary>
        private LFTypeList relations = new LFTypeList();
        #endregion

        #region Properties

        /// <summary>
        /// 地点
        /// </summary>
        public LFTypeList Areas { get => areas; set => areas = value; }

        /// <summary>
        /// 等级
        /// </summary>
        public LFTypeList Levels { get => levels; set => levels = value; }
        /// <summary>
        /// 物品分配
        /// </summary>
        public LFTypeList Items { get => items; set => items = value; }
        /// <summary>
        /// 属性
        /// </summary>
        public LFTypeList Attributes { get => attributes; set => attributes = value; }
        /// <summary>
        /// 种族
        /// </summary>
        public LFTypeList Races { get => races; set => races = value; }
        /// <summary>
        /// 修炼等级
        /// </summary>
        public LFTypeList Ranks { get => ranks; set => ranks = value; }
        /// <summary>
        /// 动作
        /// </summary>
        public LFTypeList Actions { get => actions; set => actions = value; }

        /// <summary>
        /// 关系
        /// </summary>
        public LFTypeList Relations { get => relations; set => relations = value; }


        #endregion

        #region Constructors
        public LFWorldSetting()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 打开配置
        /// </summary>
        public void Open()
        {
            Areas.Open(World.Info.Path + @"\Settings", "Area");
            Races.Open(World.Info.Path + @"\Settings", "Race");
            Items.Open(World.Info.Path + @"\Settings", "Item");
            Attributes.Open(World.Info.Path + @"\Settings", "Attribute");
            Levels.Open(World.Info.Path + @"\Settings", "Level");
            Ranks.Open(World.Info.Path + @"\Settings", "Rank");
            Actions.Open(World.Info.Path + @"\Settings", "Action");
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            Areas.Save(World.Info.Path + @"\Settings", "Area");
            Races.Save(World.Info.Path + @"\Settings", "Race");
            Items.Save(World.Info.Path + @"\Settings", "Item");
            Attributes.Save(World.Info.Path + @"\Settings", "Attribute");
            Levels.Save(World.Info.Path + @"\Settings", "Level");
            Ranks.Save(World.Info.Path + @"\Settings", "Rank");
            Actions.Save(World.Info.Path + @"\Settings", "Action");
        }

        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}