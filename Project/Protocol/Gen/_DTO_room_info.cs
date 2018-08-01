using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_room_info : DTO
	{
		public int ct;
public string map;
public string name;
public int roomId;
		
		public _DTO_room_info(  )
		{
			
		}
public _DTO_room_info( int ct,string map,string name,int roomId )
		{
			this.ct = ct;
this.map = map;
this.name = name;
this.roomId = roomId;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.Write( this.ct );
buffer.WriteUTF8( this.map );
buffer.WriteUTF8( this.name );
buffer.Write( this.roomId );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.ct = buffer.ReadInt();
this.map = buffer.ReadUTF8();
this.name = buffer.ReadUTF8();
this.roomId = buffer.ReadInt();
		}

		public override string ToString()
		{
			return $"ct:{this.ct},map:{this.map},name:{this.name},roomId:{this.roomId}";
		}
	}
}