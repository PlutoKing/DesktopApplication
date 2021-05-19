/*──────────────────────────────────────────────────────────────
 * FileName     : LFAction
 * Created      : 2020/9/28 23:03:01
 * Author       : Xu Zhe
 * Description  : 行为
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{
    public class LFAction : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;            // ID
        private LFRole _role;       // 角色

        private LFType _type = new LFType();    // 类别

        private string _content;    // 内容

        #endregion

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }

        /// <summary>
        /// 角色
        /// </summary>
        public LFRole Role { get => _role; set => _role = value; }

        /// <summary>
        /// 类型
        /// </summary>
        public LFType Type { get => _type; set => _type = value; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get => _content; set => _content = value; }

        #endregion

        #region Constructors
        public LFAction()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFAction(LFAction rhs)
        {
            this.Role = rhs._role;
            this._id = rhs._id;

            this._type = rhs._type;

            this._content = rhs._content;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFAction Clone()
        {
            return new LFAction(this);
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
