/*──────────────────────────────────────────────────────────────
 * FileName     : LFDate
 * Created      : 2020-09-28 10:11:37
 * Author       : Xu Zhe
 * Description  : 日期
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LF.FictionWorld
{
    /// <summary>
    /// 日期
    /// </summary>
    public class LFDate : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        private int _period;    // 纪
        private int _year;      // 年
        private int _month;     // 月
        private int _day;       // 日

        private int _actualYear;      // 实际年
        #endregion

        #region Properties

        /// <summary>
        /// 纪
        /// </summary>
        public int Period { get => _period; set => _period = value; }
        
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get => _year; set => _year = value; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get => _month; set => _month = value; }
        
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get => _day; set => _day = value; }

        /// <summary>
        /// 实际年
        /// </summary>
        public int ActualYear { get => _actualYear; set => _actualYear = value; }

        /// <summary>
        /// 字符串
        /// </summary>
        public string String
        {
            get { return ToString(); }
            set
            {
                ReadString(value);
            }
        }

        /// <summary>
        /// 短字符
        /// </summary>
        public string ShortString
        {
            get { return ToShortString(); }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public long Code
        {
            get { return Encode(); }
            set { Decode(value); }
        }
        #endregion

        #region Constructors
        public LFDate()
        {
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFDate(int code)
        {
            Decode(code);
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFDate(LFDate rhs)
        {
            _period = rhs._period;
            _year = rhs._year;
            _month = rhs._month;
            _day = rhs._day;
            _actualYear = rhs._actualYear;

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFDate Clone()
        {
            return new LFDate(this);
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
        /// 计算年代
        /// </summary>
        public void GetPeriod()
        {
            if (_actualYear >= 0 && _actualYear <= World.Info.Calendar.Periods[0].Count)
            {

                _year = _actualYear;
                _period = 1;
            }
            else if (_actualYear > World.Info.Calendar.Periods[0].Count && _actualYear <= World.Info.Calendar.Periods[0].Count + World.Info.Calendar.Periods[1].Count)
            {
                _year = _actualYear - World.Info.Calendar.Periods[0].Count;
                _period = 2;
            }
            else if (_actualYear > World.Info.Calendar.Periods[0].Count + World.Info.Calendar.Periods[1].Count && _actualYear <= World.Info.Calendar.Periods[0].Count + World.Info.Calendar.Periods[1].Count + World.Info.Calendar.Periods[2].Count)
            {
                _year = _actualYear - World.Info.Calendar.Periods[0].Count - World.Info.Calendar.Periods[1].Count;
                _period = 3;
            }

            else if (_actualYear > World.Info.Calendar.Periods[0].Count + World.Info.Calendar.Periods[1].Count + World.Info.Calendar.Periods[2].Count && _actualYear <= World.Info.Calendar.Periods[0].Count + World.Info.Calendar.Periods[1].Count + World.Info.Calendar.Periods[2].Count + World.Info.Calendar.Periods[3].Count)
            {
                _year = _actualYear - World.Info.Calendar.Periods[0].Count - World.Info.Calendar.Periods[1].Count - World.Info.Calendar.Periods[2].Count;
                _period = 4;
            }
            else
            {
                _year = _actualYear - World.Info.Calendar.Periods[0].Count - World.Info.Calendar.Periods[1].Count - World.Info.Calendar.Periods[2].Count - World.Info.Calendar.Periods[3].Count;
                _period = 5;
            }
        }

        public void GetActualYear()
        {
            _actualYear = _year;
            if (_period != 1)
            {
                for (int i = 1; i < _period; i++)
                {
                    _actualYear += World.Info.Calendar.Periods[i - 1].Count;
                }
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public long Encode()
        {
            return _period * 100000000 + _year * 10000 + _month * 100 + _day;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        public void Decode(long code)
        {
            _period = (int)(code / 100000000);
            _year = (int)((code % 100000000) / 10000);
            _month = (int)((code % 10000) / 100);
            _day = (int)(code % 100);
            GetActualYear();
        }

        /// <summary>
        /// 解读字符串
        /// </summary>
        /// <param name="str"></param>
        public void ReadString(string str)
        {
            try
            {
                string split1 = "纪";
                string[] arr1 = Regex.Split(str, split1, RegexOptions.IgnoreCase);
                switch (arr1[0])
                {
                    case "太古":
                        _period = 0;
                        break;
                    case "上古":
                        _period = 1;
                        break;
                    case "远古":
                        _period = 2;
                        break;
                    case "近古":
                        _period = 3;
                        break;
                    case "新古":
                        _period = 4;
                        break;
                }
                string[] arr2 = Regex.Split(arr1[1], "年", RegexOptions.IgnoreCase);
                _year = Convert.ToInt32(arr2[0]);
                string[] arr3 = Regex.Split(arr2[1], "月", RegexOptions.IgnoreCase);
                _month = Convert.ToInt32(arr3[0]);
                string[] arr4 = Regex.Split(arr3[1], "日", RegexOptions.IgnoreCase);
                _day = Convert.ToInt32(arr4[0]);

                GetActualYear();
            }
            catch { }
        }
        #endregion

        #region Operations

        /// <summary>
        /// 后一天
        /// </summary>
        public void NextDay()
        {
            if (_day == 30)
            {
                NextMonth();
                _day = 1;
            }
            else
            {
                _day++;
            }
        }

        /// <summary>
        /// 前一天
        /// </summary>
        public void PervDay()
        {
            if (_day == 0)
            {
                PrevMonth();
                _day = 30;
            }
            else
            {
                _day--;
            }
        }

        /// <summary>
        /// 后一月
        /// </summary>
        public void NextMonth()
        {
            if (_month == 12)
            {
                NextYear();
                _month = 1;
            }
            else
            {
                _month++;
            }
        }

        /// <summary>
        /// 前一月
        /// </summary>
        public void PrevMonth()
        {
            if (_month == 1)
            {
                PrevYear();
                _month = 12;
            }
            else
            {
                _month--;
            }
        }

        /// <summary>
        /// 后一年
        /// </summary>
        public void NextYear()
        {
            _actualYear++;

            GetPeriod();
        }

        /// <summary>
        /// 前一年
        /// </summary>
        public void PrevYear()
        {
            if (_actualYear != 0)
            {
                _actualYear--;
                GetPeriod();
            }
        }

        #endregion

        #region Calculations

        /// <summary>
        /// 计算天数差
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int Minus(LFDate date)
        {
            return (_actualYear - date._actualYear) * 360 + (_month - date._month) * 30 + _day - date._day;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="date">参考日期</param>
        /// <returns></returns>
        public float GetAge(LFDate date)
        {
            int days = this.Minus(date);

            return days / 360.0f;
        }

        /// <summary>
        /// 加天数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public LFDate Add(int days)
        {
            LFDate rd = this.Clone();

            while (days >= 360)
            {
                days -= 360;
                rd.NextYear();
            }
            while (days >= 30)
            {
                days -= 30;
                rd.NextMonth();
            }
            while (days >= 1)
            {
                days -= 1;
                rd.NextDay();
            }
            return rd;
        }

        /// <summary>
        /// 是否早于某天
        /// </summary>
        /// <param name="date">参考日期</param>
        /// <returns></returns>
        public bool IsBefore(LFDate date)
        {
            int days = Minus(date);
            return days < 0;
        }

        /// <summary>
        /// 相等判断
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool Equals(LFDate d)
        {
            return (_period == d.Period && _year == d.Year && _month == d.Month && _day == d.Day);
        }
        #endregion

        #region Override

        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "NaN";
            try
            {
                str = World.Info.Calendar.Periods[_period].Name + _year.ToString("0000") + "年" + _month.ToString("00") + "月" + _day.ToString("00") + "日";
            }
            catch { }
            return str;
        }

        public string ToShortString()
        {
            return _actualYear.ToString("00000") + "-" + _month.ToString("00") + "-" + _day.ToString("00");

        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion

    }
}
