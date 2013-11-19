using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTPMonitor.DB;
using System.Data.SqlClient;

namespace FTPMonitor
{
    public class DataInfo
    {
        private string id;
        /// <summary>
        /// 数据ID
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        /// <summary>
        /// 数据名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string satellite;
        /// <summary>
        /// 卫星
        /// </summary>
        public string Satellite
        {
            get { return satellite; }
            set { satellite = value; }
        }
        private string sensor;
        /// <summary>
        /// 传感器
        /// </summary>
        public string Sensor
        {
            get { return sensor; }
            set { sensor = value; }
        }
        private string photoTime;
        /// <summary>
        /// 拍摄时间
        /// </summary>
        public string PhotoTime
        {
            get { return photoTime; }
            set { photoTime = value; }
        }
        private string createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        private double centerLat;
        /// <summary>
        /// 中心点纬度
        /// </summary>
        public double CenterLat
        {
            get { return centerLat; }
            set { centerLat = value; }
        }
        private double centerLon;
        /// <summary>
        /// 中心点经度
        /// </summary>
        public double CenterLon
        {
            get { return centerLon; }
            set { centerLon = value; }
        }

        private int rownum;
        /// <summary>
        /// 轨道行号
        /// </summary>
        public int Rownum
        {
            get { return rownum; }
            set { rownum = value; }
        }
        private int colnum;
        /// <summary>
        /// 轨道列号
        /// </summary>
        public int Colnum
        {
            get { return colnum; }
            set { colnum = value; }
        }

        private string fullPath;
        /// <summary>
        /// 完整本地ftp路径
        /// </summary>
        public string FullPath
        {
            get { return fullPath; }
            set { fullPath = value; }
        }
        /// <summary>
        /// 重写输入字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CC.MergeParams('@', name, photoTime, createTime, fullPath);
        }
    }
}
