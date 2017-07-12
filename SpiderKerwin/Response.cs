using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Xuhengxiao.SpiderKerwin
{
    /// <summary>
    /// 定义一个解析的委托，
    /// </summary>
    /// <param name="res"></param>
    public delegate void ParseEventHandler(string str_html);

    /// <summary>
    /// 这个类作为传输数据用，包含url，parse方法(委托)，和xpath方法
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 保存网址的
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 保存解析方法的，由这个方法对页面进行解析。
        /// </summary>
        public ParseEventHandler Parse { get; set; }

        /// <summary>
        /// 是否已经被爬的。
        /// </summary>
        public bool isParse { get; set; }

    }
}
