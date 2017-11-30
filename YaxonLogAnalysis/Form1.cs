using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Configuration;
using YaxonLogAnalysis.Common;
using YaxonLogAnalysis.BLL;
using YaxonLogAnalysis.Model;
using Yaxon.PublicBLL.Public;
using System.Threading;

namespace YaxonLogAnalysis
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
            InitThread();
        }
        delegate void SetListBoxCallback(string str);
        string Pro_Name = System.Configuration.ConfigurationSettings.AppSettings["Pro_Name"];//匹配内容
        int Joinms = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Joinms"]);//间隔时间
        int IsUploadTime = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["IsUploadTime"]);//是否按固定的时间上传 
        int BeginHourTime = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["BeginHourTime"]);//开始执行时间
        int EndHourTime = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["EndHourTime"]);//结束执行时间
        string ContainsStr = System.Configuration.ConfigurationSettings.AppSettings["ContainsStr"];//匹配内容
        private Dictionary<string, Thread> DictThread = new Dictionary<string, Thread>();//线程
        Dictionary<string, string> dic = null; // 文件名,路径
        bool IsUpExceptionData = false, IsFinishUp = false;
        int IsFirstExceptionData = 0;
        #region 私有方法

        /// <summary>
        /// -1线程异常;0不上传数据;1上传数据
        /// </summary>
        private string _SmarkFlag = "-1";

        /// <summary>
        /// 0不上传数据到云端；1上传数据至云端
        /// </summary>
        private string SmarkFlag
        {
            get
            {
                if (this._SmarkFlag == "-1")//如果是异常数据将重新取
                    _SmarkFlag = "1";
                return _SmarkFlag;
            }
        }

        /// <summary>
        /// 线程监控    
        /// </summary>
        private Thread ThreadWatch;


        int IsStart = 0;
        /// <summary>
        /// 初始化线程
        /// </summary>
        private void InitThread()
        {
            #region 初始化线程

            DictThread.Add("ReadLog", new Thread(CenterThread));//读日志

            DictThread.Add("UpLog", new Thread(CenterThread));//上报日志

            DictThread.Add("UpExceptionLogData", new Thread(CenterThread));//上报日志

            #endregion
        }

        /// <summary>
        /// 线程开始
        /// </summary>
        private void InitStart()
        {
            try
            {
                if (SmarkFlag == "1")
                {
                    if (DictThread["ReadLog"].IsAlive == false)
                    {
                        DictThread["ReadLog"] = null;
                        DictThread["ReadLog"] = new Thread(CenterThread);
                        DictThread["ReadLog"].Start(new object[] { "ReadLog" });
                        Thread.Sleep(100);
                    }

                    if (DictThread["UpLog"].IsAlive == false)
                    {
                        DictThread["UpLog"] = null;
                        DictThread["UpLog"] = new Thread(CenterThread);
                        DictThread["UpLog"].Start(new object[] { "UpLog" });
                        Thread.Sleep(100);
                    }
                    if (DictThread["UpExceptionLogData"].IsAlive == false)
                    {
                        DictThread["UpExceptionLogData"] = null;
                        DictThread["UpExceptionLogData"] = new Thread(CenterThread);
                        DictThread["UpExceptionLogData"].Start(new object[] { "UpExceptionLogData" });
                        Thread.Sleep(100);
                    }
                }
                else if (SmarkFlag == "0")
                {
                    LogUtil.WriteInfo("系统设置成不上传进出数据");
                    InsertListBox(string.Format("{0} 系统设置成不上传进出数据", DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException.GetBaseException();
                }
                LogUtil.WriteInfo("InitStart异常：" + ex.Message);
                InsertListBox(string.Format("{0} InitStart异常：{1}", DateTime.Now.ToString(), ex.Message));
                this._SmarkFlag = "-1";
            }
        }
        /// <summary>
        /// 线程结束
        /// </summary>
        private void InitStop()
        {
            if (this._SmarkFlag == "-1")
            {
                if (DictThread["ReadLog"].IsAlive)
                {
                    DictThread["ReadLog"].Abort();
                }
                if (DictThread["UpLog"].IsAlive)
                {
                    DictThread["UpLog"].Abort();
                }
                if (DictThread["UpExceptionLogData"].IsAlive)
                {
                    DictThread["UpExceptionLogData"].Abort();
                }
            }
        }

        #endregion

        #region 线程中心
        /// <summary>
        /// 智能线程监控
        /// </summary>
        private void SmartWatch()
        {
            do
            {
                try
                {
                    if (this._SmarkFlag == "-1")
                    {
                        LogUtil.WriteInfo(string.Format("SmartWatch:开始停止线程 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                        InitStop();
                        this.ThreadWatch.Join(5000);//停止5秒等待线程完全结束
                        LogUtil.WriteInfo(string.Format("CloudWatch:开始重启线程 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                        InitStart();
                        LogUtil.WriteInfo(string.Format("CloudWatch:重启线程完成 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                    }

                }
                catch (ThreadAbortException ex)
                {
                    LogUtil.WriteInfo("SmartWatch(智能线程监控)：" + ex.Message);
                    break;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteInfo("SmartWatch(智能线程监控)关掉：" + ex.Message);
                    continue;
                }
                this.ThreadWatch.Join(5000);
            }
            while (ThreadWatch.IsAlive);
        }

        #endregion 线程中心


        public void CenterThread(object obj)
        {
            string logMg = "";
            object[] objs = (object[])obj;
            string threadName = (string)objs[0];
            Thread tempThread = this.DictThread[threadName];

            if (tempThread == null)
            {
                InsertListBox(string.Format("上报线程threadName【{0}】启动失败，请检查！", threadName));
                LogUtil.WriteInfo(string.Format("上报线程threadName【{0}】启动失败，请检查！", threadName));
                return;
            }
            else
            {
                InsertListBox(string.Format("上报线程threadName【{0}】启动成功！", threadName));
                LogUtil.WriteInfo(string.Format("上报线程threadName【{0}】启动成功！", threadName));
            }

            do
            {
                try
                {
                    switch (threadName)
                    {
                        case "ReadLog": // 读日志

                            ReadLog();

                            break;
                        //case "UpLog": // 上报日志

                        //    UpLog();

                        //    break;
                        //case "UpExceptionLogData":
                        //    UpExceptionLogData();
                        //    break;
                        default:
                            break;
                    }
                }
                catch (ThreadAbortException ex)
                {
                    logMg = string.Format("线程{0}：{1}", threadName, ex.Message);
                    LogUtil.WriteInfo(logMg);
                    InsertListBox(logMg);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException.GetBaseException();
                    }
                    logMg = string.Format("线程{0}执行失败  {1} {2}", threadName, ex.Message, ex.StackTrace);
                    LogUtil.WriteInfo(logMg);
                    InsertListBox(string.Format("{0} {1}", DateTime.Now.ToString(), logMg));
                    tempThread.Join(1000);
                    continue;
                }

            } while (true);

        }



        #region 解析日志上报

        /// <summary>
        /// 解析日志上报
        /// </summary>
        public void ReadLog()
        {
            if (IsUpExceptionData)
            {
                return;
            }
            string fileName = "";
            try
            {
                if (dic == null)// 等于null时，走原始流程。
                {
                    GC.Collect();// 垃圾回收，避免内存不足。
                    string LogPath = System.Configuration.ConfigurationSettings.AppSettings["LogPath"]; // 文件路径
                    string LastFileName = ConfigurationManager.AppSettings["LastFileName"];// System.Configuration.ConfigurationSettings.AppSettings["LastFileName"];
                    int currHour = Convert.ToInt32(DateTime.Now.Hour);
                    if (IsUploadTime == 1)
                    {
                        if (currHour < BeginHourTime || currHour >= EndHourTime) // 执行固定的时间
                        {
                            DictThread["ReadLog"].Join(600);
                            return;
                        }
                    }
                    if (Convert.ToInt64(Convert.ToDateTime(LastFileName).ToString("yyyyMMddHH")) >= Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHH")))// 执行比当前时间小的文件
                    {
                        DictThread["ReadLog"].Join(600);
                        return;
                    }
                    string LastPath = LastFileName.Replace("-", "").Substring(0, 8);// 上次的目录
                    string LastFileNameInfo = "CRM" + LastFileName.Replace("-", "").Replace(" ", "").Substring(0, 10) + ".log";// 上次的日志
                    string filePathSub = Path.Combine(LastPath, LastFileNameInfo);
                    string filePath = Path.Combine(LogPath, filePathSub);

                    AnysisLog(LastFileNameInfo, filePath, Pro_Name);

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    //修改配置文件  
                    config.AppSettings.Settings["LastFileName"].Value = Convert.ToDateTime(LastFileName).AddHours(1).ToString("yyyy-MM-dd HH:00:00");
                    //保存  
                    config.Save(ConfigurationSaveMode.Modified);
                    // 强制重新载入配置文件的ConnectionStrings配置节
                    ConfigurationManager.RefreshSection("appSettings");
                }
                else
                {
                    if (dic.Count == 0)
                    {
                        return;
                    }
                    //放入内存流，以便逐行读取 
                    foreach (var item in dic)
                    {
                        GC.Collect();// 垃圾回收，避免内存不足。
                        fileName = item.Key;
                        AnysisLog(item.Key, item.Value, Pro_Name);
                    }
                    dic.Clear();
                }


            }
            catch (Exception ex)
            {
                InsertListBox(string.Format("文件【{0}】执行异常：{1}", fileName, ex.Message));
                LogUtil.WriteInfo("文件【" + fileName + "】执行异常：" + ex.Message);

            }
        }


        /// <summary>
        /// 解析日志并上报数据库公共方法
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="Pro_Name"></param>
        /// <param name="td"></param>
        public void AnysisLog(string fileName, string filePath, string Pro_Name)
        {
            bool IsReading = false, IsFirst = true, IsMatch = false;
            string specialInfo = string.Empty, LogPrint = string.Empty;

            FileStream fs = null;
            //MemoryStream ms = new MemoryStream(File.ReadAllBytes(filePath));
            StreamReader sr = null;
            try
            {

                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                #region 读流

                while (sr.Peek() > -1)
                {
                    //GC.Collect();// 垃圾回收，避免内存不足。
                    string info = sr.ReadLine();

                    if (info.Replace(" ", "").Length == 2 && info.Replace(" ", "") == "}]" && IsReading)
                    {

                        specialInfo += info.Replace("\\n", "");
                        specialInfo = "[{ LogPrint:\"" + LogPrint + "\"," + specialInfo;
                        IsReading = false; IsFirst = true;
                        List<UpDefinedVisit> upDefinedVisit = JsonHelper.DeserializeJsonToList<UpDefinedVisit>(specialInfo);
                        if (!upDefinedVisit[0].inPos.bd.p.ToString().Contains(ContainsStr) 
                            && !upDefinedVisit[0].outPos.bd.p.ToString().Contains(ContainsStr))
                        {
                            continue;
                        }
                        string parms1 = CRM_LogAnaysitContentBLL.Add(fileName, specialInfo, 1, upDefinedVisit, Pro_Name, 0); // 苹果数据
                        string parms2 = CRM_LogAnaysitContentBLL.AddType2(fileName, specialInfo, 1, upDefinedVisit, Pro_Name, 0, 0); // 苹果数据
                        DataSet ds = CRM_LogAnaysitContentBLL.UpLogRunpro("P_LogAnaysitTool_DefinedVisit", parms1);
                        if (ds.Tables[0].Rows[0]["ack"].ToString() == "1")
                        {
                            int visitID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                            for (int i = 0; i < upDefinedVisit[0].defined.Count; i++)
                            {
                                if (upDefinedVisit[0].defined[i].type == "Photo")
                                {
                                    Photo photo = JsonHelper.DeserializeJsonToObject<Photo>(upDefinedVisit[0].defined[i].value);
                                    if (photo.photoId == "")
                                    { continue; }
                                }


                                string paramsstr = string.Format("@VisitID={0},@FieldID={1},@FieldValue='{2}'",
                                   visitID, upDefinedVisit[0].defined[i].markId, upDefinedVisit[0].defined[i].value);


                                CRM_LogAnaysitContentBLL.UpLogRunpro("P_LogAnaysitTool_CusShopVisitItemInfo", paramsstr);
                            }

                        }
                        else
                        {
                            LogUtil.WriteInfo("上传失败：" + parms1);
                        }

                        specialInfo = string.Empty;
                        LogPrint = string.Empty;
                    }

                    if (IsReading)
                    { specialInfo += info.Replace("\\n", ""); }

                    if (info.Contains(Pro_Name) && info.Length <= 50 && IsFirst)
                    {

                        string json = CombineInsideSpaces(info.Substring(22));
                        string personID = info.Substring(23).Split('_')[0];
                        if (personID == "0")
                        {
                            LogPrint = info.Substring(0, 19);
                            IsReading = true; IsFirst = false; IsMatch = true;
                        }
                        else
                        {
                            specialInfo = string.Empty;
                            LogPrint = string.Empty;
                            IsReading = false; IsFirst = true;
                            continue;
                        }
                    }
                    //do something
                }

                #endregion

                if (!IsMatch)
                {
                    InsertListBox(string.Format("文件【{0}】没有匹配的数据！", fileName));
                    LogUtil.WriteInfo("文件【" + fileName + "】没有匹配的数据！");
                }
                else
                {
                    InsertListBox(string.Format("文件【{0}】已执行完毕（读日志）！", fileName));
                    LogUtil.WriteInfo("文件【" + fileName + "】已执行完毕（读日志）！");
                }

            }
            //catch (OutOfMemoryException ex)
            //{
            //    InsertListBox(string.Format("文件【{0}】内存溢出，以文件流的形式重新调用【AnysisLog】", filePath));
            //    LogUtil.WriteInfo(string.Format("文件【{0}】内存溢出，以文件流的形式重新调用【AnysisLog】", filePath));
            //    AnysisLog(fileName, filePath, Pro_Name, 0);
            //    return;

            //}
            catch (FileNotFoundException ex) { }
            catch (DirectoryNotFoundException ex) { }
            catch (Exception ex)
            {
                InsertListBox(string.Format("文件【{0}】,调用【AnysisLog】异常：{1})", filePath, ex.Message));
                LogUtil.WriteInfo(string.Format("文件【{0}】,调用【AnysisLog】异常：{1})", filePath, ex.Message));
            }
            finally
            {
                //if (ms != null)
                //{
                //    ms.Close();
                //    ms.Dispose();
                //}
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        public void UpLog()
        {
            if (IsUpExceptionData)
            {
                return;
            }
            DataTable dt = CRM_LogAnaysitContentBLL.GetLogInfoByIsUpload(0);//未上传的数据

            if (dt != null && dt.Rows.Count > 0)
            {
                int Id = Convert.ToInt32(dt.Rows[0][0].ToString());
                string fileName = dt.Rows[0][1].ToString();
                string contents = dt.Rows[0][2].ToString();
                string paramss = dt.Rows[0][3].ToString();
                DataSet ds = CRM_LogAnaysitContentBLL.UpLogRunpro("YL_VehicleVisit8_M", paramss);
                if (ds != null && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)// 正常（没报异常）
                {

                    CRM_LogAnaysitContentBLL.UpdateLog(Id, 1); // 数据（0:未上传，1：上传成功，-1上传失败）
                    InsertListBox(string.Format("文件【{0}】数据上报成功：{1}", fileName, contents));
                    LogUtil.WriteInfo(string.Format("文件【{0}】数据上报成功：{1}", fileName, contents));
                    DictThread["UpLog"].Join(Joinms);

                }
                else
                {
                    CRM_LogAnaysitContentBLL.UpdateLog(Id, -1); // 数据（0:未上传，1：上传成功，-1上传失败）
                    InsertListBox(string.Format("文件【{0}】数据上报失败：{1}", fileName, contents));
                    LogUtil.WriteInfo(string.Format("文件【{0}】数据上报失败：{1}", fileName, contents));
                    DictThread["UpLog"].Join(Joinms);
                }
            }

        }

        private void SelectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dic = new Dictionary<string, string>();
                //初始化一个OpenFileDialog类
                OpenFileDialog fileDialog = new OpenFileDialog();

                //判断用户是否正确的选择了文件
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获取用户选择文件的后缀名
                    string extension = Path.GetExtension(fileDialog.FileName);
                    //声明允许的后缀名
                    string[] str = new string[] { ".log", ".txt" };
                    if (!((IList)str).Contains(extension))
                    {
                        MessageBox.Show("仅能上传log,txt格式的文件！");
                        InsertListBox("仅能上传log,txt格式的文件！");
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                        if (fileInfo != null)
                        {
                            InsertListBox(string.Format("当前可执行文件：{0}", fileInfo.Name));
                            dic.Add(fileInfo.Name, fileInfo.FullName);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                InsertListBox("选择文件异常：" + ex.Message);
                LogUtil.WriteInfo("选择文件异常：" + ex.Message);
            }

        }

        private void selectDireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dic = new Dictionary<string, string>();
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = false;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo[] dataFiles = new DirectoryInfo(folderBrowserDialog.SelectedPath).GetFiles();
                    foreach (FileInfo file in dataFiles)
                    {
                        if (file.Name.Contains(".log") || file.Name.Contains(".txt"))
                        {
                            dic.Add(file.Name, file.FullName);
                        }
                    }
                    if (dic.Count == 0)
                    {
                        MessageBox.Show("仅能上传log,txt格式的文件！");
                        InsertListBox("仅能上传log,txt格式的文件！");
                    }
                    else
                    {
                        foreach (var item in dic)
                        {
                            InsertListBox(string.Format("当前目录包含可执行文件：{0}", item.Key));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                InsertListBox("选择目录异常：" + ex.Message);
                LogUtil.WriteInfo("选择目录异常：" + ex.Message);
            }
        }
        #endregion



        /// <summary>
        /// 保留一个空格
        /// </summary>
        /// <param name="orinStr">内容</param>
        /// <returns></returns>
        public static string CombineInsideSpaces(string orinStr)
        {
            List<int> poses = new List<int>();
            for (int i = 0; i <= orinStr.Length - 1; i++)
            {//获取所有空格位置
                if (orinStr[i] == ' ')
                {
                    poses.Add(i);
                }
            }
            for (int i = poses.Count - 1; i > 0; i--)
            {//遍历每个空格位置,检查前位 是否未空格
                var cur = poses[i];
                var prev = poses[i - 1];
                if (prev == cur - 1)
                {
                    orinStr = orinStr.Remove(cur, 1);
                }
            }
            return orinStr;
        }
        /// <summary>
        /// 插入listbox
        /// </summary>
        /// <param name="info">内容</param>
        public void InsertListBox(string info)
        {
            try
            {
                if (listBox1.InvokeRequired)  //控件是否跨线程？如果是，则执行括号里代码          
                {
                    if (this.IsDisposed == false || this.IsHandleCreated)
                    {
                        SetListBoxCallback setListCallback = new SetListBoxCallback(InsertListBox);   //实例化委托对象          
                        listBox1.Invoke(setListCallback, info);   //重新调用SetListBox函数          
                    }
                }
                else  //否则，即是本线程的控件，控件直接操作            
                {
                    if (listBox1.Items.Count > 100)
                        listBox1.Items.Clear();
                    listBox1.Items.Insert(0, string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), info));
                }
            }
            catch
            {
                ;
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ThreadWatch != null && this.ThreadWatch.IsAlive)
            {
                this.ThreadWatch.Abort();
                this.ThreadWatch = null;
            }
            this._SmarkFlag = "-1";
            LogUtil.WriteInfo("停止发送");
            InsertListBox("停止发送");
            System.Environment.Exit(0);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ThreadWatch != null && this.ThreadWatch.IsAlive)
            {
                this.ThreadWatch.Abort();
                this.ThreadWatch = null;
            }
            this._SmarkFlag = "-1";
            LogUtil.WriteInfo("停止发送");
            InsertListBox("停止发送");

            System.Environment.Exit(0);
        }

        private void BeginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogUtil.WriteInfo("启动...");
            InsertListBox("启动...");
            this._SmarkFlag = "-1";
            ThreadWatch = new Thread(new ThreadStart(SmartWatch));
            ThreadWatch.Start();
            this.BeginToolStripMenuItem.Enabled = false;
            //  this.JsonToolStripMenuItem.Enabled = false;
            this.selectDireToolStripMenuItem.Enabled = false;
            this.SelectFileToolStripMenuItem.Enabled = false;
            this.ExceptionToolStripMenuItem.Enabled = false;
            this.EndToolStripMenuItem.Enabled = true;

        }

        private void EndToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this._SmarkFlag = "-1";
            InitStop();
            if (this.ThreadWatch != null && this.ThreadWatch.IsAlive)
            {
                this.ThreadWatch.Abort();
                this.ThreadWatch = null;
            }
            this._SmarkFlag = "0";
            this.dic = null;
            this.IsUpExceptionData = false;
            this.IsFinishUp = false;
            this.BeginToolStripMenuItem.Enabled = true;
            this.EndToolStripMenuItem.Enabled = true;
            // this.JsonToolStripMenuItem.Enabled = true;
            this.selectDireToolStripMenuItem.Enabled = true;
            this.SelectFileToolStripMenuItem.Enabled = true;
            this.ExceptionToolStripMenuItem.Enabled = true;
            this.EndToolStripMenuItem.Enabled = false;
            LogUtil.WriteInfo("停止发送");
            InsertListBox("停止发送");

        }

        private void JsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonForm jf = new JsonForm();
            jf.Show();

        }

        public void UpExceptionLogData()
        {
            if (!IsUpExceptionData || IsFinishUp)
            {
                return;
            }
            try
            {
                if (IsFirstExceptionData == 0)
                {
                    CRM_LogAnaysitContentaAssistsBLL.PLDelete();
                    CRM_LogAnaysitContentaAssistsBLL.PLAdd();
                    IsFirstExceptionData = 1;
                }

                DataTable dt = CRM_LogAnaysitContentaAssistsBLL.QueryLogData();
                if (dt != null && dt.Rows.Count > 0)
                {
                    int logAssistID = Convert.ToInt32(dt.Rows[0][0]);
                    int logID = Convert.ToInt32(dt.Rows[0][1]);
                    string fileName = dt.Rows[0][2].ToString();
                    string contents = dt.Rows[0][3].ToString();
                    string paramss = dt.Rows[0][4].ToString();
                    DataSet ds = CRM_LogAnaysitContentBLL.UpLogRunpro("YL_VehicleVisit8_M", paramss);
                    if (ds != null && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)// 正常（没报异常）
                    {

                        CRM_LogAnaysitContentBLL.UpdateLog(logID, 1); // 数据（0:未上传，1：上传成功，-1上传失败）
                        CRM_LogAnaysitContentaAssistsBLL.Delete(logAssistID);
                        InsertListBox(string.Format("文件【{0}】处理异常数据上报成功：{1}", fileName, contents));
                        LogUtil.WriteInfo(string.Format("文件【{0}】处理异常数据上报成功：{1}", fileName, contents));
                        DictThread["UpExceptionLogData"].Join(Joinms);

                    }
                    else
                    {
                        CRM_LogAnaysitContentaAssistsBLL.Delete(logAssistID);
                        InsertListBox(string.Format("文件【{0}】处理异常数据上报失败：{1}", fileName, contents));
                        LogUtil.WriteInfo(string.Format("文件【{0}】处理异常数据上报失败：{1}", fileName, contents));
                        DictThread["UpExceptionLogData"].Join(Joinms);
                    }
                }
                else
                {
                    IsFinishUp = true;
                    InsertListBox("处理异常数据已执行完毕！");
                    LogUtil.WriteInfo("处理异常数据已执行完毕！");
                }

            }
            catch (Exception ex)
            {
                IsFinishUp = true;
                InsertListBox("处理异常数据异常：" + ex.Message);
                LogUtil.WriteInfo("处理异常数据异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 处理异常数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExceptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            this.IsUpExceptionData = true;
            this.IsFinishUp = false;
            this.IsFirstExceptionData = 0;
            LogUtil.WriteInfo("启动异常处理数据...");
            InsertListBox("启动异常处理数据...");
            this._SmarkFlag = "-1";
            ThreadWatch = new Thread(new ThreadStart(SmartWatch));
            ThreadWatch.Start();
            this.BeginToolStripMenuItem.Enabled = false;
            //  this.JsonToolStripMenuItem.Enabled = false;
            this.selectDireToolStripMenuItem.Enabled = false;
            this.SelectFileToolStripMenuItem.Enabled = false;
            this.ExceptionToolStripMenuItem.Enabled = false;
            this.EndToolStripMenuItem.Enabled = true;
        }

    }
}
