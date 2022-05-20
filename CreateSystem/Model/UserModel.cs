using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateSystem.Common;

namespace CreateSystem.Model
{
    public class UserModel : BindObject
    {
        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; this.OnPropertyChanged(); }
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; this.OnPropertyChanged(); }
        }
        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; this.OnPropertyChanged(); }
        }

    }
}
