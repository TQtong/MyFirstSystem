using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.Common
{
    public class MD5Provider
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">字符串拼接后的密码</param>
        /// <returns>返回加密后的密码</returns>
        public static string GetMD5String(string str)
        {
            MD5 mD5 = MD5.Create();
            byte[] pwd = mD5.ComputeHash(Encoding.UTF8.GetBytes(str));
            string password = "";
            foreach (var item in pwd)
            {
                password += item.ToString("X2");
            }
            password = password.ToLower();
            return password;
        }
    }
}
