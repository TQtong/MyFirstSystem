using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.Model
{
    public class MonsterCategoryItemModel
    {
        public bool IsSelected { get; set; }
        public string CategoryName { get; set; }

        public MonsterCategoryItemModel(string name, bool state = false)
        {
            CategoryName = name;
            IsSelected = state;
        }
    }
}
