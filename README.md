## Native.SDK 优点介绍

> 1. 程序集脱库打包
> 2. 原生c#开发体验
> 3. 完美翻译酷QApi
> 4. 支持酷Q应用打包
> 5. 支持代码实时调试

## Native.SDK 项目结构

![SDK结构](https://raw.githubusercontent.com/Jie2GG/Image_Folder/master/Sdk结构.png "SDK结构") <br/>

## Native.SDK 开发环境

>1. Visual Studio 2012 或更高版本
>2. Microsoft .Net Framework 4.0 **(XP系统支持的最后一个版本)**

## Native.SDK 开发流程

	1. 下载并打开 Native.VB
	2. 打开 Native.VB 项目属性, 修改 "应用程序" 中的 "程序集名称" 为你的AppId (具体参见 http://d.cqp.me/Pro/开发/基础信息)
	3. 展开 Native.VB 项目, 修改 "Native.Csharp.json" 文件名为你的AppId
	4. 打开 Native.VB -> Event 文件夹, 修改 "Event_AppInitialize.cs" 中 "AppInfo" 方法的 "e.AppId" 的值为你的AppId
	
	此时 Native.SDK 的开发环境已经配置成功!

## Native.SDK 调试流程

	1. 打开菜单 生成 -> 配置管理器, 修改 "Native.VB" 项目的生成方式为 "Debug x86" 生成方式
	2. 打开项目 Native.VB 项目属性, 修改 "生成" 中的 "输出路径" 至酷Q的 "app" 目录
	3. 修改 "调试" 中的 "启动操作" 为 "启动外部程序", 并且定位到酷Q主程序
	4. 打开菜单 工具 -> 选项 -> 调试, 关闭 "要求源文件与原始版本匹配" 选项
	
	此时 Native.SDK 已经可以进行实时调试!

## Native.SDK 常见问题

> 1. 关于 "Fody.WeavingTask" 任务意外失败.

	由于不知道为啥, 下载后的文件 "FodyIsolated.dll" (位于: packages\Fody2.0.0) 文件处于锁定状态, 请解锁该文件即可正常编译

## Native.SDK 已知问题
	
> 1. ~~对于 "EnApi.GetMsgFont" 方法, 暂时无法根据酷Q回传的指针获取字体信息, 暂时无法使用~~ <span style="color:red">(由于酷Q不解析此参数, 弃用)</span>
> 2. ~~对于 "HttpHelper.GetData" 方法, 抛出异常, 暂时无法使用~~ <font color=#FF0000>(已经修复, 但是封装了新的HTTP类, 弃用)</font>
> 3. ~~对于 "AuthCode" 被多插件共用, 导致应用之间串数据~~ <font color=#FF0000>(已修复)</font>
> 4. ~~对于接收消息时, 颜文字表情, 特殊符号乱码, 当前正在寻找转换方式~~ <font color=#FF0000>(已修复)</font>

## Native.SDK 更新日志
> 2019年01月23日 版本: V2.4.2

	1. 新增 IniObject.Load() 方法在加载了文件之后保持文件路径, 修改结束后可直接 Save() 不传路径参数保存
	2. 修复 IniObject.ToString() 方法的在转换 IniValue 时可能出错
	3. 补充 IniSection.ToString() 方法, 可以直接把当前实例转换为字符串, 可以直接被 IniObject.Parse() 解析
	4. 针对之后要推出的 Ini 配置项 序列化与反序列化问题做出优化

> 2019年01月22日 版本: V2.4.1

	1. 重载 IniSection 类的索引器, 现在获取值时 key 不存在不会抛异常, 而是返回 IniValue.Empty, 设置值时 key 不存在会直接调用 Add 方法将 key 加入到内部集合
	2. 重载 IniObject 类的 string 索引器, 现在设置 "节" 时, 不存在节会直接调用 Add 方法将节加入到内部集合

> 2019年01月21日 版本: V2.4.0
	
	说明: 本次更新主要解决 Native.Csharp.UI 项目不会被载入的问题, 描述为:当有多个 Native 开发的插件项目同时被酷
	      Q载入时, 会导致所有的插件项目只载入第一个 Native.Csharp.UI ! 所以请已经使用的 Native.Csharp.UI 项目
	      的开发者, 将现有的 Native.Charp.UI 项目的命名空间修改为其它命名空间(包括项目内的所有 xaml, cs)
	
	1. 移除 Native.Csharp.UI 项目, 保证后续被开发的项目窗体不会有冲突.
	
> 2019年01月21日 版本: V2.3.7

	1. 新增 IniValue 类针对基础数据类型转换时返回指定默认值的方法.

> 2019年01月20日 版本: V2.3.6

	1. 修复 IniObject 类针对解析过程中, 遇到 Value 中包含 "=", 从而导致匹配到的 Key 和 Value 不正确的问题 

> 2019年01月19日 版本: V2.3.5

	1. 新增 HttpWebClient 类针对 .Net Framework 4.0 的 https 的完整验证协议 感谢 @ycxYI[https://github.com/ycxYI]

> 2019年01月14日 版本: V2.3.4

	1. 修改 导出给酷Q的回调函数, 消息参数类型为 IntPtr 
	2. 修复 获取 酷Q GB18030 字符串, 转码异常的问题
	3. 修改 IniObject 类的继承类由 List<T> 转换为 Dictionary<TKey, TValue>
	4. 新增 IniObject 类的 string 类型索引器
	5. 新增 IniObject 类的 Add(IniSection) 方法, 默认以 IniSection.Name 作为键
	6. 新增 IniObject 类的 ToArray() 方法, 将返回一个 IniSection[]
	7. 重载 IniObject 类的 Add(string, IniSection) 方法, 无效化 string 参数, 默认以 IniSection.Name 作为键

> 2019年01月14日 版本: V2.3.3
	
	1. 还原 酷Q消息回调部分的导出函数的字符串指针 IntPtr -> String 类型, 修复此问题导致 酷Q 直接闪退
	
> 2019年01月11日 版本: V2.3.2

	1. 修复 传递给 酷Q 的消息编码不正确导致的许多文字在 QQ 无法正常显示的问题 感谢 @kotoneme[https://github.com/kotoneme], @gif8512 酷Q论坛[https://cqp.cc/home.php?mod=space&uid=454408&do=profile&from=space]
	2. 修复 酷Q 传递给 SDK 的消息由于编码不正确可能导致的开发问题 感谢 @kotoneme[https://github.com/kotoneme], @gif8512 酷Q论坛[https://cqp.cc/home.php?mod=space&uid=454408&do=profile&from=space]

> 2019年01月08日 版本: V2.3.1

	1. 修改 "nameof()" 方法调用为其等效的字符串形式, 修复 VS2012 编译报错

> 2018年12月29日 版本: V2.3.0
	
	说明: 此次更新改动较大, 请开发者在升级时备份好之前的代码!!!
	
	1. 分离了 Sdk.Cqp 为单独一个项目, 提升可移植性
	2. 修改 Native.Csharp.Sdk.Cqp.Api 中的 "EnApi" 的类名称为 "CqApi"
	3. 修改 "CqApi" 对象的构造方式, 由 "单例对象" 换为 "实例对象"
	4. 新增 "IniObject", "IniSection", "IniValue" , 位于 Native.Csharp.Tool.IniConfig.Linq (专门用于 Ini 配置项的类, 此类已完全面向对象)
	5. 弃用 Native.Csharp.Tool 中的 "IniFile" 类, (该类还能使用但是不再提供后续更新)

> 2018年12月13日 版本: V2.1.0

	1. 修复 DllExport 可能编译出失效的问题
  	2. 修复 异常处理 在try后依旧会向酷Q报告当前代码错误的问题
	3. 修复 异常处理 返回消息格式错误
	4. 修复 WPF窗体 多次加载会导致酷Q奔溃的问题
	5. 修复 "有效时间" 转换不正确 感谢 @BackRunner[https://github.com/backrunner]
	5. 分离 Sdk.Cqp.Tool -> Native.Csharp.Tool 提升代码可移植性

> 2018年12月07日 版本: V2.0.1

  	1. 修复 获取群列表永远为 null 感谢 @kotoneme[https://github.com/kotoneme]
	2. 修复 非简体中文系统下获取的字符串为乱码问题 感谢 @kotoneme[https://github.com/kotoneme]

> 2018年12月06日 版本: V2.0.0
	
	1. 重构 插件框架代码
	2. 修复 多插件同时运行时 "AuthCode" 发生串应用问题
	3. 优化 代码编译流程, 减少资源文件合并次数, 提升代码编译速率
	4. 优化 插件开发流程
	5. 新增 酷Q插件调试功能, 同时支持 Air/Pro 版本

> 2018年12月05日 版本: V1.1.2

	1. 修复 HttpWebClient 问题

> 2018年12月05日 版本: V1.1.1

	1. 尝试修复多应用由于 "AuthCode" 内存地址重复的问题导致调用API时会串应用的问题
	2. 优化SDK加载速度

> 2018年12月04日 版本: V1.1.0

	1. 由于酷Q废弃了消息接收事件中的 "font" 参数, SDK已经将其移除
	2. 修复 HttpHelper 类 "GetData" 方法中抛出异常
	3. 新增 HttpWebClient 类
	4. 新增 PUT, DELETE 请求方式
	5. 新增 在任何请求方式下 Cookies 提交, 回传, 自动合并更新
	6. 新增 在任何请求方式下 Headers 提交, 回传
	7. 新增 在任何请求方式下可以传入用于代理请求的 WebProxy 对象
	8. 新增 在任何请求方式下可以控制是否跟随响应 HTTP 服务器的重定向请求, 以及应和重定向次数
	9. 新增 可控制 "POST" 请求方式下的 "Content-Type" 标头的控制, 达到最大兼容性

> 2018年11月30日 版本: V1.0.0

	1. 打包上传项目
