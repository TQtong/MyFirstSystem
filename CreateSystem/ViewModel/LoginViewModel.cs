using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CreateSystem.Common;
using CreateSystem.DataAccess;
using CreateSystem.Model;
using CreateSystem.View;

namespace CreateSystem.ViewModel
{
    public class LoginViewModel:NotifyBase
    {


        public CommandBase CloseWindowCommand { get; set; } = new CommandBase();

        public LoginModel LoginModel { get; set; }

        public CommandBase LoginCommand { get; set; } = new CommandBase();

        private Visibility _showProgress = Visibility.Collapsed;

        /// <summary>
        /// 进度条
        /// </summary>
        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value;
                this.DoNotify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginViewModel()
        {
            this.CloseWindowCommand.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Close();
            });
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });

            this.LoginModel = new LoginModel();
            this.LoginModel.UserName = "admin";
            this.LoginModel.Password = "123456";
            this.LoginModel.ValidationCode = "utf8";

            this.LoginCommand.DoExecute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return true;
            });
        }

        /// <summary>
        /// 判断用户是否输入与输入是否正确
        /// </summary>
        /// <param name="o"></param>
        public void DoLogin(object o)
        {
            this.ShowProgress = Visibility.Visible;
            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                this.LoginModel.ErrorMessage = "请输入用户名!";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                this.LoginModel.ErrorMessage = "请输入密码!";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.ValidationCode))
            {
                this.LoginModel.ErrorMessage = "请输入验证码!";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (LoginModel.ValidationCode.ToLower() != "utf8")
            {
                this.LoginModel.ErrorMessage = "验证码输入错误!";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            //防止查询时候耗时过长而卡死，故加了一个多线程防止卡死
            Task.Run(new Action(async () =>
            {
                //等待2秒
                await Task.Delay(2000);
                try
                {
                    var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (user == null)
                    {
                        //MessageBox.Show("登录失败!用户名或密码错误");
                        throw new Exception("登录失败！用户名或密码错误！");
                    }
                    //获取的数据保存到全局变量上
                    GlobalValues.UserInfo = user;

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        (o as Window).DialogResult = true;
                    }));
                }
                catch (Exception ex)
                {
                    this.LoginModel.ErrorMessage = ex.Message;
                }
                finally
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.ShowProgress = Visibility.Collapsed;
                    }));
                }
            }));
            
        }
    }
}
