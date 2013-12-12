using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPMonitor.Data
{
    class DataFactory
    {
        protected static BaseData CreateDataIntance(string dataName)
        {
            if (dataName.Contains("GF"))
            {
                return new GFData();
            }
            if (dataName.Contains("ZY"))
            {
                return new ZYData();
            }
            if (dataName.Contains("HJ"))
            {
                return new HJData();
            }
            return null;
        }
        /// <summary>
        /// 根据数据名称和数据路径返回数据元信息
        /// </summary>
        /// <param name="dataName">数据名称</param>
        /// <param name="fullPath">数据路径</param>
        /// <returns>数据元信息</returns>
        public static DataInfo CreateDataInfo(string dataName, string fullPath)
        {
            BaseData baseData = CreateDataIntance(dataName);
            if (baseData == null)
            {
                return null;
            }
            return baseData.GetDataInfo(fullPath);
        }
    }
}
