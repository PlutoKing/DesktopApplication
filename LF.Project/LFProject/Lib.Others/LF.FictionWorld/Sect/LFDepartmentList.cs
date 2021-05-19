/*──────────────────────────────────────────────────────────────
 * FileName     : LFDepartmentList
 * Created      : 2020-09-28 19:04:08
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFDepartmentList : ObservableCollection<LFDepartment>, ICloneable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public LFDepartmentList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFDepartmentList(LFDepartmentList rhs)
        {
            foreach (LFDepartment obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFDepartmentList Clone()
        {
            return new LFDepartmentList(this);
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

        #region File



        
        #endregion


        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
