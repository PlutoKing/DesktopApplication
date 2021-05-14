/*──────────────────────────────────────────────────────────────
 * FileName     : LFTa.cs
 * Created      : 2021-05-13 11:41:40
 * Author       : Xu Zhe
 * Description  : Task Allocation 任务分配算法
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Algorithm.TaskAllocation
{
    /// <summary>
    /// Task Allocation
    /// </summary>
    public class LFTa
    {
        #region Fields
        private List<LFTask> _tasks = new List<LFTask>();  // 要完成的任务
        private List<LFAgent> _agents = new List<LFAgent>();        // 执行任务的对象

        #endregion

        #region Properties
        /// <summary>
        /// 任务数目
        /// </summary>
        public int TaskCount
        {
            get { return _tasks.Count; }
        }

        /// <summary>
        /// 执行者数目
        /// </summary>
        public int AgentCount
        {
            get { return _agents.Count; }
        }

        public List<LFAgent> Agents { get => _agents; set => _agents = value; }
        public List<LFTask> Tasks { get => _tasks; set => _tasks = value; }

        #endregion

        #region Constructors
        public LFTa()
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