using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class MD5pwd
    {
        public static string getmd5(string code)
        {
            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(code));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "");
        }
    }
}
