using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FTPMonitor
{
    class CC
    {
        public static string monitorFolder;
        /// <summary>
        /// 合并参数为字符串
        /// </summary>
        /// <param name="fix">分隔符</param>
        /// <param name="str">参数列表</param>
        /// <returns></returns>
        public static string MergeParams(char fix, params string[] str)
        {
            string result = "";
            foreach (var item in str)
            {
                result += item + fix;
            }
            return result.Substring(0, result.Length - 1);
        }
        /// <summary>
        /// 获取经纬度条件的sql语句
        /// </summary>
        /// <param name="south"></param>
        /// <param name="north"></param>
        /// <param name="west"></param>
        /// <param name="east"></param>
        /// <returns></returns>
        public static string CommbineLocationCriteria(string south, string north, string west, string east)
        {
            StringBuilder strbuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(south))
            {
                strbuilder.AppendFormat(" and centerlat > '{0}'", south);
            }
            if (!string.IsNullOrEmpty(north))
            {
                strbuilder.AppendFormat(" and centerlat < '{0}'", north);
            }
            if (!string.IsNullOrEmpty(west))
            {
                strbuilder.AppendFormat(" and centerlon > '{0}'", west);
            }
            if (!string.IsNullOrEmpty(east))
            {
                strbuilder.AppendFormat(" and centerlon < '{0}'", east);
            }
            return strbuilder.ToString();
        }
        /// <summary>
        /// 构造拍摄时间和到达时间的sql语句
        /// </summary>
        /// <param name="phototime"></param>
        /// <param name="createtime"></param>
        /// <returns></returns>
        public static string CombineTimeCriteria(string phototime, string createtime)
        {
            StringBuilder strbuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(phototime))
            {
                strbuilder.AppendFormat(" and phototime = '{0}' ", phototime);
            }
            if (!string.IsNullOrEmpty(createtime))
            {
                DateTime dt = Convert.ToDateTime(createtime);
                DateTime dtPre = dt.AddDays(-1);
                DateTime dtMax = new DateTime(dt.Year, dt.Month, dt.Day, 17, 30, 0);
                DateTime dtMin = new DateTime(dtPre.Year, dtPre.Month, dtPre.Day, 17, 30, 0);
                strbuilder.AppendFormat(" and createtime > '{0}' and createtime < '{1}'", dtMin, dtMax);
            }
            return strbuilder.ToString();
        }
        /// <summary>
        ///  构造数据是否存在的sql语句
        /// </summary>
        /// <param name="isExisted"></param>
        /// <returns></returns>
        public static string CombineIsExistedCriteria(int isExisted)
        {
            string str = "";
            if (isExisted == 1 || isExisted == 0)
            {
                str = string.Format(" and isexisted = '{0}' ", isExisted);
            }
            return str;
        }

        #region
        private static int number = 0;
        private static object obj = new object();
        private static Random random = new Random();
        /// <summary>
        /// 获得随机编号
        /// </summary>
        /// <returns></returns>
        public static string getRandom()
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
        #endregion
    }
}
