/*──────────────────────────────────────────────────────────────
 * FileName     : LFPidController
 * Created      : 2021-05-12 23:20:01
 * Author       : Xu Zhe
 * Description  : PID控制器
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Collections.ObjectModel;

namespace LF.UnmannedAerialVehicle
{
    /// <summary>
    /// PID控制器
    /// </summary>
    public class LFPidController:INotifyPropertyChanged
    {
        #region Fields
        // <summary>
        /// 定义属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private double _kp;
        private double _ki;
        private double _kd;

        private double errOld;
        private double errSum;
        #endregion

        #region Properties
        /// <summary>
        /// 比例系数
        /// </summary>
        public double Kp { get => _kp; set => _kp = value; }

        /// <summary>
        /// 积分系数
        /// </summary>
        public double Ki { get => _ki; set => _ki = value; }

        /// <summary>
        /// 微分系数
        /// </summary>
        public double Kd { get => _kd; set => _kd = value; }
        #endregion

        #region Constructors
        public LFPidController()
        {
        }

        public LFPidController(double kp, double ki, double kd)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
        }

        public LFPidController(LFPidController rhs)
        {
            this._kp = rhs._kp;
            this._ki = rhs._ki;
            this._kd = rhs._kd;

        }

       
        #endregion

        #region Methods
        /// <summary>
        /// PID控制器
        /// </summary>
        /// <param name="expectedVal">期望值</param>
        /// <param name="measuredVal">测量值</param>
        /// <returns>操纵量</returns>
        public double Run(double expectedVal, double measuredVal)
        {
            double err = expectedVal - measuredVal;
            double Pout = _kp * err;

            errSum += err;                 // 误差值积分

            double Iout = _ki * errSum;

            double derr = err - errOld;    // 误差值微分

            double Dout = _kd * derr;

            double output = Pout + Iout + Dout;

            return output;
        }
        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

    }
}
