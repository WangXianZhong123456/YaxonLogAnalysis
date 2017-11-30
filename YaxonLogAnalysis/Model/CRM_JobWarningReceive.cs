using System;
using System.Collections.Generic;
using System.Text;

namespace YaxonLogAnalysis.Model
{
    //CRM_JobWarningReceive
    public class CRM_JobWarningReceive
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// UserName
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// Telphone
        /// </summary>		
        private string _telphone;
        public string Telphone
        {
            get { return _telphone; }
            set { _telphone = value; }
        }
        /// <summary>
        /// Email
        /// </summary>		
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
