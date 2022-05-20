using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CreateSystem.Common;
using CreateSystem.Model;

namespace CreateSystem.ViewModel
{
    public class MainViewModel : BindObject
    {

        public UserModel UserInfo { get; set; }

        private string  _search;

        public string  Search
        {
            get { return _search; }
            set { _search = value; this.OnPropertyChanged(); }
        }

        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.OnPropertyChanged(); }
        }

        public CommandBase NavChangeCommand { get; set; }

        public MainViewModel()
        {
            UserInfo = new UserModel();
            this.NavChangeCommand = new CommandBase();
            NavChangeCommand.DoExecute = new Action<object>(DoChanged);
            NavChangeCommand.DoCanExecute = new Func<object, bool>((o) => true);

            DoChanged("HomeView");
        }

        public void DoChanged(object obj)
        {
            Type type = Type.GetType("CreateSystem.View." + obj.ToString());
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }

    }
}
