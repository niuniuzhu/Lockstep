#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;
using XLua.CSObjectWrap;


namespace XLua
{
    public static class XLuaGenIniterRegister
	{
	    

	    public static void Init()
        {
		    XLua.InternalGlobals.extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			XLua.InternalGlobals.genTryArrayGetPtr = StaticLuaCallbacksEx.__tryArrayGet;
            XLua.InternalGlobals.genTryArraySetPtr = StaticLuaCallbacksEx.__tryArraySet;

		    XLua.LuaEnv.AddIniter((luaenv, translator) => {
			    
				translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Core.Math.Vec2), CoreMathVec2Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(Core.Math.Vec3), CoreMathVec3Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(Core.Math.Vec4), CoreMathVec4Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(Core.Math.Bounds), CoreMathBoundsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Core.Math.Rect), CoreMathRectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Misc.LLogger), LogicMiscLLoggerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Misc.FrameScheduler), LogicMiscFrameSchedulerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Misc.TimeScheduler), LogicMiscTimeSchedulerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Misc.Scheduler), LogicMiscSchedulerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Model.EntityFlag), LogicModelEntityFlagWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Model.EntityData), LogicModelEntityDataWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Model.BattleData), LogicModelBattleDataWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Battle), LogicBattleWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Controller.Entity), LogicControllerEntityWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Controller.Bio), LogicControllerBioWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Controller.Buff), LogicControllerBuffWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Logic.Test), LogicTestWrap.__Register);
				
				
				
			});
		}
	}
}
