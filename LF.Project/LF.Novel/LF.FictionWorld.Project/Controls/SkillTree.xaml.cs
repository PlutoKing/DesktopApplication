/*──────────────────────────────────────────────────────────────
 * FileName     : SkillTree.cs
 * Created      : 2021-05-27 21:17:41
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LF.FictionWorld.Project.Controls
{
    /// <summary>
    /// SkillTree.xaml 的交互逻辑
    /// </summary>
    public partial class SkillTree : UserControl
    {
        #region Fields

        LFSkillNodeList nodeList = new LFSkillNodeList();
        #endregion
        public LFSkillList SkillList
        {
            get { return (LFSkillList)GetValue(SkillListProperty); }
            set { SetValue(SkillListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SkillList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SkillListProperty =
            DependencyProperty.Register("SkillList", typeof(LFSkillList), typeof(SkillTree), new PropertyMetadata(new LFSkillList(), OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as SkillTree)?.Refresh();

        #region Constructors
        public SkillTree()
        {
            InitializeComponent();


            SetCurrentValue(SkillListProperty, new LFSkillList());
        }
        #endregion

        #region Methods

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            nodeList.Clear();

            foreach (LFSkill skill in SkillList)
            {
                if (skill.Precondition == 0)
                {
                    LFSkillNode node = new LFSkillNode()
                    {
                        ID = skill.ID,
                        Name = skill.Name,
                        Brief = skill.Brief,
                        Childs = new LFSkillNodeList(),
                    };
                    nodeList.Add(node);
                }
                else
                {
                    LFSkillNode node = new LFSkillNode()
                    {
                        ID = skill.ID,
                        Name = skill.Name,
                        Brief = skill.Brief,
                        Childs = new LFSkillNodeList(),
                    };
                    LFSkillNode tmp = nodeList.GetNode(skill.Precondition);
                    if (tmp != null)
                    {
                        tmp.Childs.Add(node);
                    }

                }
            }
            DtgSklls.ItemsSource = nodeList;
        }

        #endregion

        #region Events


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

    #endregion

}

    public class LFSkillNode : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private byte _id;           // ID
        private string _name;       // 名称
        private string _brief;      // 简介
        private LFSkillNodeList _childs;     // 子分类

        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public byte ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get => _brief;
            set
            {
                _brief = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brief"));
            }
        }

        /// <summary>
        /// 子分类
        /// </summary>
        public LFSkillNodeList Childs
        {
            get => _childs; set
            {
                _childs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Childs"));
            }
        }
        #endregion

        #region Constructors
        public LFSkillNode()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFSkillNode(LFSkillNode rhs)
        {
            _id = rhs._id;
            _name = rhs._name;
            _brief = rhs._brief;

            if (rhs._childs != null)
            {
                _childs = new LFSkillNodeList();
                _childs = _childs.Clone();
            }

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSkillNode Clone()
        {
            return new LFSkillNode(this);
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



        #endregion
    }

    public class LFSkillNodeList : ObservableCollection<LFSkillNode>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFSkillNodeList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFSkillNodeList(LFSkillNodeList rhs)
        {
            foreach (LFSkillNode obj in rhs)
            {
                Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSkillNodeList Clone()
        {
            return new LFSkillNodeList(this);
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
        public LFSkillNode GetNode(int id)
        {
            foreach(LFSkillNode node in this)
            {
                if(node.ID == id)
                {
                    return node;
                }

                if (node.Childs != null)
                {
                    LFSkillNode tmp = node.Childs.GetNode(id);
                    if (tmp != null)
                        return tmp;
                }
            }
            return null;
        }
        #endregion
    }

}
