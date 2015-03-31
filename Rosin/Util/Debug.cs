using System;
using System.Collections.Generic;
using System.Text;

using Fiddler;

namespace Rosin.Util
{
    public class Debug
    {
        public static void Log(string info)
        {
            // FiddlerApplication.Log.LogString(info); // open only in develop
        }
    }
}

// 编译错误：缺少编译器要求的成员“system.Runtime.CompilerServices.ExtensionAttribute..ctor”
// 解决方案，静态类中声明一个
namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}
