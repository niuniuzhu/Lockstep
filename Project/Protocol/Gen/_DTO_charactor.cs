using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_charactor : DTO
	{
		public string name;
public string uid;
		
		public _DTO_charactor(  )
		{
			
		}
public _DTO_charactor( string name,string uid )
		{
			this.name = name;
this.uid = uid;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.WriteUTF8( this.name );
buffer.WriteUTF8( this.uid );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.name = buffer.ReadUTF8();
this.uid = buffer.ReadUTF8();
		}

		public override string ToString()
		{
			return $"name:{this.name},uid:{this.uid}";
		}
	}
}