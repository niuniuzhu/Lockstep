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
    public static class PackUnpack
    {
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Vec2 val)
		{
		    val = new Core.Math.Vec2();
            int top = LuaAPI.lua_gettop(L);
			
			if (Utils.LoadField(L, idx, "x"))
            {
			    
                translator.Get(L, top + 1, out val.x);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "y"))
            {
			    
                translator.Get(L, top + 1, out val.y);
				
            }
            LuaAPI.lua_pop(L, 1);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Vec2 field)
        {
            
            if(!LuaAPI.xlua_pack_float2(buff, offset, field.x, field.y))
            {
                return false;
            }
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Vec2 field)
        {
            field = default(Core.Math.Vec2);
            
            float x = default(float);
            float y = default(float);
            
            if(!LuaAPI.xlua_unpack_float2(buff, offset, out x, out y))
            {
                return false;
            }
            field.x = x;
            field.y = y;
            
            
            return true;
        }
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Vec3 val)
		{
		    val = new Core.Math.Vec3();
            int top = LuaAPI.lua_gettop(L);
			
			if (Utils.LoadField(L, idx, "x"))
            {
			    
                translator.Get(L, top + 1, out val.x);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "y"))
            {
			    
                translator.Get(L, top + 1, out val.y);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "z"))
            {
			    
                translator.Get(L, top + 1, out val.z);
				
            }
            LuaAPI.lua_pop(L, 1);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Vec3 field)
        {
            
            if(!LuaAPI.xlua_pack_float3(buff, offset, field.x, field.y, field.z))
            {
                return false;
            }
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Vec3 field)
        {
            field = default(Core.Math.Vec3);
            
            float x = default(float);
            float y = default(float);
            float z = default(float);
            
            if(!LuaAPI.xlua_unpack_float3(buff, offset, out x, out y, out z))
            {
                return false;
            }
            field.x = x;
            field.y = y;
            field.z = z;
            
            
            return true;
        }
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Vec4 val)
		{
		    val = new Core.Math.Vec4();
            int top = LuaAPI.lua_gettop(L);
			
			if (Utils.LoadField(L, idx, "x"))
            {
			    
                translator.Get(L, top + 1, out val.x);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "y"))
            {
			    
                translator.Get(L, top + 1, out val.y);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "z"))
            {
			    
                translator.Get(L, top + 1, out val.z);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "w"))
            {
			    
                translator.Get(L, top + 1, out val.w);
				
            }
            LuaAPI.lua_pop(L, 1);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Vec4 field)
        {
            
            if(!LuaAPI.xlua_pack_float4(buff, offset, field.x, field.y, field.z, field.w))
            {
                return false;
            }
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Vec4 field)
        {
            field = default(Core.Math.Vec4);
            
            float x = default(float);
            float y = default(float);
            float z = default(float);
            float w = default(float);
            
            if(!LuaAPI.xlua_unpack_float4(buff, offset, out x, out y, out z, out w))
            {
                return false;
            }
            field.x = x;
            field.y = y;
            field.z = z;
            field.w = w;
            
            
            return true;
        }
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Color4 val)
		{
		    val = new Core.Math.Color4();
            int top = LuaAPI.lua_gettop(L);
			
			if (Utils.LoadField(L, idx, "r"))
            {
			    
                translator.Get(L, top + 1, out val.r);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "g"))
            {
			    
                translator.Get(L, top + 1, out val.g);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "b"))
            {
			    
                translator.Get(L, top + 1, out val.b);
				
            }
            LuaAPI.lua_pop(L, 1);
			
			if (Utils.LoadField(L, idx, "a"))
            {
			    
                translator.Get(L, top + 1, out val.a);
				
            }
            LuaAPI.lua_pop(L, 1);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Color4 field)
        {
            
            if(!LuaAPI.xlua_pack_float4(buff, offset, field.r, field.g, field.b, field.a))
            {
                return false;
            }
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Color4 field)
        {
            field = default(Core.Math.Color4);
            
            float r = default(float);
            float g = default(float);
            float b = default(float);
            float a = default(float);
            
            if(!LuaAPI.xlua_unpack_float4(buff, offset, out r, out g, out b, out a))
            {
                return false;
            }
            field.r = r;
            field.g = g;
            field.b = b;
            field.a = a;
            
            
            return true;
        }
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Bounds val)
		{
		    val = new Core.Math.Bounds();
            int top = LuaAPI.lua_gettop(L);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Bounds field)
        {
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Bounds field)
        {
            field = default(Core.Math.Bounds);
            
            return true;
        }
        
		
		public static void UnPack(ObjectTranslator translator, RealStatePtr L, int idx, out Core.Math.Rect val)
		{
		    val = new Core.Math.Rect();
            int top = LuaAPI.lua_gettop(L);
			
		}
		
        public static bool Pack(IntPtr buff, int offset, Core.Math.Rect field)
        {
            
            return true;
        }
        public static bool UnPack(IntPtr buff, int offset, out Core.Math.Rect field)
        {
            field = default(Core.Math.Rect);
            
            return true;
        }
        
    }
}