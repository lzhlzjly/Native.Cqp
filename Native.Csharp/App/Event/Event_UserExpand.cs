﻿using Native.Csharp.Sdk.Cqp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Event
{
	public class Event_UserExpand
	{
		#region --字段--
		private static readonly Lazy<Event_UserExpand> _instance = new Lazy<Event_UserExpand>(() => new Event_UserExpand());
		#endregion

		#region --属性--
		/// <summary>
		/// 获取 Event_UserExpand 实例对象
		/// </summary>
		public static Event_UserExpand Instance { get { return _instance.Value; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 隐藏构造函数
		/// </summary>
		private Event_UserExpand()
		{

		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 打开控制台窗口
		/// </summary>
		/// <param name="sender">触发此事件的对象</param>
		/// <param name="e">附加的参数</param>
		public void OpenConsoleWindow(object sender, EventArgs e)
		{
			//本事件将会在酷Q【主线程】中被触发, 请注意线程以免卡住酷Q
			
			//SDK支持载入 WinForm 或 WPF 类型的窗体, 建议您新建一个窗体项目, 引用到本项目中, 
			//然后 new 窗口进行载入.
		}
		#endregion
	}
}
