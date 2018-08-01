using System;
using System.Collections.Generic;
using Logic.Controller;
using Logic.Misc;
using Logic.Model;
using XLua;

namespace Logic
{
	public static class LuaGenConfig
	{
		[LuaCallCSharp]
		public static List<Type> LuaCallCSharp = new List<Type>() {
			typeof(object),
			typeof(Core.Math.Vec2),
			typeof(Core.Math.Vec3),
			typeof(Core.Math.Vec4),
			typeof(Core.Math.Bounds),
			typeof(Core.Math.Rect),
			typeof(LLogger),
			typeof(FrameScheduler),
			typeof(TimeScheduler),
			typeof(Scheduler),
			typeof(EntityFlag),
			typeof(EntityData),
			typeof(BattleData),
			typeof(Battle),
			typeof(Entity),
			typeof(Bio),
			typeof(Buff),
			typeof(Test),
		};

		[CSharpCallLua]
		public static List<Type> CSharpCallLua = new List<Type>() {
			typeof(UpdateHandler),
			typeof(TimerEntry.TimerHandler),
			typeof(TimerEntry.CompleteHandler),
			typeof(ScheduleEntry.ScheduleHandler),
			typeof(ScheduleEntry.CompleteHandler),
		};

		[GCOptimize]
		static List<Type> GCOptimize => new List<Type>() {
			typeof(Core.Math.Vec2),
			typeof(Core.Math.Vec3),
			typeof(Core.Math.Vec4),
			typeof(Core.Math.Color4),
			typeof(Core.Math.Bounds),
			typeof(Core.Math.Rect),
		};

		[BlackList]
		public static List<List<string>> BlackList = new List<List<string>>()  {
			new List<string>(){ "Client.Logic.Battle", "Dispose"},
			new List<string>(){ "Client.Logic.Battle", "Simulate", "System.Single", "System.Single"},
			new List<string>(){ "Client.Logic.Controller.Bio", "OnAttrChanged", "Client.Logic.Controller.Attr", "System.Object", "System.Object"},
			new List<string>(){ "Client.Logic.Controller.Entity", "OnAttrChanged", "Client.Logic.Controller.Attr", "System.Object", "System.Object"},
			new List<string>(){ "Client.Logic.Controller.Buff", "OnAttrChanged", "Client.Logic.Controller.Attr", "System.Object", "System.Object"},
			new List<string>(){ "Client.Logic.Misc.Scheduler", "Update", "System.Single"},
		};

	}
}