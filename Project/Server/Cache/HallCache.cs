using Core.Misc;
using Protocol;
using System.Collections.Generic;

namespace Server.Cache
{
	public class HallCache
	{
		public delegate void ForeachUserHandler( string user );

		readonly object _lockObj = new object();

		readonly List<string> _users = new List<string>();

		public PResult Enter( string userId )
		{
			if ( this.Exist( userId ) )
				return PResult.ENTER_HALL_FAILED;

			lock ( this._lockObj )
			{
				this._users.Add( userId );
			}

			Logger.Log( $"玩家{userId}进入大厅"  );

			return PResult.SUCCESS;
		}

		public PResult Leave( string userId )
		{
			bool result;
			lock ( this._lockObj )
			{
				result = this._users.Remove( userId );
			}

			if ( !result )
				return PResult.LEAVE_HALL_FAILED;

			Logger.Log( $"玩家{userId}离开大厅"  );

			return PResult.SUCCESS;
		}

		public bool Exist( string userId )
		{
			bool result;
			lock ( this._lockObj )
			{
				result = this._users.Contains( userId );
			}
			return result;
		}

		public void ForeachUser( ForeachUserHandler handler )
		{
			lock ( this._lockObj )
			{
				int count = this._users.Count;
				for ( int i = 0; i < count; i++ )
					handler( this._users[i] );
			}
		}
	}
}