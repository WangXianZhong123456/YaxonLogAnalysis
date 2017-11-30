using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YaxonLogAnalysis.Common;
using System.Data;

namespace YaxonLogAnalysis.DAL
{
    class CRM_LogAnaysitContentaAssistsDal
    {
        public void Add(int logID)
        {
            try
            {
                string strSql = "insert into LogAnaysitContentAssisat(LogID) VALUES(" + logID + ")";

                DbHelperSQL.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("插入【LogAnaysitContentAssisat】表失败：" + ex.Message);
            }

        }

        public void Delete(int ID)
        {
            try
            {
                string strSql = "delete from LogAnaysitContentAssisat where ID=" + ID;

                DbHelperSQL.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("删除【LogAnaysitContentAssisat】表失败：" + ex.Message);
            }
        }

        public void PLAdd()
        {
            try
            {
                string strSql = "insert into LogAnaysitContentAssisat(LogID) select ID from LogAnaysitContent where isupload=-1 order by fileName";
                DbHelperSQL.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("批量插入【LogAnaysitContentAssisat】表失败：" + ex.Message);
            }
        }

        public void PLDelete()
        {
            try
            {
                string strSql = "TRUNCATE TABLE LogAnaysitContentAssisat";

                DbHelperSQL.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("批量删除【LogAnaysitContentAssisat】表失败：" + ex.Message);
            }
        }

        public DataTable QueryLogData()
        {
            DataTable dt = null;
            try
            {
                string sql = "SELECT TOP 1 a.id,b.id,b.fileName,b.contents,b.params FROM LogAnaysitContentAssisat a INNER JOIN LogAnaysitContent b ON a.logId = b.id ";
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
                LogUtil.WriteInfo("查询数据【QueryLogData】表失败：" + ex.Message);
            }
            return dt;
        }
    }
}
