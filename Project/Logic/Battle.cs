using Core.Math;
using Logic.AI;
using Logic.Controller;
using Logic.Manager;
using Logic.Misc;
using Logic.Model;
using org.critterai.nav;
using System.Collections.Generic;
using XLua;
using Utils = Logic.Misc.Utils;

namespace Logic
{
	public struct BattleParams
	{
		public int frameRate;
		public int framesPerKeyFrame;
		public string uid;
		public string id;
		public int rndSeed;
		public Player[] players;

		public struct Player
		{
			public string id;
			public string name;
			public string cid;
			public byte skin;
			public int team;
		}
	}

	public class Battle : Controller.IDisposable, IScriptable
	{
		public string rid { get; private set; }
		public int frame { get; private set; }
		public float deltaTime { get; private set; }
		public float time { get; private set; }
		public float realTime { get; private set; }
		public BattleData data { get; private set; }
		public Vec3 basePoint1 => this.data.basePoint1;
		public Vec3 basePoint2 => this.data.basePoint2;
		public ConsistentRandom random => this._random;
		public TimeScheduler timer0 => this._timer0;
		public LuaEnv luaEnv => this._luaEnv;

		private Script _script;
		private LuaEnv _luaEnv;
		private readonly UpdateContext _context;
		private readonly EntityManager _entityManager;
		private readonly BuffManager _buffManager;
		private readonly ConsistentRandom _random;
		private readonly TimeScheduler _timer0;
		private readonly NavMeshProxy _pathManager;

		public Battle( BattleParams param, Navmesh navmesh, LuaEnv luaEnv )
		{
			this.rid = param.id;
			this.data = ModelFactory.GetBattleData( Utils.GetIDFromRID( this.rid ) );

			this._luaEnv = luaEnv;
			this._context = new UpdateContext();
			this._entityManager = new EntityManager( this );
			this._buffManager = new BuffManager( this );
			this._random = new ConsistentRandom( param.rndSeed );
			this._pathManager = new NavMeshProxy();
			this._timer0 = new TimeScheduler();
			this._pathManager.Create( navmesh );

			if ( !string.IsNullOrEmpty( this.data.script ) )
			{
				this._script = new Script( this, this._luaEnv, this.data.script );
				this._script.Call( Script.S_ON_BATTLE_INITIALIZED );
			}

			this.CreatePlayers( param );

			foreach ( KeyValuePair<string, BattleData.Structure> kv in this.data.structures )
			{
				BattleData.Structure def = this.data.structures[kv.Key];
				this.CreateBio( def.id, def.pos, def.dir, def.team );
			}

			foreach ( KeyValuePair<string, BattleData.Neutral> kv in this.data.neutrals )
			{
				BattleData.Neutral def = this.data.neutrals[kv.Key];
				this.CreateBio( def.id, def.pos, def.dir, def.team );
			}
		}

		public void Dispose()
		{
			this._pathManager.Dispose();
			this._buffManager.Dispose();
			this._entityManager.Dispose();//顺序很重要
			if ( this._script != null )
			{
				this._script.Call( Script.S_ON_BATTLE_DESTROIED );
				this._script.Dispose();
				this._script = null;
			}
			this._timer0.Dispose();
			this._luaEnv = null;
			this.data = null;

			SyncEventHelper.DestroyBattle();
		}

		public Vec3 RandomPoint( Vec3 center, float range )
		{
			Vec2 insideUnitCircle = this._random.insideUnitCircle * range;
			return new Vec3( center.x + insideUnitCircle.x, 0, center.z + insideUnitCircle.y );
		}

		private void CreatePlayers( BattleParams battleParams )
		{
			int count = battleParams.players.Length;
			for ( int i = 0; i < count; i++ )
			{
				BattleParams.Player player = battleParams.players[i];
				EntityParam param = new EntityParam();
				param.rid = player.cid + "@" + player.id;
				param.uid = player.id;
				param.position = this.RandomPoint( this.data.bornPos1, this.data.bornRange );
				param.direction = this.data.bornDir1;
				param.team = player.team;
				this._entityManager.CreateBio( param );
			}
		}

		public Bio CreateBio( string id, Vec3 position, Vec3 direction, int team )
		{
			EntityParam param = new EntityParam();
			param.rid = this._random.IdHash( id );
			param.position = position;
			param.direction = direction;
			param.team = team;
			return this._entityManager.CreateBio( param );
		}

		public T CreateBio<T>( string id, Vec3 position, Vec3 direction, int team ) where T : Bio, new()
		{
			EntityParam param = new EntityParam();
			param.rid = this._random.IdHash( id );
			param.position = position;
			param.direction = direction;
			param.team = team;
			return this._entityManager.CreateBio<T>( param );
		}

		public Missile CreateMissile( string id, Vec3 position, Vec3 direction )
		{
			EntityParam param = new EntityParam();
			param.rid = this._random.IdHash( id );
			param.position = position;
			param.direction = direction;
			param.team = -1;
			return this._entityManager.CreateMissile( param );
		}

		internal List<Entity> GetEntities()
		{
			return this._entityManager.entities;
		}

		public Entity GetEntity( string rid )
		{
			return this._entityManager.GetEntity( rid );
		}

		public Bio GetBio( string rid )
		{
			return this._entityManager.GetBio( rid );
		}

		public Missile GetMissile( string rid )
		{
			return this._entityManager.GetMissile( rid );
		}

		public Buff CreateBuff( string buffId, string skillId, int lvl, Bio caster, Bio target, Vec3 targetPoint )
		{
			string bid = this._random.IdHash( buffId );
			Buff buff = this._buffManager.Create( bid, skillId, lvl, caster, target, targetPoint );
			return buff;
		}

		public Buff GetBuff( string rid )
		{
			return this._buffManager.Get( rid );
		}

		public Buff GetBuff( string ownerId, string buffId )
		{
			return this._buffManager.Get( ownerId, buffId );
		}

		#region path proxy
		public Vec3[] GetPathCorners( Vec3 src, Vec3 dest )
		{
			NavStatus status = this._pathManager.CalculatePath( src, dest, out Vec3[] corners );
			if ( status != NavStatus.Sucess )
				LLogger.Warning( status );
			if ( corners == null )
				return null;
			int count = corners.Length;
			for ( int i = 0; i < count; i++ )
				corners[i].y = 0f;
			return corners;
		}

		public bool NavMeshRaycast( Vec3 src, Vec3 dest, out Vec3 hitPosition, out Vec3 hitNormal )
		{
			NavStatus status = this._pathManager.Raycast( src, dest, out float hitParameter, out hitNormal );
			if ( status != NavStatus.Sucess )
				LLogger.Warning( status );
			hitPosition = src + ( dest - src ) * hitParameter;
			//True if the ray is terminated before reaching target position. Otherwise returns false.
			return hitParameter < 1;
		}

		public Vec3 SampleNavPosition( Vec3 searchPoint )
		{
			NavStatus status = this._pathManager.GetNearestPoint( searchPoint, out Vec3 resultPoint );
			if ( status != NavStatus.Sucess )
				LLogger.Warning( status );
			return resultPoint;
		}
		#endregion

		public void HandleMove( string id, Vec3 targetPoint )
		{
			Bio bio = this.GetBio( id );
			if ( bio == null )
				return;

			if ( !bio.CanMove() )
				return;

			bio.Move( targetPoint );
		}

		public void HandleTrack( string id, string targetId )
		{
			Bio bio = this.GetBio( id );
			if ( bio == null )
				return;

			if ( !bio.CanMove() )
				return;

			Bio target = this.GetBio( targetId );
			if ( target == null )
				return;

			bio.Track( target );
		}

		public void HandleUseSkill( string sid, string srcId, string targetId, Vec3 targetPoint )
		{
			Bio src = this.GetBio( srcId );
			if ( src == null )
				return;

			Bio target = this.GetBio( targetId );
			Skill skill = src.GetSkill( sid );
			src.UseSkill( skill, target, targetPoint );
		}

		public void HandleRelive( string rid )
		{
			Bio bio = this.GetBio( rid );

			bio?.Relive();
		}

		public void HandleUpgradeSkill( string rid, string sid )
		{
			Bio bio = this.GetBio( rid );
			if ( bio == null )
				return;

			Skill skill = bio.GetSkill( sid );
			bio.UpgradeSkill( skill );
		}

		public void Simulate( float realDeltaTime, float deltaTime )
		{
			++this.frame;

			this.deltaTime = deltaTime;
			this.realTime += realDeltaTime;
			this.time += this.deltaTime;

			this._context.deltaTime = this.deltaTime;
			this._context.time = this.time;
			this._context.frame = this.frame;

			this._timer0.Update( this.deltaTime );
			this._buffManager.Update( this._context );
			this._entityManager.Update( this._context );
		}
	}
}