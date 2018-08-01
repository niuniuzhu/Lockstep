using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_sync_time : DTO
	{
		public long clientTime;
public long serverTime;
		
		public _DTO_sync_time(  )
		{
			
		}
public _DTO_sync_time( long clientTime,long serverTime )
		{
			this.clientTime = clientTime;
this.serverTime = serverTime;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.Write( this.clientTime );
buffer.Write( this.serverTime );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.clientTime = buffer.ReadLong();
this.serverTime = buffer.ReadLong();
		}

		public override string ToString()
		{
			return $"clientTime:{this.clientTime},serverTime:{this.serverTime}";
		}
	}
}