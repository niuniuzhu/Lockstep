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
    public class CoreMathBoundsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Core.Math.Bounds);
			Utils.BeginObjectRegister(type, L, translator, 1, 11, 5, 5);
			Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__eq", __EqMeta);
            
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClosestPoint", _m_ClosestPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMinMax", _m_SetMinMax);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Encapsulate", _m_Encapsulate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Expand", _m_Expand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Intersect", _m_Intersect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IntersectMovingBoundsByAxis", _m_IntersectMovingBoundsByAxis);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IntersectMovingBounds", _m_IntersectMovingBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHashCode", _m_GetHashCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equals", _m_Equals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "center", _g_get_center);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "size", _g_get_size);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "extents", _g_get_extents);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "min", _g_get_min);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "max", _g_get_max);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "center", _s_set_center);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "size", _s_set_size);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "extents", _s_set_extents);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "min", _s_set_min);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "max", _s_set_max);
            
			
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
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<Core.Math.Vec3>(L, 2) && translator.Assignable<Core.Math.Vec3>(L, 3))
				{
					Core.Math.Vec3 center;translator.Get(L, 2, out center);
					Core.Math.Vec3 size;translator.Get(L, 3, out size);
					
					Core.Math.Bounds __cl_gen_ret = new Core.Math.Bounds(center, size);
					translator.PushCoreMathBounds(L, __cl_gen_ret);
                    
					return 1;
				}
				
				if (LuaAPI.lua_gettop(L) == 1)
				{
				    translator.PushCoreMathBounds(L, default(Core.Math.Bounds));
			        return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Bounds constructor!");
            
        }
        
		
        
		
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __EqMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<Core.Math.Bounds>(L, 1) && translator.Assignable<Core.Math.Bounds>(L, 2))
				{
					Core.Math.Bounds leftside;translator.Get(L, 1, out leftside);
					Core.Math.Bounds rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside == rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of == operator, need Core.Math.Bounds!");
            
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 point;translator.Get(L, 2, out point);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Contains( point );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClosestPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 point;translator.Get(L, 2, out point);
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.ClosestPoint( point );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 point;translator.Get(L, 2, out point);
                    float distance;
                    
                        Core.Math.Vec3 __cl_gen_ret = __cl_gen_to_be_invoked.ClosestPoint( point, out distance );
                        translator.PushCoreMathVec3(L, __cl_gen_ret);
                    LuaAPI.lua_pushnumber(L, distance);
                        
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Bounds.ClosestPoint!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMinMax(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Vec3 min;translator.Get(L, 2, out min);
                    Core.Math.Vec3 max;translator.Get(L, 3, out max);
                    
                    __cl_gen_to_be_invoked.SetMinMax( min, max );
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Encapsulate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 point;translator.Get(L, 2, out point);
                    
                    __cl_gen_to_be_invoked.Encapsulate( point );
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Bounds>(L, 2)) 
                {
                    Core.Math.Bounds bounds;translator.Get(L, 2, out bounds);
                    
                    __cl_gen_to_be_invoked.Encapsulate( bounds );
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Bounds.Encapsulate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Expand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float amount = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.Expand( amount );
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Vec3>(L, 2)) 
                {
                    Core.Math.Vec3 amount;translator.Get(L, 2, out amount);
                    
                    __cl_gen_to_be_invoked.Expand( amount );
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Bounds.Expand!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Intersect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Bounds>(L, 2)) 
                {
                    Core.Math.Bounds bounds;translator.Get(L, 2, out bounds);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( bounds );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Ray>(L, 2)) 
                {
                    Core.Math.Ray ray;translator.Get(L, 2, out ray);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( ray );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Sphere>(L, 2)) 
                {
                    Core.Math.Sphere sphere;translator.Get(L, 2, out sphere);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( sphere );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<Core.Math.Ray>(L, 2)) 
                {
                    Core.Math.Ray ray;translator.Get(L, 2, out ray);
                    float distance;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( ray, out distance );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    LuaAPI.lua_pushnumber(L, distance);
                        
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 2;
                }
                if(__gen_param_count == 3&& translator.Assignable<Core.Math.Ray>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Core.Math.Ray ray;translator.Get(L, 2, out ray);
                    float distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float parametric;
                    Core.Math.Vec3 normal;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( ray, distance, out parametric, out normal );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    LuaAPI.lua_pushnumber(L, parametric);
                        
                    translator.PushCoreMathVec3(L, normal);
                        
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 3;
                }
                if(__gen_param_count == 3&& translator.Assignable<Core.Math.Bounds>(L, 2)&& translator.Assignable<Core.Math.Bounds>(L, 3)) 
                {
                    Core.Math.Bounds bounds;translator.Get(L, 2, out bounds);
                    Core.Math.Bounds boundsIntersect;translator.Get(L, 3, out boundsIntersect);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Intersect( bounds, ref boundsIntersect );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.PushCoreMathBounds(L, boundsIntersect);
                        translator.UpdateCoreMathBounds(L, 3, boundsIntersect);
                        
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Math.Bounds.Intersect!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IntersectMovingBoundsByAxis(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Bounds bounds;translator.Get(L, 2, out bounds);
                    float d = (float)LuaAPI.lua_tonumber(L, 3);
                    Core.Math.Bounds.Axis axis;translator.Get(L, 4, out axis);
                    float t;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IntersectMovingBoundsByAxis( bounds, d, axis, out t );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    LuaAPI.lua_pushnumber(L, t);
                        
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IntersectMovingBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    Core.Math.Bounds bounds;translator.Get(L, 2, out bounds);
                    Core.Math.Vec3 d;translator.Get(L, 3, out d);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.IntersectMovingBounds( bounds, d );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetHashCode(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    object other = translator.GetObject(L, 2, typeof(object));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Equals( other );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
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
            
            
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_center(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.center);
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
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.size);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_extents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.extents);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_min(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.min);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_max(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.PushCoreMathVec3(L, __cl_gen_to_be_invoked.max);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_center(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.center = __cl_gen_value;
            
                translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
            
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
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.size = __cl_gen_value;
            
                translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_extents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.extents = __cl_gen_value;
            
                translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_min(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.min = __cl_gen_value;
            
                translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_max(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Core.Math.Bounds __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                Core.Math.Vec3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.max = __cl_gen_value;
            
                translator.UpdateCoreMathBounds(L, 1, __cl_gen_to_be_invoked);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
