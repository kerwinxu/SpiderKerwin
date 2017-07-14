using System;
using System.Collections.Generic;
using System.Text;
using Xuhengxiao.SpiderKerwin;

namespace SpiderDemo
{
    public class Spider163News : SpiderBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Spider163News()
        {
            DownLoaderWebRequest down = new DownLoaderWebRequest();
            Scheduler = new SchedulerBase(down);
            add("http://news.163.com/", parse);
        }

        public void   parse(string str_html)
        {
           
            var links = SelectNodes(str_html, "//a");
            foreach (var item in links)
            {
                PiplineItem163NewsLinks p = new PiplineItem163NewsLinks();
                p.Link = item.GetAttributeValue("href",string.Empty);
                sendItem(p);
            }
            

        }
    }
}