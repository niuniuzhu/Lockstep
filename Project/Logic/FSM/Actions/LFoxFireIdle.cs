using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.Model;
using Logic.Property;

namespace Logic.FSM.Actions
{
	public class LFoxFireIdle : BioAction
	{
		public Bio caster;
		public float distance;
		public Quat angleSpeed;

		private List<Entity> _temp1 = new List<Entity>();
		private List<Entity> _temp2 = new List<Entity>();
		private float _delay;
		private float _time;

		protected override void OnEnter( object[] param )
		{
			FoxFire foxFire = ( FoxFire )this.owner;
			this._delay = foxFire.battle.random.NextFloat( foxFire.dashDelay, foxFire.dashDelay2 );
			this._time = 0f;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			FoxFire foxFire = ( FoxFire )this.owner;

			foxFire.property.Equal( Attr.Position,
							Vec3.Normalize( this.angleSpeed * ( foxFire.property.position - this.caster.property.position ) ) *
							this.distance + this.caster.property.position );

			this._time += context.deltaTime;

			if ( this._time < this._delay )
				return;

			this._time = 0f;

			EntityUtils.GetEntitiesInCircle( foxFire.battle.GetEntities(), foxFire.property.position,
															 foxFire.detectRange, ref this._temp1 );
			EntityUtils.FilterTarget( foxFire, CampType.Hostile | CampType.Neutral, EntityFlag.Hero, ref this._temp1,
													  ref this._temp2 );

			if ( this._temp2.Count > 0 )
			{
				int index = foxFire.battle.random.Next( 0, this._temp2.Count );//上限是闭区间
				foxFire.Emmit( ( Bio )this._temp2[index] );
			}
			else
			{
				this._temp2.Clear();
				EntityUtils.FilterTarget( foxFire, CampType.Hostile, EntityFlag.SmallPotato, ref this._temp1,
														  ref this._temp2 );
				if ( this._temp2.Count > 0 )
				{
					int index = foxFire.battle.random.Next( 0, this._temp2.Count );
					foxFire.Emmit( ( Bio )this._temp2[index] );
				}
			}
			this._temp1.Clear();
			this._temp2.Clear();
		}
	}
}