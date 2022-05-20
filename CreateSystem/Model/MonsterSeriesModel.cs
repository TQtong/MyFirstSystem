using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.Model
{
    public class MonsterSeriesModel
    {
        /// <summary>
        /// 怪兽名称
        /// </summary>
        public string MonsterName { get; set; }

        /// <summary>
        /// 饼图数据
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// 具体访问数据
        /// </summary>
        public ObservableCollection<MonsterSeriesDataModel> MonsterSeriesDataModels { get; set; } 
    }
}
