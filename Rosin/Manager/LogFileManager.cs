using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Rosin.Manager
{
    static class LogFileManager
    {
        /**
         * delete log file by key
         * */
        static public void DeleteFile(string key)
        {
            // 释放进程
            if (key != "" && LogDataManager.Instance().GetKey() == key)
            {
                LogDataManager.Instance().Clear();
            }

            // 删除文件
            string fileName = key + ".txt";
            string filePath = Config.FiddlerPath.RosinLogDir + @"\" + fileName;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /**
         * export log file by key
         * */
        static public void ExportFile(string key)
        { 
            string fileName = key + ".txt";
            string sourceFileDir = Config.FiddlerPath.RosinLogDir;
            string targetFileDir = "";

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择日志导出目录";
            folderBrowserDialog.ShowDialog();

            targetFileDir = folderBrowserDialog.SelectedPath;

            if (targetFileDir != "")
            {
                File.Copy(sourceFileDir + @"\" + fileName, targetFileDir + @"\" + fileName, true);
            }
        }
    }
}
