我这个库是做网络蜘蛛用的，根据scrapy改的。
我想分几部分，
	UrlAndParse : 这个类的作用很简单，只是保存网址和相关的解析操作的。
	DownLoaderBase : 专门处理下载的。
		方法：
			Start : 开始一个网址的
		事件：
			DocumentComplete ：作用是，下载完毕后，通知给调度器的。
	PiplineItemBase  : 输出数据的,要对数据有什么操作，由这个类继承。

	SchedulerBase  ：调度器，
		addUrl : 增加一个网址，顺便看看有没有重复的。
		DocumentComplete ： 网址下载后，首先调用蜘蛛解析（用事件吧），然后取得下一个网址，调用DownLoaderBase下载。
	SpiderBase ： 蜘蛛
		构造函数：
			
		方法：
			parse ：解析的，可以有parse1/parse2/parse3等等，不同的网址，不同的解析，解析的网址送到SchedulerBase，解析的数据送到PiplineItemBase
			addUrl ：增加一个要解析的网址和相应的解析方法，这个是调用SchedulerBase的addUrl
			sendData : 发送数据，这个是调用PiplineItemBase进行操作。