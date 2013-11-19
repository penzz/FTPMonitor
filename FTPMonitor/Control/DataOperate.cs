using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using LogLib;
using FTPMonitor.DB;
using FTPMonitor.Data;

namespace FTPMonitor
{
    class DataOperate
    {
        /// <summary>
        /// 新数据到达事件
        /// </summary>
        public event Action<DataInfo> NewDataEvent;
        /// <summary>
        /// 删除数据事件
        /// </summary>
        public event Action<string> DeleteDataEvent;

        private static DataOperate dataOperate = new DataOperate();

        public static DataOperate GetInstance()
        {
            return dataOperate;
        }
        /// <summary>
        /// 删除数据，外部调用
        /// </summary>
        /// <param name="fullPath"></param>
        public void RemoveData(string fullPath)
        {
            string[] infos = fullPath.Split('\\');
            int length = infos.Length;
            string name = infos[length - 1];
            int count = DeleteOneData(fullPath);
            if (count < 0)
            {
                string str = new StringBuilder().AppendFormat("删除数据{0}失败", name).ToString();
                LogManager.AddMessage(MessageType.ERR, str);
            }
            else
            {
                LogManager.AddMessage(MessageType.DEL, name);
            }
        }
        /// <summary>
        /// 增加新数据
        /// </summary>
        /// <param name="dataInfo"></param>
        public void AddNewData(DataInfo dataInfo)
        {
            //数据入库
            int count = InsertData(dataInfo);
            if (count < 0)
            {
                string str = new StringBuilder().AppendFormat("数据{0}入库失败", dataInfo.Name).ToString();
                LogManager.AddMessage(MessageType.ERR, str);
            }
            else
            {
                LogManager.AddMessage(MessageType.NEW, dataInfo.Name);
            }
        }

        /// <summary>
        /// 增加新数据
        /// </summary>
        /// <param name="dataInfo"></param>
        public void LoadOneData(DataInfo dataInfo)
        {
            //数据入库
            int count = InsertData(dataInfo);
            if (count < 0)
            {
                string str = new StringBuilder().AppendFormat("数据{0}入库失败", dataInfo.Name).ToString();
                LogManager.AddMessage(MessageType.ERR, str);
            }
            else
            {
                LogManager.AddMessage(MessageType.NEW, dataInfo.Name);
            }
        }

        #region 操作数据库
        /// <summary>
        /// 设置数据库中所有数据不存在
        /// </summary>
        /// <returns></returns>
        public int SetDataNotExist()
        {
            string sql = String.Format("update DataInfo set isexisted = '0'");
            return DataBaseControl.RunSqlForCount(sql);
        }
        /// <summary>
        /// 更新存在的数据，如果不存在则添加新纪录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public int UpdateOrInsertData(string name, string fullpath)
        {
            string sql = string.Format("update DataInfo set isexisted = '1' where fullpath = '{0}'", fullpath);
            int count = DataBaseControl.RunSqlForCount(sql);
            if (count <= 0)
            {
                DataInfo datainfo = DataFactory.CreateDataInfo(name, fullpath);
                if (datainfo == null)
                {
                    return Int32.MaxValue;
                }
                count = InsertData(datainfo);
            }
            return count;
        }

        /// <summary>
        /// 插入一条新数据信息
        /// </summary>
        /// <param name="datainfo"></param>
        /// <returns></returns>
        private int InsertData(DataInfo datainfo)
        {
            string sql = String.Format("insert into DataInfo(id,name,satellite,sensor,phototime,createtime,centerlat,centerlon,rownum,colnum,fullpath) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", datainfo.Id, datainfo.Name, datainfo.Satellite, datainfo.Sensor, datainfo.PhotoTime, datainfo.CreateTime, datainfo.CenterLat, datainfo.CenterLon, datainfo.Rownum, datainfo.Colnum, datainfo.FullPath);
            return DataBaseControl.RunSqlForCount(sql);
        }

        /// <summary>
        /// 根据全路径删除一条数据
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private int DeleteOneData(string fullPath)
        {
            string sql = string.Format("update DataInfo set isexisted = '0' where fullpath = '{0}'", fullPath);
            return DataBaseControl.RunSqlForCount(sql);
        }

        #endregion
    }
}
