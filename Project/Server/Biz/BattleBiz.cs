using Core.Misc;
using Core.Net;
using Protocol;
using Protocol.Gen;
using Server.Dao;
using Server.Logic.Battle;
using Server.Misc;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Server.Biz
{
	public class BattleBiz
	{
		private readonly Random _rnd = new Random();

		private readonly ConcurrentDictionary<string, StepLocker> _idToLocker = new ConcurrentDictionary<string, StepLocker>();
		private readonly ConcurrentDictionary<string, StepLocker> _uidToLocker = new ConcurrentDictionary<string, StepLocker>();

		public PResult Create( Room room )
		{
			string rid = Utils.MakeRidFromID( room.map );
			int seed = this._rnd.Next();

			StepLocker stepLocker = new StepLocker( rid );

			int count = room.teamOne.Count;
			for ( int i = 0; i < count; i++ )
			{
				string userId = room.teamOne[i].id;
				this._uidToLocker[userId] = stepLocker;
				stepLocker.AddUser( userId );
			}

			count = room.teamTwo.Count;
			for ( int i = 0; i < count; i++ )
			{
				string userId = room.teamTwo[i].id;
				this._uidToLocker[userId] = stepLocker;
				stepLocker.AddUser( userId );
			}

			this._idToLocker[stepLocker.id] = stepLocker;
			_DTO_player_info[] players = DTOHelper.GetPlayerInfoInRoom( room );

			stepLocker.ForeachUser( userId =>
			{
				IUserToken token = BizFactory.USER_BIZ.GetToken( userId );
				token.CALL_BATTLE_ACMD_ENTER_BATTLE(
					StepLocker.FRAME_RATE, StepLocker.FRAMES_PER_KEYFRAME, rid,
					players, seed, userId );
			} );

			return PResult.SUCCESS;
		}

		public PResult Leave( string userId )
		{
			if ( !this.UserInBattle( userId ) )
				return PResult.USER_NOT_INT_BATTLE;

			StepLocker stepLocker = this.GetFromUserId( userId );
			this._uidToLocker.TryRemove( userId, out _ );
			int userCount = stepLocker.RemoveUser( userId );
			if ( userCount == 0 )
			{
				stepLocker.finished = true;
				Logger.Log( $"战场结束:{stepLocker.id}" );
			}
			return PResult.SUCCESS;
		}

		public PResult HandleBattleCreated( string userId )
		{
			StepLocker stepLocker = this.GetFromUserId( userId );
			if ( stepLocker == null )
				return PResult.STEPLOCKER_NOT_FOUND;

			++stepLocker.createdCount;
			if ( stepLocker.createdCount == stepLocker.userCount )
			{
				ThreadPool.QueueUserWorkItem( state =>
				{
					stepLocker.Start();
					while ( !stepLocker.finished )
					{
						stepLocker.Update();
						Thread.Sleep( 5 );
					}
				} );
				Logger.Log( $"创建战场:{stepLocker.id}" );
				return PResult.SUCCESS;
			}
			return PResult.WAIT_FOR_USER_BATTLE_CREATED;
		}

		public PResult HandleAction( string userId, _DTO_frame_info dto )
		{
			StepLocker stepLocker = this.GetFromUserId( userId );
			if ( stepLocker == null )
				return PResult.STEPLOCKER_NOT_FOUND;

			stepLocker.HandleAction( dto );

			return PResult.SUCCESS;
		}

		public PResult HandleEndBattle( string userId )
		{
			StepLocker stepLocker = this.GetFromUserId( userId );
			if ( stepLocker == null )
				return PResult.STEPLOCKER_NOT_FOUND;

			++stepLocker.endedCount;
			if ( stepLocker.endedCount >= stepLocker.userCount )
			{
				stepLocker.finished = true;
				Logger.Log( $"战斗结束:{stepLocker.id}" );
				return PResult.SUCCESS;
			}
			return PResult.WAIT_FOR_USER_ENDED;
		}

		public StepLocker Get( string id )
		{
			this._idToLocker.TryGetValue( id, out StepLocker stepLocker );
			return stepLocker;
		}

		public StepLocker GetFromUserId( string userId )
		{
			this._uidToLocker.TryGetValue( userId, out StepLocker stepLocker );
			return stepLocker;
		}

		public bool UserInBattle( string userId )
		{
			return this._uidToLocker.ContainsKey( userId );
		}
	}
}