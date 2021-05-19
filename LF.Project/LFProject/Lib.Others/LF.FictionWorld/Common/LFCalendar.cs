/*──────────────────────────────────────────────────────────────
 * FileName     : LFCalendar
 * Created      : 2020-09-28 10:23:02
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 历法
    /// </summary>
    public class LFCalendar : INotifyPropertyChanged, ICloneable
    {
        #region Fields

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // 纪元及年数
        private InformationList _periods = new InformationList();

        private InformationList _months = new InformationList();
        private int _daysInYear = 360;    // 一年天数
        private int _monthsInYear = 12;    // 一年月数

        private int _hoursInDay;    // 一天时数

        #endregion

        #region Properties

        /// <summary>
        /// 纪元
        /// </summary>
        public InformationList Periods { get => _periods; set => _periods = value; }

        /// <summary>
        /// 月
        /// </summary>
        public InformationList Months { get => _months; set => _months = value; }

        /// <summary>
        /// 一年天数
        /// </summary>
        public int DaysInYear { get => _daysInYear; set => _daysInYear = value; }

        /// <summary>
        /// 一年月数
        /// </summary>
        public int MonthsInYear { get => _monthsInYear; set => _monthsInYear = value; }

        /// <summary>
        /// 一天小时数
        /// </summary>
        public int HoursInDay { get => _hoursInDay; set => _hoursInDay = value; }
        
        #endregion

        #region Constructors
        public LFCalendar()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFCalendar(LFCalendar rhs)
        {
            _daysInYear = rhs._daysInYear;
            _monthsInYear = rhs._monthsInYear;
            _hoursInDay = rhs._hoursInDay;

            _periods = rhs._periods.Clone();
            _months = rhs._months.Clone();

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFCalendar Clone()
        {
            return new LFCalendar(this);
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

        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                 // 定义文件
            xmlDoc.Load(path + @"\Calendar.xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;               // 读取根节点

            _daysInYear = Convert.ToInt32(root.GetAttribute("DaysInYear"));

            XmlElement elePeriods = (XmlElement)root.GetElementsByTagName("Periods")[0];                        // 读取子节点
            foreach (XmlNode node in elePeriods.ChildNodes)
            {
                XmlElement elePeriod = (XmlElement)node;
                CalendarInformation info = new CalendarInformation
                {
                    ID = Convert.ToInt32(elePeriod.GetAttribute("ID")),
                    Name = elePeriod.GetAttribute("Name"),
                    Count = Convert.ToInt32(elePeriod.GetAttribute("Count"))
                };
                _periods.Add(info);
            }

            XmlElement eleMonths = (XmlElement)root.GetElementsByTagName("Months")[0];                        // 读取子节点
            foreach (XmlNode node in eleMonths.ChildNodes)
            {
                XmlElement eleMonth = (XmlElement)node;
                CalendarInformation info = new CalendarInformation
                {
                    ID = Convert.ToInt32(eleMonth.GetAttribute("ID")),
                    Name = eleMonth.GetAttribute("Name"),
                    Count = Convert.ToInt32(eleMonth.GetAttribute("Count"))
                };
                _months.Add(info);
            }

        }


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

    #region Class
    public class CalendarInformation : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;       // 定义属性改变事件

        public int ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public CalendarInformation()
        {

        }

        public CalendarInformation(CalendarInformation rhs)
        {
            this.ID = rhs.ID;
            this.Name = rhs.Name;
            this.Count = rhs.Count;
        }

        public CalendarInformation Clone()
        {
            return new CalendarInformation(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    public class InformationList : ObservableCollection<CalendarInformation>, ICloneable
    {

        public InformationList()
        {

        }

        public InformationList(InformationList rhs)
        {
            foreach (CalendarInformation info in rhs)
            {
                this.Add(info.Clone());
            }
        }

        public InformationList Clone()
        {
            return new InformationList(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }


    }

    #endregion
}
