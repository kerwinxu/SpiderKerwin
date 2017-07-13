using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Xuhengxiao.SpiderKerwin
{
    /// <summary>
    /// 用WebRequest实现的下载器。
    /// </summary>
    public class DownLoaderWebRequest:DownLoaderBase
    {

        /// <summary>
        /// 下载页面
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static  string GetHtml(string url) //传入要下载的网址
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(testUrl(url));
            req.Method = "GET";
            string str;
            HttpWebResponse Stream = req.GetResponse() as HttpWebResponse;
            string strEncoding = Stream.CharacterSet;
            using (StreamReader reader = new StreamReader(Stream.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                str = reader.ReadToEnd();
                return str;
            }

        }

        /// <summary>
        /// 测试url格式,返回一个合法的url字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static  string testUrl(string str)
        {
            //首先判断是否以http://或者https://开头
            if (str.StartsWith("http://")|| str.StartsWith("https://"))
            {
                return str;
            }
            //我简单点，只做这个处理。
            return "http://" + str;
        }

        public override void Start(Response res)
        {
            
            base.Start(res);//首先调用基类的
            string str_html = GetHtml(res.Url);//然后下载页面
            OnDocumentComplete(str_html);//然后调用事件，发布事件。


        }
    }
}
