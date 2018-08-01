using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Model;
using Logic.Property;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LAttack : BioAction
	{
		private Skill _skill;
		private Bio _target;
		private Vec3 _targetPoint;
		private float _atkTime;
		private float _firingTime;
		private float _time;

		protected override void OnEnter( object[] param )
		{
			this._skill = ( Skill )param[0];
			this._target = ( Bio )param[1];
			this._targetPoint = ( Vec3 )param[2];

			this._target?.AddRef();

			this._time = 0f;
			this._atkTime = this._skill.atkTime / this.owner.property.attackSpeedFactor;
			this._firingTime = this._skill.firingTime / this.owner.property.attackSpeedFactor;

			this._skill.property.Equal( Attr.Cooldown, MathUtils.Max( this._atkTime, this._skill.cd ) );

			this.owner.UpdateVelocity( Vec3.zero );
			this.owner.brain.enable = false;
			this.owner.usingSkill = this._skill;
			this.owner.property.Equal( Attr.InterruptTime, this.owner.battle.time + this._skill.sufTime );
			this.owner.property.Add( Attr.Mana, -this._skill.manaCost );

			if ( this._skill.castType == CastType.Dash )
			{
				this.owner.property.Add( Attr.Dashing, 1 );
				Vec3 point = this._target?.property.position ?? this._targetPoint;
				this.owner.steering.dash.Set( point, this._skill.dashStartSpeed, this._skill.dashSpeedCurve,
											  this._atkTime );
				this.owner.steering.On( SteeringBehaviors.BehaviorType.Dash );
			}

			if ( this._skill.firingTime > this._skill.atkTime )
				LLogger.Warning( "Firing time must less or equal then attack time." );

			if ( MathUtils.Approximately( this._firingTime, 0f ) )
				this.OnFire();

			if ( MathUtils.Approximately( this._atkTime, 0f ) )
				this.NextState();
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Dash );
			this.owner.usingSkill = null;
			this.owner.property.Equal( Attr.InterruptTime, 0f );
			this.owner.brain.enable = true;
			this.owner.property.Add( Attr.Dashing, -1 );
			this._skill = null;
			this._target?.RedRef();
			this._target = null;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			this._time += context.deltaTime;

			if ( this._firingTime >= 0f && this._time >= this._firingTime )
				this.OnFire();

			if ( this._time >= this._atkTime )
				this.NextState();
		}

		private void OnFire()
		{
			this._firingTime = -1f;

			bool hasMissile = !string.IsNullOrEmpty( this._skill.missile );
			if ( hasMissile )
			{
				Missile missile = this.owner.battle.CreateMissile( this._skill.missile, this.owner.PointToWorld( this.owner.firingPoint ), this.owner.property.direction );
				missile.Emmit( this._skill.id, this._skill.property.lvl, this.owner, this._target, this._targetPoint );
			}
			else
				this.BeginBuff( this._skill, this.owner, this._target, this._targetPoint );
		}

		private void BeginBuff( Skill skill, Bio caster, Bio target, Vec3 targetPoint )
		{
			if ( target != null &&
				 target.isDead )
				return;

			int count = skill.buffs.Length;
			for ( int i = 0; i < count; i++ )
				this.owner.battle.CreateBuff( skill.buffs[i], skill.id, skill.property.lvl, caster, target, targetPoint );
		}

		private void NextState()
		{
			SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
			this.owner.ChangeState( FSMStateType.Idle );
		}
	}
}