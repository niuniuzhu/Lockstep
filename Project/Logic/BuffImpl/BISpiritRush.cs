using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.Model;

namespace Logic.BuffImpl
{
	//参数extra_s 0-missileId
	public class BISpiritRush : BIBase
	{
		private List<Entity> _temp1 = new List<Entity>();
		private List<Entity> _temp2 = new List<Entity>();

		protected override void OnTargetTrigger( Bio target, int triggerIndex )
		{
			base.OnTargetTrigger( target, triggerIndex );

			EntityUtils.GetEntitiesInCircle( this._buff.battle.GetEntities(), this._buff.property.position,
															 this._buff.radius, ref this._temp1 );
			EntityUtils.FilterTarget( this._buff.caster, CampType.Hostile | CampType.Neutral, EntityFlag.Hero, ref this._temp1,
													  ref this._temp2 );

			if ( this._temp2.Count > 0 )
			{
				int index = this._buff.battle.random.Next( 0, this._temp2.Count );//上限是闭区间
				this.CreateMissile( ( Bio )this._temp2[index] );
			}
			else
			{
				this._temp2.Clear();
				EntityUtils.FilterTarget( this._buff.caster, CampType.Hostile, EntityFlag.SmallPotato, ref this._temp1,
														  ref this._temp2 );
				if ( this._temp2.Count > 0 )
				{
					int index = this._buff.battle.random.Next( 0, this._temp2.Count );
					this.CreateMissile( ( Bio )this._temp2[index] );
				}
			}
			this._temp1.Clear();
			this._temp2.Clear();
		}

		private void CreateMissile( Bio target )
		{
			Missile missile = this._buff.battle.CreateMissile( this._buff.extra_s[0], this._buff.caster.PointToWorld( this._buff.caster.firingPoint ), this._buff.caster.property.direction );
			missile.Emmit( this._buff.extra_s[1], 0, this._buff.caster, target, Vec3.zero );
		}
	}
}