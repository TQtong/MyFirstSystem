using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.Model
{
    public class MonsterModel
    {
        /// <summary>
        /// 怪兽名称
        /// </summary>
        public string MonsterName { get; set; }

        /// <summary>
        /// 怪兽图片
        /// </summary>
        public string MonsterPicture { get; set; }

        /// <summary>
        /// 怪兽地址
        /// </summary>
        public string MonsterUrl { get; set; }

        /// <summary>
        /// 怪兽描述
        /// </summary>
        public string MonsterDescription { get; set; }

        /// <summary>
        /// 怪兽管理者
        /// </summary>
        public List<string> MonsterManagers { get; set; }

        /// <summary>
        /// 显示骨架(界面骨架)
        /// </summary>
        public bool IsShowSkeleton { get; set; }
    }
}
