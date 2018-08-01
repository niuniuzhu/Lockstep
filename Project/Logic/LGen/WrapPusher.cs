#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public static class WrapPusher
    {
        
        public static void Init()
		{
			LuaEnv.AddIniter(  ( luaenv, t ) =>
			{
				t.RegisterPushAndGetAndUpdate<Core.Math.Vec2>(t.PushCoreMathVec2, t.GetEx, t.UpdateCoreMathVec2);
				t.RegisterPushAndGetAndUpdate<Core.Math.Vec3>(t.PushCoreMathVec3, t.GetEx, t.UpdateCoreMathVec3);
				t.RegisterPushAndGetAndUpdate<Core.Math.Vec4>(t.PushCoreMathVec4, t.GetEx, t.UpdateCoreMathVec4);
				t.RegisterPushAndGetAndUpdate<Core.Math.Color4>(t.PushCoreMathColor4, t.GetEx, t.UpdateCoreMathColor4);
				t.RegisterPushAndGetAndUpdate<Core.Math.Bounds>(t.PushCoreMathBounds, t.GetEx, t.UpdateCoreMathBounds);
				t.RegisterPushAndGetAndUpdate<Core.Math.Rect>(t.PushCoreMathRect, t.GetEx, t.UpdateCoreMathRect);
				t.RegisterPushAndGetAndUpdate<Logic.Model.EntityFlag>(t.PushLogicModelEntityFlag, t.GetEx, t.UpdateLogicModelEntityFlag);
			} );
		}
        
        
		static int CoreMathVec2_TypeID = -1;

        public static void PushCoreMathVec2(this ObjectTranslator t, RealStatePtr L, Core.Math.Vec2 val)
        {
            if (CoreMathVec2_TypeID == -1)
            {
			    bool is_first;
                CoreMathVec2_TypeID = t.getTypeId(L, typeof(Core.Math.Vec2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, CoreMathVec2_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Vec2 ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Vec2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec2_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec2");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Vec2");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Vec2)t.objectCasters.GetCaster(typeof(Core.Math.Vec2))(L, index, null);
        }
		
        public static void UpdateCoreMathVec2(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Vec2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec2_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec2");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Vec2 ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int CoreMathVec3_TypeID = -1;

        public static void PushCoreMathVec3(this ObjectTranslator t, RealStatePtr L, Core.Math.Vec3 val)
        {
            if (CoreMathVec3_TypeID == -1)
            {
			    bool is_first;
                CoreMathVec3_TypeID = t.getTypeId(L, typeof(Core.Math.Vec3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, CoreMathVec3_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Vec3 ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Vec3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec3_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec3");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Vec3");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Vec3)t.objectCasters.GetCaster(typeof(Core.Math.Vec3))(L, index, null);
        }
		
        public static void UpdateCoreMathVec3(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Vec3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec3_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec3");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Vec3 ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int CoreMathVec4_TypeID = -1;

        public static void PushCoreMathVec4(this ObjectTranslator t, RealStatePtr L, Core.Math.Vec4 val)
        {
            if (CoreMathVec4_TypeID == -1)
            {
			    bool is_first;
                CoreMathVec4_TypeID = t.getTypeId(L, typeof(Core.Math.Vec4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, CoreMathVec4_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Vec4 ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Vec4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec4_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec4");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Vec4");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Vec4)t.objectCasters.GetCaster(typeof(Core.Math.Vec4))(L, index, null);
        }
		
        public static void UpdateCoreMathVec4(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Vec4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathVec4_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Vec4");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Vec4 ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int CoreMathColor4_TypeID = -1;

        public static void PushCoreMathColor4(this ObjectTranslator t, RealStatePtr L, Core.Math.Color4 val)
        {
            if (CoreMathColor4_TypeID == -1)
            {
			    bool is_first;
                CoreMathColor4_TypeID = t.getTypeId(L, typeof(Core.Math.Color4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, CoreMathColor4_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Color4 ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Color4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathColor4_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Color4");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Color4");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Color4)t.objectCasters.GetCaster(typeof(Core.Math.Color4))(L, index, null);
        }
		
        public static void UpdateCoreMathColor4(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Color4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathColor4_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Color4");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Color4 ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int CoreMathBounds_TypeID = -1;

        public static void PushCoreMathBounds(this ObjectTranslator t, RealStatePtr L, Core.Math.Bounds val)
        {
            if (CoreMathBounds_TypeID == -1)
            {
			    bool is_first;
                CoreMathBounds_TypeID = t.getTypeId(L, typeof(Core.Math.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 0, CoreMathBounds_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Bounds ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathBounds_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Bounds");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Bounds");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Bounds)t.objectCasters.GetCaster(typeof(Core.Math.Bounds))(L, index, null);
        }
		
        public static void UpdateCoreMathBounds(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathBounds_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Bounds");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Bounds ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int CoreMathRect_TypeID = -1;

        public static void PushCoreMathRect(this ObjectTranslator t, RealStatePtr L, Core.Math.Rect val)
        {
            if (CoreMathRect_TypeID == -1)
            {
			    bool is_first;
                CoreMathRect_TypeID = t.getTypeId(L, typeof(Core.Math.Rect), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 0, CoreMathRect_TypeID);
            if (!PackUnpack.Pack(buff, 0, val))
                throw new Exception("pack fail fail for Core.Math.Rect ,value="+val);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Core.Math.Rect val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathRect_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Rect");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				if (!PackUnpack.UnPack(buff, 0, out val))
                    throw new Exception("unpack fail for Core.Math.Rect");
                
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			    PackUnpack.UnPack(t, L, index, out val);
            else
                val = (Core.Math.Rect)t.objectCasters.GetCaster(typeof(Core.Math.Rect))(L, index, null);
        }
		
        public static void UpdateCoreMathRect(this ObjectTranslator t, RealStatePtr L, int index, Core.Math.Rect val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CoreMathRect_TypeID)
				    throw new Exception("invalid userdata for Core.Math.Rect");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!PackUnpack.Pack(buff, 0,  val))
                    throw new Exception("pack fail for Core.Math.Rect ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		static int LogicModelEntityFlag_TypeID = -1;
		static int LogicModelEntityFlag_EnumRef = -1;
        

        public static void PushLogicModelEntityFlag(this ObjectTranslator t, RealStatePtr L, Logic.Model.EntityFlag val)
        {
            if (LogicModelEntityFlag_TypeID == -1)
            {
			    bool is_first;
                LogicModelEntityFlag_TypeID = t.getTypeId(L, typeof(Logic.Model.EntityFlag), out is_first);
				
				if (LogicModelEntityFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Logic.Model.EntityFlag));
				    LogicModelEntityFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, LogicModelEntityFlag_EnumRef) == 1)
			    return;
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, LogicModelEntityFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
                throw new Exception("pack fail fail for Logic.Model.EntityFlag ,value="+val);
			
			LuaAPI.lua_getref(L, LogicModelEntityFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetEx(this ObjectTranslator t, RealStatePtr L, int index, out Logic.Model.EntityFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != LogicModelEntityFlag_TypeID)
				    throw new Exception("invalid userdata for Logic.Model.EntityFlag");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                
				if (!CopyByValue.UnPack(buff, 0, out e))
                    throw new Exception("unpack fail for Logic.Model.EntityFlag");
                
				val = (Logic.Model.EntityFlag)e;
                
            }
            else
                val = (Logic.Model.EntityFlag)t.objectCasters.GetCaster(typeof(Logic.Model.EntityFlag))(L, index, null);
        }
		
        public static void UpdateLogicModelEntityFlag(this ObjectTranslator t, RealStatePtr L, int index, Logic.Model.EntityFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != LogicModelEntityFlag_TypeID)
				    throw new Exception("invalid userdata for Logic.Model.EntityFlag");
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                    throw new Exception("pack fail for Logic.Model.EntityFlag ,value="+val);
            }
            else
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
        }
        
        
		// table cast optimze
		
        
    }
	
	public class StaticLuaCallbacksEx
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, XLua.ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(Core.Math.Vec2[]))
			{
			    Core.Math.Vec2[] array = obj as Core.Math.Vec2[];
				translator.PushCoreMathVec2(L, array[index]);
				return true;
			}
			else if (type == typeof(Core.Math.Vec3[]))
			{
			    Core.Math.Vec3[] array = obj as Core.Math.Vec3[];
				translator.PushCoreMathVec3(L, array[index]);
				return true;
			}
			else if (type == typeof(Core.Math.Vec4[]))
			{
			    Core.Math.Vec4[] array = obj as Core.Math.Vec4[];
				translator.PushCoreMathVec4(L, array[index]);
				return true;
			}
			else if (type == typeof(Core.Math.Color4[]))
			{
			    Core.Math.Color4[] array = obj as Core.Math.Color4[];
				translator.PushCoreMathColor4(L, array[index]);
				return true;
			}
			else if (type == typeof(Core.Math.Bounds[]))
			{
			    Core.Math.Bounds[] array = obj as Core.Math.Bounds[];
				translator.PushCoreMathBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(Core.Math.Rect[]))
			{
			    Core.Math.Rect[] array = obj as Core.Math.Rect[];
				translator.PushCoreMathRect(L, array[index]);
				return true;
			}
			else if (type == typeof(Logic.Model.EntityFlag[]))
			{
			    Logic.Model.EntityFlag[] array = obj as Logic.Model.EntityFlag[];
				translator.PushLogicModelEntityFlag(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, XLua.ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(Core.Math.Vec2[]))
			{
			    Core.Math.Vec2[] array = obj as Core.Math.Vec2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Core.Math.Vec3[]))
			{
			    Core.Math.Vec3[] array = obj as Core.Math.Vec3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Core.Math.Vec4[]))
			{
			    Core.Math.Vec4[] array = obj as Core.Math.Vec4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Core.Math.Color4[]))
			{
			    Core.Math.Color4[] array = obj as Core.Math.Color4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Core.Math.Bounds[]))
			{
			    Core.Math.Bounds[] array = obj as Core.Math.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Core.Math.Rect[]))
			{
			    Core.Math.Rect[] array = obj as Core.Math.Rect[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Logic.Model.EntityFlag[]))
			{
			    Logic.Model.EntityFlag[] array = obj as Logic.Model.EntityFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}