using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace FTPMonitor
{
    class ExcelOperate
    {
        /// <summary>   
        /// 直接导出Excel   
        /// </summary>   
        /// <param name="datatable">数据源DataSet</param>   
        /// <param name="fileName">保存文件名(例如：E:\a.xls)</param>   
        /// <returns></returns>   
        public static bool ExportExcel(System.Data.DataTable datatable, string fileName)
        {
            if (datatable.Rows.Count < 0 || fileName == string.Empty)
            {
                return false;
            }
            Application excel = new Application();
            int rowindex = 1;
            int colindex = 0;
            Workbook work = excel.Workbooks.Add(true);
            foreach (DataColumn col in datatable.Columns)
            {
                colindex++;
                excel.Cells[1, colindex] = GetColName(col.ColumnName);
            }
            foreach (DataRow row in datatable.Rows)
            {
                rowindex++;
                colindex = 0;
                foreach (DataColumn col in datatable.Columns)
                {
                    colindex++;
                    excel.Cells[rowindex, colindex] = row[col.ColumnName].ToString();
                }
            }
            excel.Visible = false;
            excel.ActiveWorkbook.Saved = true;
            excel.ActiveWorkbook.SaveCopyAs(fileName);
            excel.Quit();
            excel = null;
            GC.Collect();
            return true;
        }
        private static string GetColName(string name)
        {
            string newName = "";
            switch (name)
            {
                case "id":
                    newName = "编号";
                    break;
                case "name":
                    newName = "名称";
                    break;
                case "satellite":
                    newName = "卫星";
                    break;
                case "sensor":
                    newName = "传感器";
                    break;
                case "phototime":
                    newName = "拍摄时间";
                    break;
                case "createtime":
                    newName = "创建时间";
                    break;
                case "centerlat":
                    newName = "中心点纬度";
                    break;
                case "centerlon":
                    newName = "中心点经度";
                    break;
                case "rownum":
                    newName = "轨道行号";
                    break;
                case "colnum":
                    newName = "轨道列号";
                    break;
                case "fullpath":
                    newName = "存放路径";
                    break;
                case "isexisted":
                    newName = "是否存在";
                    break;
                default:
                    newName = name;
                    break;
            }
            return newName;
        }
    }
}
