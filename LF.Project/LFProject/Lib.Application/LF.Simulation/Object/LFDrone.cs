/*──────────────────────────────────────────────────────────────
 * FileName     : LFDrone.cs
 * Created      : 2021-05-15 15:10:12
 * Author       : Xu Zhe
 * Description  : 无人机
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Simulation
{
    /// <summary>
    /// 无人机
    /// </summary>
    public class LFDrone
    {
        #region Fields
        private int _id;        // ID

        
        #endregion

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }

        #endregion

        #region Constructors
        public LFDrone()
        {
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