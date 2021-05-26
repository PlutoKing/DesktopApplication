/*──────────────────────────────────────────────────────────────
 * FileName     : LFMoon.cs
 * Created      : 2021-05-25 11:39:55
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
    /// 月亮
    /// </summary>
    public class LFMoon : INotifyPropertyChanged,ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;        // ID
        private string _name;   // 名称

        private int _cycle;     // 公转周期
        private int _shift;     // 初始偏移量
        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public int ID
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
        /// 公转周期
        /// </summary>
        public int Cycle
        {
            get => _cycle;
            set
            {
                _cycle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cycle"));
            }
        }

        /// <summary>
        /// 初始偏移量
        /// </summary>
        public int Shift
        {
            get => _shift;
            set
            {
                _shift = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shift"));
            }
        }
        #endregion

        #region Constructors
        public LFMoon()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFMoon(LFMoon rhs)
        {
            _name = rhs._name;
            _cycle = rhs._cycle;
            _shift = rhs._shift;
        }


        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return new LFMoon(this);
        }
        #endregion

        #region Methods

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}