using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace YaxonLogAnalysis.Common
{
    public class ConstInfo
    {

        public static readonly string msg_valicode = ConfigurationManager.AppSettings["msg_valicode"].ToString();

        public static readonly string msg_url = ConfigurationManager.AppSettings["msg_url"].ToString();

        public static readonly string msg_sms = ConfigurationManager.AppSettings["msg_sms"].ToString();
        
    }
}
