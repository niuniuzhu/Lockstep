using System.Collections.Generic;
using Logic;
using Logic.Controller;
using View.Controller;

namespace View.Manager
{
	public class VEntityManager
	{
		private readonly GPool _gPool = new GPool();

		private VBattle _battle;

		private readonly List<VEntity> _entities = new List<VEntity>();
		private readonly Dictionary<string, VEntity> _idToEntity = new Dictionary<string, VEntity>();

		private readonly List<VBio> _bios = new List<VBio>();
		private readonly Dictionary<string, VBio> _idToBio = new Dictionary<string, VBio>();

		private readonly List<VMissile> _missiles = new List<VMissile>();
		private readonly Dictionary<string, VMissile> _idToMissile = new Dictionary<string, VMissile>();

		private readonly List<Effect> _effects = new List<Effect>();
		private readonly Dictionary<string, Effect> _idToEffect = new Dictionary<string, Effect>();

		public List<VEntity> entities => this._entities;

		public VEntityManager( VBattle battle )
		{
			this._battle = battle;
		}

		public void Dispose()
		{
			//effect需要自行管理
			int count = this._entities.Count;
			for ( int i = 0; i < count; i++ )
				this._entities[i].markToDestroy = true;
			this.DestroyEnties();
			this._gPool.Dispose();
			this._battle = null;
		}

		private T CreateEntity<T>( EntityParam param ) where T : VEntity, new()
		{
			bool isNEw = this._gPool.Pop( out T entity );
			this._idToEntity[param.rid] = entity;
			this._entities.Add( entity );
			if ( isNEw )
				entity.Init( this._battle );
			entity.OnAddedToBattle( param );
			return entity;
		}

		public VPlayer CreatePlayer( EntityParam param )
		{
			VPlayer entity = this.CreateEntity<VPlayer>( param );
			this._idToBio[param.rid] = entity;
			this._bios.Add( entity );
			return entity;
		}

		public VBio CreateBio( EntityParam param )
		{
			VBio entity = this.CreateEntity<VBio>( param );
			this._idToBio[param.rid] = entity;
			this._bios.Add( entity );
			return entity;
		}

		public T CreateBio<T>( EntityParam param ) where T : VBio, new()
		{
			T entity = this.CreateEntity<T>( param );
			this._idToBio[param.rid] = entity;
			this._bios.Add( entity );
			return entity;
		}

		public VMissile CreateMissile( EntityParam param )
		{
			VMissile entity = this.CreateEntity<VMissile>( param );
			this._idToMissile[param.rid] = entity;
			this._missiles.Add( entity );
			return entity;
		}

		public Effect CreateEffect( EntityParam param )
		{
			Effect entity = this.CreateEntity<Effect>( param );
			this._idToEffect[param.rid] = entity;
			this._effects.Add( entity );
			return entity;
		}

		public VEntity GetEntity( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToEntity.TryGetValue( id, out VEntity entity );
			return entity;
		}

		public VBio GetBio( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToBio.TryGetValue( id, out VBio entity );
			return entity;
		}

		public VMissile GetMissile( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToMissile.TryGetValue( id, out VMissile entity );
			return entity;
		}

		public Effect GetEffect( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;
			this._idToEffect.TryGetValue( id, out Effect entity );
			return entity;
		}

		private void DestroyEnties()
		{
			int count = this._entities.Count;
			for ( int i = 0; i < count; i++ )
			{
				VEntity entity = this._entities[i];
				if ( !entity.markToDestroy )
					continue;

				entity.OnRemoveFromBattle();

				switch ( entity )
				{
					case VBio bio:
						this._bios.Remove( bio );
						this._idToBio.Remove( bio.rid );
						break;

					case VMissile missile:
						this._missiles.Remove( missile );
						this._idToMissile.Remove( missile.rid );
						break;

					case Effect effect:
						this._effects.Remove( effect );
						this._idToEffect.Remove( effect.rid );
						break;
				}

				this._entities.RemoveAt( i );
				this._idToEntity.Remove( entity.rid );
				this._gPool.Push( entity );
				--i;
				--count;
			}
		}

		public void Update( UpdateContext context )
		{
			//更新状态
			this.UpdateState( context );
			//处理战斗信息
			//清理实体
			this.DestroyEnties();
		}

		public void UpdateState( UpdateContext context )
		{
			int count = this._bios.Count;
			for ( int i = 0; i < count; i++ )
			{
				VEntity entity = this._bios[i];
				entity.UpdateState( context );
			}

			count = this._missiles.Count;
			for ( int i = 0; i < count; i++ )
			{
				VEntity entity = this._missiles[i];
				entity.UpdateState( context );
			}

			count = this._effects.Count;
			for ( int i = 0; i < count; i++ )
			{
				VEntity entity = this._effects[i];
				entity.UpdateState( context );
			}
		}
	}
}