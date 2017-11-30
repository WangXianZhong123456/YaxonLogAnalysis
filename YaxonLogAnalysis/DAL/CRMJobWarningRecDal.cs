using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace YaxonLogAnalysis.DAL
{
    public class CRMJobWarningRecDal
    {

        //public int AddUser(CRMJobWarningRec userInfo)
        //{
        //    // //对数据库进添加一个用户操作
        //    string commandText = "INSERT INTO CRM_JobWarningReceive(UserName,Telphone,Email)values(@UserName,@Telphone,@Email)";
        //    SqlParameter[] paras = new SqlParameter[]
        //    {
        //       new SqlParameter ("@UserName",SqlDbType.VarChar,50,userInfo.UserName),
        //       new SqlParameter ("@Telphone",SqlDbType.VarChar,50,userInfo.Telphone ),
        //       new SqlParameter ("@Email",SqlDbType.VarChar,50,userInfo.Telphone )

        //    };
        //    return SqlHelper.ExecteNonQuery(commandText, CommandType.Text, commandText, paras);
        //}

        // public int DelUser(int ID)
        // {
        //     string commandText = "DELETE FROM CRM_JobWarningReceive WHERE ID =@ID";
        //     SqlParameter[] paras = new SqlParameter[]
        //    {
        //       new SqlParameter ("@ID",SqlDbType.Int,50,ID.ToString())

        //    };
        //     return SqlHelper.ExecteNonQuery(commandText, CommandType.Text, commandText, paras);

        // }

    }
}
