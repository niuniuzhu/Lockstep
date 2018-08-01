using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_player_info : DTO
	{
		public string cid;
public string name;
public bool ready;
public byte skin;
public byte team;
public string uid;
		
		public _DTO_player_info(  )
		{
			
		}
public _DTO_player_info( string cid,string name,bool ready,byte skin,byte team,string uid )
		{
			this.cid = cid;
this.name = name;
this.ready = ready;
this.skin = skin;
this.team = team;
this.uid = uid;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.WriteUTF8( this.cid );
buffer.WriteUTF8( this.name );
buffer.Write( this.ready );
buffer.Write( this.skin );
buffer.Write( this.team );
buffer.WriteUTF8( this.uid );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.cid = buffer.ReadUTF8();
this.name = buffer.ReadUTF8();
this.ready = buffer.ReadBool();
this.skin = buffer.ReadByte();
this.team = buffer.ReadByte();
this.uid = buffer.ReadUTF8();
		}

		public override string ToString()
		{
			return $"cid:{this.cid},name:{this.name},ready:{this.ready},skin:{this.skin},team:{this.team},uid:{this.uid}";
		}
	}
}