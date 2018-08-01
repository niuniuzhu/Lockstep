using System.Collections.Generic;
using UnityEngine;
using View.Controller;

namespace View.BuffStateImpl
{
	//参数4-每隔指定层数升级一次特效
	public class VBSEssenceTheft : VBSBase
	{
		private readonly Dictionary<string, int> _chargesPreSkill = new Dictionary<string, int>();
		private int _totalCharges;
		private Effect _fx;

		private int _level = -1;
		private int level
		{
			set
			{
				if ( this._level == value )
					return;
				this._level = value;
				this.UpdateFx();
			}
		}

		protected override void Reset()
		{
			this._totalCharges = 0;
			this._chargesPreSkill.Clear();
			this.level = -1;
		}

		protected override void CreateInternal()
		{
			this.level = 0;
		}

		protected override void DamageInternal( VBuff buff, VBio target, float damage )
		{
			if ( buff.skillData.isCommon )
				return;

			int charge;
			if ( !this._chargesPreSkill.TryGetValue( buff.skillData.id, out charge ) )
			{
				this._chargesPreSkill[buff.skillData.id] = 0;
				charge = 0;
			}
			int maxChargePreSkill = ( int ) this.extra[0];
			if ( charge < maxChargePreSkill )
			{
				++this._totalCharges;
				++charge;
				this._chargesPreSkill[buff.skillData.id] = charge > maxChargePreSkill ? maxChargePreSkill : charge;
			}

			if ( this._totalCharges > ( int ) this.extra[1] )
			{
				this._totalCharges = 0;
				this._chargesPreSkill.Clear();
			}

			this.level = this._totalCharges / ( int ) this.extra[3];
		}

		private void SpawnFx( string id )
		{
			if ( this._fx != null && this._fx.id != id )
				this.DespawnFx();

			if ( this._fx == null )
			{
				this._fx = this.owner.battle.CreateEffect( id );
				this._fx.SetupTerritory( this.owner, this.owner, Vector3.zero );
			}
		}

		private void UpdateFx()
		{
			if ( this._level == -1 )
			{
				this.DespawnFx();
			}
			if ( this._level == 0 )
			{
				this.SpawnFx( this.extra_s[0] );
			}
			else if ( this._level == 1 )
			{
				this.SpawnFx( this.extra_s[1] );
			}
			else if ( this._level == 2 )
			{
				this.SpawnFx( this.extra_s[2] );
			}
			else
			{
				this.SpawnFx( this.extra_s[3] );
			}
		}

		private void DespawnFx()
		{
			if ( this._fx != null )
			{
				this._fx.markToDestroy = true;
				this._fx = null;
			}
		}
	}
}