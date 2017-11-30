using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Configuration;
using System.Net.Configuration;

namespace YaxonLogAnalysis.Common
{
    public class MsgUtil
    {

        static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        static MailSettingsSectionGroup mailSettings = NetSectionGroup.GetSectionGroup(config).MailSettings;

        /// <summary>
        /// 短信服务
        /// </summary>
        /// <param name="mobileNo">号码</param>
        /// <param name="msg">发送内容</param>
        /// <returns></returns>
        public static void SendMsg(string mobileNo, string msg)
        {
            string srcString = "";
            try
            {
                int ts = DateTime.Now.Millisecond;

                string postString = String.Format("valicode={0}&mobile={1}&ts={2}&sms={3}", ConstInfo.msg_valicode, mobileNo, ts.ToString(), msg);
                byte[] postData = Encoding.GetEncoding("GBK").GetBytes(postString);

                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = webClient.UploadData(ConstInfo.msg_url, "POST", postData);
                srcString = Encoding.UTF8.GetString(responseData);
            }
            catch (Exception ex)
            {
                Log.WriteLog("====================>短信服务 Exception:" + ex.ToString() + "<====================");
            }
        }
        
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="email_to">邮件接受者</param>
        public static void SendMail(string subject, string body, string email_to)
        {
            try
            {
                MailMessage mm = new MailMessage(mailSettings.Smtp.From, email_to, subject, body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                SmtpClient stmp = new SmtpClient();
                stmp.UseDefaultCredentials = true;
                stmp.Credentials = new System.Net.NetworkCredential(mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password);
                stmp.Send(mm);
            }
            catch(Exception ex)
            {
                Log.WriteLog("====================>发送邮件 Exception:" + ex.ToString() + "<====================");
            }
        }

    }
}
