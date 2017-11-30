using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace YaxonLogAnalysis.BLL
{

    public class CRM_JobWarningBLL
    {
        private readonly YaxonLogAnalysis.DAL.CRM_JobWarning dal = new YaxonLogAnalysis.DAL.CRM_JobWarning();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(YaxonLogAnalysis.Model.CRM_JobWarning model)
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

        public bool UpdateByID(System.Guid id)
        {
            return dal.UpdateByID(id);
        }

    }
}
