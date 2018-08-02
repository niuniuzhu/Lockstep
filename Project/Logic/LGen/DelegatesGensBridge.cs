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


namespace XLua
{
    public static class DelegatesGensBridge
    {
		
		public static void __Gen_Delegate_Imp0(this DelegateBridgeBase d, float p0)
		{
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (d.luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = d.rawL;
                int err_func =LuaAPI.load_error_func(L, d.errorFuncRef);
                
                
                LuaAPI.lua_getref(L, d.pLuaReference);
                
                LuaAPI.lua_pushnumber(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    d.pLuaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
		}
        
		public static void __Gen_Delegate_Imp1(this DelegateBridgeBase d, int p0, float p1, object p2)
		{
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (d.luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = d.rawL;
                int err_func =LuaAPI.load_error_func(L, d.errorFuncRef);
                ObjectTranslator translator = d.translator;
                
                LuaAPI.lua_getref(L, d.pLuaReference);
                
                LuaAPI.xlua_pushinteger(L, p0);
                LuaAPI.lua_pushnumber(L, p1);
                translator.PushAny(L, p2);
                
                int __gen_error = LuaAPI.lua_pcall(L, 3, 0, err_func);
                if (__gen_error != 0)
                    d.pLuaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
		}
        
		public static void __Gen_Delegate_Imp2(this DelegateBridgeBase d)
		{
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (d.luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = d.rawL;
                int err_func =LuaAPI.load_error_func(L, d.errorFuncRef);
                
                
                LuaAPI.lua_getref(L, d.pLuaReference);
                
                
                int __gen_error = LuaAPI.lua_pcall(L, 0, 0, err_func);
                if (__gen_error != 0)
                    d.pLuaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
		}
        
        
		public static void Init()
		{
		    DelegateBridge.Gen_Flag = true;

			DelegateBridgeBase.getDelegateByTypeHook = ( d, type ) =>
			{
			
				if (type == typeof(Logic.Misc.UpdateHandler))
				{
					return new Logic.Misc.UpdateHandler(d.__Gen_Delegate_Imp0);
				}
			
				if (type == typeof(Logic.Misc.TimerEntry.TimerHandler))
				{
					return new Logic.Misc.TimerEntry.TimerHandler(d.__Gen_Delegate_Imp1);
				}
			
				if (type == typeof(Logic.Misc.ScheduleEntry.ScheduleHandler))
				{
					return new Logic.Misc.ScheduleEntry.ScheduleHandler(d.__Gen_Delegate_Imp1);
				}
			
				if (type == typeof(Logic.Misc.TimerEntry.CompleteHandler))
				{
					return new Logic.Misc.TimerEntry.CompleteHandler(d.__Gen_Delegate_Imp2);
				}
			
				if (type == typeof(Logic.Misc.ScheduleEntry.CompleteHandler))
				{
					return new Logic.Misc.ScheduleEntry.CompleteHandler(d.__Gen_Delegate_Imp2);
				}
			
				return null;
			};
		}
	}
}