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
using System.Windows.Media.Animation;
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
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(Instrument), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(Instrument), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 大刻度线数量
        /// </summary>
        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(Instrument), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 刻度盘背景色
        /// </summary>
        public Brush PlateBackground
        {
            get { return (Brush)GetValue(PlateBackgroundProperty); }
            set { SetValue(PlateBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlateBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlateBackgroundProperty =
            DependencyProperty.Register("PlateBackground", typeof(Brush), typeof(Instrument), new PropertyMetadata(default(Brush)));



        /// <summary>
        /// 字体大小
        /// </summary>
        public int ScaleTextSize
        {
            get { return (int)GetValue(ScaleTextSizeProperty); }
            set { SetValue(ScaleTextSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleTextSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleTextSizeProperty =
            DependencyProperty.Register("ScaleTextSize", typeof(int), typeof(Instrument), new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 字体、刻度颜色
        /// </summary>
        public Brush ScaleBrush
        {
            get { return (Brush)GetValue(ScaleBrushProperty); }
            set { SetValue(ScaleBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleBrushProperty =
            DependencyProperty.Register("ScaleBrush", typeof(Brush), typeof(Instrument), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnPropertyChanged)));





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
            //确定圆的中心位置
            double radius = backEllipse.Width / 2;

            if (double.IsNaN(radius)) return;

            //清除之前画的圆
            drawCanvas.Children.Clear();

            int scaleText = (int)((MaxValue - MinValue) / Interval);//大刻度线刻度值的步长
            double step = 270.0 / (MaxValue - MinValue); //获取刻度步长

            for (int i = 0; i < MaxValue - MinValue; i++) //循环生成100个刻度值
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 13) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 13) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = ScaleBrush;
                lineScale.StrokeThickness = 1;

                drawCanvas.Children.Add(lineScale);
            }

            step = 270.0 / 10; //大刻度位置
            for (int i = 0; i <= Interval; i++) //生成大刻度
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 18) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 18) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = ScaleBrush;
                lineScale.StrokeThickness = 1;

                drawCanvas.Children.Add(lineScale);

                Console.WriteLine(ScaleTextSize);

                TextBlock textScale = new TextBlock();
                textScale.Width = 34;
                textScale.TextAlignment = TextAlignment.Center;
                textScale.FontSize = ScaleTextSize;
                textScale.Text = (scaleText * i).ToString();
                textScale.Foreground = ScaleBrush;
                Canvas.SetLeft(textScale, radius - (radius - 36) * Math.Cos((i * step - 45) * Math.PI / 180) - 17);//x坐标（注：减17因为设置了宽度，为了以圆的中心位置对齐）
                Canvas.SetTop(textScale, radius - (radius - 36) * Math.Sin((i * step - 45) * Math.PI / 180) - 10);//y坐标（注：减10因为设置了字体大小，为了以圆的中心位置对齐）

                drawCanvas.Children.Add(textScale);
            }

            //中间圆弧
            string data = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            data = string.Format(data, radius / 2, radius, radius * 1.5);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            circle.Data = (Geometry)converter.ConvertFrom(data);

            //中心点
            data = "M{0} {1}, {1} {2}, {1} {3}";
            data = string.Format(data, radius * 0.3, radius, radius-5, radius + 5);
            converter = TypeDescriptor.GetConverter(typeof(Geometry));
            point.Data = (Geometry)converter.ConvertFrom(data);

            //指针跟值变化而变化
            step = 270.0 / (MaxValue - MinValue);
            rtPoint.Angle = Value * step -45;

            //给指针变化加个动画

            DoubleAnimation doubleAnimation = new DoubleAnimation(Value * step - 45, new Duration(TimeSpan.FromMilliseconds(2000)));
            rtPoint.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
        }
    }
}
