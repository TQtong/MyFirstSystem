using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CreateSystem.Common
{
    public class PasswordHelper
    {
        /// <summary>
        /// false表示没更新,true表示更新了
        /// </summary>
        static bool _isUpdata = false;

        /// <summary>
        /// 定义一个字段用于获取用户输入的密码
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(PasswordProperty, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// 获取密码
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(PasswordProperty).ToString();
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetPassword(DependencyObject d, string value)
        {
            d.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// 定义一个字段用于监听密码框的改变
        /// </summary>
        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        /// <summary>
        /// 读取密码
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        /// <summary>
        /// 写入密码
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(AttachProperty, value);
        }

        /// <summary>
        /// 密码框改变后，对改变后的密码框进行事件绑定
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged -= Password_PasswordChanged;
            if (!_isUpdata)
                password.Password = e.NewValue.ToString();
            password.PasswordChanged += Password_PasswordChanged;
        }

        /// <summary>
        /// 为密码框添加改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged += Password_PasswordChanged;
        }

        /// <summary>
        /// 用于监听密码框师傅有改动，有就调用写入函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdata = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdata = false;
        }
    }
}
