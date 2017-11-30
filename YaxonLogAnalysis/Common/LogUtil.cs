using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YaxonLogAnalysis.Common
{
    class LogUtil
    {
        public static void WriteInfo( string message)
        {
            try
            {
                string path = System.Environment.CurrentDirectory + "\\Log";
                path = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd"));

                string fileName = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string filePath = Path.Combine(path, fileName);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                FileStream fs = new FileStream(filePath, FileMode.Append);

                //直接把内容写进去
                message = string.Format("\r\n\r\n打印时间：{0}\r\n打印内容：{1}", DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"), message);
                byte[] by = System.Text.Encoding.Default.GetBytes(message);
                fs.Write(by, 0, by.Length);
                fs.Dispose();
            }
            catch 
            {
                
          
            }
        }
    }
}
