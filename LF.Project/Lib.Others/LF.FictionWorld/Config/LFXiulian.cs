/*──────────────────────────────────────────────────────────────
 * FileName     : LFXiulian.cs
 * Created      : 2021-05-30 20:49:04
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using LF.Mathematics;

namespace LF.FictionWorld
{
    /// <summary>
    /// 修炼模型
    /// </summary>
    public class LFXiulian : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 修炼列表
        /// </summary>
        private LFRankList _rankList = new LFRankList();

        private double _limitFactor;
        private double _startAge;
        private int _shift;

        /// <summary>
        /// 修炼模型计算参数
        /// </summary>
        private double[] _params;       // 修炼模型计算参数

        #endregion

        #region Properties
        /// <summary>
        /// 修炼列表
        /// </summary>
        public LFRankList RankList
        {
            get => _rankList;
            set
            {
                _rankList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RankList"));
            }
        }

        /// <summary>
        /// 修炼模型计算参数
        /// </summary>
        public double[] Params
        {
            get => _params;
            set
            {
                _params = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Params"));
            }
        }
        public double LimitFactor
        {
            get => _limitFactor;
            set
            {
                _limitFactor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LimitFactor"));
            }
        }

        /// <summary>
        /// 修炼开始年龄
        /// </summary>
        public double StartAge
        {
            get => _startAge;
            set
            {
                _startAge = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartAge"));
            }
        }


        #endregion

        #region Constructors
        public LFXiulian()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFXiulian(LFXiulian rhs)
        {
            _rankList = rhs._rankList.Clone();
            _params = rhs._params;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFXiulian Clone()
        {
            return new LFXiulian(this);
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

        /// <summary>
        /// 计算系数
        /// </summary>
        public void GetParams()
        {
            int n = _rankList.Count;
            double[] g = new double[n];
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = _rankList[i].Level;
                _rankList[i].GetTalent(_limitFactor,_startAge,2);
                g[i] = _rankList[i].Talent;
            }

            // 多项式拟合，求解模型参数
            LFFitting fitting = new LFFitting
            {
                X = new LFVector(VectorType.Col, x),
                Y = new LFVector(VectorType.Col, g)
            };
            _params = fitting.PolynomialFitting(n);

        }
        /// <summary>
        /// 计算修炼年龄
        /// </summary>
        /// <param name="t">天赋值</param>
        /// <param name="l">等级</param>
        /// <returns>年龄</returns>
        public double GetAge(double t, double l)
        {
            // 计算天赋值中间量
            double tmp = 0;
            for (int i = 0; i < _params.Length; i++)
            {
                tmp += _params[i] * Math.Pow(t, _params.Length - i - 1);
            }

            double age = _startAge * Math.Exp(tmp * (l - 1));
            return age;
        }

        public void Open(string path)
        {
            /* XML基础操作 */
            XmlDocument xmlDoc = new XmlDocument();                 // 定义文件
            xmlDoc.Load(path + @"\Xiulian.xml");                    // 加载文件
            XmlElement root = xmlDoc.DocumentElement;               // 读取根节点

            _startAge = Convert.ToDouble(root.GetAttribute("StartAge"));
            _limitFactor = Convert.ToDouble(root.GetAttribute("LimitFactor"));
            _shift = Convert.ToInt32(root.GetAttribute("Shift"));
            
            foreach (XmlNode node in root.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                LFRank rank = new LFRank
                {
                    ID = Convert.ToInt32(ele.GetAttribute("ID")),
                    Level = Convert.ToInt32(ele.GetAttribute("Level")),
                    Name = World.Setting.Ranks.GetType(Convert.ToInt32(ele.GetAttribute("Name"))),
                    AgeLimit = Convert.ToDouble(ele.GetAttribute("AgeLimit"))
                };
                _rankList.Add(rank);
            }
        }
        #endregion
    }

    /// <summary>
    /// 修炼模型
    /// </summary>
    public class LFRank : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;            // ID
        private int _level;         // 天赋等级
        private LFType _name;       // 修为上限
        private double _ageLimit;   // 寿限

        private double _age;        // 突破年限
        private double _talent;     // 天赋值

        #endregion

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }
        /// <summary>
        /// 天赋等级
        /// </summary>
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
            }
        }
        /// <summary>
        /// 修为上限
        /// </summary>
        public LFType Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        /// <summary>
        /// 寿限
        /// </summary>
        public double AgeLimit
        {
            get => _ageLimit;
            set
            {
                _ageLimit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AgeLimit"));
            }
        }
        /// <summary>
        /// 突破年限
        /// </summary>
        public double Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Age"));
            }
        }
        /// <summary>
        /// 天赋值
        /// </summary>
        public double Talent
        {
            get => _talent;
            set
            {
                _talent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Talent"));
            }
        }

        #endregion

        #region Constructors
        public LFRank()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFRank(LFRank rhs)
        {
            _id = rhs._id;
            _name = rhs._name;
            _level = rhs._level;
            _ageLimit = rhs._ageLimit;
            _age = rhs._age;
            _talent = rhs._talent;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFRank Clone()
        {
            return new LFRank(this);
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

        /// <summary>
        /// 计算天赋值
        /// </summary>
        /// <param name="startAge"></param>
        /// <param name="shift"></param>
        public void GetTalent(double limitFactor, double startAge,int shift)
        {
            _age = _ageLimit * limitFactor;
            _talent = Math.Log(_age / startAge) / (_level + shift);
        }

        #endregion

    }

    /// <summary>
    /// 修炼模型列表
    /// </summary>
    public class LFRankList : ObservableCollection<LFRank>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFRankList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFRankList(LFRankList rhs)
        {
            foreach (LFRank obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该修炼模型列表相同的新实例</returns>
        public LFRankList Clone()
        {
            return new LFRankList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该修炼模型列表相同的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #endregion
    }

}