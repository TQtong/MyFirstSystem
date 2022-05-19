using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CreateSystem.Common
{
    public class CommandBase : ICommand
    {
        /// <summary>
        /// 改变事件
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 是否可执行的判断
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return DoCanExecute?.Invoke(parameter) == true;
        }

        /// <summary>
        /// 执行体
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
             DoExecute?.Invoke(parameter);
        }

        /// <summary>
        /// 定义执行体的委托
        /// </summary>
        public Action<object> DoExecute { get; set; }

        /// <summary>
        /// 定义执行判断的委托
        /// </summary>
        public Func<object,bool> DoCanExecute { get; set; }

        /// <summary>
        /// 获取改变事件
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
