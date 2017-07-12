using System;
using System.Collections.Generic;
using System.Text;

namespace Xuhengxiao.SpiderKerwin
{
    /// <summary>
    /// 调度器的基类，单线程版本的。
    /// </summary>
    public class SchedulerBase
    {


        /// <summary>
        /// 下载器的，单线程版本的。
        /// </summary>
        private DownLoaderBase _downloader;

        /// <summary>
        /// 是否在运行，主要用于第一次。
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// 构造函数的参数就是下载器，这个是单线程版本
        /// </summary>
        /// <param name="_down"></param>
        public SchedulerBase(DownLoaderBase _down)
        {
            _downloader = _down;
            _down.DocumentComplete += DocumentComplete;            
        }


        /// <summary>
        /// 存储网址等信息的。
        /// </summary>
        private List<Response> lst_Response = new List<Response>();

        /// <summary>
        /// 增加一个网址。
        /// </summary>
        /// <param name="res"></param>
        public void add(Response res)
        {
            if (lst_Response == null)
            {
                lst_Response = new List<Response>();
            }
            //还得判断是否已经在里边了。
            foreach (var item in lst_Response)
            {
                //如果有相同的，就推出啦
                if (item.Url == res.Url)
                {
                    return;
                }
            }
            lst_Response.Add(res);
            //如果没有在运行，就调用实际的方法去运行调度器
            if (!isRunning)
            {
                next();
            }
            
        }

        /// <summary>
        /// 取得第一个没有访问的网址
        /// </summary>
        /// <returns></returns>
        public Response getIsParse()
        {
            //首先还得判断是否为空值。
            if (lst_Response == null)
            {
                return null;
            }
            //遍历
            foreach (var item in lst_Response)
            {
                //如果没有访问
                if (item.isParse == false)
                {
                    //就返回这个
                    return item;
                }
            }
            //如果没有找到，就返回空值。
            return null;
        }

        /// <summary>
        /// 进行下一页的
        /// </summary>
        public void next()
        {
            //首先取得一个网址，然后将这个设置成已经下载
            Response res = getIsParse();//取得没有被爬的
            if (res==null)
            {
                //如果没有需要爬的，就退出吧
                isRunning = false;
                return;
            }
            res.isParse = true;//设置已经被爬
            _downloader.Start(res);//让下载器去下载
            isRunning = true;//设置状态的。
        }


        /// <summary>
        /// 监听者，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void DocumentComplete(Object sender, DocumentCompleteEventArgs e)
        {
            //进行下一页，并且调用解析吧。
            e.RESPONSE.Parse(e.HTML);//先解析
            next();//然后下一个网址

        }

    }
}
