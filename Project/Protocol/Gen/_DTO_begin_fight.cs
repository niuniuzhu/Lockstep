using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_begin_fight : DTO
	{
		public string host;
public string map;
public string name;
public _DTO_player_info[] players;
public int roomId;
		
		public _DTO_begin_fight(  )
		{
			
		}
public _DTO_begin_fight( string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			this.host = host;
this.map = map;
this.name = name;
this.players = players;
this.roomId = roomId;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.WriteUTF8( this.host );
buffer.WriteUTF8( this.map );
buffer.WriteUTF8( this.name );
int count = this.players.Length;
				buffer.Write( ( ushort )count );
				for ( int i = 0; i < count; ++i )
					this.players[i].Serialize( buffer );
				
buffer.Write( this.roomId );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.host = buffer.ReadUTF8();
this.map = buffer.ReadUTF8();
this.name = buffer.ReadUTF8();
int count = buffer.ReadUShort();
				this.players = new _DTO_player_info[count];
				for ( int i = 0; i < count; ++i )
				{
					var players = this.players[i] = new _DTO_player_info();
					players.Deserialize( buffer );
				}
this.roomId = buffer.ReadInt();
		}

		public override string ToString()
		{
			return $"host:{this.host},map:{this.map},name:{this.name},players:{this.players},roomId:{this.roomId}";
		}
	}
}