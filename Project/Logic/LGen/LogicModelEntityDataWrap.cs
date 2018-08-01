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
    public class LogicModelEntityDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Model.EntityData);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 32, 32);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "name", _g_get_name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "model", _g_get_model);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "noShadow", _g_get_noShadow);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "flag", _g_get_flag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "size", _g_get_size);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mass", _g_get_mass);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "volumetric", _g_get_volumetric);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fov", _g_get_fov);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "speed", _g_get_speed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rotSpeed", _g_get_rotSpeed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "scale", _g_get_scale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "trackDistance", _g_get_trackDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "goldBase", _g_get_goldBase);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skillPointBase", _g_get_skillPointBase);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "firingPoint", _g_get_firingPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hitPoint", _g_get_hitPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "destructImmediately", _g_get_destructImmediately);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lifeTime", _g_get_lifeTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "levels", _g_get_levels);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skills", _g_get_skills);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "aiDatas", _g_get_aiDatas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "script", _g_get_script);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "flightType", _g_get_flightType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "arc", _g_get_arc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hitFx", _g_get_hitFx);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shaderName", _g_get_shaderName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shadowVisible", _g_get_shadowVisible);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "positionType", _g_get_positionType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rotationType", _g_get_rotationType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spare", _g_get_spare);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "name", _s_set_name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "model", _s_set_model);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "noShadow", _s_set_noShadow);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "flag", _s_set_flag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "size", _s_set_size);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mass", _s_set_mass);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "volumetric", _s_set_volumetric);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fov", _s_set_fov);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "speed", _s_set_speed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rotSpeed", _s_set_rotSpeed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "scale", _s_set_scale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "trackDistance", _s_set_trackDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "goldBase", _s_set_goldBase);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "skillPointBase", _s_set_skillPointBase);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "firingPoint", _s_set_firingPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hitPoint", _s_set_hitPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "destructImmediately", _s_set_destructImmediately);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "lifeTime", _s_set_lifeTime);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "levels", _s_set_levels);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "skills", _s_set_skills);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "aiDatas", _s_set_aiDatas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "script", _s_set_script);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "flightType", _s_set_flightType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "duration", _s_set_duration);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "arc", _s_set_arc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hitFx", _s_set_hitFx);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shaderName", _s_set_shaderName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shadowVisible", _s_set_shadowVisible);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "positionType", _s_set_positionType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rotationType", _s_set_rotationType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spare", _s_set_spare);
            
			
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
					
					Logic.Model.EntityData __cl_gen_ret = new Logic.Model.EntityData();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Model.EntityData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.id);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.name);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.model);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_noShadow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.noShadow);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_flag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.PushLogicModelEntityFlag(L, __cl_gen_to_be_invoked.flag);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_size(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.size);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mass(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.mass);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_volumetric(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.volumetric);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fov(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.fov);
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
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.speed);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rotSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.rotSpeed);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_scale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.scale);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_trackDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.trackDistance);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_goldBase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.goldBase);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skillPointBase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.skillPointBase);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_firingPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.firingPoint);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hitPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.hitPoint);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_destructImmediately(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.destructImmediately);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lifeTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.lifeTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_levels(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.levels);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skills(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.skills);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_aiDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.aiDatas);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_script(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.script);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_flightType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.flightType);
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
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.duration);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_arc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.arc);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hitFx(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.hitFx);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shaderName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.shaderName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shadowVisible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.shadowVisible);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_positionType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.positionType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rotationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.rotationType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spare(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.spare);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.id = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.name = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.model = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_noShadow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.noShadow = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_flag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Logic.Model.EntityFlag __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.flag = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_size(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.size = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mass(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.mass = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_volumetric(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.volumetric = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fov(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.fov = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.speed = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rotSpeed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.rotSpeed = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_scale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.scale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_trackDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.trackDistance = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_goldBase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.goldBase = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_skillPointBase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.skillPointBase = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_firingPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                System.Nullable<Core.Math.Vec3> __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.firingPoint = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hitPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                System.Nullable<Core.Math.Vec3> __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.hitPoint = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_destructImmediately(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.destructImmediately = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_lifeTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.lifeTime = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_levels(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.levels = (Logic.Model.EntityData.Level[])translator.GetObject(L, 2, typeof(Logic.Model.EntityData.Level[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_skills(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.skills = (string[])translator.GetObject(L, 2, typeof(string[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_aiDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.aiDatas = (Logic.Model.AIData[])translator.GetObject(L, 2, typeof(Logic.Model.AIData[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_script(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.script = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_flightType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Logic.Model.FlightType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.flightType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.duration = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_arc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.arc = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hitFx(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.hitFx = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shaderName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.shaderName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shadowVisible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.shadowVisible = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_positionType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Logic.Model.EffectPositionType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.positionType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rotationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Logic.Model.EffectRotationType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.rotationType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spare(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.EntityData __cl_gen_to_be_invoked = (Logic.Model.EntityData)translator.FastGetCSObj(L, 1);
                Logic.Model.Spare __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.spare = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
