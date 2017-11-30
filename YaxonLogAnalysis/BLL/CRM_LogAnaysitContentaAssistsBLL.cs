using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YaxonLogAnalysis.DAL;
using System.Data;

namespace YaxonLogAnalysis.BLL
{
    class CRM_LogAnaysitContentaAssistsBLL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="logID"></param>
        public static void Add(int logID)
        {
            CRM_LogAnaysitContentaAssistsDal dal = new CRM_LogAnaysitContentaAssistsDal();
            dal.Add(logID);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        public static void Delete(int ID)
        {
            CRM_LogAnaysitContentaAssistsDal dal = new CRM_LogAnaysitContentaAssistsDal();
            dal.Delete(ID);
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        public static void PLAdd()
        {
            CRM_LogAnaysitContentaAssistsDal dal = new CRM_LogAnaysitContentaAssistsDal();
            dal.PLAdd();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        public static void PLDelete()
        {
            CRM_LogAnaysitContentaAssistsDal dal = new CRM_LogAnaysitContentaAssistsDal();
            dal.PLDelete();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryLogData()
        {
            CRM_LogAnaysitContentaAssistsDal dal = new CRM_LogAnaysitContentaAssistsDal();
            return dal.QueryLogData();
        }
    }
}
