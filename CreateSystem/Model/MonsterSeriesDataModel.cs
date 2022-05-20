using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.Model
{
    public class MonsterSeriesDataModel
    {
        /// <summary>
        /// 怪兽访问标题
        /// </summary>
        public string MonsterSeriesName { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public decimal CurrentValue { get; set; }

        /// <summary>
        /// 访问数据是否增长
        /// </summary>
        public bool IsGrowing { get; set; }

        /// <summary>
        /// 变化率
        /// </summary>
        public int ChangeRate { get; set; }
    }
}
