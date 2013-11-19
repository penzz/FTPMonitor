using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FTPMonitor
{
    class Class1
    {
        //经纬度
        //excel
        //HJ星数据

        /**
        //pagerControl1.GetPageEvent += new PagerLib.PagerControl.GetPageHandle(pagerControl1_GetPageEvent);
        //GetSource();
        /// <summary>
        /// 分页控件属性改变
        /// </summary>
        void pagerControl1_GetPageEvent()
        {
            GetSource();
        }

        private void GetSource()
        {
            //获得总记录条数
            //string sqlcount = "select count(*) from originaldata";
            //pagerControl1.RecordCount = DBAccess.GetCount(sqlcount);
            ////获得当前页
            //int cur = pagerControl1.CurrentPage;
            ////获得每页显示的记录数
            //int rows = pagerControl1.RowsPerPage;
            ////计算显示记录的开始值
            //int start = (cur - 1) * rows;
            ////计算显示记录的结束值
            //int end = cur * rows + 1;
            ////获得从开始值到结束值的记录
            //string sql = string.Format("select * from (select rownum rn,t.* from (select * from originaldata) t where rownum <{0}) where rn>{1}", end, start);
            //DataTable DT = DBAccess.GetDataTable(sql);
            ////将得到的记录绑定到DataGridView显示给用户
            //this.dataGridView1.DataSource = DT.DefaultView;\
         * 
         * 
         * SELECT   TOP (3) id, name, satellite, sensor, phototime, createtime, centerlat, centerlon, rownum, colnum, fullpath, photolongtime, 
                producttime, sceneID, productID, orderid, ordercreatetime, preprocesstime, achiefeedbacktime
FROM      DataInfo
WHERE   (id NOT IN
                    (SELECT   TOP (3) id
                     FROM      DataInfo AS DataInfo_1
                     ORDER BY id))
ORDER BY id
        }
        public void ReadDataInfo(string south, string north, string west, string east, string phototime, string createtime)
        {
            string sql = String.Format("select * from datainfo where centerlat > '{0}' and centerlat < '{1}' and centerlon > '{2}' and centerlon < '{3}' ", south, north, west, east);
            if (phototime != "")
            {
                DateTime dt = DateTime.Parse(phototime);
                string time = String.Format("{0:yyyyMMdd}", dt);
                sql += string.Format("and phototime = '{0}' ", time);
            }
            if (createtime != "")
            {
                DateTime dt = DateTime.Parse(createtime);
                DateTime dtPre = dt.AddDays(-1);
                DateTime dtMax = new DateTime(dt.Year, dt.Month, dt.Day, 17, 0, 0);
                DateTime dtMin = new DateTime(dtPre.Year, dtPre.Month, dtPre.Day, 17, 0, 0);
                sql += string.Format("and createtime > '{0}' and createtime < '{1}'", dtMin, dtMax);
            }s
            SqlDataReader reader = DBAccess.GetDataReader(sql);
        }

        /// <summary>
        /// 获得数据是否满足输入条件
        /// </summary>
        /// <param name="datainfo"></param>
        /// <returns></returns>
        public bool GetIsNeedData(DataInfo datainfo)
        {
            bool isNeedData = datainfo.CenterLat >= south && datainfo.CenterLat <= north && datainfo.CenterLon >= west && datainfo.CenterLon <= east;
            if (!string.IsNullOrEmpty(photoTime))
            {
                isNeedData = (isNeedData && photoTime == datainfo.PhotoTime);
            }
            DateTime datainfoCreateTime = Convert.ToDateTime(datainfo.CreateTime);

            if (this.dtCreateTime.Text != "")
            {
                DateTime dtPre = createtime.AddDays(-1);
                DateTime dtMax = new DateTime(createtime.Year, createtime.Month, createtime.Day, 17, 30, 0);
                DateTime dtMin = new DateTime(dtPre.Year, dtPre.Month, dtPre.Day, 17, 30, 0);
                isNeedData = (isNeedData && dtMax > datainfoCreateTime && createtime > dtMin);
            }
            return isNeedData;
        }

        /// <summary>
        /// 遍历监控目录下的所有目录，查找符合条件的数据并复制
        /// </summary>
        /// <param name="dir"></param>
        void FindAndCopy(DirectoryInfo dir)
        {
            foreach (DirectoryInfo nextDir in dir.GetDirectories())
            {
                FindAndCopy(nextDir);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                DataInfo datainfo = CC.GetDataInfo(file.FullName);
                if (datainfo == null)
                {
                    continue;
                }
                bool b = GetIsNeedData(datainfo);
                if (b)
                {
                    fileoperate.ReadDataInfo(south.ToString(), north.ToString(), west.ToString(), east.ToString(), photoTime, createtime);
                }
            }
        }
         * */
    }
}
