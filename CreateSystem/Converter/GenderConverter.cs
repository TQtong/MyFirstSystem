using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CreateSystem.Converter
{
    /// <summary>
    /// 定义一个转换器用于判断数据和前端的值对比，显示对应的性别选中状态
    /// </summary>
    public class GenderConverter : IValueConverter
    {
        /// <summary>
        /// 从Model向ViewModel传值
        /// </summary>
        /// <param name="value">Model上绑定的值</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">ViewModel绑定的值</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }
            return value.ToString() == parameter.ToString();
        }

        /// <summary>
        /// 从ViewModel向Model传值
        /// </summary>
        /// <param name="value">Model上绑定的值</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">ViewModel绑定的值</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
