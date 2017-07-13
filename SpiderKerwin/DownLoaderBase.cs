using System;
using System.Collections.Generic;
using System.Text;

namespace Xuhengxiao.SpiderKerwin
{


    /// <summary>
    /// 下载器的基类
    /// </summary>
    public class DownLoaderBase
    {
        //保存网址、解析的类的，
        private Response _response;

        /// <summary>
        /// 网页下载完毕事件定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DocumentCompleteEventHandler(Object sender, DocumentCompleteEventArgs e);
        /// <summary>
        /// 事件的声明
        /// </summary>
        public event DocumentCompleteEventHandler DocumentComplete;

        /// <summary>
        /// 调用事件，
        /// </summary>
        /// <param name="res"></param>
        public void OnDocumentComplete(string _html )
        {
            if (DocumentComplete != null)
            {
                //构造事件的参数
                DocumentCompleteEventArgs e = new DocumentCompleteEventArgs(_response, _html);
                //发送事件，实际是调用委托（函数指针）
                DocumentComplete(this, e);
            }

        }


        /// <summary>
        /// 访问一个网址,这个方法可以倍继承。
        /// </summary>
        /// <param name="res"></param>
        virtual public void Start(Response res)
        {
            _response = res;//先保存这个信息，等下载完毕的时候，需要填充html的，然后调用OnDocumentComplete发送事件
            //throw new System.NotImplementedException();
            
        }
    }


    /// <summary>
    /// 这个是网页下载完毕后事件的参数
    /// </summary>
    public class DocumentCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// 保存页面信息
        /// </summary>
        public Response RESPONSE { get; set; }
        /// <summary>
        /// 保存HTML的
        /// </summary>
        public string HTML { get; set; }
        public DocumentCompleteEventArgs(Response res,string _html)
        {
            RESPONSE = res;
            HTML = _html;
        }
    }
}
