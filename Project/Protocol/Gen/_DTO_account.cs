using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_account : DTO
	{
		public string account;
public string password;
		
		public _DTO_account(  )
		{
			
		}
public _DTO_account( string account,string password )
		{
			this.account = account;
this.password = password;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			buffer.WriteUTF8( this.account );
buffer.WriteUTF8( this.password );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			this.account = buffer.ReadUTF8();
this.password = buffer.ReadUTF8();
		}

		public override string ToString()
		{
			return $"account:{this.account},password:{this.password}";
		}
	}
}