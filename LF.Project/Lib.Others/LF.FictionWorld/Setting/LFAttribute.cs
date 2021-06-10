/*──────────────────────────────────────────────────────────────
 * FileName     : LFAttribute.cs
 * Created      : 2021-05-25 20:48:58
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    public class LFAttribute :  ObservableCollection<LFType>, ICloneable
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
        /// <param name="code">编码</param>
        public LFAttribute(int code)
        {
            _code = code;
            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param Value="obj"></param>
        public LFAttribute(LFAttribute obj)
        {
            _code = obj._code;
            Clear();
            foreach (LFType type in obj)
            {
                Add(type.Clone());
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
            return Clone();
        }
        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 清空
        /// </summary>
        public void ClearAttribute()
        {
            Clear();
            _code = 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a">属性</param>
        public void AddAttribute(LFType a)
        {
            Add(a);     // 添加
            Encode();   // 重新编码
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            Sort();        // 先排序，后编码
            _code = 0;
            foreach (LFType t in this)
            {
                _code += (int)Math.Pow(2, t.Code);
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
                        Add(World.Setting.Attributes.GetType(i));
                    }
                }
            }
        }
        #endregion

        #region Sort Methods
        /// <summary>
        /// 按照索引排序
        /// </summary>
        public void Sort()
        {
            List<LFType> list = new List<LFType>();
            foreach (LFType obj in this)
            {
                list.Add(obj);
            }
            list.Sort(delegate (LFType O1, LFType O2) { return O1.Code.CompareTo(O2.Code); });
            Clear();
            foreach (LFType obj in list)
            {
                Add(obj);
            }
        }
        #endregion

        #region String Override Methods
        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            foreach (LFType type in this)
            {
                str += type.Value + " - ";
            }
            if (str == "")
                return "无";

            return str.Substring(0, str.Length - 3);
        }
        #endregion
        #endregion
    }
}