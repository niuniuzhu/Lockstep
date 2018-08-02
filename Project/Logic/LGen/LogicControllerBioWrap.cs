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
    public class LogicControllerBioWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.Controller.Bio);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 14, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSkill", _m_GetSkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanCharmed", _m_CanCharmed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanMove", _m_CanMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanUseSkill", _m_CanUseSkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WithinFov", _m_WithinFov);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WithinSkillRange", _m_WithinSkillRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeState", _m_ChangeState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyAllDisableBuffStates", _m_DestroyAllDisableBuffStates);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBuffState", _m_GetBuffState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UseSkill", _m_UseSkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Move", _m_Move);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Track", _m_Track);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pursue", _m_Pursue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Attack", _m_Attack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Relive", _m_Relive);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uid", _g_get_uid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reliveTime", _g_get_reliveTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reliveGold", _g_get_reliveGold);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "goldBountyAwarded", _g_get_goldBountyAwarded);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "expBountyAwarded", _g_get_expBountyAwarded);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "upgradeExpNeeded", _g_get_upgradeExpNeeded);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "upgradeSkillPointObtained", _g_get_upgradeSkillPointObtained);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fov", _g_get_fov);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skills", _g_get_skills);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "commonSkill", _g_get_commonSkill);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "usingSkill", _g_get_usingSkill);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fsm", _g_get_fsm);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isDead", _g_get_isDead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sensorySystem", _g_get_sensorySystem);
            
			
			
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
					
					Logic.Controller.Bio __cl_gen_ret = new Logic.Controller.Bio();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Controller.Bio constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.Controller.Skill __cl_gen_ret = __cl_gen_to_be_invoked.GetSkill( id );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanCharmed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.CanCharmed(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.CanMove(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanUseSkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.CanUseSkill( id );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WithinFov(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Entity target = (Logic.Controller.Entity)translator.GetObject(L, 2, typeof(Logic.Controller.Entity));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.WithinFov( target );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WithinSkillRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Entity target = (Logic.Controller.Entity)translator.GetObject(L, 2, typeof(Logic.Controller.Entity));
                    Logic.Controller.Skill skill = (Logic.Controller.Skill)translator.GetObject(L, 3, typeof(Logic.Controller.Skill));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.WithinSkillRange( target, skill );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count >= 3&& translator.Assignable<Logic.FSM.FSMStateType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 4) || translator.Assignable<object>(L, 4))) 
                {
                    Logic.FSM.FSMStateType type;translator.Get(L, 2, out type);
                    bool force = LuaAPI.lua_toboolean(L, 3);
                    object[] param = translator.GetParams<object>(L, 4);
                    
                    __cl_gen_to_be_invoked.ChangeState( type, force, param );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<Logic.FSM.FSMStateType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    Logic.FSM.FSMStateType type;translator.Get(L, 2, out type);
                    bool force = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.ChangeState( type, force );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 1&& translator.Assignable<Logic.FSM.FSMStateType>(L, 2)) 
                {
                    Logic.FSM.FSMStateType type;translator.Get(L, 2, out type);
                    
                    __cl_gen_to_be_invoked.ChangeState( type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.Controller.Bio.ChangeState!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAllDisableBuffStates(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DestroyAllDisableBuffStates(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBuffState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string id = LuaAPI.lua_tostring(L, 2);
                    
                        Logic.BuffStateImpl.BSBase __cl_gen_ret = __cl_gen_to_be_invoked.GetBuffState( id );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UseSkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Skill skill = (Logic.Controller.Skill)translator.GetObject(L, 2, typeof(Logic.Controller.Skill));
                    Logic.Controller.Bio target = (Logic.Controller.Bio)translator.GetObject(L, 3, typeof(Logic.Controller.Bio));
                    Core.Math.Vec3 targetPoint;translator.Get(L, 4, out targetPoint);
                    
                    __cl_gen_to_be_invoked.UseSkill( skill, target, targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Core.Math.Vec3 targetPoint;translator.Get(L, 2, out targetPoint);
                    
                    __cl_gen_to_be_invoked.Move( targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Track(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Bio target = (Logic.Controller.Bio)translator.GetObject(L, 2, typeof(Logic.Controller.Bio));
                    
                    __cl_gen_to_be_invoked.Track( target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pursue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Skill skill = (Logic.Controller.Skill)translator.GetObject(L, 2, typeof(Logic.Controller.Skill));
                    Logic.Controller.Bio target = (Logic.Controller.Bio)translator.GetObject(L, 3, typeof(Logic.Controller.Bio));
                    Core.Math.Vec3 targetPoint;translator.Get(L, 4, out targetPoint);
                    
                    __cl_gen_to_be_invoked.Pursue( skill, target, targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Attack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Logic.Controller.Skill skill = (Logic.Controller.Skill)translator.GetObject(L, 2, typeof(Logic.Controller.Skill));
                    Logic.Controller.Bio target = (Logic.Controller.Bio)translator.GetObject(L, 3, typeof(Logic.Controller.Bio));
                    Core.Math.Vec3 targetPoint;translator.Get(L, 4, out targetPoint);
                    
                    __cl_gen_to_be_invoked.Attack( skill, target, targetPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Relive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Relive(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.uid);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reliveTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.reliveTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reliveGold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.reliveGold);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_goldBountyAwarded(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.goldBountyAwarded);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_expBountyAwarded(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.expBountyAwarded);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_upgradeExpNeeded(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.upgradeExpNeeded);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_upgradeSkillPointObtained(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.upgradeSkillPointObtained);
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
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.fov);
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
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.skills);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_commonSkill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.commonSkill);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_usingSkill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.usingSkill);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fsm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.fsm);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isDead);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sensorySystem(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Logic.Controller.Bio __cl_gen_to_be_invoked = (Logic.Controller.Bio)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.sensorySystem);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
