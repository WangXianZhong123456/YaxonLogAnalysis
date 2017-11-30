using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using YaxonLogAnalysis.Common;
namespace YaxonLogAnalysis.DAL 
{
	 	//CRM服务器作业预警
		public partial class CRM_JobWarning
	{
   		     
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CRM_JobWarning");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(YaxonLogAnalysis.Model.CRM_JobWarning model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CRM_JobWarning(");			
            strSql.Append("IDNO,DateValue,LinkServer,HostName,HostIP,JobID,JobName,JobStartTime,JobExeTime,JobAvgRuntime,State");
			strSql.Append(") values (");
            strSql.Append("@IDNO,@DateValue,@LinkServer,@HostName,@HostIP,@JobID,@JobName,@JobStartTime,@JobExeTime,@JobAvgRuntime,@State");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@IDNO", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@DateValue", SqlDbType.DateTime) ,            
                        new SqlParameter("@LinkServer", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HostName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HostIP", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobStartTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@JobExeTime", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@JobAvgRuntime", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@State", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = Guid.NewGuid();                        
            parameters[1].Value = model.DateValue;                        
            parameters[2].Value = model.LinkServer;                        
            parameters[3].Value = model.HostName;                        
            parameters[4].Value = model.HostIP;                        
            parameters[5].Value = model.JobID;                        
            parameters[6].Value = model.JobName;                        
            parameters[7].Value = model.JobStartTime;                        
            parameters[8].Value = model.JobExeTime;                        
            parameters[9].Value = model.JobAvgRuntime;                        
            parameters[10].Value = model.State;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}

        public bool UpdateByID(System.Guid IDNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CRM_JobWarning set ");

            strSql.Append(" State = 1  ");
            strSql.Append(" where IDNO = @IDNO ");

            SqlParameter[] parameters = {
			            new SqlParameter("@IDNO", SqlDbType.UniqueIdentifier,16) ,            
              
            };

            parameters[0].Value = IDNO;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(YaxonLogAnalysis.Model.CRM_JobWarning model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CRM_JobWarning set ");
			                        
            //strSql.Append(" IDNO = @IDNO , ");                                    
            strSql.Append(" DateValue = @DateValue , ");                                    
            strSql.Append(" LinkServer = @LinkServer , ");                                    
            strSql.Append(" HostName = @HostName , ");                                    
            strSql.Append(" HostIP = @HostIP , ");                                    
            strSql.Append(" JobID = @JobID , ");                                    
            strSql.Append(" JobName = @JobName , ");                                    
            strSql.Append(" JobStartTime = @JobStartTime , ");                                    
            strSql.Append(" JobExeTime = @JobExeTime , ");                                    
            strSql.Append(" JobAvgRuntime = @JobAvgRuntime , ");                                    
            strSql.Append(" State = @State  ");
            strSql.Append(" where IDNO = @IDNO ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@IDNO", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@DateValue", SqlDbType.DateTime) ,            
                        new SqlParameter("@LinkServer", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HostName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HostIP", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobStartTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@JobExeTime", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@JobAvgRuntime", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@State", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.IDNO;                        
            parameters[1].Value = model.DateValue;                        
            parameters[2].Value = model.LinkServer;                        
            parameters[3].Value = model.HostName;                        
            parameters[4].Value = model.HostIP;                        
            parameters[5].Value = model.JobID;                        
            parameters[6].Value = model.JobName;                        
            parameters[7].Value = model.JobStartTime;                        
            parameters[8].Value = model.JobExeTime;                        
            parameters[9].Value = model.JobAvgRuntime;                        
            parameters[10].Value = model.State;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CRM_JobWarning ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM CRM_JobWarning ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM CRM_JobWarning ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

