using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_action_info : DTO
	{
		public string sender;
public byte type;
public float x;
public float y;
public float z;
public string target;
public string sid;
public string src;
		
		public _DTO_action_info(  )
		{
			
		}
public _DTO_action_info( string sender,byte type,float x,float y,float z,string target,string sid,string src )
		{
			this.sender = sender;
this.type = type;
this.x = x;
this.y = y;
this.z = z;
this.target = target;
this.sid = sid;
this.src = src;
		}
public _DTO_action_info( string sender,byte type,float x,float y,float z )
		{
			this.sender = sender;
this.type = type;
this.x = x;
this.y = y;
this.z = z;
		}
public _DTO_action_info( string sender,byte type,string target )
		{
			this.sender = sender;
this.type = type;
this.target = target;
		}
public _DTO_action_info( string sender,byte type,string sid,string src,string target,float x,float y,float z )
		{
			this.sender = sender;
this.type = type;
this.sid = sid;
this.src = src;
this.target = target;
this.x = x;
this.y = y;
this.z = z;
		}
public _DTO_action_info( string sender,byte type )
		{
			this.sender = sender;
this.type = type;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.WriteUTF8( this.sender );
buffer.Write( this.type );
			if ( this.type == byte.Parse("1"))
			{
				buffer.Write( this.x );
buffer.Write( this.y );
buffer.Write( this.z );
			}

			if ( this.type == byte.Parse("2")||this.type == byte.Parse("5"))
			{
				buffer.WriteUTF8( this.target );
			}

			if ( this.type == byte.Parse("3"))
			{
				buffer.WriteUTF8( this.sid );
buffer.WriteUTF8( this.src );
buffer.WriteUTF8( this.target );
buffer.Write( this.x );
buffer.Write( this.y );
buffer.Write( this.z );
			}

			if ( this.type == byte.Parse("4"))
			{
				
			}
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.sender = buffer.ReadUTF8();
this.type = buffer.ReadByte();
			if ( this.type == byte.Parse("1"))
			{
				this.x = buffer.ReadFloat();
this.y = buffer.ReadFloat();
this.z = buffer.ReadFloat();
			}

			if ( this.type == byte.Parse("2")||this.type == byte.Parse("5"))
			{
				this.target = buffer.ReadUTF8();
			}

			if ( this.type == byte.Parse("3"))
			{
				this.sid = buffer.ReadUTF8();
this.src = buffer.ReadUTF8();
this.target = buffer.ReadUTF8();
this.x = buffer.ReadFloat();
this.y = buffer.ReadFloat();
this.z = buffer.ReadFloat();
			}

			if ( this.type == byte.Parse("4"))
			{
				
			}
		}

		public override string ToString()
		{
			return $"sender:{this.sender},type:{this.type},x:{this.x},y:{this.y},z:{this.z},target:{this.target},sid:{this.sid},src:{this.src}";
		}
	}
}