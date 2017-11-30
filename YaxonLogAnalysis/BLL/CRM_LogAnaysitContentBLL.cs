using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YaxonLogAnalysis.DAL;
using System.Data;
using YaxonLogAnalysis.Model;

namespace YaxonLogAnalysis.BLL
{
    class CRM_LogAnaysitContentBLL
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static string Add(string fileName, string contents, int flag, List<UpDefinedVisit> obj, string Pro_Name, int IsUpload)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.Add(fileName, contents, flag, obj, Pro_Name, IsUpload);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static string AddType2(string fileName, string contents, int flag, List<UpDefinedVisit> obj, string Pro_Name, int IsUpload, int visitID)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.AddType2(fileName, contents, flag, obj, Pro_Name, IsUpload, visitID);
        }
        /// <summary>
        /// 通过内容获取日志信息
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static DataTable GetLogInfoByContents(string contents)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.GetLogInfoByContents(contents);
        }
        /// <summary>
        /// 获取还没有上传的日志
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static DataTable GetLogInfoByIsUpload(int IsUpload)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.GetLogInfoByIsUpload(IsUpload);
        }

        /// <summary>
        /// 通过ID修改log的上传标志
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="IsUpload">0:未上传，1：上传成功，-1上传失败</param>
        /// <returns></returns>
        public static void UpdateLog(int Id, int IsUpload)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            dal.UpdateLog(Id, IsUpload);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="proname">名称</param>
        /// <param name="paramss">参数</param>
        /// <returns></returns>
        public static DataSet UpLogRunpro(string proname, string paramss)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.UpLogRunpro(proname, paramss);

        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="proName">过程名</param>
        /// <param name="paramss">参数</param>
        /// <returns></returns>
        public static DataSet RunProcedure(string proName, List<object> paramss)
        {
            CRM_LogAnaysitContentDal dal = new CRM_LogAnaysitContentDal();
            return dal.RunProcedure(proName, paramss);
        }
    }
}
