using CreateSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CreateSystem.Common
{
    public class MonsterDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 有内容显示
        /// </summary>
        public DataTemplate DefaultTempalte { get; set; }

        /// <summary>
        /// 无内容显示
        /// </summary>
        public DataTemplate SkeletonTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if ((item as MonsterModel).IsShowSkeleton)
            {
                return SkeletonTemplate;
            }

            return DefaultTempalte;
        }
    }
}
