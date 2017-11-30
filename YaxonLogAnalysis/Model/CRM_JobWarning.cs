using System;
using System.Collections.Generic;
using System.Text;

namespace YaxonLogAnalysis.Model
{
    //CRM服务器作业预警
    public class CRM_JobWarning
    {

        /// <summary>
        /// ID号
        /// </summary>		
        private Guid _idno;
        public Guid IDNO
        {
            get { return _idno; }
            set { _idno = value; }
        }
        /// <summary>
        /// 日期
        /// </summary>		
        private DateTime _datevalue;
        public DateTime DateValue
        {
            get { return _datevalue; }
            set { _datevalue = value; }
        }
        /// <summary>
        /// 链接服务器
        /// </summary>		
        private string _linkserver;
        public string LinkServer
        {
            get { return _linkserver; }
            set { _linkserver = value; }
        }
        /// <summary>
        /// 服务器名称
        /// </summary>		
        private string _hostname;
        public string HostName
        {
            get { return _hostname; }
            set { _hostname = value; }
        }
        /// <summary>
        /// 服务器IP
        /// </summary>		
        private string _hostip;
        public string HostIP
        {
            get { return _hostip; }
            set { _hostip = value; }
        }
        /// <summary>
        /// 作业ID
        /// </summary>		
        private string _jobid;
        public string JobID
        {
            get { return _jobid; }
            set { _jobid = value; }
        }
        /// <summary>
        /// 作业名称
        /// </summary>		
        private string _jobname;
        public string JobName
        {
            get { return _jobname; }
            set { _jobname = value; }
        }
        /// <summary>
        /// 作业开始时间
        /// </summary>		
        private DateTime _jobstarttime;
        public DateTime JobStartTime
        {
            get { return _jobstarttime; }
            set { _jobstarttime = value; }
        }
        /// <summary>
        /// 作业执行时间
        /// </summary>		
        private long _jobexetime;
        public long JobExeTime
        {
            get { return _jobexetime; }
            set { _jobexetime = value; }
        }
        /// <summary>
        /// 作业平均时间
        /// </summary>		
        private long _jobavgruntime;
        public long JobAvgRuntime
        {
            get { return _jobavgruntime; }
            set { _jobavgruntime = value; }
        }
        /// <summary>
        /// 0:未读取 1已读取
        /// </summary>		
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

    }
}
