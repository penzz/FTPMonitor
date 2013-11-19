using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FTPMonitor
{
    /// <summary>
    /// 环境星
    /// </summary>
    class HJData : BaseData
    {
        public override DataInfo GetDataInfo(string fullpath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fullpath);
                string[] infos = fullpath.Split('\\');
                int length = infos.Length;
                DataInfo dataInfo = new DataInfo();
                dataInfo.Name = infos[length - 1];
                dataInfo.CreateTime = string.Format("{0:yyyy/MM/dd HH:mm:ss}", fileInfo.CreationTime);
                string[] strs = dataInfo.Name.Split('-');
                dataInfo.Satellite = strs[0];
                dataInfo.Sensor = strs[1];
                dataInfo.Rownum = Convert.ToInt32(strs[2]);
                dataInfo.Colnum = Convert.ToInt32(strs[3]);

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
