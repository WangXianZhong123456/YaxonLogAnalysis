using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using YaxonLogAnalysis.Common;

namespace YaxonLogAnalysis.DAL 
{
	 	//CRM_JobWarningReceive
		public partial class CRM_JobWarningReceive
	{
   		     
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CRM_JobWarningReceive");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(YaxonLogAnalysis.Model.CRM_JobWarningReceive model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CRM_JobWarningReceive(");			
            strSql.Append("UserName,Telphone,Email");
			strSql.Append(") values (");
            strSql.Append("@UserName,@Telphone,@Email");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            //new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Telphone", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Email", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.UserName;                        
            parameters[1].Value = model.Telphone;                        
            parameters[2].Value = model.Email;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(YaxonLogAnalysis.Model.CRM_JobWarningReceive model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CRM_JobWarningReceive set ");
			                        
            //strSql.Append(" ID = @ID , ");                                    
            strSql.Append(" UserName = @UserName , ");                                    
            strSql.Append(" Telphone = @Telphone , ");                                    
            strSql.Append(" Email = @Email  ");
            strSql.Append(" where  ID = @ID");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Telphone", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Email", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.UserName;                        
            parameters[2].Value = model.Telphone;                        
            parameters[3].Value = model.Email;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CRM_JobWarningReceive ");
            strSql.Append(" where ID = @ID");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,16)           
              
            };
            parameters[0].Value = ID;

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
		/// 得到一个对象实体
		/// </summary>
        public YaxonLogAnalysis.Model.CRM_JobWarningReceive GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, UserName, Telphone, Email  ");			
			strSql.Append("  from CRM_JobWarningReceive ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};


            YaxonLogAnalysis.Model.CRM_JobWarningReceive model = new YaxonLogAnalysis.Model.CRM_JobWarningReceive();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
			    if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID= Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
				}
			    model.UserName= ds.Tables[0].Rows[0]["UserName"].ToString();
																																model.Telphone= ds.Tables[0].Rows[0]["Telphone"].ToString();
																																model.Email= ds.Tables[0].Rows[0]["Email"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM CRM_JobWarningReceive ");
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
			strSql.Append(" FROM CRM_JobWarningReceive ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

