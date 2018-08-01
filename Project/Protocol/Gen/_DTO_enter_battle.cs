using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_enter_battle : DTO
	{
		public int frameRate;
public int framesPerKeyFrame;
public string mapId;
public _DTO_player_info[] players;
public int rndSeed;
public string uid;
		
		public _DTO_enter_battle(  )
		{
			
		}
public _DTO_enter_battle( int frameRate,int framesPerKeyFrame,string mapId,_DTO_player_info[] players,int rndSeed,string uid )
		{
			this.frameRate = frameRate;
this.framesPerKeyFrame = framesPerKeyFrame;
this.mapId = mapId;
this.players = players;
this.rndSeed = rndSeed;
this.uid = uid;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.Write( this.frameRate );
buffer.Write( this.framesPerKeyFrame );
buffer.WriteUTF8( this.mapId );
int count = this.players.Length;
				buffer.Write( ( ushort )count );
				for ( int i = 0; i < count; ++i )
					this.players[i].Serialize( buffer );
				
buffer.Write( this.rndSeed );
buffer.WriteUTF8( this.uid );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.frameRate = buffer.ReadInt();
this.framesPerKeyFrame = buffer.ReadInt();
this.mapId = buffer.ReadUTF8();
int count = buffer.ReadUShort();
				this.players = new _DTO_player_info[count];
				for ( int i = 0; i < count; ++i )
				{
					var players = this.players[i] = new _DTO_player_info();
					players.Deserialize( buffer );
				}
this.rndSeed = buffer.ReadInt();
this.uid = buffer.ReadUTF8();
		}

		public override string ToString()
		{
			return $"frameRate:{this.frameRate},framesPerKeyFrame:{this.framesPerKeyFrame},mapId:{this.mapId},players:{this.players},rndSeed:{this.rndSeed},uid:{this.uid}";
		}
	}
}