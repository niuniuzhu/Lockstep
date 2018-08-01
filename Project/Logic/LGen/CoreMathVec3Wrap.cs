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
    public class CoreMathVec3Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Core.Math.Vec3);
			Utils.BeginObjectRegister(type, L, translator, 6, 25, 3, 3);
			Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__add", __AddMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__sub", __SubMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__unm", __UnmMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__mul", __MulMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__div", __DivMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__eq", __EqMeta);
            
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equals", _m_Equals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHashCode", _m_GetHashCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Set", _m_Set);
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
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Cross", _m_Cross);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AproxEqualsBox", _m_AproxEqualsBox);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApproxEquals", _m_ApproxEquals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RotateAround", _m_RotateAround);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IntersectsTriangle", _m_IntersectsTriangle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Reflect", _m_Reflect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Refract", _m_Refract);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InersectNormal", _m_InersectNormal);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InersectRay", _m_InersectRay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InersectLine", _m_InersectLine);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InersectPlane", _m_InersectPlane);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "x", _g_get_x);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "y", _g_get_y);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "z", _g_get_z);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "x", _s_set_x);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "y", _s_set_y);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "z", _s_set_z);
            
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, __NewIndexer,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 30, 11, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Distance", _m_Distance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DistanceSquared", _m_DistanceSquared_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Angle", _m_Angle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClampMagnitude", _m_ClampMagnitude_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Normalize", _m_Normalize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NormalizeSafe", _m_NormalizeSafe_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Dot", _m_Dot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Cross", _m_Cross_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OrthoNormalVector", _m_OrthoNormalVector_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SlerpUnclamped", _m_SlerpUnclamped_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Slerp", _m_Slerp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LerpUnclamped", _m_LerpUnclamped_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Lerp", _m_Lerp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SmoothDamp", _m_SmoothDamp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MoveTowards", _m_MoveTowards_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RotateTowards", _m_RotateTowards_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OrthoNormalize", _m_OrthoNormalize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Project", _m_Project_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ProjectOnPlane", _m_ProjectOnPlane_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Reflect", _m_Reflect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Hermite", _m_Hermite_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DegToRad", _m_DegToRad_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RadToDeg", _m_RadToDeg_xlua_st_);
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
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "positiveInfinityVector", _g_get_positiveInfinityVector);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "negativeInfinityVector", _g_get_negativeInfinityVector);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 4 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					float x = (float)LuaAPI.lua_tonumber(L, 2);
					float y = (float)LuaAPI.lua_tonumber(L, 3);
					float z = (float)LuaAPI.lua_tonumber(L, 4);
					
					Core.Math.Vec3 __cl_gen_ret = new Core.Math.Vec3(x, y, z);
					translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
					return 1;
				}
				
				if (LuaAPI.lua_gettop(L) == 1)
				{
				    translator.PushCoreMathVec3(L, default(Core.Math.Vec3));
			        return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec3 constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					
					Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
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
				
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3))
				{
					
					Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
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
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec3(L, leftside + rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside + rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of + operator, need Core.Math.Vec3!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __SubMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec3(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside - rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of - operator, need Core.Math.Vec3!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __UnmMeta(RealStatePtr L)
        {
            
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
                Core.Math.Vec3 rightside;translator.Get(L, 1, out rightside);
                translator.PushCoreMathVec3(L, - rightside);
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
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec3(L, leftside * rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside * rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of * operator, need Core.Math.Vec3!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __DivMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec2>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec2 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec2>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec2 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					float rightside = (float)LuaAPI.lua_tonumber(L, 2);
					
					translator.PushCoreMathVec3(L, leftside / rightside);
					
					return 1;
				}
            
			
				if (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					float leftside = (float)LuaAPI.lua_tonumber(L, 1);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					translator.PushCoreMathVec3(L, leftside / rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of / operator, need Core.Math.Vec3!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __EqMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Vec3>(L, 1) && translator.Assignable<Core.Math.Vec3>(L, 2))
				{
					Core.Math.Vec3 leftside;translator.Get(L, 1, out leftside);
					Core.Math.Vec3 rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside == rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of == operator, need Core.Math.Vec3!");
            
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Equals( obj );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetHashCode(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Set(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    float x = (float)LuaAPI.lua_tonumber(L, 2);
                    float y = (float)LuaAPI.lua_tonumber(L, 3);
                    float z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.Set( x, y, z );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    float maxLength = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.ClampMagnitude( maxLength );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Magnitude(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.SqrMagnitude(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 2, out vector);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Distance( vector );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 2, out vector);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.DistanceSquared( vector );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Negate(  );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 scale;translator.Get(L, 2, out scale);
                    
                    __cl_gen_to_be_invoked.Scale( scale );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 2, out v);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.Dot( v );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Normalize(  );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.NormalizeSafe(  );
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Cross(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 2, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.Cross( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 2, out vector);
                    float tolerance = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.AproxEqualsBox( vector, tolerance );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 2, out vector);
                    float tolerance = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ApproxEquals( vector, tolerance );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RotateAround(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    float angle = (float)LuaAPI.lua_tonumber(L, 2);
                    Core.Math.Vec3 axis;translator.Get(L, 3, out axis);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.RotateAround( angle, axis );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IntersectsTriangle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 p0;translator.Get(L, 2, out p0);
                    Core.Math.Vec3 p1;translator.Get(L, 3, out p1);
                    Core.Math.Vec3 p2;translator.Get(L, 4, out p2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IntersectsTriangle( p0, p1, p2 );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reflect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 planeNormal;translator.Get(L, 2, out planeNormal);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.Reflect( planeNormal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Refract(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 normal;translator.Get(L, 2, out normal);
                    float refractionIndex = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.Refract( normal, refractionIndex );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InersectNormal(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 normal;translator.Get(L, 2, out normal);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.InersectNormal( normal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InersectRay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 rayOrigin;translator.Get(L, 2, out rayOrigin);
                    Core.Math.Vec3 rayDirection;translator.Get(L, 3, out rayDirection);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.InersectRay( rayOrigin, rayDirection );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InersectLine(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Line3 line;translator.Get(L, 2, out line);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.InersectLine( line );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InersectPlane(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 planeNormal;translator.Get(L, 2, out planeNormal);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.InersectPlane( planeNormal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<Core.Math.Vec3>(L, 2)&& translator.Assignable<Core.Math.Vec3>(L, 3)) 
                {
                    Core.Math.Vec3 planeNormal;translator.Get(L, 2, out planeNormal);
                    Core.Math.Vec3 planeLocation;translator.Get(L, 3, out planeLocation);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.InersectPlane( planeNormal, planeLocation );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec3.InersectPlane!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Distance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec3.Distance( v0, v1 );
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
                    Core.Math.Vec3 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec3.DistanceSquared( v0, v1 );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Angle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 from;translator.Get(L, 1, out from);
                    Core.Math.Vec3 to;translator.Get(L, 2, out to);
                    
                        float __cl_gen_ret = Core.Math.Vec3.Angle( from, to );
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    float maxLength = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.ClampMagnitude( v, maxLength );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Normalize( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.NormalizeSafe( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        float __cl_gen_ret = Core.Math.Vec3.Dot( v0, v1 );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Cross_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v0;translator.Get(L, 1, out v0);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Cross( v0, v1 );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OrthoNormalVector_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.OrthoNormalVector( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SlerpUnclamped_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 from;translator.Get(L, 1, out from);
                    Core.Math.Vec3 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.SlerpUnclamped( from, to, t );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Slerp_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 from;translator.Get(L, 1, out from);
                    Core.Math.Vec3 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Slerp( from, to, t );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 from;translator.Get(L, 1, out from);
                    Core.Math.Vec3 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.LerpUnclamped( from, to, t );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 from;translator.Get(L, 1, out from);
                    Core.Math.Vec3 to;translator.Get(L, 2, out to);
                    float t = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Lerp( from, to, t );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SmoothDamp_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 current;translator.Get(L, 1, out current);
                    Core.Math.Vec3 target;translator.Get(L, 2, out target);
                    Core.Math.Vec3 currentVelocity;translator.Get(L, 3, out currentVelocity);
                    float smoothTime = (float)LuaAPI.lua_tonumber(L, 4);
                    float maxSpeed = (float)LuaAPI.lua_tonumber(L, 5);
                    float deltaTime = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.SmoothDamp( current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    translator.PushCoreMathVec3(L, currentVelocity);
                        translator.UpdateCoreMathVec3(L, 3, currentVelocity);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveTowards_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 current;translator.Get(L, 1, out current);
                    Core.Math.Vec3 target;translator.Get(L, 2, out target);
                    float maxDistanceDelta = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.MoveTowards( current, target, maxDistanceDelta );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RotateTowards_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 current;translator.Get(L, 1, out current);
                    Core.Math.Vec3 target;translator.Get(L, 2, out target);
                    float maxRadiansDelta = (float)LuaAPI.lua_tonumber(L, 3);
                    float maxMagnitudeDelta = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.RotateTowards( current, target, maxRadiansDelta, maxMagnitudeDelta );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OrthoNormalize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 va;translator.Get(L, 1, out va);
                    Core.Math.Vec3 vb;translator.Get(L, 2, out vb);
                    Core.Math.Vec3 vc;translator.Get(L, 3, out vc);
                    
                    Core.Math.Vec3.OrthoNormalize( ref va, ref vb, ref vc );
                    translator.PushCoreMathVec3(L, va);
                        translator.UpdateCoreMathVec3(L, 1, va);
                        
                    translator.PushCoreMathVec3(L, vb);
                        translator.UpdateCoreMathVec3(L, 2, vb);
                        
                    translator.PushCoreMathVec3(L, vc);
                        translator.UpdateCoreMathVec3(L, 3, vc);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Project_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 1, out vector);
                    Core.Math.Vec3 onNormal;translator.Get(L, 2, out onNormal);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Project( vector, onNormal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ProjectOnPlane_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 vector;translator.Get(L, 1, out vector);
                    Core.Math.Vec3 planeNormal;translator.Get(L, 2, out planeNormal);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.ProjectOnPlane( vector, planeNormal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reflect_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 inDirection;translator.Get(L, 1, out inDirection);
                    Core.Math.Vec3 inNormal;translator.Get(L, 2, out inNormal);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Reflect( inDirection, inNormal );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Hermite_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 value1;translator.Get(L, 1, out value1);
                    Core.Math.Vec3 tangent1;translator.Get(L, 2, out tangent1);
                    Core.Math.Vec3 value2;translator.Get(L, 3, out value2);
                    Core.Math.Vec3 tangent2;translator.Get(L, 4, out tangent2);
                    float t = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Hermite( value1, tangent1, value2, tangent2, t );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DegToRad_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.DegToRad( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RadToDeg_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.RadToDeg( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Max( v, value );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 1)&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Max( v, v1 );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec3.Max!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Min_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    float v1 = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Min( v, v1 );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 1)&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    Core.Math.Vec3 v1;translator.Get(L, 2, out v1);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Min( v, v1 );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Vec3.Min!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Abs_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Abs( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Pow( v, value );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Floor( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
                    Core.Math.Vec3 v;translator.Get(L, 1, out v);
                    
                        Core.Math.Vec3 __cl_gen_ret = Core.Math.Vec3.Round( v );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                    
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.one);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.minusOne);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.zero);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.right);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.left);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.up);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.down);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.forward);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.backward);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.positiveInfinityVector);
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
			    translator.PushCoreMathVec3(L, Core.Math.Vec3.negativeInfinityVector);
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.z);
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.x = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
            
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.y = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
            
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
			
                Core.Math.Vec3 __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                __cl_gen_to_be_invoked.z = (float)LuaAPI.lua_tonumber(L, 2);
            
                translator.UpdateCoreMathVec3(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
