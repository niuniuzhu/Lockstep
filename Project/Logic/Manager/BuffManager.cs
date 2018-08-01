using System.Collections.Generic;
using Core.Math;
using Logic.Controller;

namespace Logic.Manager
{
	public class BuffManager
	{
		private readonly GPool _gPool = new GPool();
		private readonly List<Buff> _buffs = new List<Buff>();
		private readonly Dictionary<string, Buff> _idToBuff = new Dictionary<string, Buff>();
		private Battle _battle;

		public BuffManager( Battle battle )
		{
			this._battle = battle;
		}

		public void Dispose()
		{
			int count = this._buffs.Count;
			for ( int i = 0; i < count; i++ )
				this._buffs[i].markToDestroy = true;
			this.DestroyBuffs();
			this._gPool.Dispose();
			this._battle = null;
		}

		public Buff Create( string rid, string skillId, int lvl, Bio caster, Bio target, Vec3 targetPoint )
		{
			bool isNew = this._gPool.Pop( out Buff buff );
			this._buffs.Add( buff );
			this._idToBuff[rid] = buff;
			if ( isNew )
				buff.Init( this._battle );
			buff.OnAddedToBattle( rid, skillId, lvl, caster, target, targetPoint );
			return buff;
		}

		public Buff Get( string rid )
		{
			this._idToBuff.TryGetValue( rid, out Buff buff );
			return buff;
		}

		public Buff Get( string ownerId, string buffId )
		{
			int count = this._buffs.Count;
			for ( int i = 0; i < count; i++ )
			{
				Buff buff = this._buffs[i];
				if ( buff.id == buffId &&
					 buff.caster.rid == ownerId )
					return buff;
			}
			return null;
		}

		internal void Update( UpdateContext context )
		{
			//更新状态
			this.UpdateState( context );
			//清理buff
			this.DestroyBuffs();
		}

		private void UpdateState( UpdateContext context )
		{
			int count = this._buffs.Count;
			for ( int i = 0; i < count; i++ )
				this._buffs[i].UpdateState( context );
		}

		private void DestroyBuffs()
		{
			int count = this._buffs.Count;
			for ( int i = 0; i < count; i++ )
			{
				Buff buff = this._buffs[i];
				if ( !buff.markToDestroy )
					continue;
				buff.OnRemoveFromBattle();
				this._buffs.Remove( buff );
				this._idToBuff.Remove( buff.rid );
				this._gPool.Push( buff );
				--i;
				--count;
			}
		}
	}
}