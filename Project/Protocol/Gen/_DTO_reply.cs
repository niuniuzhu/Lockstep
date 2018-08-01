using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_reply : DTO
	{
		public ushort result;
public ushort src_cmd;
public byte src_module;
		
		public _DTO_reply(  )
		{
			
		}
public _DTO_reply( ushort result,ushort src_cmd,byte src_module )
		{
			this.result = result;
this.src_cmd = src_cmd;
this.src_module = src_module;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.Write( this.result );
buffer.Write( this.src_cmd );
buffer.Write( this.src_module );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.result = buffer.ReadUShort();
this.src_cmd = buffer.ReadUShort();
this.src_module = buffer.ReadByte();
		}

		public override string ToString()
		{
			return $"result:{this.result},src_cmd:{this.src_cmd},src_module:{this.src_module}";
		}
	}
}