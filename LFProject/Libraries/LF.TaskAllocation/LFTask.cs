/*──────────────────────────────────────────────────────────────
 * FileName     : LFTask.cs
 * Created      : 2021-05-13 11:31:19
 * Author       : Xu Zhe
 * Description  : 任务
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.TaskAllocation
{
    public class LFTask
    {
        #region Fields
        private int _id = 0;            // 任务编号
        private bool _isDone = false;   // 任务是否已完成

        private double _earliest;       // 任务最早可以执行时间
        private double _latest;         // 任务最迟可以执行时间（时间窗约束）

        private double _x;              // 任务位置X
        private double _y;              // 任务位置Y
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFTask()
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