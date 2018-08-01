using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_request_room_list : DTO
	{
		public byte count;
public byte from;
		
		public _DTO_request_room_list(  )
		{
			
		}
public _DTO_request_room_list( byte count,byte from )
		{
			this.count = count;
this.from = from;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.Write( this.count );
buffer.Write( this.from );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.count = buffer.ReadByte();
this.from = buffer.ReadByte();
		}

		public override string ToString()
		{
			return $"count:{this.count},from:{this.from}";
		}
	}
}