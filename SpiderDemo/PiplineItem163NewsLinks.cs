using System;
using System.Collections.Generic;
using System.Text;
using Xuhengxiao.SpiderKerwin;

namespace SpiderDemo
{
    public  class PiplineItem163NewsLinks:PiplineItemBase
    {
        public string Link { get; set; }
        public override void deal()
        {
            base.deal();
            //这个处理仅仅是输出而已
            Console.WriteLine(Link);
        }
    }
}
