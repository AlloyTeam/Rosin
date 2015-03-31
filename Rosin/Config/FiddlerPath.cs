using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Rosin.Config
{
    static class FiddlerPath
    {
        static public string FiddlerInstallPath = "";
        static public string FiddlerInstallDir = "";

        static public string RosinDir = "";
        static public string RosinLogDir = "";

        static public string RuleFilePath = "";
        static public string ScriptFilePath = "";
        static public string RecordFilePath = "";

        static public void InitPath()
        {
            FiddlerInstallPath = Application.ExecutablePath;
            FiddlerInstallDir = Path.GetDirectoryName(FiddlerInstallPath);

            RosinDir = FiddlerInstallDir + @"\Scripts\Rosin";
            RosinLogDir = RosinDir + @"\Log";

            RuleFilePath = RosinDir + @"\InjectionRule.xml";
            ScriptFilePath = RosinDir + @"\InjectionScript.js";
            RecordFilePath = RosinDir + @"\InjectionList.xml";
        }
    }
}
