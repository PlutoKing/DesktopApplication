/*──────────────────────────────────────────────────────────────
 * FileName     : LFCalendar.cs
 * Created      : 2021-05-25 11:22:50
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 历法
    /// </summary>
    public class LFCalendar : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _daysInYear;    // 一年天数
        private LFCalendarInfoList _eras;   // 分代
        private LFCalendarInfoList _months; // 月份信息
        private LFCalendarInfoList _weeks;    // 周名称 
        private ObservableCollection<LFMoon> _moonList;    // 月相
        #endregion

        #region Properties

        /// <summary>
        /// 一年天数
        /// </summary>
        public int DaysInYear
        {
            get => _daysInYear;
            set
            {
                _daysInYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DaysInYear"));
            }
        }
        /// <summary>
        /// 年代划分
        /// </summary>
        public LFCalendarInfoList Eras
        {
            get => _eras; 
            set
            {
                _eras = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Eras"));
            }
        }
        /// <summary>
        /// 月份划分
        /// </summary>
        public LFCalendarInfoList Months
        {
            get => _months;
            set
            {
                _months = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Months"));
            }
        }
        /// <summary>
        /// 星期设定
        /// </summary>
        public LFCalendarInfoList Weeks
        {
            get => _weeks;
            set
            {
                _weeks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Weeks"));
            }
        }
        /// <summary>
        /// 月相
        /// </summary>
        public ObservableCollection<LFMoon> MoonList
        {
            get => _moonList;
            set
            {
                _moonList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MoonList"));
            }
        }
        /// <summary>
        /// 纪元数
        /// </summary>
        public int NumberOfEras
        {
            get => _eras.Count;
        }
        /// <summary>
        /// 一年月数
        /// </summary>
        public int MonthsInYear
        {
            get => _months.Count;
        }
        /// <summary>
        /// 一周天数
        /// </summary>
        public int DaysInWeek
        {
            get => _weeks.Count;
        }
        /// <summary>
        /// 月亮数
        /// </summary>
        public int NumberOfMoons
        {
            get => _moonList.Count;
        }
        #endregion

        #region Constructors
        public LFCalendar()
        {
            _eras = new LFCalendarInfoList();
            _months = new LFCalendarInfoList();
            _weeks = new LFCalendarInfoList();
            _moonList = new ObservableCollection<LFMoon>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                 // 定义文件
            xmlDoc.Load(path + @"\Calendar.xml");                   // 加载文件
            XmlElement root = xmlDoc.DocumentElement;               // 读取根节点

            _daysInYear = Convert.ToInt32(root.GetAttribute("DaysInYear"));
            // 读取纪元
            XmlElement elePeriods = (XmlElement)root.GetElementsByTagName("Eras")[0];
            foreach (XmlNode node in elePeriods.ChildNodes)
            {
                XmlElement elePeriod = (XmlElement)node;
                LFCalendarInfo info = new LFCalendarInfo
                {
                    ID = Convert.ToInt32(elePeriod.GetAttribute("ID")),
                    Name = elePeriod.GetAttribute("Name"),
                    Count = Convert.ToInt32(elePeriod.GetAttribute("Count"))
                };
                _eras.Add(info);
            }
            // 读取月份
            XmlElement eleMonths = (XmlElement)root.GetElementsByTagName("Months")[0];
            foreach (XmlNode node in eleMonths.ChildNodes)
            {
                XmlElement eleMonth = (XmlElement)node;
                LFCalendarInfo info = new LFCalendarInfo
                {
                    ID = Convert.ToInt32(eleMonth.GetAttribute("ID")),
                    Name = eleMonth.GetAttribute("Name"),
                    Count = Convert.ToInt32(eleMonth.GetAttribute("Count"))
                };
                _months.Add(info);
            }
            // 读取周
            XmlElement eleWeeks = (XmlElement)root.GetElementsByTagName("Weeks")[0];
            foreach (XmlNode node in eleWeeks.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFCalendarInfo info = new LFCalendarInfo
                {
                    ID = Convert.ToInt32(ele.GetAttribute("ID")),
                    Name = ele.GetAttribute("Name"),
                    Count = 0
                };
                _weeks.Add(info);
            }
            // 读取月相
            XmlElement eleMoons = (XmlElement)root.GetElementsByTagName("Moons")[0];
            foreach (XmlNode node in eleMoons.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFMoon moon = new LFMoon()
                {
                    ID = Convert.ToInt32(ele.GetAttribute("ID")),
                    Name = ele.GetAttribute("Name"),
                    Cycle = Convert.ToInt32(ele.GetAttribute("Cycle")),
                    Shift = Convert.ToInt32(ele.GetAttribute("Shift")),
                };
                _moonList.Add(moon);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            /* 基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                                 // 定义文件
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); // 定义声明
            xmlDoc.AppendChild(dec);                                                // 插入声明
            XmlElement root = xmlDoc.CreateElement("Calendar");                     // 定义根节点

            root.SetAttribute("DaysInYear", _daysInYear.ToString());
            xmlDoc.AppendChild(root);                                               // 插入根节点

            // 写入年代
            XmlElement eleEras = xmlDoc.CreateElement("Eras");
            foreach(LFCalendarInfo info in _eras)
            {
                XmlElement ele = xmlDoc.CreateElement("Era");
                ele.SetAttribute("ID", info.ID.ToString());
                ele.SetAttribute("Name", info.Name);
                ele.SetAttribute("Count", info.Count.ToString());
                eleEras.AppendChild(ele);
            }
            root.AppendChild(eleEras);
            // 写入月份
            XmlElement eleMonths = xmlDoc.CreateElement("Months");
            foreach (LFCalendarInfo info in _months)
            {
                XmlElement ele = xmlDoc.CreateElement("Month");
                ele.SetAttribute("ID", info.ID.ToString());
                ele.SetAttribute("Name", info.Name);
                ele.SetAttribute("Count", info.Count.ToString());
                eleMonths.AppendChild(ele);
            }
            root.AppendChild(eleMonths);
            /* 保存文件 */
            xmlDoc.Save(path + @"\Calendar.xml");
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}