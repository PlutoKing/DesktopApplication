﻿/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorldData.cs
 * Created      : 2021-05-25 11:18:58
 * Author       : Xu Zhe
 * Description  : 世界数据：历史、物品、文化、秘籍、地点、角色、势力、情节
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
    /// 世界数据
    /// </summary>
    public class LFWorldData : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private LFItemList _itemList = new LFItemList();    // 物品

        private LFItem _item = new LFItem();                // 当前物品

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

        }

        /// <summary>
        /// 保存世界数据
        /// </summary>
        public void Save()
        {
            ItemList.Save(World.Info.Path + @"\Data", "Items");

        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}