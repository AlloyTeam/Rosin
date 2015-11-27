using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Rosin.Config
{
    internal static class FiddlerPath
    {
        public static string FiddlerInstallPath { get; set; }

        public static string FiddlerInstallDir { get; set; }

        public static string RosinDir { get; set; }

        public static string RosinLogDir { get; set; }

        public static string RuleFilePath { get; set; }

        public static string ScriptFilePath { get; set; }

        public static string RecordFilePath { get; set; }

        static FiddlerPath()
        {
            FiddlerInstallPath = "";
            FiddlerInstallDir = "";
            RosinDir = "";
            RosinLogDir = "";
            RuleFilePath = "";
            ScriptFilePath = "";
            RecordFilePath = "";
        }

        public static void InitPath()
        {
            FiddlerInstallPath = Application.ExecutablePath;
            FiddlerInstallDir = Path.GetDirectoryName(FiddlerInstallPath);

            RosinDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Rosin");
            RosinLogDir = RosinDir + @"\Log";

            RuleFilePath = RosinDir + @"\InjectionRule.xml";
            ScriptFilePath = RosinDir + @"\InjectionScript.js";
            RecordFilePath = RosinDir + @"\InjectionList.xml";
        }
    }
}