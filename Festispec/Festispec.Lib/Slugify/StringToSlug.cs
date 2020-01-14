using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Festispec.Lib.Slugify
{
    public static class StringToSlug
    {
        public static string Slugify(string text)
        {
            Random random = new Random();
            var str = text.RemoveAccent().ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");
            str += random.Next(100, 685876543).ToString();
            str += random.Next(25, 99).ToString();
            return str;
        }

        private static string RemoveAccent(this string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
