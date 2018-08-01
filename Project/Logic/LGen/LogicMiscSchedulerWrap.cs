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
    public class LogicMiscSchedulerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Misc.Scheduler);
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
					
					Logic.Misc.Scheduler __cl_gen_ret = new Logic.Misc.Scheduler();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.Scheduler constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Register(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.Scheduler __cl_gen_to_be_invoked = (Logic.Misc.Scheduler)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.ScheduleEntry>(L, 2)) 
                {
                    Logic.Misc.ScheduleEntry obj = (Logic.Misc.ScheduleEntry)translator.GetObject(L, 2, typeof(Logic.Misc.ScheduleEntry));
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( obj );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& translator.Assignable<float[]>(L, 2)&& translator.Assignable<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 3)&& translator.Assignable<Logic.Misc.ScheduleEntry.CompleteHandler>(L, 4)&& translator.Assignable<object>(L, 5)) 
                {
                    float[] times = (float[])translator.GetObject(L, 2, typeof(float[]));
                    Logic.Misc.ScheduleEntry.ScheduleHandler timerCallback = translator.GetDelegate<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 3);
                    Logic.Misc.ScheduleEntry.CompleteHandler completeCallback = translator.GetDelegate<Logic.Misc.ScheduleEntry.CompleteHandler>(L, 4);
                    object param = translator.GetObject(L, 5, typeof(object));
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( times, timerCallback, completeCallback, param );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<float[]>(L, 2)&& translator.Assignable<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 3)&& translator.Assignable<Logic.Misc.ScheduleEntry.CompleteHandler>(L, 4)) 
                {
                    float[] times = (float[])translator.GetObject(L, 2, typeof(float[]));
                    Logic.Misc.ScheduleEntry.ScheduleHandler timerCallback = translator.GetDelegate<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 3);
                    Logic.Misc.ScheduleEntry.CompleteHandler completeCallback = translator.GetDelegate<Logic.Misc.ScheduleEntry.CompleteHandler>(L, 4);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.Register( times, timerCallback, completeCallback );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.Scheduler.Register!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unregister(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.Scheduler __cl_gen_to_be_invoked = (Logic.Misc.Scheduler)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    uint id = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.Unregister( id );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 2)) 
                {
                    Logic.Misc.ScheduleEntry.ScheduleHandler callback = translator.GetDelegate<Logic.Misc.ScheduleEntry.ScheduleHandler>(L, 2);
                    
                    __cl_gen_to_be_invoked.Unregister( callback );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Logic.Misc.ScheduleEntry>(L, 2)) 
                {
                    Logic.Misc.ScheduleEntry obj = (Logic.Misc.ScheduleEntry)translator.GetObject(L, 2, typeof(Logic.Misc.ScheduleEntry));
                    
                    __cl_gen_to_be_invoked.Unregister( obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Misc.Scheduler.Unregister!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Misc.Scheduler __cl_gen_to_be_invoked = (Logic.Misc.Scheduler)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Misc.ScheduleEntry obj = (Logic.Misc.ScheduleEntry)translator.GetObject(L, 2, typeof(Logic.Misc.ScheduleEntry));
                    
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
