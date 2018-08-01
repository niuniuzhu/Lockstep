using System.Collections.Generic;

namespace Server.Dao
{
	public class Room
	{
		public const int TEAM_MAX = 4;

		public int id;
		public string hostId;
		public string name;
		public int createTime;
		public string map;
		public readonly List<Player> teamOne = new List<Player>( TEAM_MAX );
		public readonly List<Player> teamTwo = new List<Player>( TEAM_MAX );
		public bool fightLock;
		public int mapReadyCount;

		public Room()
		{
			this.Reset();
		}

		public Player GetPlayer( string userId )
		{
			int pos;
			if ( ( pos = this.IndexOf( 0, userId ) ) >= 0 )
				return this.teamOne[pos];

			return this.teamTwo[this.IndexOf( 1, userId )];
		}

		public bool ContainsUser( string userId )
		{
			if ( this.IndexOf( 0, userId ) < 0 && this.IndexOf( 1, userId ) < 0 )
				return false;
			return true;
		}

		public int IndexOf( byte teamId, string userId )
		{
			List<Player> team = teamId == 0 ? this.teamOne : this.teamTwo;
			int count = team.Count;
			for ( int i = 0; i < count; i++ )
			{
				if ( team[i].id == userId )
					return i;
			}
			return -1;
		}

		public void AddUser( int teamId, Player player )
		{
			List<Player> team = teamId == 0 ? this.teamOne : this.teamTwo;
			if ( team.Contains( player ) )
				return;
			team.Add( player );
		}

		public Player RemoveUser( string userId )
		{
			int pos;
			if ( ( pos = this.IndexOf( 0, userId ) ) >= 0 )
				return this.RemoveUserAt( 0, pos );
			pos = this.IndexOf( 1, userId );
			return this.RemoveUserAt( 1, pos );
		}

		public Player RemoveUserAt( byte teamId, int index )
		{
			List<Player> team = teamId == 0 ? this.teamOne : this.teamTwo;
			Player player = team[index];
			team.RemoveAt( index );
			return player;
		}

		public Player RemoveUser( byte teamId, string userId )
		{
			int pos = this.IndexOf( teamId, userId );
			return this.RemoveUserAt( teamId, pos );
		}

		public byte GetUserTeam( string userId )
		{
			if ( this.IndexOf( 0, userId ) >= 0 )
				return 0;
			return 1;
		}

		public void SetHero( string userId, string hero )
		{
			int pos;
			if ( ( pos = this.IndexOf( 0, userId ) ) >= 0 )
			{
				this.SetHeroAt( 0, pos, hero );
			}
			else
			{
				pos = this.IndexOf( 1, userId );
				this.SetHeroAt( 1, pos, hero );
			}
		}

		public void SetHeroAt( int teamId, int index, string hero )
		{
			List<Player> team = teamId == 0 ? this.teamOne : this.teamTwo;
			team[index].hero = hero;
		}

		public int ChangeTeam( string userId, byte toTeamId )
		{
			byte fromTeamId = this.GetUserTeam( userId );
			if ( fromTeamId == toTeamId )
				return -1;
			List<Player> toTeam = toTeamId == 0 ? this.teamOne : this.teamTwo;
			if ( toTeam.Count >= TEAM_MAX )
				return -2;
			Player player = this.RemoveUser( fromTeamId, userId );
			this.AddUser( toTeamId, player );
			return 0;
		}

		public void ChangeModel( string userId, byte modelId )
		{
			Player player = this.GetPlayer( userId );
			player.model = modelId;
		}

		public void ChangeSkin( string userId, byte skinId )
		{
			Player player = this.GetPlayer( userId );
			player.skin = skinId;
		}

		public bool IsUserReady( string userId )
		{
			return this.GetPlayer( userId ).ready;
		}

		public bool SetReady( string userId )
		{
			Player player = this.GetPlayer( userId );
			if ( player.ready )
				return false;
			player.ready = true;
			return true;
		}

		public bool CancelReady( string userId )
		{
			Player player = this.GetPlayer( userId );
			if ( !player.ready )
				return false;
			player.ready = false;
			return true;
		}

		public bool IsAllUserReady()
		{
			int count = this.teamOne.Count;
			for ( int i = 0; i < count; i++ )
			{
				Player player = this.teamOne[i];
				if ( !player.ready && player.id != this.hostId )//除房主以外
					return false;
			}
			count = this.teamTwo.Count;
			for ( int i = 0; i < count; i++ )
			{
				Player player = this.teamTwo[i];
				if ( !player.ready && player.id != this.hostId )
					return false;
			}
			return true;
		}

		public void Reset()
		{
			this.id = -1;
			this.hostId = string.Empty;
			this.name = string.Empty;
			this.createTime = -1;
			this.map = "m0";
			this.teamOne.Clear();
			this.teamTwo.Clear();
			this.fightLock = false;
			this.mapReadyCount = 0;
		}

		public class Player
		{
			public string id;
			public string name;
			public string hero;
			public byte model;
			public byte skin;
			public bool ready;
		}
	}
}