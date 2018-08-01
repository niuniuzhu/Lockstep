using System.Collections.Generic;
using Logic.Controller;

namespace Logic.Manager
{
	public class EntityManager
	{
		private readonly GPool _gPool = new GPool();

		private Battle _battle;

		private readonly List<Entity> _entities = new List<Entity>();
		private readonly Dictionary<string, Entity> _idToEntity = new Dictionary<string, Entity>();

		private readonly List<Bio> _bios = new List<Bio>();
		private readonly Dictionary<string, Bio> _idToBio = new Dictionary<string, Bio>();

		private readonly List<Missile> _missiles = new List<Missile>();
		private readonly Dictionary<string, Missile> _idToMissile = new Dictionary<string, Missile>();

		public List<Entity> entities => this._entities;

		public EntityManager( Battle battle )
		{
			this._battle = battle;
		}

		public void Dispose()
		{
			int count = this._entities.Count;
			for ( int i = 0; i < count; i++ )
				this._entities[i].markToDestroy = true;
			this.DestroyEnties();
			this._gPool.Dispose();
			this._battle = null;
		}

		private T CreateEntity<T>( EntityParam param ) where T : Entity, new()
		{
			bool isNew = this._gPool.Pop( out T entity );
			this._idToEntity[param.rid] = entity;
			this._entities.Add( entity );
			if ( isNew )
				entity.Init( this._battle );
			entity.OnAddedToBattle( param );
			return entity;
		}

		public Bio CreateBio( EntityParam param )
		{
			Bio entity = this.CreateEntity<Bio>( param );
			this._idToBio[param.rid] = entity;
			this._bios.Add( entity );
			return entity;
		}

		public T CreateBio<T>( EntityParam param ) where T : Bio, new()
		{
			T entity = this.CreateEntity<T>( param );
			this._idToBio[param.rid] = entity;
			this._bios.Add( entity );
			return entity;
		}

		public Missile CreateMissile( EntityParam param )
		{
			Missile entity = this.CreateEntity<Missile>( param );
			this._idToMissile[param.rid] = entity;
			this._missiles.Add( entity );
			return entity;
		}

		public Entity GetEntity( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToEntity.TryGetValue( id, out Entity entity );
			return entity;
		}

		public Bio GetBio( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToBio.TryGetValue( id, out Bio entity );
			return entity;
		}

		public Missile GetMissile( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToMissile.TryGetValue( id, out Missile entity );
			return entity;
		}

		internal void Update( UpdateContext context )
		{
			//更新状态
			this.UpdateState( context );
			//处理战斗信息
			this.UpdateFight( context );
			//清理实体
			this.DestroyEnties();
		}

		private void UpdateState( UpdateContext context )
		{
			//bio
			int count = this._bios.Count;
			for ( int i = 0; i < count; i++ )
			{
				Bio entity = this._bios[i];
				entity.UpdateState( context );
			}
			//missile
			count = this._missiles.Count;
			for ( int i = 0; i < count; i++ )
			{
				Missile entity = this._missiles[i];
				entity.UpdateState( context );
			}
		}

		private void UpdateFight( UpdateContext context )
		{
			int count = this._bios.Count;
			for ( int i = 0; i < count; i++ )
			{
				Entity entity = this._bios[i];
				entity.UpdateFight( context );
			}
		}

		private void DestroyEnties()
		{
			int count = this._entities.Count;
			for ( int i = 0; i < count; i++ )
			{
				Entity entity = this._entities[i];
				if ( !entity.markToDestroy )
					continue;

				entity.OnRemoveFromBattle();

				switch ( entity )
				{
					case Bio _:
						this._bios.Remove( ( Bio )entity );
						this._idToBio.Remove( entity.rid );
						break;

					case Missile _:
						this._missiles.Remove( ( Missile )entity );
						this._idToMissile.Remove( entity.rid );
						break;
				}

				this._entities.RemoveAt( i );
				this._idToEntity.Remove( entity.rid );
				this._gPool.Push( entity );
				--i;
				--count;
			}
		}
	}
}