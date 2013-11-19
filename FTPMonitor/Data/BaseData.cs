using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FTPMonitor
{
    /// <summary>
    /// 遥感数据基类
    /// </summary>
    class BaseData
    {
        private int number = 0;
        private object obj = new object();
        private Random random = new Random();
        /// <summary>
        /// 获得随机编号
        /// </summary>
        /// <returns></returns>
        protected string getRandom()
        {
            lock (obj)
            {
                DateTime dtnow = DateTime.Now;
                if (++number >= 10000)
                {
                    number = 0;
                }
                return string.Format("{0:yyyyMMddHHmmss}{1:000}{2:0000}", dtnow, dtnow.Millisecond, number);
            }
        }
        /// <summary>
        /// 根据全路径获取数据信息
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public virtual DataInfo GetDataInfo(string fullpath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fullpath);
                string[] infos = fullpath.Split('\\');
                int length = infos.Length;
                DataInfo dataInfo = new DataInfo();
                dataInfo.Name = infos[length - 1];
                dataInfo.CreateTime = string.Format("{0:yyyy/MM/dd HH:mm:ss}", fileInfo.CreationTime);
                string[] strs = dataInfo.Name.Split('_');
                dataInfo.Satellite = strs[0];
                dataInfo.Sensor = strs[1];
                dataInfo.CenterLon = Convert.ToDouble(strs[2].Substring(1));
                if (strs[2].Substring(0, 1) == "W")
                {
                    dataInfo.CenterLon *= -1;
                }

                dataInfo.CenterLat = Convert.ToDouble(strs[3].Substring(1));
                if (strs[3].Substring(0, 1) == "S")
                {
                    dataInfo.CenterLat *= -1;
                }

                dataInfo.PhotoTime = strs[4];
                dataInfo.FullPath = fullpath;
                string id = strs[5].Substring(0, strs[5].Length - 7);
                dataInfo.Id = id + getRandom();

                return dataInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
