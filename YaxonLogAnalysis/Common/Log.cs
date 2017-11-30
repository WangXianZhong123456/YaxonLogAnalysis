using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YaxonLogAnalysis.Common
{
    public class Log
    {
        public static void WriteLog(string ex)
        {
            var dir = System.Environment.CurrentDirectory;
            var direLog = dir + "/Log/";
            try
            {
                if (!Directory.Exists(direLog)) Directory.CreateDirectory(direLog);
                using (FileStream fs = new FileStream(direLog + @"\" + DateTime.Now.ToString("yyMMdd") + "error.txt"
                    , FileMode.Append
                    , FileAccess.Write
                    , FileShare.Read))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                        sw.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "请求服务：" + ex);
                }
            }
            catch
            {
            }
        }
    }
}
