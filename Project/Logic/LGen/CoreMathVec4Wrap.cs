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
    public class CoreMathVec4Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Core.Math.Vec4);
			Utils.BeginObjectRegister(type, L, translator, 6, 15, 4, 4);
			Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__add", __AddMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__sub", __SubMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__unm", __UnmMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__mul", __MulMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__div", __DivMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__eq", __EqMeta);
            
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equals", _m_Equals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHashCode", _m_GetHashCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClampMagnitude", _m_ClampMagnitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Magnitude", _m_Magnitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SqrMagnitude", _m_SqrMagnitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Distance", _m_Distance);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DistanceSquared", _m_DistanceSquared);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Negate", _m_Negate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Scale", _m_Scale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dot", _m_Dot);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Normalize", _m_Normalize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NormalizeSafe", _m_NormalizeSafe);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AproxEqualsBox", _m_AproxEqualsBox);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApproxEquals", _m_ApproxEquals);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "x", _g_get_x);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "y", _g_get_y);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "z", _g_get_z);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "w", _g_get_w);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "x", _s_set_x);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "y", _s_set_y);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "z", _s_set_z);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "w", _s_set_w);
            
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, __NewIndexer,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 15, 13, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Distance", _m_Distance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DistanceSquared", _m_DistanceSquared_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClampMagnitude", _m_ClampMagnitude_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Normalize", _m_Normalize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NormalizeSafe", _m_NormalizeSafe_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Dot", _m_Dot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LerpUnclamped", _m_LerpUnclamped_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Lerp", _m_Lerp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Max", _m_Max_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Min", _m_Min_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Abs", _m_Abs_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Pow", _m_Pow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Floor", _m_Floor_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Round", _m_Round_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "one", _g_get_one);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "minusOne", _g_get_minusOne);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "zero", _g_get_zero);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "right", _g_get_right);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "left", _g_get_left);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "up", _g_get_up);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "down", _g_get_down);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "forward", _g_get_forward);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "backward", _g_get_backward);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "high", _g_get_high);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "low", _g_get_low);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "positiveInfinityVector", _g_get_positiveInfinityVector);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "negativeInfinityVector", _g_get_negativeInfinityVector);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 5 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					float x = (float)LuaAPI.lua_tonumber(L, 2);
					float y = (float)LuaAPI.lua_tonumber(L, 3);
					float z = (float)LuaAPI.lua_tonumber(L, 4);
					float w = (float)LuaAPI.lua_tonumber(L, 5);
					
					Core.Math.Vec4 __cl_gen_ret = new Core.Math.Vec4(x, y, z, w);
					translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
					return 1;
				}
				
				if (LuaAPI.lua_gettop(L) == 1)
				{
				    translator.PushCoreMathVec4(L, default(Core.Math.Vec4));
			        return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec4 constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					
					Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
					int index = LuaAPI.xlua_tointeger(L, 2);
					LuaAPI.lua_pushboolean(L, true);
					LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked[index]);
					return 2;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
			
            LuaAPI.lua_pushboolean(L, false);
			return 1;
        }
		
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __NewIndexer(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			try {
				
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3))
				{
					
					Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
					int key = LuaAPI.xlua_tointeger(L, 2);
					__cl_gen_to_be_invoked[key] = (float)LuaAPI.lua_tonumber(L, 3);
					LuaAPI.lua_pushboolean(L, true);
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
			
			LuaAPI.lua_pushboolean(L, false);
            return 1;
        }
		
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __AddMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside + rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of + operator, need Core.Math.Vec4!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __SubMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside - rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of - operator, need Core.Math.Vec4!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __UnmMeta(RealStatePtr L)
        {
            
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
                Core.Math.Vec4 rightside;translator.Get(L, 1, out rightside);
                translator.PushCoreMathVec4(L, - rightside);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __MulMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Mat4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Mat4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside * rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of * operator, need Core.Math.Vec4!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __DivMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec4(L, leftside / rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of / operator, need Core.Math.Vec4!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __EqMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec4>(L, 1) && translator.Assignable<Core.Math.Vec4>(L, 2))
				{
					Core.Math.Vec4 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec4 rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside == rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of == operator, need Core.Math.Vec4!");
            
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Equals( obj );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHashCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetHashCode(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClampMagnitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    float maxLength = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.ClampMagnitude( maxLength );
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Magnitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Magnitude(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SqrMagnitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.SqrMagnitude(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Distance(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 vector;translator.Get(L, 2, out vector);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Distance( vector );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DistanceSquared(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 vector;translator.Get(L, 2, out vector);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.DistanceSquared( vector );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Negate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Negate(  );
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Scale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 scale;translator.Get(L, 2, out scale);
                    
                    __cl_gen_to_be_invoked.Scale( scale );
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dot(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 vector;translator.Get(L, 2, out vector);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Dot( vector );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Normalize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Normalize(  );
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NormalizeSafe(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.NormalizeSafe(  );
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AproxEqualsBox(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 vector;translator.Get(L, 2, out vector);
                    float tolerance = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.AproxEqualsBox( vector, tolerance );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApproxEquals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec4 vector;translator.Get(L, 2, out vector);
                    float tolerance = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ApproxEquals( vector, tolerance );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Distance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec4 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec4.Distance( v0, v1 );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DistanceSquared_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec4 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec4.DistanceSquared( v0, v1 );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClampMagnitude_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    float maxLength = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.ClampMagnitude( v, maxLength );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Normalize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Normalize( v );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NormalizeSafe_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.NormalizeSafe( v );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dot_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec4 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec4.Dot( v0, v1 );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LerpUnclamped_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 from;translator.Get(L, 1, out from);
                    Core.Math.Vec4 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.LerpUnclamped( from, to, t );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Lerp_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 from;translator.Get(L, 1, out from);
                    Core.Math.Vec4 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Lerp( from, to, t );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Max_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec4>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Max( v, value );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec4>(L, 1)&& translator.Assignable<Core.Math.Vec4>(L, 2)) 
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    Core.Math.Vec4 values;translator.Get(L, 2, out values);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Max( v, values );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec4.Max!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Min_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec4>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Min( v, value );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec4>(L, 1)&& translator.Assignable<Core.Math.Vec4>(L, 2)) 
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    Core.Math.Vec4 values;translator.Get(L, 2, out values);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Min( v, values );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec4.Min!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Abs_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Abs( v );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pow_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    float power = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Pow( v, power );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Floor_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Floor( v );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Round_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec4 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec4 __cl_gen_ret = Core.Math.Vec4.Round( v );
                        translator.PushCoreMathVec4(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_one(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.one);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_minusOne(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.minusOne);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_zero(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.zero);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_right(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.right);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_left(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.left);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_up(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.up);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_down(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.down);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_forward(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.forward);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_backward(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.backward);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_high(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.high);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_low(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.low);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_positiveInfinityVector(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.positiveInfinityVector);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_negativeInfinityVector(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushCoreMathVec4(L, Core.Math.Vec4.negativeInfinityVector);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_x(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.x);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_y(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.y);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_z(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.z);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_w(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.w);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_x(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.x = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_y(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.y = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_z(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.z = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_w(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Vec4 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.w = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec4(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
