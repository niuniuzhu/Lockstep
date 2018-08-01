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
    public class LogicControllerBuffWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Controller.Buff);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 28, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRef", _m_AddRef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RedRef", _m_RedRef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "radius", _g_get_radius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "areaFx", _g_get_areaFx);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "extra", _g_get_extra);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "extra_s", _g_get_extra_s);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "speed", _g_get_speed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxTriggerTargets", _g_get_maxTriggerTargets);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "perTargetTriggerCount", _g_get_perTargetTriggerCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxTriggerCount", _g_get_maxTriggerCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "trigger", _g_get_trigger);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "deadType", _g_get_deadType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spawnPoint", _g_get_spawnPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "orbit", _g_get_orbit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "canInterrupt", _g_get_canInterrupt);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "enterStates", _g_get_enterStates);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "triggerStates", _g_get_triggerStates);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "battle", _g_get_battle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "property", _g_get_property);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skillData", _g_get_skillData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "caster", _g_get_caster);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetPoint", _g_get_targetPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "campType", _g_get_campType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetFlag", _g_get_targetFlag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rangeType", _g_get_rangeType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rid", _g_get_rid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reference", _g_get_reference);
            
			
			
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
					
					Logic.Controller.Buff __cl_gen_ret = new Logic.Controller.Buff();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Controller.Buff constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool log = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.AddRef( log );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.AddRef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Controller.Buff.AddRef!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RedRef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool log = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.RedRef( log );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.RedRef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Controller.Buff.RedRef!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_radius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.radius);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_areaFx(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.areaFx);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_extra(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.extra);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_extra_s(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.extra_s);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.duration);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.speed);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxTriggerTargets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.maxTriggerTargets);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_perTargetTriggerCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.perTargetTriggerCount);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxTriggerCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.maxTriggerCount);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_trigger(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.trigger);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_deadType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.deadType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.id);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spawnPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.spawnPoint);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_orbit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.orbit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_canInterrupt(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.canInterrupt);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enterStates(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.enterStates);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_triggerStates(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.triggerStates);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_battle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.battle);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_property(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.property);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skillData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.skillData);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_caster(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.caster);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.target);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.targetPoint);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_campType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.campType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetFlag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.PushLogicModelEntityFlag(L, __cl_gen_to_be_invoked.targetFlag);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rangeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.rangeType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.rid);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reference(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Buff __cl_gen_to_be_invoked = (Logic.Controller.Buff)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.reference);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
