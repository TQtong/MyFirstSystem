using System;
using System.Collections.Generic;
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

namespace CustomControlLibrary
{
    /// <summary>
    /// Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class Instrument : UserControl
    {
        public Instrument()
        {
            InitializeComponent();
            this.SizeChanged += Instrument_SizeChanged;
        }

        //依赖属性
        //只有在依赖对象上才有GetValue()和SetValue()
        /// <summary>
        /// 自定义属性的包装器
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Instrument), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged))); 
        //第一个参数的意思是注册属性的名字，
        //第二个参数的意思是这个属性的类型，
        //第三个是这个属性属于谁，
        //第四个参数是属性改变会触发的委托

        /// <summary>
        /// 属性值改变时，触发函数对view中定义的方法调用
        /// </summary>
        /// <param name="obj">view对象</param>
        /// <param name="e">事件参数</param>
        public static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as Instrument).Refresh();
        }


        /// <summary>
        /// 设置椭圆为圆，以控件最小的值为直径生成圆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instrument_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.backEllipse.Width = minSize;
            this.backEllipse.Height = minSize;
        }

        private void Refresh()
        {

        }
    }
}
