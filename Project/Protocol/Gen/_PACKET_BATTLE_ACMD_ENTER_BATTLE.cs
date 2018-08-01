using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 32000 )]
	public class _PACKET_BATTLE_ACMD_ENTER_BATTLE : Packet
	{
		public _DTO_enter_battle dto;

		public _PACKET_BATTLE_ACMD_ENTER_BATTLE() : base( 104, 32000 )
		{
		}

		public _PACKET_BATTLE_ACMD_ENTER_BATTLE( _DTO_enter_battle dto ) : base( 104, 32000 )
		{
			this.dto = dto;
		}

		public _PACKET_BATTLE_ACMD_ENTER_BATTLE( int frameRate,int framesPerKeyFrame,string mapId,_DTO_player_info[] players,int rndSeed,string uid ) : base( 104, 32000 )
		{
			this.dto = new _DTO_enter_battle( frameRate,framesPerKeyFrame,mapId,players,rndSeed,uid );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_enter_battle();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}