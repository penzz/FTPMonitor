using FTPMonitor.DB;
using FTPMonitor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FTPMonitor.Control
{
    class DataHelper
    {
        public static int GetCount(string criteria)
        {
            string sqlcount = String.Format("select count(*) from DataInfo where  1=1 {0}", criteria);
            return Convert.ToInt32(DataBaseControl.RunSqlForScalar(sqlcount));
        }

        public static DataTable GetDataTable(int start, int end, string criteria)
        {
            string sql = string.Format("select top ({0}) * from DataInfo where 1=1 {1} and (id NOT IN (" +
           "select top ({2}) id from DataInfo where 1=1 {3} order by id)) order by id", start, criteria, end, criteria);
            return DataBaseControl.RunSqlForDataTable(sql);
        }

        public static DataTable GetDataTable(string criteria)
        {
            string sql = String.Format("select id,name,satellite,sensor,phototime,createtime,centerlat,centerlon,fullpath,isexisted from datainfo where 1=1 {0}", criteria);
            return DataBaseControl.RunSqlForDataTable(sql);
        }

        public static string GetCriteria(QueryParameter queryPara)
        {
            StringBuilder criteria = new StringBuilder();
            if (!queryPara.isToday)
            {
                criteria.Append(CC.CommbineLocationCriteria(queryPara.southLat.ToString(), queryPara.northLat.ToString(), queryPara.westLon.ToString(), queryPara.eastLon.ToString()));
            }
            criteria.Append(CC.CombineTimeCriteria(queryPara.photoTime, queryPara.createTime));
            if (!queryPara.hasDelete)
            {
                criteria.Append(CC.CombineIsExistedCriteria(1));
            }
            return criteria.ToString();
        }
    }
}
