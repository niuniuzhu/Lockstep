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
    public class LogicBattleWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Battle);
			Utils.BeginObjectRegister(type, L, translator, 0, 18, 11, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RandomPoint", _m_RandomPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateBio", _m_CreateBio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateMissile", _m_CreateMissile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEntity", _m_GetEntity);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBio", _m_GetBio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMissile", _m_GetMissile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateBuff", _m_CreateBuff);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBuff", _m_GetBuff);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPathCorners", _m_GetPathCorners);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NavMeshRaycast", _m_NavMeshRaycast);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SampleNavPosition", _m_SampleNavPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleMove", _m_HandleMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleTrack", _m_HandleTrack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleUseSkill", _m_HandleUseSkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleRelive", _m_HandleRelive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleUpgradeSkill", _m_HandleUpgradeSkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Simulate", _m_Simulate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "rid", _g_get_rid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "frame", _g_get_frame);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "deltaTime", _g_get_deltaTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "time", _g_get_time);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "realTime", _g_get_realTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "data", _g_get_data);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "basePoint1", _g_get_basePoint1);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "basePoint2", _g_get_basePoint2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "random", _g_get_random);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timer0", _g_get_timer0);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaEnv", _g_get_luaEnv);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<Logic.BattleParams>(L, 2) && translator.Assignable<org.critterai.nav.Navmesh>(L, 3) && translator.Assignable<XLua.LuaEnv>(L, 4))
				{
					Logic.BattleParams param;translator.Get(L, 2, out param);
					org.critterai.nav.Navmesh navmesh = (org.critterai.nav.Navmesh)translator.GetObject(L, 3, typeof(org.critterai.nav.Navmesh));
					XLua.LuaEnv luaEnv = (XLua.LuaEnv)translator.GetObject(L, 4, typeof(XLua.LuaEnv));
					
					Logic.Battle __cl_gen_ret = new Logic.Battle(param, navmesh, luaEnv);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Battle constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RandomPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Core.Math.Vec3 center;translator.Get(L, 2, out center);
                    float range = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.RandomPoint( center, range );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateBio(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    Core.Math.Vec3 position;translator.Get(L, 3, out position);
                    Core.Math.Vec3 direction;translator.Get(L, 4, out direction);
                    int team = LuaAPI.xlua_tointeger(L, 5);
                    
                        Logic.Controller.Bio __cl_gen_ret = __cl_gen_to_be_invoked.CreateBio( id, position, direction, team );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateMissile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    Core.Math.Vec3 position;translator.Get(L, 3, out position);
                    Core.Math.Vec3 direction;translator.Get(L, 4, out direction);
                    
                        Logic.Controller.Missile __cl_gen_ret = __cl_gen_to_be_invoked.CreateMissile( id, position, direction );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEntity(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.Controller.Entity __cl_gen_ret = __cl_gen_to_be_invoked.GetEntity( rid );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBio(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.Controller.Bio __cl_gen_ret = __cl_gen_to_be_invoked.GetBio( rid );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMissile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.Controller.Missile __cl_gen_ret = __cl_gen_to_be_invoked.GetMissile( rid );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateBuff(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string buffId = LuaAPI.lua_tostring(L, 2);
                    string skillId = LuaAPI.lua_tostring(L, 3);
                    int lvl = LuaAPI.xlua_tointeger(L, 4);
                    Logic.Controller.Bio caster = (Logic.Controller.Bio)translator.GetObject(L, 5, typeof(Logic.Controller.Bio));
                    Logic.Controller.Bio target = (Logic.Controller.Bio)translator.GetObject(L, 6, typeof(Logic.Controller.Bio));
                    Core.Math.Vec3 targetPoint;translator.Get(L, 7, out targetPoint);
                    
                        Logic.Controller.Buff __cl_gen_ret = __cl_gen_to_be_invoked.CreateBuff( buffId, skillId, lvl, caster, target, targetPoint );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBuff(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.Controller.Buff __cl_gen_ret = __cl_gen_to_be_invoked.GetBuff( rid );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string ownerId = LuaAPI.lua_tostring(L, 2);
                    string buffId = LuaAPI.lua_tostring(L, 3);
                    
                        Logic.Controller.Buff __cl_gen_ret = __cl_gen_to_be_invoked.GetBuff( ownerId, buffId );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Battle.GetBuff!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPathCorners(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Core.Math.Vec3 src;translator.Get(L, 2, out src);
                    Core.Math.Vec3 dest;translator.Get(L, 3, out dest);
                    
                        Core.Math.Vec3[] __cl_gen_ret = __cl_gen_to_be_invoked.GetPathCorners( src, dest );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NavMeshRaycast(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Core.Math.Vec3 src;translator.Get(L, 2, out src);
                    Core.Math.Vec3 dest;translator.Get(L, 3, out dest);
                    Core.Math.Vec3 hitPosition;
                    Core.Math.Vec3 hitNormal;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.NavMeshRaycast( src, dest, out hitPosition, out hitNormal );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.PushCoreMathVec3(L, hitPosition);
                        
                    translator.PushCoreMathVec3(L, hitNormal);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SampleNavPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Core.Math.Vec3 searchPoint;translator.Get(L, 2, out searchPoint);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.SampleNavPosition( searchPoint );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    Core.Math.Vec3 targetPoint;translator.Get(L, 3, out targetPoint);
                    
                    __cl_gen_to_be_invoked.HandleMove( id, targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    string targetId = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.HandleTrack( id, targetId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleUseSkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sid = LuaAPI.lua_tostring(L, 2);
                    string srcId = LuaAPI.lua_tostring(L, 3);
                    string targetId = LuaAPI.lua_tostring(L, 4);
                    Core.Math.Vec3 targetPoint;translator.Get(L, 5, out targetPoint);
                    
                    __cl_gen_to_be_invoked.HandleUseSkill( sid, srcId, targetId, targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleRelive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.HandleRelive( rid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleUpgradeSkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string rid = LuaAPI.lua_tostring(L, 2);
                    string sid = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.HandleUpgradeSkill( rid, sid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Simulate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float realDeltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    float deltaTime = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.Simulate( realDeltaTime, deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.rid);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_frame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.frame);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_deltaTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.deltaTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_time(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.time);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_realTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.realTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.data);
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
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
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
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.basePoint2);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_random(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.random);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timer0(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.timer0);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Battle __cl_gen_to_be_invoked = (Logic.Battle)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.luaEnv);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
