using System;
using System.Collections.Generic;
using System.Text;

namespace Rosin.Config
{
    class AboutWording
    {
        public static string[] WORDING_ARR= new string[]{
            "版本：v1.0.1",
            "项目地址：https://github.com/AlloyTeam/Rosin",
            "更新日志",
            "  v1.0.1 -- 2015.03.28",
            "\t1、修改BUG",
            "  v1.0.0 -- 2015.01.20",
            "\t1、支持日志级别按颜色区分",
            "\t2、支持按级别筛选日志",
            "\t3、支持日志文件导出、清除、删除",
            "\t4、支持日志记录自动清理",
            "\t5、支持日志内容JSON对象解析",
            "\t6、支持script error信息输出",
            "\t7、支持跨域script error信息获取",
            "\t8、优化日志展示",
            "  v0.0.1 -- 2014.12.03",
            "\t1、支持console日志接收",
            "\t2、支持fiddler日志展示",
            "\t3、支持页面规则配置",
            "\t4、支持日志列表展示",
        };

        public static string GetAboutWord()
        {
            return String.Join("\r\n", WORDING_ARR);
        }
    }
}
