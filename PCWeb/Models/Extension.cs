using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PCWeb.Models
{
    public static class Extension
    {
        public static string ConvertoBase64(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
        }
        public static string StringToBinary(this string strText)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strText);
                BitArray bitArray = new BitArray(bytes);
                StringBuilder bits = new StringBuilder();
                foreach (bool item in bitArray)
                {
                    bits.Append(item == true ? "1" : "0");
                }
                return bits.ToString();
            }
            return string.Empty;

        }
        public static string Md5(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                var bytehash=HashAlgorithm.Create("Md5").ComputeHash(bytes);
                return bytehash.ByteToHexString();
            }
            return string.Empty;
        }
        public static string SHA1(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                var bytehash = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
                return bytehash.ByteToHexString();
            }
            return string.Empty;
        }
        public static string ByteToHexString(this byte[] buffer)
        {
            StringBuilder builder = new StringBuilder(4);
            foreach (var item in buffer)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
        public static string ConvetToBase(this int number, int baseNumber)
        {
            return ToBin(number);

        }

        public static string ToOct(this int input)
        {

            string hex = string.Empty;
            while (input != 0)
            {
                var rem = (input % 8);
                hex += rem;



                input = input / 8;
            }

            return new String(hex.ToCharArray().Reverse().ToArray());
        }
        public static string ToHex(this int input)
        {
            string[] arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string hex = string.Empty;
            while (input != 0)
            {
                var rem = (input % 16) - 1;
                if (rem > 8 && rem <= 14)
                    hex += arr[rem];
                else
                    hex += rem + 1;
                input = input / 16;
            }

            return new String(hex.ToCharArray().Reverse().ToArray());
        }
        public static string ToBin(int n)
        {
            char[] b = new char[32];
            StringBuilder bin = new StringBuilder();
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    bin.Append("1");
                }
                else
                {
                    bin.Append("0");
                }
                i++;
                if (i == 8 || i % 8 == 0)
                    bin.Append(" ");
            }
            return new String(bin.ToString().ToCharArray().Reverse().ToArray());
        }

        public static int ToInt(this string input)
        {
            if (!String.IsNullOrEmpty(input))
                return Convert.ToInt32(input);
            return 0;
        }

    }
}