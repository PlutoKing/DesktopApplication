/*──────────────────────────────────────────────────────────────
 * FileName     : LFRole.cs
 * Created      : 2021-04-29 10:35:46
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace EX01DataContext
{
    /// <summary>
    /// 角色
    /// </summary>
    public class LFRole:INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// 定义属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name ="姓名";
        private int _age = 12;

        private ObservableCollection<int> _tmp = new ObservableCollection<int>() { 1, 2, 3, 4, 5 };
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        public int Age { get => _age; set => _age = value; }
        public ObservableCollection<int> Tmp { get => _tmp; set => _tmp = value; }

        #endregion

        #region Constructors
        public LFRole()
        {

        }
        #endregion

        #region Methods

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