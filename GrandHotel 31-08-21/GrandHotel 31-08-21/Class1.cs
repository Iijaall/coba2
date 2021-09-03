using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrandHotel_31_08_21
{
    class Class1
    {
        public static int Count;
        public static Regex RegexEmail = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z-0-9]+.{1,100}(com)+$");

        public static string SHA256(string str)
        {
            using (SHA256 sHA = SHA256Managed.Create())
            {
                var b = string.Concat(sHA.ComputeHash(Encoding.UTF8.GetBytes(str)).Select(s => s.ToString()));
                return b;
            }
        }
        //public static string SHA256(string inputString)
        //{
        //    SHA256 sha256 = SHA256Managed.Create();
        //    byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        //    byte[] hash = sha256.ComputeHash(bytes);
        //    return GetStringFromHash(hash);

        //}
        //private static string GetStringFromHash(byte[] hash)
        //{
        //    StringBuilder result = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        result.Append(hash[i].ToString("X2"));
        //    }
        //    return result.ToString();
        //}
    }
}
