/*──────────────────────────────────────────────────────────────
 * FileName     : AttributeSelector.cs
 * Created      : 2021-05-25 21:52:29
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
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
    /// AttributeSelector.xaml 的交互逻辑
    /// </summary>
    public partial class AttributeSelector : UserControl
    {
        #region Fields

        #endregion

        #region Properties


        /// <summary>
        /// 属性列表
        /// </summary>
        public LFAttribute Attributes
        {
            get { return (LFAttribute)GetValue(AttributesProperty); }
            set { SetValue(AttributesProperty, value); }
        }

        public static readonly DependencyProperty AttributesProperty =
            DependencyProperty.Register("Attributes", typeof(LFAttribute), typeof(AttributeSelector), new PropertyMetadata(new LFAttribute(), OnPropertyChanged));



        #endregion

        #region Constructors
        public AttributeSelector()
        {
            InitializeComponent();
            ListAttributes.ItemsSource = World.Setting.Attributes;

            SetCurrentValue(AttributesProperty, new LFAttribute());
        }
        #endregion

        #region Methods
        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            foreach (LFType a in Attributes)
            {
                LFType t = World.Setting.Attributes.GetType(a.Value);
                ListAttributes.SelectedItems.Add(t);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 属性变化
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as AttributeSelector)?.Refresh();

        /// <summary>
        /// 选项发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAttributes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LFAttribute aList = new LFAttribute();
            foreach (LFType t in ListAttributes.SelectedItems)
            {
                aList.AddAttribute(t);
            }
            SetCurrentValue(AttributesProperty, aList);
        }
        #endregion
    }
}
