/*──────────────────────────────────────────────────────────────
 * FileName     : LFDate.cs
 * Created      : 2021-05-25 15:01:43
 * Author       : Xu Zhe
 * Description  : 
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

        private int _year;      // 年
        private int _month;     // 月
        private int _day;       // 日

        private int _era;       // 纪
        private int _eraYear;   // 纪元中的年


        private readonly string[] charaters = new string[]
        {
            "纪",
            "年",
            "月",
            "日"
        };
        #endregion

        #region Properties
        /// <summary>
        /// 年
        /// </summary>
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
            }
        }

        /// <summary>
        /// 月
        /// </summary>
        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Month"));
            }
        }

        /// <summary>
        /// 日
        /// </summary>
        public int Day
        {
            get => _day; 
            set
            {
                _day = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Day"));
            }
        }

        /// <summary>
        /// 纪
        /// </summary>
        public int Era
        {
            get => _era; set
            {
                _era = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Era"));
            }
        }
        /// <summary>
        /// 纪元中的年
        /// </summary>
        public int EraYear
        {
            get => _eraYear;
            set
            {
                _eraYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EraYear"));
            }
        }

        /// <summary>
        /// 日期编码
        /// </summary>
        public long Code
        {
            get { return Encode(); }
            set { Decode(value); }
        }

        /// <summary>
        /// 字符串
        /// </summary>
        public string String
        {
            get { return ToString(); }
            set { ReadString(value); }
        }

        /// <summary>
        /// 短字符串
        /// </summary>
        public string ShortString
        {
            get { return ToShortString(); }
        }
        #endregion

        #region Constructors
        public LFDate()
        {

        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        public LFDate(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
            GetEra();
            Encode();
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="era">纪</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        public LFDate(int era,int year,int month,int day)
        {
            _era = era;
            _year = year;
            _month = month;
            _day = day;
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="code">编码</param>
        public LFDate(long code)
        {
            Decode(code);
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="str">字符串</param>
        public LFDate(string str)
        {
            ReadString(str);
        }
        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFDate(LFDate rhs)
        {
            _year = rhs._year;
            _month = rhs._month;
            _day = rhs._day;
            _era = rhs._era;
            _eraYear = rhs._eraYear;
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
            return Clone();
        }
        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 计算年代
        /// </summary>
        public void GetEra()
        {
            _eraYear = _year;
            _era = 1;

            for(int i = 0; i < World.Info.Calendar.NumberOfEras; i++)
            {
                if (_eraYear <= World.Info.Calendar.Eras[i].Count)
                {
                    _era = i+1;
                    return;
                }
                else
                {
                    _eraYear -= World.Info.Calendar.Eras[i].Count;
                }
            }
        }

        /// <summary>
        /// 已知纪元年求真实年
        /// </summary>
        public void GetYear()
        {
            _year = _eraYear;
            if (_era != 1)
            {
                for(int i = 1; i < _era; i++)
                {
                    _year += World.Info.Calendar.Eras[i-1].Count;
                }
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public long Encode()
        {
           return  _era * 100000000 +
                _eraYear * 10000 +
                _month * 100 +
                _day;
        }

        /// <summary>
        /// 解码
        /// </summary>
        public void Decode(long code)
        {
            _era = (int)(code / 100000000);
            _eraYear = (int)(code % 100000000 / 10000);
            _month = (int)(code % 10000 / 100);
            _day = (int)(code % 100);
            GetYear();
        }

        /// <summary>
        /// 解读字符串
        /// </summary>
        /// <param name="str"></param>
        public void ReadString(string str)
        {
            try
            {
                // 从第一个符号处分开
                string[] arr1 = Regex.Split(str, charaters[0], RegexOptions.IgnoreCase);
                _era = World.Info.Calendar.Eras.GetID(arr1[0])-1;
                string[] arr2 = Regex.Split(arr1[1], charaters[1], RegexOptions.IgnoreCase);
                _eraYear = Convert.ToInt32(arr2[0]);
                string[] arr3 = Regex.Split(arr2[1], charaters[2], RegexOptions.IgnoreCase);
                _month = Convert.ToInt32(arr3[0]);
                string[] arr4 = Regex.Split(arr3[1], charaters[3], RegexOptions.IgnoreCase);
                _day = Convert.ToInt32(arr4[0]);

                GetYear();
            }
            catch { }
        }
        #endregion

        #region Operating Methods

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
            if (_month == World.Info.Calendar.Months.Count)
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
                _month = World.Info.Calendar.Months.Count;
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
            _year++;

            GetEra();
        }

        /// <summary>
        /// 前一年
        /// </summary>
        public void PrevYear()
        {
            if (_year != 0)
            {
                _year--;
                GetEra();
            }
        }

        #endregion

        #region Calculating Methods

        /// <summary>
        /// 计算天数差
        /// </summary>
        /// <param name="date"></param>
        /// <returns>相差天数</returns>
        public int Minus(LFDate date)
        {
            int tmp1 = (_year - date._year) * World.Info.Calendar.DaysInYear;
            int tmp3 = _day - date._day;
            int tmp2 = 0;
            if (_month < date._month)
            {
                for (int i = _month; i < date._month; i++)
                {
                    tmp2 += World.Info.Calendar.Months[i].Count;
                }
            }
            else
            {
                for (int i = date._month; i < _month; i++)
                {
                    tmp2 += World.Info.Calendar.Months[i].Count;
                }
            }

            return tmp1 + tmp2 + tmp3;
        }
        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="date">参考日期</param>
        /// <returns></returns>
        public double GetAge(LFDate date)
        {
            int days = Minus(date);

            return days / World.Info.Calendar.DaysInYear;
        }

        /// <summary>
        /// 加天数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public LFDate Add(int days)
        {
            LFDate rd = Clone();

            while (days >= World.Info.Calendar.DaysInYear)
            {
                days -= World.Info.Calendar.DaysInYear;
                rd.NextYear();
            }
            while (days >= World.Info.Calendar.Months[rd._month-1].Count)
            {
                days -= World.Info.Calendar.Months[rd._month-1].Count;
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
            return ( _year == d.Year && _month == d.Month && _day == d.Day);
        }
        #endregion

        #region Override

        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            try
            {
                str = World.Info.Calendar.Eras[_era].Name +"纪" + _eraYear.ToString("0000") + "年" + _month.ToString("00") + "月" + _day.ToString("00") + "日";
            }
            catch { }
            return str;
        }

        public string ToShortString()
        {
            return _year.ToString("00000") + "-" + _month.ToString("00") + "-" + _day.ToString("00");

        }

        #endregion

        #endregion
    }
}