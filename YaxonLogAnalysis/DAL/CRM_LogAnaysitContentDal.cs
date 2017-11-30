using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YaxonLogAnalysis.Common;
using YaxonLogAnalysis.Model;

namespace YaxonLogAnalysis.DAL
{
    class CRM_LogAnaysitContentDal
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public string Add(string fileName, string contents, int flag, List<UpDefinedVisit> obj, string Pro_Name, int IsUpload)
        {

            float InLon = ((uint)obj[0].inPos.bd.x) / (float)(3600 * 1000);
            float InLat = ((uint)obj[0].inPos.bd.y) / (float)(3600 * 1000);
            float OutLon = ((uint)obj[0].outPos.bd.x) / (float)(3600 * 1000);
            float OutLat = ((uint)obj[0].outPos.bd.y) / (float)(3600 * 1000);
            string paramsstr = "";
            try
            {
                paramsstr = string.Format("@TemplateID={0},@RelatedVisitID={1},@IsRouteVisit={2},@ShopID={3}," +
                                           "@Date='{4}',@InTime='{5}',@InGpsState={6},@InLon={7}," +
                                           "@InLat={8},@OutTime='{9}',@OutGpsState={10}," +
                                           "@OutLon={11}, @OutLat={12},@IsPass={13},@PassReason='{14}'," +
                                           "@Position='{15}'",
                                            obj[0].schemeId, obj[0].relatedVisitId, obj[0].isRouteVisit, obj[0].shopId,
                                            obj[0].date, obj[0].inTime, 1, InLon,
                                            InLat, obj[0].outTime, 1,
                                            OutLon, OutLat, obj[0].isPass, obj[0].passReason,
                                            obj[0].outPos.bd.p);


                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LogAnaysitContent(");
                strSql.Append("FileName,Contents,Flag,Params,IsUpload,CreateTime,ModifyTime,LogPrint,Type");
                strSql.Append(") values (");
                strSql.Append("@FileName,@contents,@flag,@Params,@IsUpload,@createTime,@ModifyTime,@LogPrint,@Type");
                strSql.Append(") ");

                SqlParameter[] parameters = {
                        //new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar) ,     
                        new SqlParameter("@contents", SqlDbType.VarChar) ,            
                        new SqlParameter("@flag", SqlDbType.Int) , 
                        new SqlParameter("@Params", SqlDbType.VarChar) ,   
                        new SqlParameter("@IsUpload", SqlDbType.Int) ,   
                        new SqlParameter("@createTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyTime", SqlDbType.DateTime)  ,
                        new SqlParameter("@LogPrint", SqlDbType.VarChar)  ,
                        new SqlParameter("@Type", SqlDbType.Int)  
              
            };
                parameters[0].Value = fileName;
                parameters[1].Value = contents;
                parameters[2].Value = flag;
                parameters[3].Value = paramsstr;
                parameters[4].Value = IsUpload;
                parameters[5].Value = DateTime.Now;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = obj[0].LogPrint;
                parameters[8].Value = 0;
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("插入【LogAnaysitContent】表失败：" + ex.Message);
            }
            return paramsstr;

        }
        public class Photo
        {
            public string photoId;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public string AddType2(string fileName, string contents, int flag, List<UpDefinedVisit> obj, string Pro_Name, int IsUpload, int visitID)
        {

            string paramsstr = "";
            try
            {
                for (int i = 0; i < obj[0].defined.Count; i++)
                {
                    if (obj[0].defined[i].type == "Photo")
                    {
                        Photo photo = JsonHelper.DeserializeJsonToObject<Photo>(obj[0].defined[i].value);
                        if (photo.photoId == "")
                        { continue; }
                    }

                    paramsstr = string.Format("@VisitID={0},@FieldID={1},@FieldValue='{2}'",
                                visitID, obj[0].defined[i].markId, obj[0].defined[i].value);


                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into LogAnaysitContent(");
                    strSql.Append("FileName,Contents,Flag,Params,IsUpload,CreateTime,ModifyTime,LogPrint,Type");
                    strSql.Append(") values (");
                    strSql.Append("@FileName,@contents,@flag,@Params,@IsUpload,@createTime,@ModifyTime,@LogPrint,@Type");
                    strSql.Append(") ");

                    SqlParameter[] parameters = {
                        //new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar) ,     
                        new SqlParameter("@contents", SqlDbType.VarChar) ,            
                        new SqlParameter("@flag", SqlDbType.Int) , 
                        new SqlParameter("@Params", SqlDbType.VarChar) ,   
                        new SqlParameter("@IsUpload", SqlDbType.Int) ,   
                        new SqlParameter("@createTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyTime", SqlDbType.DateTime)  ,
                        new SqlParameter("@LogPrint", SqlDbType.VarChar)  ,
                        new SqlParameter("@Type", SqlDbType.Int)};

                    parameters[0].Value = fileName;
                    parameters[1].Value = contents;
                    parameters[2].Value = flag;
                    parameters[3].Value = paramsstr;
                    parameters[4].Value = IsUpload;
                    parameters[5].Value = DateTime.Now;
                    parameters[6].Value = DateTime.Now;
                    parameters[7].Value = obj[0].LogPrint;
                    parameters[8].Value = 1;
                    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }

            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("插入【LogAnaysitContent】表失败：" + ex.Message);
            }
            return paramsstr;

        }
        /// <summary>
        /// 通过内容获取日志信息
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public DataTable GetLogInfoByContents(string contents)
        {
            DataTable dt = null;
            try
            {
                dt = DbHelperSQL.Query(string.Format("SELECT ID,IsUpload FROM dbo.LogAnaysitContent WHERE Contents='{0}' order by createtime", contents)).Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
                LogUtil.WriteInfo("查询【GetLogInfoByContents】表失败：" + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 获取还没有上传的日志
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public DataTable GetLogInfoByIsUpload(int IsUpload)
        {
            DataTable dt = null;
            try
            {
                dt = DbHelperSQL.Query(string.Format("SELECT top 1 ID,FileName,Contents,Params FROM dbo.LogAnaysitContent WHERE IsUpload={0}", IsUpload)).Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
                LogUtil.WriteInfo("查询【GetLogInfoByIsUpload】表失败：" + ex.Message);
            }
            return dt;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="proname">名称</param>
        /// <param name="paramss">参数</param>
        /// <returns></returns>
        public DataSet UpLogRunpro(string proname, string paramss)
        {
            DataSet ds = null;
            try
            {
                ds = DbHelperSQL.Query(string.Format("EXEC {0} {1}", proname, paramss));
            }
            catch (Exception ex)
            {
                ds = null;
                LogUtil.WriteInfo("查询【UpLogRunpro】表失败：" + ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// 通过ID修改log的上传标志
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="IsUpload">0:未上传，1：上传成功，-1上传失败</param>
        /// <returns></returns>
        public void UpdateLog(int Id, int IsUpload)
        {
            try
            {
                DbHelperSQL.ExecuteSql(string.Format("UPDATE LogAnaysitContent SET IsUpload={0},ModifyTime=getdate()  WHERE ID={1}", IsUpload, Id));
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo("修改【UpdateLog】表失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public DataSet RunProcedure(string proName, List<object> paramss)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parameters = {
                        new SqlParameter("@PersonID", SqlDbType.Int) ,            
                        new SqlParameter("@ShopID", SqlDbType.Int) ,            
                        new SqlParameter("@GetTime", SqlDbType.DateTime),
                        new SqlParameter("@GPSState", SqlDbType.Int) ,
                        new SqlParameter("@Longitude", SqlDbType.Decimal) ,
                        new SqlParameter("@Latitude", SqlDbType.Decimal) ,
                        new SqlParameter("@OriginLongitude", SqlDbType.Decimal) ,
                        new SqlParameter("@OriginLatitude", SqlDbType.Decimal) ,
                        new SqlParameter("@LeaveTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LeaveGPSState", SqlDbType.Int) ,
                        new SqlParameter("@LeaveLongitude", SqlDbType.Decimal) ,
                        new SqlParameter("@LeaveLatitude", SqlDbType.Decimal) ,
                        new SqlParameter("@OriginLeaveLongitude", SqlDbType.Decimal) ,
                        new SqlParameter("@OriginLeaveLatitude", SqlDbType.Decimal) ,
                        new SqlParameter("@DisplayRegister", SqlDbType.VarChar) ,
                        new SqlParameter("@DisplayGift", SqlDbType.VarChar) ,
                        new SqlParameter("@DisplayMoney", SqlDbType.VarChar) ,
                        new SqlParameter("@DisplayIDs", SqlDbType.VarChar) ,
                        new SqlParameter("@OrderHead", SqlDbType.VarChar) ,
                        new SqlParameter("@OrderDetail", SqlDbType.VarChar) ,
                        new SqlParameter("@Gift", SqlDbType.VarChar) ,
                        new SqlParameter("@Remark", SqlDbType.VarChar) ,
                        new SqlParameter("@GetLeaveRemark", SqlDbType.VarChar) ,
                        new SqlParameter("@StrStoreInOut", SqlDbType.VarChar) ,
                        new SqlParameter("@AssetChange", SqlDbType.VarChar) ,
                        new SqlParameter("@AssetRepair", SqlDbType.VarChar) ,
                                        };
                for (int i = 0; i < paramss.Count; i++)
                {
                    parameters[i].Value = paramss[i];
                }
                ds = DbHelperSQL.RunProcedure(proName, parameters, "table1");

            }
            catch (Exception ex)
            {
                ds = null;
                LogUtil.WriteInfo("调用【" + proName + "】存储过程失败：" + ex.Message);
            }
            return ds;
        }

    }
}
