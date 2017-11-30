using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace YaxonLogAnalysis.BLL
{
    public class CRM_JobWarningReceiveBLL
    {
        private readonly YaxonLogAnalysis.DAL.CRM_JobWarningReceive dal = new YaxonLogAnalysis.DAL.CRM_JobWarningReceive();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(YaxonLogAnalysis.Model.CRM_JobWarningReceive model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            return dal.Delete(ID);
        }
		
    }
}
