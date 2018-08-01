using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	public class _DTO_room_list : DTO
	{
		public _DTO_room_info[] rs;
		
		public _DTO_room_list(  )
		{
			
		}
public _DTO_room_list( _DTO_room_info[] rs )
		{
			this.rs = rs;
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );

			int count = this.rs.Length;
				buffer.Write( ( ushort )count );
				for ( int i = 0; i < count; ++i )
					this.rs[i].Serialize( buffer );
				
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );

			int count = buffer.ReadUShort();
				this.rs = new _DTO_room_info[count];
				for ( int i = 0; i < count; ++i )
				{
					var rs = this.rs[i] = new _DTO_room_info();
					rs.Deserialize( buffer );
				}
		}

		public override string ToString()
		{
			return $"rs:{this.rs}";
		}
	}
}