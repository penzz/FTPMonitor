using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace FTPMonitor
{
    class Pressfile
    {
        /// <summary>
        /// 解压缩文件包,返回解压缩路径
        /// </summary>
        /// <param name="PackagePath">包含路径的完整文件名</param>
        public static string UnpressFile(string strPath)
        {
            string temppackageName = Path.GetFileNameWithoutExtension(strPath);
            string UnpackagePath = Path.GetDirectoryName(strPath) + @"\" + Path.GetFileNameWithoutExtension(temppackageName);
            string temppackagePath = Path.Combine(UnpackagePath, temppackageName);
            ProcessStartInfo zipfirst_exe = new ProcessStartInfo();
            zipfirst_exe.FileName = "7za.exe";
            zipfirst_exe.Arguments = string.Format("x {0} -o{1} * -aoa", strPath, UnpackagePath);

            //将packagePath中的压缩包解压到同目录同文件名下的文件夹中
            zipfirst_exe.CreateNoWindow = true;
            zipfirst_exe.UseShellExecute = false;
            try
            {
                Process processGz = Process.Start(zipfirst_exe);
                processGz.WaitForExit();
                zipfirst_exe.Arguments = string.Format("x {0} -o{1} * -aoa", temppackagePath, UnpackagePath);
                processGz = Process.Start(zipfirst_exe);
                processGz.WaitForExit();
                processGz.Close();
                File.Delete(temppackagePath);
            }
            catch
            {
                Console.WriteLine("输入文件有误！");
            }
            return UnpackagePath;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="SaveFilesPath">需要压缩的文件夹</param>
        /// <param name="SavePackagePath">压缩后的文件存放路径</param>
        public static void PressFile(string SaveFilesPath, string SavePackagePath, string newfilename)
        {
            string[] tempstr = SaveFilesPath.Split('\\');
            //SavePackagePath = Path.Combine(SavePackagePath, tempstr[tempstr.Length - 1]) + ".tar";
            SavePackagePath = Path.Combine(SavePackagePath, newfilename) + ".tar";
            ProcessStartInfo zipfirst_exe = new ProcessStartInfo();
            zipfirst_exe.FileName = "7za.exe";
            zipfirst_exe.Arguments = string.Format("a {0} * -aoa", "\"" + SavePackagePath + "\"");

            zipfirst_exe.CreateNoWindow = true;
            zipfirst_exe.UseShellExecute = false;
            //将文件夹压缩成tar格式 压缩后文件名 SavePackagePath.tar
            zipfirst_exe.WorkingDirectory = SaveFilesPath;
            try
            {
                Process processGz = Process.Start(zipfirst_exe);
                processGz.WaitForExit();
                string SavePackagePath2 = SavePackagePath + ".gz";
                //二次压缩，*{1}表示通配查找已经压缩好的tar
                zipfirst_exe.Arguments = string.Format("a {0} *{1} -aoa", SavePackagePath2, Path.GetFileName(SavePackagePath));
                //设置工作目录为.tar格式文件所在文件夹目录
                zipfirst_exe.WorkingDirectory = Path.GetDirectoryName(SavePackagePath);
                processGz = Process.Start(zipfirst_exe);
                processGz.WaitForExit();
                processGz.Close();
                File.Delete(SavePackagePath);
            }
            catch
            {

                Console.WriteLine("保存文件有误！");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPath">目标路径.tar.gz</param>
        /// <param name="destPath">原文件目录</param>
        public static void ZipDirectory(string dirPath, string destPath)
        {
            Process p = new Process();
            p.StartInfo.WorkingDirectory = Application.StartupPath + "\\";
            p.StartInfo.FileName = "7za.exe";
            //string s = string.Format(@"a {0} .\{1}\*",dirPath, @"F:\HJ1B-CCD1-15-68-20110614-L20000556117\HJ1B-CCD1-15-68-20110614-L20000556117");
            p.StartInfo.Arguments = string.Format(@"a {0} {1}", dirPath, destPath);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
            p.Close();
        }

        /// <summary>
        /// 压缩多个文件至一个压缩包
        /// </summary>
        /// <param name="fileList">文件列表</param>
        /// <param name="zipStyle">压缩类型如tar,gzip</param>
        /// <param name="DestzipFileName">目标压缩包名称,不带后缀名</param>
        /// <param name="DestzipPath">压缩包存放路径</param>
        public void ZipFiles(List<string> fileList, string zipStyle, string DestzipFileName, string DestzipPath)
        {
            foreach (string str in fileList)
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Application.StartupPath + "\\";
                p.StartInfo.FileName = "7za.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //p.StartInfo.Arguments = string.Format("a {0} {1}","D:\\\"GF数据\"\\a.7z",textBoxFile.Text);
                string s = string.Format("a -t{0} {1} {2}", zipStyle, Path.Combine(DestzipPath, DestzipFileName), str);
                p.StartInfo.Arguments = string.Format("a -t{0} {1} {2}", zipStyle, Path.Combine(DestzipPath, DestzipFileName), str);
                p.Start();
                p.WaitForExit();
            }
        }
        /// <summary>
        /// 解压文件（删除原文件）
        /// </summary>
        /// <param name="zipFileName">要解压的文件</param>
        /// <param name="decDir">解压后文件存放的文件夹路径</param>
        /// <returns></returns>
        public static string DecompresFile(string zipFileName, string decDir)
        {
            if (Directory.Exists(decDir) == false)
            {
                Directory.CreateDirectory(decDir);
            }
            string upackpath = Path.Combine(decDir, Path.GetFileNameWithoutExtension(zipFileName));
            ProcessStartInfo zip_exe = new ProcessStartInfo();
            zip_exe.FileName = "7za.exe";
            zip_exe.Arguments = string.Format("e {0} -o{1} * -aoa", zipFileName, "\"" + decDir + "\"");
            zip_exe.CreateNoWindow = true;
            zip_exe.UseShellExecute = false;
            zip_exe.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Process processGz = Process.Start(zip_exe);
            processGz.WaitForExit();
            processGz.Close();
            if (File.Exists(upackpath) == false)
            {
                //此文件被用户重命名过，压缩包中的文件与压缩包名称不一致
                string[] fileArray = Directory.GetFiles(decDir, "*.tar");
                upackpath = fileArray.Length > 0 ? fileArray[0] : string.Empty;
            }
            return upackpath;
        }
        /// <summary>
        /// 解压压缩文件到指定目录
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="destDir"></param>
        public void UnZipFile(string zipFileName, string destDir)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Application.StartupPath + "\\";
                p.StartInfo.FileName = "7za.exe";
                p.StartInfo.Arguments = string.Format("e {0} -o{1}", zipFileName, destDir);
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show("解压文件失败" + er.Message);
            }
        }
        /// <summary>
        /// 解压压缩包中同种类型的文件
        /// </summary>
        /// <param name="zipName"></param>
        /// <param name="destFilePath"></param>
        /// <param name="fileStyle">通配符 例如"*.xml"来查找目录中所有xml格式的文件</param>
        public void UnZipOneFile(string zipName, string destFilePath, string fileStyle)
        {
            try
            {
                if (Path.GetFileName(zipName).EndsWith("tar.gz"))
                {
                    UnZipFile(zipName, destFilePath);
                    string secondZipName = Path.Combine(destFilePath, Path.GetFileNameWithoutExtension(zipName));
                    Process p = new Process();
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\";
                    p.StartInfo.FileName = "7za.exe";
                    string s = string.Format("e {0} -o{1} {2}", zipName, destFilePath, fileStyle);
                    p.StartInfo.Arguments = string.Format("e -r {0} -o{1} {2}", secondZipName, destFilePath, fileStyle);
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\";
                    p.StartInfo.FileName = "7za.exe";
                    string s = string.Format("e {0} -o{1} {2}", zipName, destFilePath, fileStyle);
                    p.StartInfo.Arguments = string.Format("e -r {0} -o{1} {2}", zipName, destFilePath, fileStyle);
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("解压文件失败" + er.Message);
            }
        }
    }
}
