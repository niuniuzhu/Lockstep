#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class LogicMiscTimeSchedulerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Misc.TimeScheduler);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Register", _m_Register);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Unregister", _m_Unregister);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Logic.Misc.TimeScheduler __cl_gen_ret = new Logic.Misc.TimeScheduler();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.TimeScheduler constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Register(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.TimeScheduler __cl_gen_to_be_invoked = (Logic.Misc.TimeScheduler)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.TimerEntry>(L, 2)) 
                {
                    Logic.Misc.TimerEntry obj = (Logic.Misc.TimerEntry)translator.GetObject(L, 2, typeof(Logic.Misc.TimerEntry));
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( obj );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<Logic.Misc.TimerEntry.TimerHandler>(L, 5)&& translator.Assignable<Logic.Misc.TimerEntry.CompleteHandler>(L, 6)&& translator.Assignable<object>(L, 7)) 
                {
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    int repeat = LuaAPI.xlua_tointeger(L, 3);
                    bool startImmediately = LuaAPI.lua_toboolean(L, 4);
                    Logic.Misc.TimerEntry.TimerHandler timerCallback = translator.GetDelegate<Logic.Misc.TimerEntry.TimerHandler>(L, 5);
                    Logic.Misc.TimerEntry.CompleteHandler completeCallback = translator.GetDelegate<Logic.Misc.TimerEntry.CompleteHandler>(L, 6);
                    object param = translator.GetObject(L, 7, typeof(object));
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( interval, repeat, startImmediately, timerCallback, completeCallback, param );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<Logic.Misc.TimerEntry.TimerHandler>(L, 5)&& translator.Assignable<Logic.Misc.TimerEntry.CompleteHandler>(L, 6)) 
                {
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    int repeat = LuaAPI.xlua_tointeger(L, 3);
                    bool startImmediately = LuaAPI.lua_toboolean(L, 4);
                    Logic.Misc.TimerEntry.TimerHandler timerCallback = translator.GetDelegate<Logic.Misc.TimerEntry.TimerHandler>(L, 5);
                    Logic.Misc.TimerEntry.CompleteHandler completeCallback = translator.GetDelegate<Logic.Misc.TimerEntry.CompleteHandler>(L, 6);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( interval, repeat, startImmediately, timerCallback, completeCallback );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.TimeScheduler.Register!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unregister(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.TimeScheduler __cl_gen_to_be_invoked = (Logic.Misc.TimeScheduler)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    uint id = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.Unregister( id );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.TimerEntry.TimerHandler>(L, 2)) 
                {
                    Logic.Misc.TimerEntry.TimerHandler callback = translator.GetDelegate<Logic.Misc.TimerEntry.TimerHandler>(L, 2);
                    
                    __cl_gen_to_be_invoked.Unregister( callback );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.TimerEntry>(L, 2)) 
                {
                    Logic.Misc.TimerEntry obj = (Logic.Misc.TimerEntry)translator.GetObject(L, 2, typeof(Logic.Misc.TimerEntry));
                    
                    __cl_gen_to_be_invoked.Unregister( obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.TimeScheduler.Unregister!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.TimeScheduler __cl_gen_to_be_invoked = (Logic.Misc.TimeScheduler)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Misc.TimerEntry obj = (Logic.Misc.TimerEntry)translator.GetObject(L, 2, typeof(Logic.Misc.TimerEntry));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Contains( obj );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
