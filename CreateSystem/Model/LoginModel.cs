using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateSystem.Common;

namespace CreateSystem.Model
{
    public class LoginModel:NotifyBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _password;

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 是否可行
        /// </summary>
        private string _validationCode;

        public string ValidationCode
        {
            get { return _validationCode; }
            set
            {
                _validationCode = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private string  _errorMessage;

        public string  ErrorMessage
        {
            get { return _errorMessage; }
            set
            { 
                _errorMessage = value;
                this.DoNotify();
            }
        }

    }
}
