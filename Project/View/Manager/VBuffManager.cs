using System.Collections.Generic;
using Logic;
using Logic.Controller;
using UnityEngine;
using View.Controller;

namespace View.Manager
{
	public class VBuffManager
	{
		private readonly GPool _gPool = new GPool();
		private readonly List<VBuff> _buffs = new List<VBuff>();
		private readonly Dictionary<string, VBuff> _idToBuff = new Dictionary<string, VBuff>();
		private VBattle _battle;

		public VBuffManager( VBattle battle )
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

		public VBuff CreateBuff( string rid, string skillId, int lvl, VBio caster, VBio target, Vector3 targetPoint )
		{
			bool isNew = this._gPool.Pop( out VBuff buff );
			this._buffs.Add( buff );
			this._idToBuff[rid] = buff;
			if ( isNew )
				buff.Init( this._battle );
			buff.OnAddedToBattle( rid, skillId, lvl, caster, target, targetPoint );
			return buff;
		}

		public VBuff Get( string rid )
		{
			this._idToBuff.TryGetValue( rid, out VBuff buff );
			return buff;
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
			{
				VBuff buff = this._buffs[i];
				if ( buff.isInBattle )
					buff.UpdateState( context );
			}
		}

		public void DestroyBuffs()
		{
			int count = this._buffs.Count;
			for ( int i = 0; i < count; i++ )
			{
				VBuff buff = this._buffs[i];
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