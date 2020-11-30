using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace kursovay.Utils
{
    public class Cryptographer
    {
        public static string Hash(string plainString)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainString);
            SHA1Managed hasher = new SHA1Managed();
            byte[] hashedBytes = hasher.ComputeHash(plainBytes);
            string hashedString = Encoding.UTF8.GetString(hashedBytes);
            return hashedString;
        }
    }
}