/*──────────────────────────────────────────────────────────────
 * FileName     : LFWorld
 * Created      : 2020-09-28 10:10:11
 * Author       : Xu Zhe
 * Description  : 世界
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LF.FictionWorld
{
    /// <summary>
    /// 世界
    /// </summary>
    public class World
    {
        #region Fields

        /// <summary>
        /// 世界信息
        /// </summary>
        public static LFWorldInfo Info = new LFWorldInfo();

        /// <summary>
        /// 世界配置
        /// </summary>

        public static LFWorldSetting Setting = new LFWorldSetting();

        /// <summary>
        /// 世界数据
        /// </summary>

        public static LFWorldData Data = new LFWorldData();

        #endregion

        #region Properties
        #endregion

        #region Constructors
        public World()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 打开
        /// </summary>
        public static void Open()
        {
            Info.Open();

            Setting.Open();

            Data.Open();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public static void Save()
        {
            Setting.Save();
            Data.Save();
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
