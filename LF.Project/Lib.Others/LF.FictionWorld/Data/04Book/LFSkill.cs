/*──────────────────────────────────────────────────────────────
 * FileName     : LFSkill.cs
 * Created      : 2021-05-27 16:55:00
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
    /// 技能
    /// </summary>
    public class LFSkill : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private byte _id;            // ID
        private string _name;       // 名称
        private string _brief;      // 简介
        private byte _level;         // 所需等级
        private byte _precondition;  // 前提条件
        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public byte ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get => _brief;
            set
            {
                _brief = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brief"));
            }
        }
        /// <summary>
        /// 等级需求
        /// </summary>
        public byte Level { get => _level;
            set
            {
                _level = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
            }
        }
        /// <summary>
        /// 前提条件
        /// </summary>
        public byte Precondition { get => _precondition;
            set
            {
                _precondition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Precondition"));
            }
        }
        #endregion

        #region Constructors
        public LFSkill()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFSkill(LFSkill rhs)
        {
            _id = rhs._id;
            _name = rhs._name;
            _brief = rhs._brief;
            _level = rhs._level;
            _precondition = rhs._precondition;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
        public LFSkill Clone()
        {
            return new LFSkill(this);
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #endregion
    }
}