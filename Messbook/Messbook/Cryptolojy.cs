using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messbook
{
    class Cryptolojy
    {
        public static string Encryption(string text,int key)
        {
            char[] rar = text.ToCharArray();
            string encryptedtext = null;

            foreach (var item in rar)
            {
                encryptedtext += Convert.ToChar(item + key);
            }
            return encryptedtext;
        }
        public static string Decryption(string text,int key)
        {
            char[] rar = text.ToCharArray();
            string decryptedtext = null;
            foreach (var item in rar)
            {
                decryptedtext += Convert.ToChar(item - key);
            }
            return decryptedtext;


        }

    }
}
