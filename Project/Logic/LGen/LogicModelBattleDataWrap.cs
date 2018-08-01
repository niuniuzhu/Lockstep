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
    public class LogicModelBattleDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Model.BattleData);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 14, 14);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadFromDef", _m_LoadFromDef);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "name", _g_get_name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "model", _g_get_model);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bornPos1", _g_get_bornPos1);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bornPos2", _g_get_bornPos2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bornDir1", _g_get_bornDir1);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bornDir2", _g_get_bornDir2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "basePoint1", _g_get_basePoint1);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "basePoint2", _g_get_basePoint2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bornRange", _g_get_bornRange);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "camera", _g_get_camera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "structures", _g_get_structures);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "neutrals", _g_get_neutrals);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "script", _g_get_script);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "name", _s_set_name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "model", _s_set_model);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bornPos1", _s_set_bornPos1);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bornPos2", _s_set_bornPos2);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bornDir1", _s_set_bornDir1);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bornDir2", _s_set_bornDir2);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "basePoint1", _s_set_basePoint1);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "basePoint2", _s_set_basePoint2);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bornRange", _s_set_bornRange);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "camera", _s_set_camera);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "structures", _s_set_structures);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "neutrals", _s_set_neutrals);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "script", _s_set_script);
            
			
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
					
					Logic.Model.BattleData __cl_gen_ret = new Logic.Model.BattleData();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Model.BattleData constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadFromDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.LoadFromDef( id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.model);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bornPos1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.bornPos1);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bornPos2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.bornPos2);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bornDir1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.bornDir1);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bornDir2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.bornDir2);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_basePoint1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.basePoint1);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_basePoint2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.basePoint2);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bornRange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.bornRange);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_camera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.camera);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_structures(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.structures);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_neutrals(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.neutrals);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.script);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.model = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bornPos1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.bornPos1 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bornPos2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.bornPos2 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bornDir1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.bornDir1 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bornDir2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.bornDir2 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_basePoint1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.basePoint1 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_basePoint2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.basePoint2 = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bornRange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.bornRange = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_camera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.camera = (Logic.Model.BattleData.Camera)translator.GetObject(L, 2, typeof(Logic.Model.BattleData.Camera));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_structures(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.structures = (System.Collections.Generic.Dictionary<string, Logic.Model.BattleData.Structure>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, Logic.Model.BattleData.Structure>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_neutrals(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.neutrals = (System.Collections.Generic.Dictionary<string, Logic.Model.BattleData.Neutral>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, Logic.Model.BattleData.Neutral>));
            
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
			
                Logic.Model.BattleData __cl_gen_to_be_invoked = (Logic.Model.BattleData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.script = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
