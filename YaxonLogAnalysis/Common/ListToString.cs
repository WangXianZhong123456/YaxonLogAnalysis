using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YaxonLogAnalysis.Common
{
    class ListToString
    {
        public static string IntListToString(List<int> _lst)
        {
            string str = "";
            foreach (int n in _lst)
            {
                str += n.ToString() + "｜";
            }
            return (str == "") ? "" : str.Remove(str.Length - 1, 1).Replace("'", "''");
        }

        public static string StringListToString(List<string> _lst)
        {
            string str = "";
            foreach (string s in _lst)
            {
                str += s.Replace("|", "") + "｜";
            }
            return (str == "") ? "" : str.Remove(str.Length - 1, 1).Replace("'", "''");
        }

        public static string ObjectListToString<T>(List<T> _lst) where T : class
        {
            string str = "";
            foreach (T t in _lst)
            {
                str += t.ToString().Replace("|", "") + "｜";
            }
            return (str == "") ? "" : str.Remove(str.Length - 1, 1).Replace("'", "''");
        }
    }
}
