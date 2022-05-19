using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateSystem.DataAccess.DataEntity;

namespace CreateSystem.Common
{
    public class GlobalValues
    {

        /// <summary>
        /// 定义一个全局变量接收从服务器传来的值
        /// </summary>
        public static UserEntity UserInfo { get; set; }

        /// <summary>
        /// 定义有个全局变量监测登录窗口状态变化
        /// </summary>
        public static bool WindowState { get; set; }
    }
}
