/*──────────────────────────────────────────────────────────────
 * FileName     : LFAttribute
 * Created      : 2020-09-28 10:46:23
 * Author       : Xu Zhe
 * Description  : 属性
 *                使用10位编码表示10种属性，0000000000表示无属性
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 属性列表
    /// </summary>
    public class LFAttribute : ObservableCollection<LFType>, ICloneable
    {
        #region Fields
        private int _code;  // 1023之内的数字，表示10位编码
        #endregion

        #region Properties

        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get => _code; set => _code = value; }

        /// <summary>
        /// 字符串
        /// </summary>
        public string String
        {
            get
            {
                return ToString();
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFAttribute()
        {

        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="code"></param>
        public LFAttribute(int code)
        {
            _code = code;
            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param Value="rhs"></param>
        public LFAttribute(LFAttribute rhs)
        {
            this._code = rhs._code;
            this.Clear();
            foreach (LFType obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }


        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFAttribute Clone()
        {
            return new LFAttribute(this);
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

        #region Common

        /// <summary>
        /// 清空
        /// </summary>
        public void ClearAttribute()
        {
            this.Clear();
            this._code = 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        public void AddAttribute(LFType a)
        {
            this.Add(a);    // 添加
            Encode();       // 重新编码
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            this.Sort();        // 先排序，后编码
            this._code = 0;
            foreach (LFType t in this)
            {
                this._code += (int)Math.Pow(2, t.Index);
            }
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode()
        {
            if (_code >= 0 && _code <= Math.Pow(2, World.Setting.Attributes.Count) - 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int a = (_code >> i) & 1;
                    if (a == 1)
                    {
                        this.Add(World.Setting.Attributes.GetType(i));
                    }
                }
            }
        } 
        #endregion

        #region Sort
        /// <summary>
        /// 按照ID排序
        /// </summary>
        public void Sort()
        {
            List<LFType> list = new List<LFType>();
            foreach (LFType obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFType O1, LFType O2) { return O1.Index.CompareTo(O2.Index); });
            this.Clear();
            foreach (LFType obj in list)
            {
                this.Add(obj);
            }
        }
        #endregion


        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            string str = "";
            foreach (LFType type in this)
            {
                str += type.Value + "-";
            }
            if (str == "")
                return "无";

            return str.Substring(0, str.Length - 1);
        }
        #endregion

    }

}
