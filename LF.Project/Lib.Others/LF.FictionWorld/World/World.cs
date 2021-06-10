/*──────────────────────────────────────────────────────────────
 * FileName     : World.cs
 * Created      : 2021-05-25 11:17:54
 * Author       : Xu Zhe
 * Description  : 世界类，用于管理架空世界的所有信息
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 世界类
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
        /// 世界配置
        /// </summary>
        public static LFWorldConfig Config = new LFWorldConfig();

        /// <summary>
        /// 世界数据
        /// </summary>
        public static LFWorldData Data = new LFWorldData();

        #endregion


        #region Constructors
        public World()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 打开世界
        /// </summary>
        public static void Open()
        {
            Info.Open();
            Setting.Open();
            Config.Open();
            Data.Open();
        }

        /// <summary>
        /// 保存世界
        /// </summary>
        public static void Save()
        {
            Info.Save();
            Setting.Save();
            Data.Save();
        }
        #endregion

    }
}