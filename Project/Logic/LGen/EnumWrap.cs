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
    
    public class LogicModelEntityFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Logic.Model.EntityFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Logic.Model.EntityFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Logic.Model.EntityFlag), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Hero", Logic.Model.EntityFlag.Hero);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SmallPotato", Logic.Model.EntityFlag.SmallPotato);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Structure", Logic.Model.EntityFlag.Structure);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Missile", Logic.Model.EntityFlag.Missile);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Effect", Logic.Model.EntityFlag.Effect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Item", Logic.Model.EntityFlag.Item);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Logic.Model.EntityFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushLogicModelEntityFlag(L, (Logic.Model.EntityFlag)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Hero"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.Hero);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SmallPotato"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.SmallPotato);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Structure"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.Structure);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Missile"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.Missile);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Effect"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.Effect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Item"))
                {
                    translator.PushLogicModelEntityFlag(L, Logic.Model.EntityFlag.Item);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Logic.Model.EntityFlag!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Logic.Model.EntityFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}