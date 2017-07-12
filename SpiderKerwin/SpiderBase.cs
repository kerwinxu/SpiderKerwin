using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xuhengxiao.SpiderKerwin
{
    /// <summary>
    /// 蜘蛛的基类
    /// </summary>
    public  class SpiderBase
    {
        /// <summary>
        /// 调度器
        /// </summary>
        public SchedulerBase Scheduler { get; set; }

        /// <summary>
        /// 无非是调用调度器的add方法。
        /// </summary>
        /// <param name="str_rul"></param>
        /// <param name="_parse"></param>
        public void add(string str_rul,ParseEventHandler _parse)
        {
            Response res = new Response();
            res.Url = str_rul;
            res.Parse = _parse;
            res.isParse = false;
            Scheduler.add(res);
        }

        #region
        //解析方法编写，委托是这个，public delegate void ParseEventHandler(Response res);
        //从这个基类继承的子类中实现这个委托。
        #endregion

        #region xpath解析相关的
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_html"></param>
        /// <param name="str_xpath"></param>
        /// <returns></returns>
        public HtmlNodeCollection SelectNodes(string str_html, string str_xpath)
        {
            //首先判断是否有网页吧
            if (str_html == null || str_xpath==null)
            {
                return null;//没有的话，就返回空值啦
            }

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(str_html);
            return doc.DocumentNode.SelectNodes(str_xpath);
            
        }

        /// <summary>
        /// 因为在SelectNodes基础上继续SelectNodes，只会以doc为基础继续xpath，而不会以单独的HtmlNode
        /// 所以出这个SelectNodes2，每个HtmlNode都是重新生成的。
        /// </summary>
        /// <param name="str_html"></param>
        /// <param name="str_xpath"></param>
        /// <returns></returns>
        public List<HtmlNode> SelectNodes2(string str_html, string str_xpath)
        {
            List<HtmlNode> list_return = new List<HtmlNode>();


            HtmlNodeCollection HtmlNodeCollection1 = SelectNodes(str_html, str_xpath);

            //然后还得判断HtmlNodeCollection1是否有值
            if (HtmlNodeCollection1==null)
            {
                return null;
            }

            //然后遍历HtmlNodeCollection1，每个节点生成一个新节点
            foreach (HtmlNode item in HtmlNodeCollection1)
            {
                HtmlNode _node = HtmlNode.CreateNode(item.OuterHtml);
                list_return.Add(_node);
            }

            return list_return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_html"></param>
        /// <param name="str_xpath"></param>
        /// <returns></returns>
        public HtmlNode SelectSingleNode(string str_html, string str_xpath)
        {
            //首先判断是否有网页吧
            if (str_html != null)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(str_html);
                return doc.DocumentNode.SelectSingleNode(str_xpath);
            }

            return null;
        }
        #endregion
    }
}
