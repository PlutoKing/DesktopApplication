/*──────────────────────────────────────────────────────────────
 * FileName     : Block.cs
 * Created      : 2021-05-18 20:30:50
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LF.Tetris
{
    /// <summary>
    /// 方块的抽象类
    /// </summary>
    public abstract class Block
    {
        #region Fields

        private List<Rectangle> _rectangles = new List<Rectangle>();    // 方块

        private DispatcherTimer Timer;     // 计时器

        private Grid _grid;

        private bool _isPause = false;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public Block()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Normal);
            Timer.Tick += Timer_Tick;
            Timer.Interval = TimeSpan.FromMilliseconds(500);
        }

        #endregion

        #region Methods


        /// <summary>
        /// 判断x行y列是否存在方块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsExistBlock(int x,int y)
        {
            foreach(var r in _grid.Children)
            {
                if (r is Rectangle)
                {
                    if (this._rectangles.Contains(r as Rectangle)) return false;
                    if (Convert.ToInt32((r as Rectangle).GetValue(Grid.ColumnProperty)) == x && Convert.ToInt32((r as Rectangle).GetValue(Grid.RowProperty)) == y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断方块是否与其他方块重叠
        /// </summary>
        /// <returns></returns>
        public bool IsOverlapping()
        {
            foreach (var r in _rectangles)
            {
                int x = Convert.ToInt32((r as Rectangle).GetValue(Grid.ColumnProperty));
                int y = Convert.ToInt32((r as Rectangle).GetValue(Grid.RowProperty));
                if (IsExistBlock(x, y)) return true;
            }
            return false;
        }



        #endregion

        #region Events

        private void Timer_Tick(object sender, EventArgs e)
        {

        }
        #endregion
    }

    #region Class

    /// <summary>
    /// 位置
    /// </summary>
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Position()
        {
        }
    }
    #endregion
}