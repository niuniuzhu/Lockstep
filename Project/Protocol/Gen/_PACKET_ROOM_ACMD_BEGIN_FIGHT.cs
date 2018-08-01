using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 32002 )]
	public class _PACKET_ROOM_ACMD_BEGIN_FIGHT : Packet
	{
		public _DTO_begin_fight dto;

		public _PACKET_ROOM_ACMD_BEGIN_FIGHT() : base( 103, 32002 )
		{
		}

		public _PACKET_ROOM_ACMD_BEGIN_FIGHT( _DTO_begin_fight dto ) : base( 103, 32002 )
		{
			this.dto = dto;
		}

		public _PACKET_ROOM_ACMD_BEGIN_FIGHT( string host,string map,string name,_DTO_player_info[] players,int roomId ) : base( 103, 32002 )
		{
			this.dto = new _DTO_begin_fight( host,map,name,players,roomId );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_begin_fight();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}